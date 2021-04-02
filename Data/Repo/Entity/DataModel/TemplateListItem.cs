using System.Windows.Media;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// テンプレートリストのアイテム
    /// </summary>
    public class TemplateListItem {

        #region Public Property
        /// <summary>
        /// id
        /// </summary>
        public long Id { set; get; }

        /// <summary>
        /// テンプレート名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 日
        /// </summary>
        public SolidColorBrush Sun { set; get; }

        /// <summary>
        /// 月
        /// </summary>
        public SolidColorBrush Mon { set; get; }

        /// <summary>
        /// 火
        /// </summary>
        public SolidColorBrush Tue { set; get; }

        /// <summary>
        /// 水
        /// </summary>
        public SolidColorBrush Wed { set; get; }

        /// <summary>
        /// 木
        /// </summary>
        public SolidColorBrush Thu { set; get; }

        /// <summary>
        /// 金
        /// </summary>
        public SolidColorBrush Fri { set; get; }

        /// <summary>
        /// 土
        /// </summary>
        public SolidColorBrush Sat { set; get; }
        #endregion

        #region Public Method
        public override string ToString() {
            return this.Name;
        }
        #endregion
    }
}
