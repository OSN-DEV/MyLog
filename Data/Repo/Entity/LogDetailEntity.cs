using MyLog.Data.Repo.Entity.DataModel;
using OsnLib.Data.Sqlite;
using System;

namespace MyLog.Data.Repo.Entity {
    /// <summary>
    /// logs_d table entity
    /// </summary>
    internal class LogDetailEntity : BaseEntity {

        #region Declaration
        /// <summary>
        /// カラム定義
        /// </summary>
        internal static class Cols {
            internal static readonly String Id = "id";
            internal static readonly String LogId = "log_id";
            internal static readonly String CategoryId = "category_id";
            internal static readonly String Priority = "priority";
            internal static readonly String Result = "result";
            internal static readonly String Todo = "todo";
            internal static readonly String PlanStart = "plan_start";
            internal static readonly String PlanEnd = "plan_end";
            internal static readonly String PlanTime = "plan_time";
            internal static readonly String ActualStart = "actual_start";
            internal static readonly String ActualEnd = "actual_end";
            internal static readonly String ActualTime = "actual_time";
            internal static readonly String Memo = "memo";
            internal static readonly String CreateAt = "create_at";
            internal static readonly String UpdateAt = "update_at";
        }

        /// <summary>
        /// テーブル名
        /// </summary>
        internal static readonly string TableName = "logs_d";

        #endregion

        #region Internal Property
        /// <summary>
        /// ID
        /// </summary>
        internal long Id { set; get; }

        /// <summary>
        /// ログ情報ID
        /// </summary>
        internal long LogId { set; get; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        internal long CategoryId { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        internal int Priority { set; get; }

        /// <summary>
        /// 結果
        /// </summary>
        internal int Result { set; get; }

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

        /// <summary>
        /// 実績時間(開始)
        /// </summary>
        internal string ActualStart { set; get; }

        /// <summary>
        /// 実績時間(終了)
        /// </summary>
        internal string ActualEnd { set; get; }

        /// <summary>
        /// 実績時間
        /// </summary>
        internal int ActualTime { set; get; }

        /// <summary>
        /// メモ
        /// </summary>
        internal string Memo { set; get; }
        #endregion

        #region Constructor
        internal LogDetailEntity() { }
        internal LogDetailEntity(MyLogDatabase database) : base(database) { }
        #endregion

        #region Internal Method
        internal override bool Create() {
            var sql = new SqlBuilder();
            sql.AppendSql($"CREATE TABLE {TableName} (")
                .AppendSql($" {Cols.Id}           INTEGER PRIMARY KEY AUTOINCREMENT")
                .AppendSql($",{Cols.LogId}        INTEGER NOT NULL")
                .AppendSql($",{Cols.CategoryId}   INTEGER NOT NULL")
                .AppendSql($",{Cols.Priority}     INTEGER NOT NULL")
                .AppendSql($",{Cols.Result}       INTEGER")
                .AppendSql($",{Cols.Todo}         TEXT")
                .AppendSql($",{Cols.PlanStart}    TEXT")
                .AppendSql($",{Cols.PlanEnd}      TEXT")
                .AppendSql($",{Cols.PlanTime}     INTEGER")
                .AppendSql($",{Cols.ActualStart}  TEXT")
                .AppendSql($",{Cols.ActualEnd}    TEXT")
                .AppendSql($",{Cols.ActualTime}   INTEGER")
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
                .AppendSql($" {Cols.LogId}")
                .AppendSql($",{Cols.CategoryId}")
                .AppendSql($",{Cols.Priority}")
                .AppendSql($",{Cols.Todo}")
                .AppendSql($",{Cols.PlanStart}")
                .AppendSql($",{Cols.PlanEnd}")
                .AppendSql($",{Cols.PlanTime}")
                .AppendSql($",{Cols.ActualStart}")
                .AppendSql($",{Cols.ActualEnd}")
                .AppendSql($",{Cols.ActualTime}")
                .AppendSql($",{Cols.Memo}")
                .AppendSql($",{Cols.CreateAt}")
                .AppendSql($",{Cols.UpdateAt}")
                .AppendSql(")")
                .AppendSql("VALUES")
                .AppendSql("(")
                .AppendSql($" @{Cols.LogId}")
                .AppendSql($",@{Cols.CategoryId}")
                .AppendSql($",@{Cols.Priority}")
                .AppendSql($",@{Cols.Todo}")
                .AppendSql($",@{Cols.PlanStart}")
                .AppendSql($",@{Cols.PlanEnd}")
                .AppendSql($",@{Cols.PlanTime}")
                .AppendSql($",@{Cols.ActualStart}")
                .AppendSql($",@{Cols.ActualEnd}")
                .AppendSql($",@{Cols.ActualTime}")
                .AppendSql($",@{Cols.Memo}")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(")");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.LogId}", this.LogId);
            paramList.Add($"@{Cols.CategoryId}", this.CategoryId);
            paramList.Add($"@{Cols.Priority}", this.Priority);
            paramList.Add($"@{Cols.Todo}", this.PlanStart);
            paramList.Add($"@{Cols.PlanStart}", this.PlanStart);
            paramList.Add($"@{Cols.PlanEnd}", this.PlanEnd);
            paramList.Add($"@{Cols.PlanTime}", this.PlanTime);
            paramList.Add($"@{Cols.ActualStart}", this.ActualStart);
            paramList.Add($"@{Cols.ActualEnd}", this.ActualEnd);
            paramList.Add($"@{Cols.ActualTime}", this.ActualTime);
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
        /// ログ情報IDをキーとしてレコードを取得する
        /// </summary>
        /// <param name="id">ログ情報ID</param>
        /// <returns>レコードセット</returns>
        internal Recordset SelectByLogHId(long id) {
            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT {TableName}.* FROM {TableName}")
                .AppendSql($"INNER JOIN {CategoryEntity.TableName} ON ")
                .AppendSql($"{TableName}.{Cols.CategoryId} = {CategoryEntity.TableName}.{CategoryEntity.Cols.Id}")
                .AppendSql($"WHERE {Cols.LogId} = @{Cols.LogId}")
                .AppendSql($"ORDER BY {CategoryEntity.TableName}.{CategoryEntity.Cols.Priority}")
                .AppendSql($",{Cols.Priority}, {Cols.Id}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.LogId}", id);
            return base.Database.OpenRecordset(sql, paramList);
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

        /// <summary>
        /// ログデータをエンティティん設定する
        /// </summary>
        /// <param name="data">ログデータ</param>
        internal void Set(LogDetailData data) {
            this.Id = data.Id;
            this.LogId = data.LogId;
            this.CategoryId = data.CategoryId;
            this.Priority = data.Priority;
            this.Result = (int)data.Result;
            this.Todo = data.Todo;
            this.PlanStart = data.PlanStart;
            this.PlanEnd = data.PlanEnd;
            this.PlanTime = data.PlanTime;
            this.ActualStart = data.ActualStart;
            this.ActualEnd = data.ActualEnd;
            this.ActualTime = data.ActualTime;
            this.Memo = data.Memo;
        }
        #endregion

    }
}
