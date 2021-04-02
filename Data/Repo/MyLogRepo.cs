using MyLog.AppCommon;
using MyLog.Data.Repo.Entity;
using MyLog.Data.Repo.Entity.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static MyLog.Component.ResultButton;

namespace MyLog.Data.Repo {
    /// <summary>
    /// repo for my log
    /// </summary>
    internal class MyLogRepo : BaseRepo {

        #region Internal Method
        /// <summary>
        /// 記録日をキーとしてログ情報を取得
        /// </summary>
        /// <param name="recordedOn">記録日</param>
        /// <returns>取得結果(該当情報が存在しない場合はnullを返却)</returns>
        internal LogData SelectByRecordedOn(string recordedOn) {
            LogData result = null;
            var startIndex = 0;

            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.Open();

                // ヘッダ情報を取得
                var logId = 0L;
                var headerEntity = new LogEntity(database);
                using (var recset = headerEntity.SelectByRecordedOn(recordedOn)) {
                    if (!recset.Read()) {
                        return result;
                    }
                    logId = recset.GetInt(LogEntity.Cols.Id);
                }
                result = new LogData {
                    Id = logId,
                    RecordedOn = recordedOn,
                    LogList = new ObservableCollection<LogDetailData>()
                };

                // カテゴリ情報を取得
                var categories = new Dictionary<long, string>();
                var categoryEntity = new CategoryEntity(database);
                using (var recset = categoryEntity.SelectVisible()) {
                    while(recset.Read()) {
                        categories.Add(recset.GetLong(CategoryEntity.Cols.Id),
                                        recset.GetString(CategoryEntity.Cols.Name));
                    }
                }

                // 明細情報を取得
                var detailEntity = new LogDetailEntity(database);
                using (var recset = detailEntity.SelectByLogHId(logId)) {
                    var currentCategory = -1L;
                    while (recset.Read()) {
                        var detail = new LogDetailData {
                            Id = recset.GetLong(LogDetailEntity.Cols.Id),
                            LogId = recset.GetLong(LogDetailEntity.Cols.LogId),
                            CategoryId = recset.GetLong(LogDetailEntity.Cols.CategoryId),
                            Priority = recset.GetInt(LogDetailEntity.Cols.Priority),
                            Result = (ResultState)recset.GetInt(LogDetailEntity.Cols.Result),
                            Todo = recset.GetString(LogDetailEntity.Cols.Todo),
                            PlanStart = recset.GetString(LogDetailEntity.Cols.PlanStart),
                            PlanEnd = recset.GetString(LogDetailEntity.Cols.PlanEnd),
                            PlanTime = recset.GetInt(LogDetailEntity.Cols.PlanTime),
                            ActualStart = recset.GetString(LogDetailEntity.Cols.ActualStart),
                            ActualEnd = recset.GetString(LogDetailEntity.Cols.ActualEnd),
                            ActualTime = recset.GetInt(LogDetailEntity.Cols.ActualTime),
                            Memo = recset.GetString(LogDetailEntity.Cols.Memo)
                        };
                        detail.Priority = recset.GetInt(LogDetailEntity.Cols.Priority);
                        detail.IsCategory = false;

                        if (currentCategory != detail.CategoryId) {
                            for (var i = startIndex; i < categories.Count; i++) {
                                var categoryId = categories.ElementAt(i).Key;
                                var category = new LogDetailData {
                                    IsCategory = true,
                                    CategoryId = categoryId,
                                    CategoryName = categories[categoryId]
                                };
                                result.LogList.Add(category);
                                if (categoryId == detail.CategoryId) {
                                    currentCategory = categoryId;
                                    startIndex = i + 1;
                                    break;
                                }
                                currentCategory = categoryId;
                            }
                        }
                        result.LogList.Add(detail);
                    }

                    startIndex = -1;
                    for (var i = 0; i < categories.Count; i++) {
                        if (currentCategory == categories.ElementAt((int)i).Key) {
                            startIndex = i;
                            break;
                        }
                    }

                    for (var i = startIndex + 1; i < categories.Count; i++) {
                        var categoryId = categories.ElementAt(i).Key;
                        if (currentCategory < categoryId) {
                            var category = new LogDetailData {
                                IsCategory = true,
                                CategoryId = categoryId,
                                CategoryName = categories[categoryId]
                            };
                            result.LogList.Add(category);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ログ情報(ヘッダ)を作成する
        /// </summary>
        /// <param name="recordedOn">記録日</param>
        /// <returns>ID</returns>
        internal long InsertHeader(string recordedOn) {
            long result;
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                try {
                    database.Open();
                    database.BeginTrans();
                    var entity = new LogEntity(database) {
                        RecordedOn = recordedOn
                    };
                    result = entity.Insert();
                    database.CommitTrans();
                } catch(Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 空のログを作成する
        /// </summary>
        /// <param name="recordedOn">記録日</param>
        /// <returns>空のログデータ</returns>
        internal LogData CreateEmptyLog(string recordedOn) {
            var result = new LogData();
            result.LogList = new ObservableCollection<LogDetailData>();
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                try {
                    database.Open();
                    var categoryEntity = new CategoryEntity(database);
                    var categories = new Dictionary<long, string>();
                    using (var recset = categoryEntity.Select()) {
                        while (recset.Read()) {
                            if (!recset.GetBool(CategoryEntity.Cols.Visible)) {
                                continue;
                            }
                            result.LogList.Add(new LogDetailData() {
                                CategoryId = recset.GetLong(CategoryEntity.Cols.Id),
                                CategoryName = recset.GetString(CategoryEntity.Cols.Name),
                                IsCategory = true
                            });
                        }
                    }

                    database.BeginTrans();
                    var logEntity = new LogEntity(database) {
                        RecordedOn = recordedOn
                    };
                    result.Id = logEntity.Insert();
                    database.CommitTrans();
                } catch (Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// テンプレートからログを作成する。
        /// </summary>
        /// <param name="recordedOn">日付</param>
        /// <returns></returns>
        internal LogData CreateLogByRecordedOn(string recordedOn) {
            long templateId;
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.Open();
                var templateEntity = new TemplateEntity(database);
                using (var recset = templateEntity.SelectByWeekDay(recordedOn)) {
                    // テンプレートが存在しない場合は空の情報を作成
                    if (!recset.Read()) {
                        return CreateEmptyLog(recordedOn);
                    }
                    templateId = recset.GetLong(TemplateEntity.Cols.Id);
                }
            }

            return this.CreateLogByTemplateId(templateId, recordedOn);
        }


        /// <summary>
        /// テンプレートからログを作成する。
        /// </summary>
        /// <param name="templateId">テンプレートID</param>
        /// /// <param name="recordedOn">日付</param>
        /// <returns></returns>
        internal LogData CreateLogByTemplateId(long templateId, string recordedOn) {

            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.Open();
                try {
                    database.BeginTrans();
                    var logEntity = new LogEntity(database) {
                        RecordedOn = recordedOn
                    };
                    var id = logEntity.Insert();

                    var templateDetailEntity = new TemplateDetailEntity(database);
                    templateDetailEntity.InsertToLog(templateId, id);
                    database.CommitTrans();
                } catch (Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }

            return this.SelectByRecordedOn(recordedOn);
        }

        /// <summary>
        /// 空行を作成する
        /// </summary>
        /// <param name="logId">ログ情報ID</param>
        /// <param name="categoryId">カテゴリID</param>
        /// <param name="order">並び順</param>
        /// <returns></returns>
        internal LogDetailData InsertEmptyRow(long logId, long categoryId, int order) {
            var result = new LogDetailData();
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.Open();
                database.BeginTrans();

                var entity = new LogDetailEntity(database) {
                    LogId = logId,
                    CategoryId = categoryId,
                    Priority = order
                };
                result.Id = entity.Insert();
                result.LogId = logId;
                result.CategoryId = categoryId;
                result.Priority = order;
                database.CommitTrans();
            }
            return result;
        }

        /// <summary>
        /// IDをキーとして並び順・カテゴリIDを更新する
        /// </summary>
        /// <param name="logList">ログデータ</param>
        internal void UpdateOrderById(ObservableCollection<LogDetailData> logList) {
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                try {
                    database.Open();
                    database.BeginTrans();
                    var entity = new LogDetailEntity(database);

                    foreach (var data in logList) {
                        if (data.IsCategory) {
                            continue;
                        }
                        entity.ClearParams();
                        entity.AddParams(LogDetailEntity.Cols.Priority, data.Priority);
                        entity.AddParams(LogDetailEntity.Cols.CategoryId, data.CategoryId);
                        entity.UpdateById(data.Id);
                    }
                    database.CommitTrans();
                }catch(Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// IDをキーとして並び順・カテゴリIDを更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="order">並び順</param>
        /// <param name="categoryId">カテゴリID</param>
        internal void UpdateOrderById(long id, int order, long categoryId) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Priority, order);
            entity.AddParams(LogDetailEntity.Cols.CategoryId, categoryId);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして結果を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="result">結果</param>
        internal void UpdateResultById(long id, int result) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, result);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとしてTodoを更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="todo">Todo</param>
        internal void UpdateTodoById(long id, string todo) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Todo, todo);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして予定時間を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="planStart">予定時間(開始)</param>
        /// <param name="planEnd">予定時間(終了)</param>
        /// <param name="planTime">予定時間</param>
        internal void UpdatePlanTimeById(long id, string planStart, string planEnd, int planTime) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.PlanStart, planStart);
            entity.AddParams(LogDetailEntity.Cols.PlanEnd, planEnd);
            entity.AddParams(LogDetailEntity.Cols.PlanTime, planTime);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして実績時間を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="actualStart">実績時間(開始)</param>
        /// <param name="actualEnd">実績時間(終了)</param>
        /// <param name="actualTime">実績時間</param>
        internal void UpdateActualTimeById(long id, string actualStart, string actualEnd, int actualTime) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.ActualStart, actualStart);
            entity.AddParams(LogDetailEntity.Cols.ActualEnd, actualEnd);
            entity.AddParams(LogDetailEntity.Cols.ActualTime, actualTime);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとしてメモを更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="memo">メモ</param>
        internal void UpdateMemoById(long id, string memo) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Memo, memo);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして削除する
        /// </summary>
        /// <param name="id">ID</param>
        internal void DeleteById(long id) {
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                try {
                    database.Open();
                    database.BeginTrans();
                    var eneityHeader = new LogEntity(database);
                    eneityHeader.DeleteById(id);
                    var entityDetail = new LogDetailEntity(database);
                    entityDetail.DeleteById(id);
                    database.CommitTrans();
                } catch (Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// IDをキーとしてログ情報(明細)を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="entity">エンティティ</param>
        private void UpdateLogDById(long id, LogDetailEntity entity) {
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                try {
                    database.Open();
                    database.BeginTrans();
                    entity.Database = database;
                    entity.UpdateById(id);
                    database.CommitTrans();
                } catch(Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }
        }
        #endregion
    }
}
