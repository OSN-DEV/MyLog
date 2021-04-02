using System.Collections.ObjectModel;
using System.Windows;

namespace MyLog.UI.ListViewProto {
    class ProtoViewModel : BaseBindable {

        private readonly ObservableCollection<DummyData> _data = new ObservableCollection<DummyData>();

        public ProtoViewModel() {
            this.CreateDummyData();
            this.OnAddTodoClick = new DelegateCommandWithParam<long>(AddTodo);
            this.OKClick = new DelegateCommand(() => {
                MessageBox.Show("OK");
            });
        }

        #region Public Property
        private int _currentIndex = 0;
        public int CurrentIndex {
            get { return this._currentIndex; }
            set {
                base.SetProperty(ref this._currentIndex, value);
            }
        }
        public ObservableCollection<DummyData> ListData {
            get { return this._data; }
        }
        public DelegateCommandWithParam<long> OnAddTodoClick { get; private set; }
        public DelegateCommand OKClick { get; set; }
        #endregion

        #region Private Method
        private void CreateDummyData() {
            _data.Add(new DummyData { IsHeader = true, Id = 100, Fld1 = "朝の時間帯" });
            _data.Add(new DummyData { IsHeader = false, Id = 1, Fld1 = "a1", Fld2 = "a2", Fld3 = "a3", Fld4 = "a4" });
            _data.Add(new DummyData { IsHeader = false, Id = 2, Fld1 = "b1", Fld2 = "b2", Fld3 = "b3", Fld4 = "b4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "c1", Fld2 = "c2", Fld3 = "c3", Fld4 = "c4" });
            _data.Add(new DummyData { IsHeader = true, Id = 200, Fld1 = "Category2" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 4, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 5, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 6, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 7, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 8, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 9, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 10, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 11, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 12, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 13, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 14, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 15, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 16, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "d1", Fld2 = "d2", Fld3 = "d3", Fld4 = "d4" });
        }

        private void AddTodo(long id) {
            // MessageBox.Show(id.ToString());
            _data.Add(new DummyData { IsHeader = false, Id = 3, Fld1 = "xx", Fld2 = "xx", Fld3 = "xx", Fld4 = "xx" });

        }
        #endregion


    }

    class DummyData {
        public bool IsHeader { get; set; } = false;
        public long Id { get; set; } = 0;
        public string Fld1 { get; set; }
        public string Fld2 { get; set; }
        public string Fld3 { get; set; }
        public string Fld4 { get; set; }
        public int Result { get; set; } = 0;
        public string Todo { get; set; } = "abcde";
        public string PlanStart { get; set; } = "10:00";
        public string PlanEnd { set; get; } = "18:30";
        public int PlanTime { set; get; } = 650;
        public string ActualStart { get; set; } = "11:11";
        public string ActualEnd { set; get; } = "22:22";
        public int ActualTime { set; get; } = 63;
        public string Memo { set; get; } = "memo";
        public string CategoryName { set; get; } = "CategoryName(1234)";

        public Visibility HeaderVisibility {
            get {
                return IsHeader ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility DataVisibility {
            get {
                return IsHeader ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}