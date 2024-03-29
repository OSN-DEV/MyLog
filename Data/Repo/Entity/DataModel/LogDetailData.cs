﻿using MyLog.UI;
using System.Windows;
using static MyLog.Component.ResultButton;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// log detail data
    /// </summary>
    public class LogDetailData : BaseBindable{

        #region Public Property
        /// <summary>
        /// id
        /// </summary>
        private long _id;
        public long Id { set { base.SetProperty(ref this._id, value); } get { return this._id; } }

        /// <summary>
        /// ログID
        /// </summary>
        public long LogId { set; get; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CategoryId { set; get; }

        /// <summary>
        /// 並び順
        /// </summary>
        private int _priority;
        public int Priority { set { base.SetProperty(ref this._priority, value); } get { return this._priority; } }

        /// <summary>
        /// 結果
        /// </summary>
        private ResultState _resuilt;
        public ResultState Result { set { base.SetProperty(ref this._resuilt, value); } get { return this._resuilt; } }

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
        public string ActualTime { set; get; }

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
