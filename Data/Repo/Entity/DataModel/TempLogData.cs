using System.Windows;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// temp log データ
    /// </summary>
    public class TempLogData {

        #region public Property
        /// <summary>
        /// id
        /// </summary>
        public long Id { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        public int Priority { set; get; }

        /// <summary>
        /// Todo
        /// </summary>
        public string Todo { set; get; }

        /// <summary>
        /// メモ
        /// </summary>
        public string Memo { set; get; }

        /// <summary>
        /// カテゴリフラグ
        /// </summary>
        public bool IsCategory { set; get; }

        /// <summary>
        /// カテゴリ名
        /// </summary>
        public string CategoryName { set; get; }

        /// <summary>
        /// カテゴリヘッダの可視
        /// </summary>
        public Visibility CategoryVisibility {
            get {
                return this.IsCategory ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// ログの可視
        /// </summary>
        public Visibility LogVisibility {
            get {
                return this.IsCategory ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        #endregion

    }
}
