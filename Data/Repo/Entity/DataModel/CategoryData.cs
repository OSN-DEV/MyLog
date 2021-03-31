namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// category data
    /// </summary>
    public class CategoryData {

        #region Public Property
        /// <summary>
        /// id
        /// </summary>
        public long Id { set; get; }

        /// <summary>
        /// カテゴリ名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        public int Priority { set; get; }

        /// <summary>
        /// 可視
        /// </summary>
        public bool Visible { set; get; }
        #endregion

    }
}
