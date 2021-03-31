using MyLog.AppCommon;
using MyLog.Data.Repo.Entity;
using MyLog.Data.Repo.Entity.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyLog.Data.Repo {
    /// <summary>
    /// repo for template
    /// </summary>
    internal class TemplateRepo : BaseRepo {

        #region Internal Method
        /// <summary>
        /// テンプレート情報を全件取得する。
        /// </summary>
        /// <returns>テンプレート情報</returns>
        internal ObservableCollection<TemplateData> Select() {
            var result = new ObservableCollection<TemplateData>();
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.Open();

                // カテゴリ情報を取得
                var categoryEntity = new CategoryEntity(database);
                var categories = new Dictionary<long, string>();
                using (var recset = categoryEntity.Select()) {
                    while (recset.Read()) {
                        categories.Add(recset.GetLong(CategoryEntity.Cols.Id),
                                        recset.GetString(CategoryEntity.Cols.Name));
                    }
                }

                var headerEntity = new TemplateEntity(database);
                var detailEntity = new TemplateDetailEntity(database);
                using (var recset = headerEntity.Select()) {
                    while (recset.Read()) {
                        // ヘッダ情報を取得
                        var templateData = new TemplateData {
                            Id = recset.GetLong(TemplateEntity.Cols.Id),
                            Name = recset.GetString(TemplateEntity.Cols.Name),
                            Mon = recset.GetBool(TemplateEntity.Cols.Mon),
                            Tue = recset.GetBool(TemplateEntity.Cols.Tue),
                            Wed = recset.GetBool(TemplateEntity.Cols.Wed),
                            Thu = recset.GetBool(TemplateEntity.Cols.Thu),
                            Fri = recset.GetBool(TemplateEntity.Cols.Fri),
                            Sat = recset.GetBool(TemplateEntity.Cols.Sat),
                            LogList = new ObservableCollection<TemplateDetailData>()
                        };

                        // 明細情報を取得
                        SelectDetailByTemplateId(templateData.LogList, detailEntity, templateData.Id, categories);
                        
                        result.Add(templateData);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 空のテンプレート情報を作成する
        /// </summary>
        /// <returns>空のテンプレートデータ</returns>
        internal TemplateData CreateEmptyTemplate() {
            var result = new TemplateData();
            result.LogList = new ObservableCollection<TemplateDetailData>();
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                database.Open();
                var categoryEntity = new CategoryEntity(database);
                var categories = new Dictionary<long, string>();
                using (var recset = categoryEntity.Select()) {
                    while (recset.Read()) {
                        if (!recset.GetBool(CategoryEntity.Cols.Visible)) {
                            continue;
                        }
                        result.LogList.Add(new TemplateDetailData() {
                            CategoryId = recset.GetLong(CategoryEntity.Cols.Id),
                            CategoryName = recset.GetString(CategoryEntity.Cols.Name),
                            IsCategory = true
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// テンプレート情報を更新する
        /// </summary>
        /// <param name="data">更新情報</param>
        /// <param name="isNew">true:新規、false:更新</param>
        /// <remarks>新規のケースもあるのでdelete → insertで処理を行う</remarks>
        internal void Update(TemplateData data, bool isNew) {
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                try {
                    database.Open();
                    database.BeginTrans();

                    var headerEntity = new TemplateEntity(database);
                    var detailEntity = new TemplateDetailEntity(database);

                    // テンプレート情報(ヘッダ)
                    headerEntity.Set(data);
                    long id = data.Id;
                    if (isNew) {
                        id = headerEntity.Insert();
                    } else {
                        headerEntity.Update();
                    }

                    // テンプレート情報(明細)
                    if (!isNew) {
                        detailEntity.DeleteByTemplateId(id);
                    }
                    foreach (var detail in data.LogList) {
                        if (detail.IsCategory) {
                            continue;
                        }
                        detailEntity.Set(detail, id);
                        detailEntity.Insert();
                    }

                    database.CommitTrans();
                } catch(Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// テンプレートIDをキーとしてテンプレート情報を削除する
        /// </summary>
        /// <param name="id"></param>
        internal void DeleteByTemplateId(long id) {
            using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
                try {
                    database.Open();
                    database.BeginTrans();

                    new TemplateEntity(database).DeleteById(id);
                    new TemplateDetailEntity(database).DeleteByTemplateId(id);

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
        /// テンプレート情報(明細)を取得
        /// </summary>
        /// <param name="detailList">明細情報のリスト</param>
        /// <param name="entity">エンティティ</param>
        /// <param name="id">id</param>
        /// <param name="categoriesBase">カテゴリ情報</param>
        private void SelectDetailByTemplateId(ObservableCollection<TemplateDetailData>  detailList, 
            TemplateDetailEntity entity, long id, Dictionary<long, string> categories) {
            
            using (var recset = entity.SelectByTemplateId(id)) {
                var currentCategory = -1L;
                while (recset.Read()) {
                    var detail = new TemplateDetailData {
                        CategoryId = recset.GetLong(LogDetailEntity.Cols.CategoryId),
                        Priority = recset.GetInt(LogDetailEntity.Cols.Priority),
                        PlanStart = recset.GetString(LogDetailEntity.Cols.PlanStart),
                        PlanEnd = recset.GetString(LogDetailEntity.Cols.PlanEnd),
                        PlanTime = recset.GetInt(LogDetailEntity.Cols.PlanTime),
                    };
                    detail.IsCategory = false;

                    if (currentCategory != detail.CategoryId) {
                        for (var i = 0; i < categories.Count; i++) {
                            var categoryId = categories.ElementAt(i).Key;
                            if (currentCategory < categoryId && categoryId <= detail.CategoryId) {
                                var category = new TemplateDetailData {
                                    IsCategory = true,
                                    CategoryId = categoryId,
                                    CategoryName = categories[categoryId]
                                };
                                detailList.Add(category);
                                currentCategory = categoryId;
                            }
                        }
                    }
                    detailList.Add(detail);
                }

                for (var i = currentCategory + 1; i < categories.Count; i++) {
                    var categoryId = categories.ElementAt((int)i).Key;
                    var category = new TemplateDetailData {
                        IsCategory = true,
                        CategoryId = categoryId,
                        CategoryName = categories[categoryId]
                    };
                    detailList.Add(category);
                }
            }
        }
        #endregion
    }
}
