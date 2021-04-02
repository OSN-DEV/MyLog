using MyLog.Data.Repo.Entity.DataModel;
using OsnLib.Data.Sqlite;
using System;


namespace MyLog.Data.Repo.Entity {
    /// <summary>
    /// templates_h table entity
    /// </summary>
    internal class TemplateEntity : BaseEntity {

        #region Declaration
        /// <summary>
        /// カラム定義
        /// </summary>
        internal static class Cols {
            internal static readonly String Id = "id";
            internal static readonly String Name = "name";
            internal static readonly String Sun = "sun";
            internal static readonly String Mon = "mon";
            internal static readonly String Tue = "tue";
            internal static readonly String Wed = "wed";
            internal static readonly String Thu = "thu";
            internal static readonly String Fri = "fri";
            internal static readonly String Sat = "sat";
            internal static readonly String CreateAt = "create_at";
            internal static readonly String UpdateAt = "update_at";
        }

        /// <summary>
        /// テーブル名
        /// </summary>
        internal static readonly string TableName = "templates_h";
        #endregion

        #region Internal Property
        /// <summary>
        /// ID
        /// </summary>
        internal long Id { set; get; }

        /// <summary>
        /// テンプレート名
        /// </summary>
        internal string Name { set; get; }

        /// <summary>
        /// 日
        /// </summary>
        internal bool Sun { set; get; }

        /// <summary>
        /// 月
        /// </summary>
        internal bool Mon { set; get; }

        /// <summary>
        /// 火
        /// </summary>
        internal bool Tue { set; get; }

        /// <summary>
        /// 水
        /// </summary>
        internal bool Wed { set; get; }

        /// <summary>
        /// 木
        /// </summary>
        internal bool Thu { set; get; }

        /// <summary>
        /// 金
        /// </summary>
        internal bool Fri { set; get; }

        /// <summary>
        /// 土
        /// </summary>
        internal bool Sat { set; get; }
        #endregion

        #region Constructor
        internal TemplateEntity(MyLogDatabase database) : base(database) { }
        #endregion

        #region Internal Method
        internal override bool Create() {
            var sql = new SqlBuilder();
            sql.AppendSql($"CREATE TABLE {TableName} (")
                .AppendSql($" {Cols.Id}           INTEGER PRIMARY KEY AUTOINCREMENT")
                .AppendSql($",{Cols.Name}         TEXT NOT NULL")
                .AppendSql($",{Cols.Sun}          INTEGER NOT NULL DEFAULT 0")
                .AppendSql($",{Cols.Mon}          INTEGER NOT NULL DEFAULT 0")
                .AppendSql($",{Cols.Tue}          INTEGER NOT NULL DEFAULT 0")
                .AppendSql($",{Cols.Wed}          INTEGER NOT NULL DEFAULT 0")
                .AppendSql($",{Cols.Thu}          INTEGER NOT NULL DEFAULT 0")
                .AppendSql($",{Cols.Fri}          INTEGER NOT NULL DEFAULT 0")
                .AppendSql($",{Cols.Sat}          INTEGER NOT NULL DEFAULT 0")
                .AppendSql($",{Cols.CreateAt}     INTEGER")
                .AppendSql($",{Cols.UpdateAt}     INTEGER")
                .Append(")");
            return 0 <= base.Database.ExecuteNonQuery(sql);
        }

        internal override long Insert() {
            var sql = new SqlBuilder();
            sql.AppendSql($"INSERT INTO {TableName}")
                .AppendSql("(")
                .AppendSql($" {Cols.Name}")
                .AppendSql($",{Cols.Sun}")
                .AppendSql($",{Cols.Mon}")
                .AppendSql($",{Cols.Tue}")
                .AppendSql($",{Cols.Wed}")
                .AppendSql($",{Cols.Thu}")
                .AppendSql($",{Cols.Fri}")
                .AppendSql($",{Cols.Sat}")
                .AppendSql($",{Cols.CreateAt}")
                .AppendSql($",{Cols.UpdateAt}")
                .AppendSql(")")
                .AppendSql("VALUES")
                .AppendSql("(")
                .AppendSql($" @{Cols.Name}")
                .AppendSql($",@{Cols.Sun}")
                .AppendSql($",@{Cols.Mon}")
                .AppendSql($",@{Cols.Tue}")
                .AppendSql($",@{Cols.Wed}")
                .AppendSql($",@{Cols.Thu}")
                .AppendSql($",@{Cols.Fri}")
                .AppendSql($",@{Cols.Sat}")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(",datetime('now', 'localtime')")
                .AppendSql(")");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.Name}", this.Name);
            paramList.Add($"@{Cols.Sun}", this.Sun);
            paramList.Add($"@{Cols.Mon}", this.Mon);
            paramList.Add($"@{Cols.Tue}", this.Tue);
            paramList.Add($"@{Cols.Wed}", this.Wed);
            paramList.Add($"@{Cols.Thu}", this.Thu);
            paramList.Add($"@{Cols.Fri}", this.Fri);
            paramList.Add($"@{Cols.Sat}", this.Sat);
            return base.Database.Insert(sql, paramList);
        }

        /// <summary>
        /// 更新を行う
        /// </summary>
        internal void Update() {
            var sql = new SqlBuilder();
            sql.AppendSql($"UPDATE {TableName} SET")
                .AppendSql($" {Cols.Name} = @{Cols.Name}")
                .AppendSql($",{Cols.Sun} = @{Cols.Sun}")
                .AppendSql($",{Cols.Mon} = @{Cols.Mon}")
                .AppendSql($",{Cols.Tue} = @{Cols.Tue}")
                .AppendSql($",{Cols.Wed} = @{Cols.Wed}")
                .AppendSql($",{Cols.Thu} = @{Cols.Thu}")
                .AppendSql($",{Cols.Fri} = @{Cols.Fri}")
                .AppendSql($",{Cols.Sat} = @{Cols.Sat}")
                .AppendSql($",{Cols.UpdateAt} = datetime('now', 'localtime')")
                .AppendSql($"WHERE {Cols.Id}=@{Cols.Id}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.Id}", this.Id);
            paramList.Add($"@{Cols.Name}", this.Name);
            paramList.Add($"@{Cols.Sun}", this.Sun);
            paramList.Add($"@{Cols.Mon}", this.Mon);
            paramList.Add($"@{Cols.Tue}", this.Tue);
            paramList.Add($"@{Cols.Wed}", this.Wed);
            paramList.Add($"@{Cols.Thu}", this.Thu);
            paramList.Add($"@{Cols.Fri}", this.Fri);
            paramList.Add($"@{Cols.Sat}", this.Sat);
            base.Database.ExecuteNonQuery(sql, paramList);
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
        /// テンプレート情報(ヘッダ)を取得する
        /// </summary>
        /// <returns></returns>
        internal Recordset Select() {
            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT * FROM {TableName}")
                .AppendSql($"ORDER BY {Cols.Name}, {Cols.Id}");
            return base.Database.OpenRecordset(sql);
        }

        /// <summary>
        /// IDをキーとしてテンプレート情報(ヘッダ)を取得する
        /// </summary>
        /// <returns></returns>
        internal Recordset SelectById(long id) {
            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT * FROM {TableName}")
                .AppendSql($"WHERE {Cols.Id} = @{Cols.Id}");
            var paramList = new ParameterList();
            paramList.Add($"@{Cols.Id}", id);
            return base.Database.OpenRecordset(sql, paramList);
        }

        /// <summary>
        /// データモデルの情報をメンバーに設定する。
        /// </summary>
        /// <param name="data">データモデル</param>
        internal void Set(TemplateData data) {
            this.Id = data.Id;
            this.Name = data.Name;
            this.Sun = data.Sun;
            this.Mon = data.Mon;
            this.Tue = data.Tue;
            this.Wed = data.Wed;
            this.Thu = data.Thu;
            this.Fri = data.Fri;
            this.Sat = data.Sat;
        }

        /// <summary>
        /// 指定された日付の曜日をキーとしてデータを取得する
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns>レコードセット</returns>
        internal Recordset SelectByWeekDay(string date) {
            var dt = DateTime.Parse(date).DayOfWeek;

            var sql = new SqlBuilder();
            sql.AppendSql($"SELECT * FROM {TableName}");
            switch(dt) {
                case DayOfWeek.Sunday:
                    sql.AppendSql($"WHERE {Cols.Sun} = 1");
                    break;
                case DayOfWeek.Monday:
                    sql.AppendSql($"WHERE {Cols.Mon} = 1");
                    break;
                case DayOfWeek.Tuesday:
                    sql.AppendSql($"WHERE {Cols.Tue} = 1");
                    break;
                case DayOfWeek.Wednesday:
                    sql.AppendSql($"WHERE {Cols.Wed} = 1");
                    break;
                case DayOfWeek.Thursday:
                    sql.AppendSql($"WHERE {Cols.Thu} = 1");
                    break;
                case DayOfWeek.Friday:
                    sql.AppendSql($"WHERE {Cols.Fri} = 1");
                    break;
                case DayOfWeek.Saturday:
                    sql.AppendSql($"WHERE {Cols.Sat} = 1");
                    break;
            }
            sql.AppendSql($"ORDER BY {Cols.Id}");
            return base.Database.OpenRecordset(sql);
        }
        #endregion
    }
}
