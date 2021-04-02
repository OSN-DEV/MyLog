using MyLog.Data.Repo;
using MyLog.Data.Repo.Entity.DataModel;
using System.Collections.Generic;
using System.Windows;

namespace MyLog.UI.TemplateSelect {
    /// <summary>
    /// template select view model
    /// </summary>
    class TemplateSelectViewModel : BaseBindable {

        #region Declaration
        private readonly Window _view;
        #endregion

        #region Public Property
        /// <summary>
        /// リスト情報
        /// </summary>
        public List<TemplateListItem> ListData { set; get; }

        /// <summary>
        /// 現在の行
        /// </summary>
        private int _currentIndex = 0;
        public int CurrentIndex {
            get { return this._currentIndex; }
            set {
                base.SetProperty(ref this._currentIndex, value);
                base.SetProperty(nameof(IsSelectEnabled));
            }
        }

        /// <summary>
        /// 選択ボタンの使用可否
        /// </summary>
        public bool IsSelectEnabled {
            get { return 0 <= this._currentIndex; }
        }

        /// <summary>
        /// 選択されたテンプレートのID
        /// </summary>
        public long SelectedTemplatId { private set; get; }

        /// <summary>
        /// 選択コマンド
        /// </summary>
        public DelegateCommand SelectCommand { set; get; }

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
        internal TemplateSelectViewModel(Window view) {
            this._view = view;

            var repo = new TemplateSelectRepo();
            this.ListData = repo.Select();

            this.SelectCommand = new DelegateCommand(SelectClick);
            this.CloseCommand = new DelegateCommand(CloseClick);
        }
        #endregion

        #region Internal Method
        /// <summary>
        /// リストアイテムダブルクリック時の処理
        /// </summary>
        internal void ListItemDoubleClick() {
            SelectClick();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 選択クリック時の処理
        /// </summary>
        private void SelectClick() {
            var item = this.ListData[this.CurrentIndex];
            this.SelectedTemplatId = item.Id;
            this._view.DialogResult = true;
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
