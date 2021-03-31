using System;
using System.Windows;
using System.Windows.Controls;

namespace MyLog.Component {
    /// <summary>
    /// 可視ボタン
    /// </summary>
    public partial class VisibleButton : UserControl {

        #region Declaration
        #endregion

        #region Constructor
        public VisibleButton() {
            InitializeComponent();
        }
        #endregion


        #region Public Property
        static VisibleButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VisibleButton), new FrameworkPropertyMetadata(typeof(VisibleButton)));
        }

        public static readonly DependencyProperty VisibleStatusProperty =
                                                        DependencyProperty.Register("VisibleStatus",
                                                                                    typeof(bool),
                                                                                    typeof(VisibleButton),
                                                                                    new FrameworkPropertyMetadata());

        /// <summary>
        /// 可視状態の設定
        /// </summary>
        public bool VisibleStatus {
            get { return (bool)GetValue(VisibleStatusProperty); }
            set {
                SetValue(VisibleStatusProperty, value);
            }
        }
        #endregion

        #region Event
        private void VisibleButton_Click(object sender, EventArgs e) {
            this.VisibleStatus = !this.VisibleStatus;
        }
        #endregion

    }
}
