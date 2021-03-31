using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyLog.Component {
    /// <summary>
    /// TextBox拡張クラス
    /// </summary>
    internal class CustomTextBox : TextBox {

        #region Declaration
        /// <summary>
        /// IMEのモード
        /// </summary>
        internal enum ImeModeType {
            /// <summary>
            /// 使用不可
            /// </summary>
            Disabled,
            /// <summary>
            /// ひらがな
            /// </summary>
            Hiragana,
            /// <summary>
            /// OFF
            /// </summary>
            Off,
            /// <summary>
            /// 変更なし
            /// </summary>
            DoNotCare
        }

        /// <summary>
        /// テキストの内容が変更された場合に発火する。発火のタイミングはフォーカス喪失時。
        /// </summary>
        internal event EventHandler TextValueChanged;

        private string _text;
        #endregion

        #region Public Property
        public ImeModeType ImeMode { set; get; } = ImeModeType.DoNotCare;
        #endregion

        #region Constructor
        public CustomTextBox() {
            this.Initialized += (sender, e) => {
                switch (this.ImeMode) {
                    case ImeModeType.Disabled:
                        InputMethod.SetIsInputMethodEnabled(this, false);
                        break;
                    case ImeModeType.Hiragana:
                        InputMethod.SetPreferredImeState(this, InputMethodState.On);
                        InputMethod.SetPreferredImeConversionMode(this, ImeConversionModeValues.FullShape | ImeConversionModeValues.Native);
                        break;
                    case ImeModeType.Off:
                        InputMethod.SetPreferredImeState(this, InputMethodState.Off);
                        break;
                    case ImeModeType.DoNotCare:
                        InputMethod.SetPreferredImeState(this, InputMethodState.DoNotCare);
                        break;
                    default:
                        throw new ArgumentException("unknown ime type : " + this.ImeMode);
                }
            };

            this.GotFocus += (sender, e) => {
                this._text = this.Text;
                this.SelectAll();
            };

            this.LostFocus += (sender, e) => {
                if (this._text != this.Text) {
                    this.TextValueChanged?.Invoke(this, null);
                }
            };
        }
        #endregion
    }
}
