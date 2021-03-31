using MyLog.Data.Repo.Entity;

namespace MyLog.Data.Repo {
    /// <summary>
    /// base repository.
    /// </summary>
    internal class BaseRepo {

        #region Internal Property 
        /// <summary>
        /// データベース
        /// </summary>
        protected MyLogDatabase Database { set; get; }
        #endregion
    }
}
