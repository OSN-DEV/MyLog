using OsnLib.Data.Sqlite;
using System;

namespace MyLog.Data.Repo.Entity {
    /// <summary>
    ///  entity base class
    /// </summary>
    internal abstract class BaseEntity {

        #region Internal Property
        /// <summary>
        /// データベース
        /// </summary>
        internal MyLogDatabase Database { set; get; }

        /// <summary>
        /// パラメータリスト
        /// </summary>
        internal ParameterList Params { set; get; } = new ParameterList();

        /// <summary>
        /// 作成日時
        /// </summary>
        internal virtual DateTime CreateAt { set; get; }

        /// <summary>
        /// 更新日時
        /// </summary>
        internal virtual DateTime UpdateAt { set; get; }
        #endregion

        #region Constructor
        internal BaseEntity() { }
        internal BaseEntity(MyLogDatabase database) {
            this.Database = database;
        }
        #endregion

        #region Internal Method
        /// <summary>
        /// テーブルを作成する
        /// </summary>
        /// <returns>true: success, false: otherwise</returns>
        internal abstract bool Create();

        /// <summary>
        /// データを挿入する
        /// </summary>
        /// <returns>if success return id, else return -1</returns>
        internal abstract long Insert();

        /// <summary>
        /// データを削除する
        /// </summary>
        internal virtual void Delete() {
            throw new NotImplementedException("Delete method is not implemented.");
        }

        /// <summary>
        /// パラメータリストをクリアする。
        /// </summary>
        internal void ClearParams() {
            this.Params.Clear();
        }

        /// <summary>
        /// パラメータを追加する
        /// </summary>
        /// <param name="key">キー(カラム名)</param>
        /// <param name="val">値</param>
        internal void AddParams(string key, string val) {
            this.Params.Add($"@{key}", val);
        }

        /// <summary>
        /// パラメータを追加する
        /// </summary>
        /// <param name="key">キー(カラム名)</param>
        /// <param name="val">値</param>
        internal void AddParams(string key, int val) {
            this.Params.Add($"@{key}", val);
        }

        /// <summary>
        /// パラメータを追加する
        /// </summary>
        /// <param name="key">キー(カラム名)</param>
        /// <param name="val">値</param>
        internal void AddParams(string key, long val) {
            this.Params.Add($"@{key}", val);
        }

        /// <summary>
        /// パラメータを追加する
        /// </summary>
        /// <param name="key">キー(カラム名)</param>
        /// <param name="val">値</param>
        internal void AddParams(string key, bool val) {
            this.Params.Add($"@{key}", MyLogDatabase.Bool2Int(val));
        }
        #endregion

    }
}
