using MyLog.Data.Repo.Entity.DataModel;
using System.Windows;
using WPF.JoshSmith.ServiceProviders.UI;

namespace MyLog.UI.Category {
    /// <summary>
    /// CategoryWindow
    /// </summary>
    public partial class CategoryWindow : Window {

        #region Declaration
        #endregion

        #region Constructor
        public CategoryWindow() {
            InitializeComponent();

            this.DataContext = new CategoryViewModel(this);

            new ListViewDragDropManager<CategoryData>(this.cData) {
                AllowStartX =0,
                AllowEndX = 24
            };
        }
        #endregion

    }
}
