using MyLog.AppCommon;
using MyLog.Data.Repo;
using MyLog.Data.Repo.Entity.DataModel;
using MyLog.UI.Category;
using MyLog.UI.Template;
using System;
using System.Linq;

namespace MyLog.UI.Main {
    /// <summary>
    /// my log main window view model
    /// </summary>
    internal class MyLogMainViewModel : BaseBindable {

        #region Declaration
        private readonly MyLogMainWindow _window;
        private const string DateFormat = "yyyy/MM/dd";
        #endregion

        #region Property
        /// <summary>
        /// 記録日
        /// </summary>
        private string _recordedOn;
        public string RecordedOn {
            set { base.SetProperty(ref this._recordedOn, value); }
            get { return this._recordedOn; }
        }

        /// <summary>
        /// 現在の行
        /// </summary>
        private int _currentIndex = 0;
        public int CurrentIndex {
            get { return this._currentIndex; }
            set {
                base.SetProperty(ref this._currentIndex, value);
            }
        }

        /// <summary>
        /// データコンテキスト
        /// </summary>
        private LogData _logData;
        public LogData LogData {
            set { 
                base.SetProperty(ref this._logData, value);
                base.SetProperty(nameof(NoData));
            }
            get { return this._logData; }
        }

        /// <summary>
        /// 該当日のログデータ有無
        /// </summary>
        public bool NoData {
            get { return this.LogData == null; }
        }

        /// <summary>
        /// 前日クリック
        /// </summary>
        public DelegateCommand PrevDayCommand { set; get; }

        /// <summary>
        /// 翌日クリック
        /// </summary>
        public DelegateCommand NextDayCommand { set; get; }

        /// <summary>
        /// カレンダークリック
        /// </summary>
        public DelegateCommand CalendarCommand { set; get; }

        /// <summary>
        /// 新規TODO作成クリック
        /// </summary>
        public DelegateCommand NewTodoCommand { set; get; }

        /// <summary>
        /// 空のTODO作成クリック
        /// </summary>
        public DelegateCommand EmptyTodoCommand { set; get; }

        /// <summary>
        /// カテゴリ編集クリック
        /// </summary>
        public DelegateCommand EditCategoryCommand { set; get; }

        /// <summary>
        /// テンプレート編集クリック
        /// </summary>
        public DelegateCommand EditTemplateCommand { set; get; }

        /// <summary>
        /// ログ追加コマンド
        /// </summary>
        public DelegateCommandWithParam<long> AddLogCommand { set; get; }

        /// <summary>
        /// Todo削除コマンド
        /// </summary>
        public DelegateCommandWithParam<int> DeleteTodoCommand { set; get; }
        #endregion

        #region Constructor
        public MyLogMainViewModel(MyLogMainWindow owner) {
            this._window = owner;
            this.Initialize();
        }
        #endregion


        #region Public Method
        /// <summary>
        /// インデックスのアイテムがドラッグ可能か判定
        /// </summary>
        /// <param name="index">リストのインデックス</param>
        /// <returns>true:可能、false:それ以外</returns>
        public bool IsValidItem(int index) {
            if (index < 0) {
                return false;
            }
            return !this.LogData.LogList[index].IsCategory;
        }

        /// <summary>
        /// ドロップイベント
        /// </summary>
        public void DropDone(int newIndex) {
            this.LogData.LogList[newIndex].CategoryId = this.LogData.LogList[newIndex - 1].CategoryId;
            this.SetPriority();
        }
        #endregion

        #region Public Method(Event)
        /// <summary>
        /// 結果変更時イベント
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="memo">メモ</param>
        public void ResultChanged(long id, int result) {
            var repo = new MyLogRepo();
            try {
                repo.UpdateResultById(id, result);
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// Todo変更時イベント
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="todo">Todo</param>
        public void TodoChanged(long id, string todo) {
            var repo = new MyLogRepo();
            try {
                repo.UpdateTodoById(id, todo);
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// メモ変更時イベント
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="memo">メモ</param>
        public void MemoChanged(long id, string memo) {
            var repo = new MyLogRepo();
            try {
                repo.UpdateMemoById(id, memo);
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// 予定時間変更イベント
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="start">開始時刻</param>
        /// <param name="end">終了時刻</param>
        /// <param name="span">時間</param>
        public void StartTimeChanged(long id, string start, string end, int span) {
            var repo = new MyLogRepo();
            try {
                repo.UpdatePlanTimeById(id, start, end, span);
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// 実績時間変更イベント
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="start">開始時刻</param>
        /// <param name="end">終了時刻</param>
        /// <param name="span">時間</param>
        public void EndTimeChanged(long id, string start, string end, int span) {
            var repo = new MyLogRepo();
            try {
                repo.UpdateActualTimeById(id, start, end, span);
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }
        #endregion

        #region Private Method(Event)
        /// <summary>
        /// 前日クリック時の処理
        /// </summary>
        private void PrevDayClick() {
            var prevDay = DateTime.Parse(this.RecordedOn).AddDays(-1);
            this.RecordedOn = prevDay.ToString(DateFormat);
            this.ShowDataByRecordedOn();
        }

        /// <summary>
        /// 翌日クリック時の処理
        /// </summary>
        private void NextDayClick() {
            var prevDay = DateTime.Parse(this.RecordedOn).AddDays(1);
            this.RecordedOn = prevDay.ToString(DateFormat);
            this.ShowDataByRecordedOn();
        }

        /// <summary>
        /// カレンダークリック時の処理
        /// </summary>
        private void CalendarClick() {
            var window = new DateSelect(this.RecordedOn) {
                Owner = this._window
            };
            if (true != window.ShowDialog()) {
                return;
            }
            this.RecordedOn = window.SelectedDate;
            this.ShowDataByRecordedOn();
        }

        /// <summary>
        /// 新規TODO作成クリック時の処理
        /// </summary>
        private void NewTodoClick() {
            var repo = new MyLogRepo();
            try {
                this.LogData = repo.CreateLog(this.RecordedOn);
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// 空のTODO作成クリック時の処理
        /// </summary>
        private void EmptyTodoClick() {
            try {
                var repo = new MyLogRepo();
                this.LogData = repo.CreateEmptyLog(this.RecordedOn);
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// カテゴリ編集クリック時の処理
        /// </summary>
        private void EditCategoryClick() {
            var window = new CategoryWindow() {
                Owner = this._window
            };
            if (true != window.ShowDialog()) {
                return;
            }
        }

        /// <summary>
        /// テンプレート編集クリック時の処理
        /// </summary>
        private void EditTemplateClick() {
            var window = new TemplateWindow() {
                Owner = this._window
            };
            window.ShowDialog();
        }


        /// <summary>
        /// Todo追加クリック時の処理
        /// </summary>
        /// <param name="categoryId">カテゴリID</param>
        private void AddLogClick(long categoryId) {
            var foundHeader = false;
            var repo = new MyLogRepo();
            var detailData = repo.InsertEmptyRow(this.LogData.Id, categoryId, -1);

            foreach (var (log, index) in this.LogData.LogList.Select((log, index) => (log, index))) {
                if (log.IsCategory && log.CategoryId == categoryId) {
                    foundHeader = true;
                    continue;
                }
                if (foundHeader && log.CategoryId != categoryId) {
                    // 該当するカテゴリの末尾に追加する
                    this.LogData.LogList.Insert(index, detailData);
                    this.SetPriority();
                    return;
                }
            }
            if (foundHeader) {
                // 対象が最後のカテゴリのケース
                this.LogData.LogList.Add(detailData);
            }
            this.SetPriority();
        }

        /// <summary>
        /// Todo削除クリック
        /// </summary>
        /// <param name="priority"></param>
        private void DeleteTodoClick(int priority) {
            // Priorityは一意なので
            foreach (var (log, index) in this.LogData.LogList.Select((log, index) => (log, index))) {
                if (priority == log.Priority) {
                    var repo = new MyLogRepo();
                    repo.DeleteById(log.Id);
                    this.LogData.LogList.Remove(log);
                    return;
                }
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 初期処理
        /// </summary>
        private void Initialize() {
            // コマンドを設定
            this.PrevDayCommand = new DelegateCommand(PrevDayClick);
            this.NextDayCommand = new DelegateCommand(NextDayClick);
            this.NewTodoCommand = new DelegateCommand(NewTodoClick);
            this.EmptyTodoCommand = new DelegateCommand(EmptyTodoClick);
            this.CalendarCommand = new DelegateCommand(CalendarClick);
            this.EditCategoryCommand = new DelegateCommand(EditCategoryClick, () => true);
            this.EditTemplateCommand = new DelegateCommand(EditTemplateClick, () => true);
            this.AddLogCommand = new DelegateCommandWithParam<long>(AddLogClick);
            this.DeleteTodoCommand = new DelegateCommandWithParam<int>(DeleteTodoClick);

            // 初期データを表示
            this.RecordedOn = DateTime.Now.ToString(DateFormat);
            this.ShowDataByRecordedOn();
        }

        /// <summary>
        /// 指定された日付のログデータを表示
        /// </summary>
        private void ShowDataByRecordedOn() {
            var repo = new MyLogRepo();
            this.LogData = repo.SelectByRecordedOn(this.RecordedOn);
        }

        /// <summary>
        /// カテゴリも含めて 並び順を振り直す
        /// </summary>
        /// <remarks>歯抜けが発生することになるが順番は保たれるので問題ないはず</remarks>
        private void SetPriority() {
            foreach (var (log, index) in this.LogData.LogList.Select((log, index) => (log, index))) {
                log.Priority = index;
            }
            var repo = new MyLogRepo();
            repo.UpdateOrderById(this.LogData.LogList);
        }
        #endregion
    }
}
