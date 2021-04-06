using MyLog.AppCommon;
using MyLog.Data.Repo.Entity;
using MyLog.Data.Repo.Entity.DataModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyLog.Data.Repo {
    /// <summary>
    /// repo for catregory
    /// </summary>
    internal class CategoryRepo: BaseRepo {
        #region Internal Method
        /// <summary>
        /// カテゴリ情報を取得
        /// </summary>
        /// <returns>取得結果(該当情報が存在しない場合は空のリストを返却)</returns>
        internal ObservableCollection<CategoryData> Select() {
            var result = new ObservableCollection<CategoryData>();
            using (var database = new MyLogDatabase(Constants.DatabaseFile())) {
                database.Open();

                var entity = new CategoryEntity(database);
                using (var recset = entity.Select()) {
                    while (recset.Read()) {
                        var data = new CategoryData() {
                            Id = recset.GetLong(CategoryEntity.Cols.Id),
                            Name = recset.GetString(CategoryEntity.Cols.Name),
                            Visible = recset.GetBool(CategoryEntity.Cols.Visible),
                            Priority = recset.GetInt(CategoryEntity.Cols.Priority)
                        };
                        result.Add(data);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// カテゴリ情報を更新する
        /// </summary>
        /// <param name="categories">カテゴリ情報</param>
        internal void Update(ObservableCollection<CategoryData> categories) {
            using (var database = new MyLogDatabase(Constants.DatabaseFile())) {
                try {
                    database.Open();
                    database.BeginTrans();

                    var entity = new CategoryEntity(database);
                    foreach (var (category, index) in categories.Select((cateogry, index) => (cateogry, index))) {
                        category.Priority = index;
                        entity.Set(category);
                        entity.UpdateById(category.Id);
                    }
                    database.CommitTrans();
                } catch (Exception ex) {
                    database.RollbackTrans();
                    throw ex;
                }
            }
        }
        #endregion
    }
}
