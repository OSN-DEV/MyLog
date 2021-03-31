using System.Windows;
using WPF.JoshSmith.ServiceProviders.UI;

namespace MyLog.UI.Main {
    /// <summary>
    /// MyLogMainWindow
    /// </summary>
    public partial class MyLogMainWindow : Window {

        #region Constructor
        public MyLogMainWindow() {
            InitializeComponent();

            var viewModel = new MyLogViewModel(this);
            this.DataContext = viewModel;
            var manager = new ListViewDragDropManager<MyLogViewModel>(this.cData) {
                AllowStartX = 0,
                AllowEndX = 24
            };
            manager.IsValidItem = viewModel.IsValidItem;
            manager.DropDone += viewModel.DropDone;
        }
        #endregion
    }
}
