using System.Windows;

namespace MyLog.UI.TemplateSelect {
    /// <summary>
    /// TemplateSelectWindow
    /// </summary>
    public partial class TemplateSelectWindow : Window {

        #region Declaration
        private TemplateSelectViewModel _viewModel;
        #endregion

        #region Public Property
        public long SelectedTemplateId {
            get { return this._viewModel.SelectedTemplatId; }
        }
        #endregion

        #region Constructor
        public TemplateSelectWindow() {
            InitializeComponent();

            this._viewModel = new TemplateSelectViewModel(this);
            this.DataContext = this._viewModel;
        }
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            this._viewModel.ListItemDoubleClick();
        }
        #endregion

    }
}
