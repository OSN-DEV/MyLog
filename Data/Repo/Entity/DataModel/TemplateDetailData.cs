using MyLog.UI;
using System.Windows;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// template detail data
    /// </summary>
    public class TemplateDetailData :BaseBindable {

        #region Public Property
        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CategoryId { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        private int _priority;
        public int Priority {
            set { base.SetProperty(ref this._priority, value); }
            get { return this._priority; }
        }

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
        public string PlanTime { set; get; }

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
