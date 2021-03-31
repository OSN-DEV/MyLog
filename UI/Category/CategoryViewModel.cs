using MyLog.AppCommon;
using MyLog.Data.Repo;
using MyLog.Data.Repo.Entity.DataModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyLog.UI.Category {
    /// <summary>
    /// category window view model
    /// </summary>
    class CategoryViewModel : BaseBindable {

        #region Declaration
        private readonly Window _view;
        #endregion

        #region Public Property
        /// <summary>
        /// リスト情報
        /// </summary>
        public ObservableCollection<CategoryData> DataContext { set; get; }

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public DelegateCommand SaveCommand { set; get; }

        /// <summary>
        /// 閉じるコマンド
        /// </summary>
        public DelegateCommand CloseCommand { set; get; }
        #endregion

        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">ウィンドウ</param>
        internal CategoryViewModel(Window view) {
            this._view = view;

            var repo = new CategoryRepo();
            this.DataContext = repo.Select();

            this.SaveCommand = new DelegateCommand(SaveClick);
            this.CloseCommand = new DelegateCommand(CloseClick);
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 保存クリック時の処理
        /// </summary>
        private void SaveClick() {
            try {
                var repo = new CategoryRepo();
                repo.Update(this.DataContext);
                this._view.DialogResult = true;
            } catch(Exception ex) {
                Message.ShowError(this._view, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// キャンセルクリック時の処理
        /// </summary>
        private void CloseClick() {
            this._view.DialogResult = false;
        }
        #endregion

    }
}
