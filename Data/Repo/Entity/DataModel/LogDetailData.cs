using System.Windows;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// log detail data
    /// </summary>
    public class LogDetailData {

        #region Public Property
        /// <summary>
        /// id
        /// </summary>
        public long Id { set; get; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CategoryId { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        public int Priority { set; get; }

        /// <summary>
        /// 結果
        /// </summary>
        public int Result { set; get; }

        /// <summary>
        /// Todo
        /// </summary>
        public string Todo { set; get; }

        /// <summary>
        /// 予定時間(開始)
        /// </summary>
        public string PlanStart { set; get; }

        /// <summary>
        /// 予定時間(終了)
        /// </summary>
        public string PlanEnd { set; get; }

        /// <summary>
        /// 予定時間
        /// </summary>
        public int PlanTime { set; get; }

        /// <summary>
        /// 実績時間(開始)
        /// </summary>
        public string ActualStart { set; get; }

        /// <summary>
        /// 実績時間(終了)
        /// </summary>
        public string ActualEnd { set; get; }

        /// <summary>
        /// 実績時間
        /// </summary>
        public int ActualTime { set; get; }

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
