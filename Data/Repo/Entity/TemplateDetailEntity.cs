using MyLog.Data.Repo.Entity.DataModel;
using OsnLib.Data.Sqlite;
using System;

namespace MyLog.Data.Repo.Entity {
    /// <summary>
    /// templates_d table entity
    /// </summary>
    internal class TemplateDetailEntity : BaseEntity {

        #region Declaration
        /// <summary>
        /// カラム定義
        /// </summary>
        internal static class Cols {
            internal static readonly String Id = "id";
            internal static readonly String TemplateId = "template_id";
            internal static readonly String CategoryId = "category_id";
            internal static readonly String Todo = "todo";
            internal static readonly String Priority = "priority";
            internal static readonly String PlanStart = "plan_start";
            internal static readonly String PlanEnd = "plan_end";
            internal static readonly String PlanTime = "plan_time";
            internal static readonly String CreateAt = "create_at";
            internal static readonly String UpdateAt = "update_at";
        }

        /// <summary>
        /// テーブル名
        /// </summary>
        internal static readonly string TableName = "templates_d";
        #endregion

        #region Internal Property
        /// <summary>
        /// ID
        /// </summary>
        internal long Id { set; get; }

        /// <summary>
        /// テンプレートID
        /// </summary>
        internal long TemplateId { set; get; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        internal long CategoryId { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        internal int Priority { set; get; }

        /// <summary>
        /// Todo
        /// </summary>
        internal string Todo { set; get; }

        /// <summary>
        /// 予定時間(開始)
        /// </summary>
        internal string PlanStart { set; get; }

        /// <summary>
        /// 予定時間(終了)
        /// </summary>
        internal string PlanEnd { set; get; }

        /// <summary>
        /// 予定時間
        /// </summary>
        internal int PlanTime { set; get; }
        #endregion

        #region Constructor
        internal TemplateDetailEntity(MyLogDatabase database) : base(database) { }
        #endregion

        #region Internal Method
        internal override bool Create() {
            var sql = new SqlBuilder();
            sql.AppendSql($"CREATE TABLE {TableName} (")
                .AppendSql($" {Cols.Id}           INTEGER PRIMARY KEY AUTOINCREMENT")
                .AppendSql($",{Cols.TemplateId}   INTEGER NOT NULL")
                .AppendSql($",{Cols.CategoryId}   INTEGER NOT NULL")
                .AppendSql($",{Cols.Priority}     INTEGER NOT NULL")
                .AppendSql($",{Cols.Todo}         TEXT")
                .AppendSql($",{Cols.PlanStart}    TEXT")
                .AppendSql($",{Cols.PlanEnd}      TEXT")
                .AppendSql($",{Cols.PlanTime}     INTEGER")
                .AppendSql($",{Cols.CreateAt}     INTEGER")
                .AppendSql($",{Cols.UpdateAt}     INTEGER")
                .Append(")");
            return 0 <= base.Database.ExecuteNonQuery(sql);
        }

        internal override long Insert() {
            var sql = new SqlBuilder();
            sql.AppendSql($"INSERT INTO {TableName}")
                .AppendSql("(")
                .AppendSql($" {Cols.TemplateId}")
                .AppendSql($",{Cols.CategoryId}")
                .AppendSql($",{Cols.Priority}")
                .AppendSql($",{Cols.Todo}")
                .AppendSql($",{Cols.PlanStart}")
                .AppendSql($",{Cols.PlanEnd}")
                .AppendSql($",{Cols.PlanTime}")
                .AppendSql($",{Cols.CreateAt}")
                .AppendSql($",{Cols.UpdateAt}")
                .AppendSql(")")
                .AppendSql("VALUES")
                .AppendSql("(")
                .AppendSql($" @{Cols.TemplateId}")
                .AppendSql($",@{Cols.CategoryId}")
                .AppendSql($",@{Cols.Priority}")
                .AppendSql($",@{Cols.Todo}")
                .AppendSql($",@{Cols.PlanStart}")
                .AppendSql($",@{Cols.PlanEnd}")
                .AppendSql($",@{Cols.PlanTime}")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(")");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.TemplateId}", this.TemplateId);
            paramList.Add($"@{Cols.CategoryId}", this.CategoryId);
            paramList.Add($"@{Cols.Priority}", this.Priority);
            paramList.Add($"@{Cols.Todo}", this.Todo);
            paramList.Add($"@{Cols.PlanStart}", this.PlanStart);
            paramList.Add($"@{Cols.PlanEnd}", this.PlanEnd);
            paramList.Add($"@{Cols.PlanTime}", this.PlanTime);
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
        /// テンプレートidをキーとしてレコードを削除する
        /// </summary>
        /// <param name="id">id</param>
        internal void DeleteByTemplateId(long id) {
            var sql = new SqlBuilder();
            sql.AppendSql($"DELETE FROM {TableName}")
                .AppendSql($"WHERE {Cols.TemplateId} = @{Cols.TemplateId}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.TemplateId}", id);
            base.Database.ExecuteNonQuery(sql, paramList);
        }

        /// <summary>
        /// テンプレートIDをキーとしてテンプレート情報(明細)を取得する
        /// </summary>
        /// <param name="templateId">テンプレートID</param>
        /// <returns></returns>
        internal Recordset SelectByTemplateId(long templateId) {
            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT * FROM {TableName}")
                .AppendSql($"WHERE {Cols.TemplateId} = @{Cols.TemplateId}")
                .AppendSql($"ORDER BY {Cols.Priority}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.TemplateId}", templateId);
            return base.Database.OpenRecordset(sql, paramList);
        }

        /// <summary>
        /// データモデルの情報をメンバーに設定する。
        /// </summary>
        /// <param name="data">データモデル</param>
        /// <param name="templateId">テンプレートID</param>
        internal void Set(TemplateDetailData data, long templateId) {
            this.TemplateId = templateId;
            this.CategoryId = data.CategoryId;
            this.Priority = data.Priority;
            this.Todo = data.Todo;
            this.PlanStart = data.PlanStart;
            this.PlanEnd = data.PlanEnd;
            this.PlanTime = data.PlanTime;
        }

        /// <summary>
        /// テンプレートの情報をログに挿入する
        /// </summary>
        /// <param name="templateId">テンプレートID</param>
        /// <param name="logId">ログID</param>
        internal void InsertToLog(long templateId, long logId) {

        }
        #endregion

    }
}
