using System;
using System.Windows.Input;

namespace MyLog.UI {
    public class DelegateCommandWithParam<T> : ICommand {

        #region Declaration
        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;
        #endregion

        #region Event
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Constructor
        public DelegateCommandWithParam(Action<T> execute) : this(execute, () => true) { }

        public DelegateCommandWithParam(Action<T> execute, Func<bool> canExecute) {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        #endregion


        #region Public Method
        public void Execute(object parameter) {
            this._execute((T)parameter);
        }

        public bool CanExecute(object parameter) {
            return this._canExecute();
        }
        #endregion
    }
}
