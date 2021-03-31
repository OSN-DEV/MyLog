using System.Collections.ObjectModel;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// log data
    /// </summary>
    public class LogData {

        #region public Property
        /// <summary>
        /// id
        /// </summary>
        public long Id { set; get; }

        /// <summary>
        /// 記録日
        /// </summary>
        public string RecordedOn { set; get; }

        /// <summary>
        /// 詳細情報
        /// </summary>
        public ObservableCollection<LogDetailData> LogList { set; get; }
        #endregion

    }
}
