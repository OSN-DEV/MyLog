using System;
using System.Windows;
using System.Windows.Controls;

namespace MyLog.Component {
    /// <summary>
    /// TimeSpanText
    /// </summary>
    public partial class TimeSpanText : UserControl {

        #region Constructor
        public TimeSpanText() {
            InitializeComponent();

            this.cStart.TextValueChanged += TimeChanged;
            this.cEnd.TextValueChanged += TimeChanged;
            this.cSpan.TextChanged += (sender, e)  => {
                if (cSpan.Text == "0") {
                    cSpan.Text = "";
                }
                var spanTime = 0;
                if (0 < this.cSpan.Text.Length) {
                    if (!int.TryParse(this.cSpan.Text, out spanTime)) {
                        spanTime = 0;
                    }
                }
                this.TimeDataChanged?.Invoke(long.Parse(this.Tag.ToString()), this.cStart.Text, this.cEnd.Text, spanTime);
            };
        }

        #endregion

        #region Event
        #region Public Event
        public delegate void TimeDataChangedHandle(long id, string start, string end, int span);
        public event TimeDataChangedHandle TimeDataChanged;
        #endregion

        private void TimeChanged(object sender, EventArgs e) {
            if (0 == this.cStart.Text.Length || 0 == this.cEnd.Text.Length) {
                return;
            }

            var start = this.ConvertStr2Date(this.cStart.Text);
            var end = this.ConvertStr2Date(this.cEnd.Text);
            var span = end - start;
            if (span.TotalMinutes < 0) {
                return;
            }
            this.cSpan.Text = span.TotalMinutes.ToString();

            var spanTime = 0;
            if (0 < this.cSpan.Text.Length) {
                spanTime = int.Parse(this.cSpan.Text);
            }
            this.TimeDataChanged?.Invoke(long.Parse(this.Tag.ToString()), this.cStart.Text, this.cEnd.Text, spanTime);
        }
        #endregion

        #region Public Property
        static TimeSpanText() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeSpanText), new FrameworkPropertyMetadata(typeof(TimeSpanText)));
        }

        public static readonly DependencyProperty StartTimeProperty = 
            DependencyProperty.Register("StartTime", typeof(string), typeof(TimeSpanText), new FrameworkPropertyMetadata());

        public string StartTime {
            get { return (string)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(string), typeof(TimeSpanText), new FrameworkPropertyMetadata());

        public string EndTime {
            get { return (string)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        public static readonly DependencyProperty SpanProperty =
            DependencyProperty.Register("Span", typeof(string), typeof(TimeSpanText), new FrameworkPropertyMetadata());

        public string Span {
            get { return (string)GetValue(SpanProperty); }
            set { SetValue(SpanProperty, value); }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 時刻を計算用のDateTime型に変換する
        /// </summary>
        /// <param name="time">時刻(99:99)</param>
        /// <returns>変換した結果</returns>
        private DateTime ConvertStr2Date(string time) {
            var now = DateTime.Now;
            return new DateTime(
                    now.Year,
                    now.Month,
                    now.Day,
                    int.Parse(time.Replace(":","").Substring(0, 2)),
                    int.Parse(time.Replace(":", "").Substring(2, 2)),
                    0);
        }
        #endregion
    }
}
