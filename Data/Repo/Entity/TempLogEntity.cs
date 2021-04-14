using OsnLib.Data.Sqlite;
using System;

namespace MyLog.Data.Repo.Entity {
    /// <summary>
    /// temp log entity
    /// </summary>
    internal class TempLogEntity : BaseEntity {

        #region Declaration
        /// <summary>
        /// カラム定義
        /// </summary>
        internal static class Cols {
            internal static readonly String Id = "id";
            internal static readonly String Priority = "priority";
            internal static readonly String Todo = "todo";
            internal static readonly String Memo = "memo";
            internal static readonly String CreateAt = "create_at";
            internal static readonly String UpdateAt = "update_at";
        }

        /// <summary>
        /// テーブル名
        /// </summary>
        internal static readonly string TableName = "temp_log";
        #endregion


        #region Internal Property
        /// <summary>
        /// ID
        /// </summary>
        internal long Id { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        internal int Priority { set; get; }

        /// <summary>
        /// Todo
        /// </summary>
        internal string Todo { set; get; }

        /// <summary>
        /// メモ
        /// </summary>
        internal string Memo { set; get; }
        #endregion

        #region Constructor
        internal TempLogEntity(){}
        internal TempLogEntity(MyLogDatabase database) : base(database) { }
        #endregion

        #region Internal Method
        internal override bool Create() {
            var sql = new SqlBuilder();
            sql.AppendSql($"CREATE TABLE {TableName} (")
                .AppendSql($" {Cols.Id}           INTEGER PRIMARY KEY AUTOINCREMENT")
                .AppendSql($",{Cols.Priority}     INTEGER NOT NULL")
                .AppendSql($",{Cols.Todo}         TEXT")
                .AppendSql($",{Cols.Memo}         TEXT")
                .AppendSql($",{Cols.CreateAt}     INTEGER")
                .AppendSql($",{Cols.UpdateAt}     INTEGER")
                .Append(")");
            return 0 <= base.Database.ExecuteNonQuery(sql);
        }

        internal override long Insert() {
            var sql = new SqlBuilder();
            sql.AppendSql($"INSERT INTO {TableName}")
                .AppendSql("(")
                .AppendSql($" {Cols.Priority}")
                .AppendSql($",{Cols.Todo}")
                .AppendSql($",{Cols.Memo}")
                .AppendSql($",{Cols.CreateAt}")
                .AppendSql($",{Cols.UpdateAt}")
                .AppendSql(")")
                .AppendSql("VALUES")
                .AppendSql("(")
                .AppendSql($" @{Cols.Priority}")
                .AppendSql($",@{Cols.Todo}")
                .AppendSql($",@{Cols.Memo}")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(")");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.Priority}", this.Priority);
            paramList.Add($"@{Cols.Todo}", this.Todo);
            paramList.Add($"@{Cols.Memo}", this.Memo);
            return base.Database.Insert(sql, paramList);
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
        /// テンプログ情報を取得
        /// </summary>
        /// <returns>レコードセット</returns>
        internal Recordset Select() {
            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT * FROM {TableName}")
                .AppendSql($"ORDER BY {Cols.Priority}");
            return base.Database.OpenRecordset(sql);
        }

        /// <summary>
        /// ログ情報を更新する
        /// </summary>
        /// <param name="id">id</param>
        /// <remarks>更新する値はParamsに事前設定しておくことが前提。</remarks>
        internal void UpdateById(long id) {
            var sql = new SqlBuilder();
            sql.AppendSql($"UPDATE {TableName} SET");
            bool isFirstRow = true;
            foreach (var param in base.Params.GetParameterList()) {
                sql.AppendSql(isFirstRow ? " " : ",")
                    .AppendSql($"{param.ParameterName.Substring(1)} = {param.ParameterName}");
                isFirstRow = false;
            }
            sql.AppendSql($",{Cols.UpdateAt} = datetime('now', 'localtime')")
                .AppendSql($"WHERE {Cols.Id} = {id}");
            base.Database.ExecuteNonQuery(sql, base.Params);
        }
        #endregion
    }
}
