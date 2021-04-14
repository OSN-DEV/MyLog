using MyLog.Component;
using MyLog.Data.Repo.Entity.DataModel;
using System.Windows;
using WPF.JoshSmith.ServiceProviders.UI;

namespace MyLog.UI.Main {
    /// <summary>
    /// MyLogMainWindow
    /// </summary>
    public partial class MyLogMainWindow : Window {

        #region Declaration
        private readonly MyLogMainViewModel _viewModel;
        #endregion

        #region Constructor
        public MyLogMainWindow() {
            InitializeComponent();

            this._viewModel = new MyLogMainViewModel(this);
            this.DataContext = this._viewModel;
            var manager = new ListViewDragDropManager<LogDetailData>(this.cData) {
                AllowStartX = 0,
                AllowEndX = 24
            };
            manager.IsValidItem = this._viewModel.IsValidItem;
            manager.DropDone += this._viewModel.DropDone;

            var templogManager = new ListViewDragDropManager<TempLogData>(this.cTempLogData) {
                AllowStartX = 0,
                AllowEndX = 24
            };
            templogManager.IsValidItem = this._viewModel.IsValidTempItem;
            templogManager.DropDone += this._viewModel.TempDropDone;
        }
        #endregion

        #region Event
        /// <summary>
        /// Todo Text Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodoTextValueChanged(object sender, System.EventArgs e) {
            var t = sender as CustomTextBox;
            this._viewModel.TodoChanged(Obj2Long(t.Tag), t.Text);
        }

        /// <summary>
        /// Memo Text Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemoChanged(object sender, System.EventArgs e) {
            var t = sender as CustomTextBox;
            this._viewModel.MemoChanged(Obj2Long(t.Tag), t.Text);
        }

        /// <summary>
        /// 結果変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultChanged(object sender, ResultButton.ResultChangedEventArgs e) {
            this._viewModel.ResultChanged(e.Id, e.Result);
        }

        /// <summary>
        /// 時間変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTimeChanged(object sender, TimeSpanText.TimeDataChangedEventArgs e) {
            this._viewModel.StartTimeChanged(e.Id, e.Start, e.End, e.Span);
        }

        /// <summary>
        /// 時間変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndTimeChanged(object sender, TimeSpanText.TimeDataChangedEventArgs e) {
            this._viewModel.EndTimeChanged(e.Id, e.Start, e.End, e.Span);
        }

        /// <summary>
        /// Todo Text Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempLogTextValueChanged(object sender, System.EventArgs e) {
            var t = sender as CustomTextBox;
            this._viewModel.TempLogChanged(Obj2Long(t.Tag), t.Text);
        }

        /// <summary>
        /// Memo Text Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempLogMemoChanged(object sender, System.EventArgs e) {
            var t = sender as CustomTextBox;
            this._viewModel.TempLogMemoChanged(Obj2Long(t.Tag), t.Text);
        }
        #endregion


        #region Private Method
        /// <summary>
        /// object から long への型変換
        /// </summary>
        /// <param name="obj">変換対象</param>
        /// <returns>変換結果</returns>
        private long Obj2Long(object obj) {
            return long.Parse(obj.ToString());
        }
        #endregion


    }
}
