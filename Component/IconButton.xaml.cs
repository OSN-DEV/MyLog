using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MyLog.Component {
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class IconButton : UserControl {

        #region Constructor
        public IconButton() {
            InitializeComponent();
        }
        #endregion

        #region Event
        private void Icon_MouseEnter(object sender, MouseEventArgs e) {
            this.cHoverImage.Visibility = Visibility.Visible;
            this.cPressedImage.Visibility = Visibility.Hidden;
        }

        private void Icon_MouseLeave(object sender, MouseEventArgs e) {
            this.cHoverImage.Visibility = Visibility.Hidden;
            this.cPressedImage.Visibility = Visibility.Hidden;
        }

        private void Icon_MouseDown(object sender, MouseButtonEventArgs e) {
            this.cHoverImage.Visibility = Visibility.Hidden;
            this.cPressedImage.Visibility = Visibility.Visible;
        }

        private void Icon_MouseUp(object sender, MouseButtonEventArgs e) {
            this.cHoverImage.Visibility = Visibility.Visible;
            this.cPressedImage.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Public Property
        static IconButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }

        public static readonly DependencyProperty IconImageProperty = DependencyProperty.Register("IconImage",
                                                                                    typeof(BitmapImage),
                                                                                    typeof(IconButton),
                                                                                    new FrameworkPropertyMetadata());

        public BitmapImage IconImage {
            get { return (BitmapImage)GetValue(IconImageProperty); }
            set {
                SetValue(IconImageProperty, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChange(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }

        public static readonly DependencyProperty MyCommandProperty = DependencyProperty.Register("MyCommand",
                                                                                typeof(ICommand),
                                                                                typeof(IconButton),
                                                                                new PropertyMetadata(null));

        public ICommand MyCommand {
            get { return (ICommand)GetValue(MyCommandProperty); }
            set { SetValue(MyCommandProperty, value); }
        }

        public static readonly DependencyProperty MyCommandParamProperty = DependencyProperty.Register("MyCommandParam",
                                                                                typeof(object),
                                                                                typeof(IconButton),
                                                                                new PropertyMetadata(null));
        public object MyCommandParam {
            get { return GetValue(MyCommandParamProperty); }
            set { SetValue(MyCommandParamProperty, value); }
        }
    }
    #endregion
}

