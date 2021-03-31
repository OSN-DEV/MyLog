using MyLog.Data.Repo.Entity.DataModel;
using OsnLib.Data.Sqlite;
using System;

namespace MyLog.Data.Repo.Entity {
    /// <summary>
    /// categories table entity
    /// </summary>
    internal class CategoryEntity : BaseEntity {

        #region Declaration
        /// <summary>
        /// カラム定義
        /// </summary>
        internal static class Cols {
            internal static readonly String Id = "id";
            internal static readonly String Name = "name";
            internal static readonly String Priority = "priority";
            internal static readonly String Visible = "visible";
            internal static readonly String CreateAt = "create_at";
            internal static readonly String UpdateAt = "update_at";
        }

        /// <summary>
        /// テーブル名
        /// </summary>
        internal static readonly string TableName = "categories";
        #endregion

        #region Internal Property
        /// <summary>
        /// ID
        /// </summary>
        internal long Id { set; get; }

        /// <summary>
        /// カテゴリ名
        /// </summary>
        internal string Name { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        internal int Priority { set; get; }

        /// <summary>
        /// 可視
        /// </summary>
        internal bool Visible { set; get; }
        #endregion

        #region Constructor
        internal CategoryEntity(MyLogDatabase database) : base(database) { }
        #endregion

        #region Internal Method
        internal override bool Create() {
            var sql = new SqlBuilder();
            sql.AppendSql($"CREATE TABLE {TableName} (")
                .AppendSql($" {Cols.Id}             INTEGER PRIMARY KEY AUTOINCREMENT")
                .AppendSql($",{Cols.Name}           TEXT NOT NULL")
                .AppendSql($",{Cols.Priority}       INTEGER NOT NULL")
                .AppendSql($",{Cols.Visible}        INTEGER NOT NULL")
                .AppendSql($",{Cols.CreateAt}       INTEGER")
                .AppendSql($",{Cols.UpdateAt}       INTEGER")
                .Append(")");
            if  (base.Database.ExecuteNonQuery(sql) < 0) {
                return false;
            }

            for(int i=0; i< 5; i++) {
                this.Name = $"Category {i + 1}";
                this.Priority = i;
                this.Visible = true;
                this.Insert();
            }
            return true;
        }

        internal override long Insert() {
            var sql = new SqlBuilder();
            sql.AppendSql($"INSERT INTO {TableName}")
                .AppendSql("(")
                .AppendSql($" {Cols.Name}")
                .AppendSql($",{Cols.Priority}")
                .AppendSql($",{Cols.Visible}")
                .AppendSql($",{Cols.CreateAt}")
                .AppendSql($",{Cols.UpdateAt}")
                .AppendSql(")")
                .AppendSql("VALUES")
                .AppendSql("(")
                .AppendSql($" @{Cols.Name}")
                .AppendSql($",@{Cols.Priority}")
                .AppendSql($",@{Cols.Visible}")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(")");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.Name}", this.Name);
            paramList.Add($"@{Cols.Priority}", this.Priority);
            paramList.Add($"@{Cols.Visible}", MyLogDatabase.Bool2Int(this.Visible));
            return base.Database.Insert(sql, paramList);
        }

        internal override void Delete() {
            var sql = new SqlBuilder();
            sql.AppendSql($"DELETE FROM {TableName}");
            base.Database.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// idをキーとしてレコードを削除する
        /// </summary>
        /// <param name="id">id</param>
        internal void DeleteById(long id) {
            var sql = new SqlBuilder();
            sql.AppendSql($"DELETE FROM {TableName}")
                .AppendSql($"WHERE {Cols.Id} = @{Cols.Id}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.Id}", id);
            base.Database.ExecuteNonQuery(sql, paramList);
        }

        /// <summary>
        /// 全件取得
        /// </summary>
        /// <returns></returns>
        internal Recordset Select() {
            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT * FROM {TableName}")
                .AppendSql($"ORDER BY {Cols.Priority}");
            return base.Database.OpenRecordset(sql);
        }

        /// <summary>
        /// データモデルの情報をメンバーに設定する。
        /// </summary>
        /// <param name="data">データモデル</param>
        internal void Set(CategoryData data) {
            this.Id = data.Id;
            this.Name = data.Name;
            this.Priority = data.Priority;
            this.Visible = data.Visible;
        }

        /// <summary>
        /// IDをキーボして更新する。
        /// </summary>
        /// <param name="id">id</param>
        internal void UpdateById(long id) {
            var sql = new SqlBuilder();
            sql.AppendSql($"UPDATE {TableName} SET")
                .AppendSql($" {Cols.Name} = @{Cols.Name}")
                .AppendSql($",{Cols.Priority} = @{Cols.Priority}")
                .AppendSql($",{Cols.Visible} = @{Cols.Visible}")
                .AppendSql($",{Cols.UpdateAt}= datetime('now', 'localtime')")
                .AppendSql($" WHERE {Cols.Id} = @{Cols.Id}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.Name}", this.Name);
            paramList.Add($"@{Cols.Priority}", this.Priority);
            paramList.Add($"@{Cols.Visible}", this.Visible);
            paramList.Add($"@{Cols.Id}", id);
            base.Database.ExecuteNonQuery(sql, paramList);
        }
        #endregion

    }
}
