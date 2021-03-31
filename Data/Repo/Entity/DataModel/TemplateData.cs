using System.Collections.ObjectModel;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// template data
    /// </summary>
    public class TemplateData {

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
        public bool Sun { set; get; }

        /// <summary>
        /// 月
        /// </summary>
        public bool Mon { set; get; }

        /// <summary>
        /// 火
        /// </summary>
        public bool Tue { set; get; }

        /// <summary>
        /// 水
        /// </summary>
        public bool Wed { set; get; }

        /// <summary>
        /// 木
        /// </summary>
        public bool Thu { set; get; }

        /// <summary>
        /// 金
        /// </summary>
        public bool Fri { set; get; }

        /// <summary>
        /// 土
        /// </summary>
        public bool Sat { set; get; }

        /// <summary>
        /// 詳細情報
        /// </summary>
        public ObservableCollection<TemplateDetailData> LogList { set; get; }
        #endregion

        #region Public Method
        public override string ToString() {
            return this.Name;
        }
        #endregion
    }
}
