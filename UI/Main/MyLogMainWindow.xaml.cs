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
        private MyLogMainViewModel _viewModel;
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
        private void CustomTextBox_TextValueChanged(object sender, System.EventArgs e) {
            var t = sender as CustomTextBox;
            this._viewModel.MemoChanged(Obj2Long(t.Tag), t.Text);
        }

        /// <summary>
        /// result 変更
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        private void ResultChanged(long id, short result) {
            this._viewModel.ResultChanged(id, result);
        }

        /// <summary>
        /// 時間変更
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="span"></param>
        private void StartTime_TimeDataChanged(long id, string start, string end, int span) {
            this._viewModel.StartTimeChanged(id, start, end, span);
        }

        /// <summary>
        /// 時間変更
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="span"></param>
        private void EndTime_TimeDataChanged(long id, string start, string end, int span) {
            this._viewModel.EndTimeChanged(id, start, end, span);
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
