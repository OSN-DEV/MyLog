using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyLog.UI {
    /// <summary>
    /// 日付選択
    /// </summary>
    public partial class DateSelect : Window {

        #region Publi Property
        /// <summary>
        /// 選択された日付
        /// </summary>
        public string SelectedDate { private set; get; }

        /// <summary>
        /// 選択コマンド
        /// </summary>
        public DelegateCommand SelectCommand { set; get; }

        /// <summary>
        /// キャンセルコマンド
        /// </summary>
        public DelegateCommand CancelCommand { set; get; }
        #endregion

        #region Constructor
        public DateSelect() {
            InitializeComponent();
            this.Initialize();
        }

        public DateSelect(string date) {
            InitializeComponent();
            this.cCalendar.SelectedDate = DateTime.Parse(date);
            this.cCalendar.DisplayDate = DateTime.Parse(date);
            this.Initialize();
        }
        #endregion

        #region Event
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e) {
            base.OnPreviewMouseDown(e);
            base.OnPreviewMouseUp(e);
            if ((Mouse.Captured is Calendar) || (Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)) {
                Mouse.Capture(null);
            }
        }
        #endregion

        #region Private Method(Event)
        /// <summary>
        /// 初期処理
        /// </summary>
        private void Initialize() {
            this.SelectCommand = new DelegateCommand(SelectClick);
            this.CancelCommand = new DelegateCommand(CancelClick);
            this.DataContext = this;
        }

        /// <summary>
        /// 選択クリック時の処理
        /// </summary>
        private void SelectClick() {
            this.SelectedDate = this.cCalendar.SelectedDate?.ToString("yyyy/MM/dd");
            this.DialogResult = true;
        }

        /// <summary>
        /// キャンセルクリック時の処理
        /// </summary>
        private void CancelClick() {
            this.DialogResult = false;
        }
        #endregion
    }
}
