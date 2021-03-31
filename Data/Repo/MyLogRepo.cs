using MyLog.AppCommon;
using MyLog.Data.Repo.Entity;
using MyLog.Data.Repo.Entity.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
                    RecordedOn = recordedOn,
                    LogList = new System.Collections.ObjectModel.ObservableCollection<LogDetailData>()
                };


                // カテゴリ情報を取得
                var categories = new Dictionary<long, string>();
                var categoryEntity = new CategoryEntity(database);
                using (var recset = categoryEntity.Select()) {
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
                            CategoryId = recset.GetLong(LogDetailEntity.Cols.CategoryId),
                            Priority = recset.GetInt(LogDetailEntity.Cols.Priority),
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
                            for (var i = 0; i < categories.Count; i++) {
                                var categoryId = categories.ElementAt(i).Key;
                                if (currentCategory < categoryId && categoryId <= detail.CategoryId) {
                                    var category = new LogDetailData {
                                        IsCategory = true,
                                        CategoryId = categoryId,
                                        CategoryName = categories[categoryId]
                                    };
                                    result.LogList.Add(category);
                                    currentCategory = categoryId;
                                }
                            }
                        }
                        result.LogList.Add(detail);
                    }

                    for (var i = currentCategory + 1; i < categories.Count; i++) {
                        var categoryId = categories.ElementAt((int)i).Key;
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
        internal LogData CreateLog(string recordedOn) {
            var result = new LogData();

            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.Open();
                var templateEntity = new TemplateEntity(database);
                long templateId;
                using (var recset = templateEntity.SelectByWeekDay(recordedOn)) {
                    // テンプレートが存在しない場合は空の情報を作成
                    if (!recset.Read()) {
                        return CreateEmptyLog(recordedOn);              
                    }
                    templateId = recset.GetLong(TemplateEntity.Cols.Id);
                    result.RecordedOn = recordedOn;
                }

                try {
                    database.BeginTrans();
                    var logEntity = new LogEntity(database) {
                        RecordedOn = recordedOn
                    };
                    result.Id = logEntity.Insert();


                    var templateDetailEntity = new TemplateDetailEntity(database);
                    templateDetailEntity.InsertToLog(templateId, result.Id);
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
        /// <param name="logHId">ログ情報ID</param>
        /// <param name="categoryId">カテゴリID</param>
        /// <param name="order">並び順</param>
        /// <returns></returns>
        internal LogDetailData InsertEmptyRow(long logHId, long categoryId, int order) {
            var result = new LogDetailData();
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.BeginTrans();

                var entity = new LogDetailEntity(database) {
                    LogHId = logHId,
                    CategoryId = categoryId,
                    Priority = order
                };
                entity.Insert();
                database.CommitTrans();
                result.Id = entity.Insert();
                result.CategoryId = categoryId;
                result.Priority = order;
            }
            return result;
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
        /// IDをキーとして予定時間(開始)を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="planStart">予定時間(開始)</param>
        internal void UpdatePlanStartById(long id, string planStart) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, planStart);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして予定時間(終了)を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="planEnd">予定時間(終了)</param>
        internal void UpdatePlanEndById(long id, string planEnd) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, planEnd);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして予定時間を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="planTime">予定時間</param>
        internal void UpdatePlanTimeById(long id, int planTime) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, planTime);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして実績時間(開始)を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="actualStart">実績時間(開始)</param>
        internal void UpdateActualStartById(long id, string actualStart) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, actualStart);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして実績時間(終了)を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="actualEnd">実績時間(終了)</param>
        internal void UpdateActualEndById(long id, string actualEnd) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, actualEnd);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとして実績時間を更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="actualTime">実績時間</param>
        internal void UpdateActuralTimeById(long id, int actualTime) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, actualTime);
            this.UpdateLogDById(id, entity);
        }

        /// <summary>
        /// IDをキーとしてメモを更新する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="memo">メモ</param>
        internal void UpdateMemoById(long id, string memo) {
            var entity = new LogDetailEntity();
            entity.AddParams(LogDetailEntity.Cols.Result, memo);
            this.UpdateLogDById(id, entity);
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
                database.BeginTrans();
                entity.Database = database;
                entity.UpdateById(id);
                database.CommitTrans();
            }
        }
        #endregion
    }
}
