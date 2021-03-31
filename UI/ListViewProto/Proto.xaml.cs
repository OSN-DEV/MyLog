using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.JoshSmith.ServiceProviders.UI;

namespace MyLog.UI.ListViewProto {
    /// <summary>
    /// Proto.xaml の相互作用ロジック
    /// </summary>
    public partial class Proto : Window {

        // ListViewDragDropManagerのサンプル
        // https://csharp.hotexamples.com/jp/examples/-/ListViewDragDropManager/-/php-listviewdragdropmanager-class-examples.html

        private readonly ProtoViewModel _context;
        private readonly ListViewDragDropManager<DummyData> _ddManager;

        public Proto() {
            InitializeComponent();

            this._context = new ProtoViewModel();
            // this.cData.ItemsSource = this._context.ListData;
            this.DataContext = this._context;

            this._ddManager = new ListViewDragDropManager<DummyData>(this.cData);
            this._ddManager.IsValidItem += (index) => {
                if (this._context.ListData[index] is DummyData item && item.IsHeader) {
                    return false;
                } else {
                    return true;
                }
            };

        }
    }


}