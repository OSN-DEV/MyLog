using OsnLib.Data.Sqlite;
using System;

namespace MyLog.Data.Repo.Entity {
    /// <summary>
    /// logs_h table entity
    /// </summary>
    internal class LogEntity : BaseEntity {

        #region Declaration
        /// <summary>
        /// カラム定義
        /// </summary>
        internal static class Cols {
            internal static readonly String Id = "id";
            internal static readonly String RecordedOn = "recorded_on";
            internal static readonly String CreateAt = "create_at";
            internal static readonly String UpdateAt = "update_at";
        }

        /// <summary>
        /// テーブル名
        /// </summary>
        internal static readonly string TableName = "logs_h";
        #endregion

        #region Internal Property
        /// <summary>
        /// ID
        /// </summary>
        internal long Id { set; get; }

        /// <summary>
        /// 記録日
        /// </summary>
        internal string RecordedOn { set; get; }
        #endregion

        #region Constructor
        internal LogEntity(MyLogDatabase database) : base(database) { }
        #endregion

        #region Internal Method
        internal override bool Create() {
            var sql = new SqlBuilder();
            sql.AppendSql($"CREATE TABLE {TableName} (")
                .AppendSql($" {Cols.Id}           INTEGER PRIMARY KEY AUTOINCREMENT")
                .AppendSql($",{Cols.RecordedOn}   TEXT NOT NULL")
                .AppendSql($",{Cols.CreateAt}     INTEGER")
                .AppendSql($",{Cols.UpdateAt}     INTEGER")
                .Append(")");
            return 0 <= base.Database.ExecuteNonQuery(sql);
        }

        internal override long Insert() {
            var sql = new SqlBuilder();
            sql.AppendSql($"INSERT INTO {TableName}")
                .AppendSql("(")
                .AppendSql($" {Cols.RecordedOn}")
                .AppendSql($",{Cols.CreateAt}")
                .AppendSql($",{Cols.UpdateAt}")
                .AppendSql(")")
                .AppendSql("VALUES")
                .AppendSql("(")
                .AppendSql($" @{Cols.RecordedOn}")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(")");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.RecordedOn}", this.RecordedOn);
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
        /// 記録日をキーとしてログ情報(ヘッダ)を取得
        /// </summary>
        /// <param name="recordeOn">記録日</param>
        /// <returns>レコードセット</returns>
        internal Recordset SelectByRecordedOn(string recordeOn) {
            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT * FROM {TableName}")
                .AppendSql($"WHERE {Cols.RecordedOn} = @{Cols.RecordedOn}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.RecordedOn}", recordeOn);
            return base.Database.OpenRecordset(sql, paramList);
        }
        #endregion

    }
}
