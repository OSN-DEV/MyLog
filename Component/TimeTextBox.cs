using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyLog.Component {
    /// <summary>
    /// 日付入力用テキストボックス
    /// </summary>
    internal class TimeTextBox: TextBox {
        #region Declaration
        private string _text;
        #endregion

        #region Event
        /// <summary>
        /// テキストの内容が変更された場合に発火する。発火のタイミングはフォーカス喪失時。
        /// </summary>
        internal event EventHandler TextValueChanged;
        #endregion


        #region Constructor
        public TimeTextBox() {
            this.Initialized += (sender, e) => {
                InputMethod.SetIsInputMethodEnabled(this, false);
            };

            this.GotFocus += (sender, e) => {
                this._text = this.Text;
                this.Text = this.Text.Replace(":", "");
                this.SelectAll();
            };

            this.LostFocus += (sender, e) => {
                this.Text = FormatTime();
                if (this._text != this.Text) {
                    this.TextValueChanged?.Invoke(this, null);
                }
            };

            this.MouseDoubleClick += (sender, e) => {
                if (0 == this.Text.Length) {
                    this.Text = DateTime.Now.ToString("HHmm");
                    this.TextValueChanged?.Invoke(this, null);
                }
            };
        }
        #endregion


        #region Private Method
        /// <summary>
        /// 時刻のフォーマット
        /// </summary>
        /// <returns>時刻と認識できない場合は空文字を返却</returns>
        private  string FormatTime() {
            if (0 == this.Text.Length) {
                return "";
            }
            var val = this.Text.PadLeft(4,'0');
            int dummyNum;
            if (!int.TryParse(val, out dummyNum)) {
                return "";
            }
            var h = int.Parse(val.Substring(0, 2));
            var m = int.Parse(val.Substring(2, 2));

            if (h < 0 || 23 < h) {
                return "";
            }
            if (m < 0 || 59 < m) {
                return "";
            }

            return String.Format("{0:D2}", h) + ":" + String.Format("{0:D2}", m);
        }
        #endregion
    }
}
