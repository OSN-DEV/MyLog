using MyLog.Data.Repo.Entity.DataModel;
using System.Windows;
using WPF.JoshSmith.ServiceProviders.UI;

namespace MyLog.UI.Template {
    /// <summary>
    /// TemplateWindow
    /// </summary>
    public partial class TemplateWindow : Window {

        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TemplateWindow() {
            InitializeComponent();

            var viewModel = new TemplateViewModel(this);
            this.DataContext = viewModel;
            var manager = new ListViewDragDropManager<TemplateDetailData>(this.cData) {
                AllowStartX = 0,
                AllowEndX = 24
            };
            manager.IsValidItem = viewModel.IsValidItem;
            manager.DropDone += viewModel.DropDone;
        }
        #endregion


    }
}
