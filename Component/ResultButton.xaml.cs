using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MyLog.Component {
    /// <summary>
    /// 結果ボタン
    /// </summary>
    public partial class ResultButton : UserControl {

        #region Declaration
        public enum ResultState: short {
            /// <summary>
            /// 未チェック
            /// </summary>
            None,
            /// <summary>
            /// 済み
            /// </summary>
            Done,
            /// <summary>
            /// キャンセル
            /// </summary>
            Cancel
        }
        #endregion

        #region Constructor
        public ResultButton() {
            InitializeComponent();
        }
        #endregion

        #region Public Property
        static ResultButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResultButton), new FrameworkPropertyMetadata(typeof(ResultButton)));
        }

        public static readonly DependencyProperty ResultStatusProperty =
                                                        DependencyProperty.Register("ResultStatus",
                                                                                    typeof(ResultState),
                                                                                    typeof(ResultButton),
                                                                                    new FrameworkPropertyMetadata());

        public ResultState ResultStatus {
            get { return (ResultState)GetValue(ResultStatusProperty); }
            set {
                SetValue(ResultStatusProperty, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChange(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }
        #endregion

        #region Event
        private void ResultButton_Click(object sender, EventArgs e) {
            switch(this.ResultStatus) {
                case ResultState.None:
                    this.ResultStatus = ResultState.Done;
                    break;
                case ResultState.Done:
                    this.ResultStatus = ResultState.Cancel;
                    break;
                default:
                    this.ResultStatus = ResultState.None;
                    break;
            }
        }
        #endregion

        #region Private Method
        #endregion
    }
}
