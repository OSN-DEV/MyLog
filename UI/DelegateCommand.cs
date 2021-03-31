using System;
using System.Windows.Input;

namespace MyLog.UI {
    public class DelegateCommand : ICommand {

        #region Declaration
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        #endregion

        #region Event
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Constructor
        public DelegateCommand(Action execute) : this(execute, () => true) { }

        public DelegateCommand(Action execute, Func<bool> canExecute) {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        #endregion

        #region Public Method
        public void Execute(object parameter) {
            this._execute();
        }

        public bool CanExecute(object parameter) {
            return this._canExecute();
        }
        #endregion
    }
}
