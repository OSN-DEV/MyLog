using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyLog.UI {
    /// <summary>
    /// bindable base class
    /// </summary>
    public class BaseBindable : INotifyPropertyChanged {

        #region Declaration
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Method
        /// <summary>
        /// set property
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="field">field variable reference</param>
        /// <param name="value">value </param>
        /// <param name="propertyName">property name</param>
        /// <returns></returns>
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (Equals(field, value)) {
                return false;
            }
            field = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        /// <summary>
        /// set property
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected virtual void SetProperty([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
