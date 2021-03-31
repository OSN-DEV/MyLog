﻿using MyLog.Data.Repo.Entity.DataModel;
using MyLog.UI.Category;
using MyLog.UI.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLog.UI.Main {
    /// <summary>
    /// my log main window view model
    /// </summary>
    internal class MyLogViewModel : BaseBindable {

        #region Declaration
        private MyLogMainWindow _owner;
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
        /// データコンテキスト
        /// </summary>
        private LogData _logData;
        public LogData LogData {
            set { base.SetProperty(ref this._logData, value); }
            get { return this._logData; }
        }

        /// <summary>
        /// 該当日のログデータ有無
        /// </summary>
        private bool _hasData;
        public bool HasData {
            set { base.SetProperty(ref this._hasData, value); }
            get { return this._hasData; }
        }

        /// <summary>
        /// 前日クリック
        /// </summary>
        public DelegateCommand PrevDayClickCommand { set; get; }

        /// <summary>
        /// 翌日クリック
        /// </summary>
        public DelegateCommand NextDayClickCommand { set; get; }

        /// <summary>
        /// カレンダークリック
        /// </summary>
        public DelegateCommand CalendarClickCommand { set; get; }

        /// <summary>
        /// 新規TODO作成クリック
        /// </summary>
        public DelegateCommand NewTodoClickCommand { set; get; }

        /// <summary>
        /// 空のTODO作成クリック
        /// </summary>
        public DelegateCommand EmptyTodoClickCommand { set; get; }

        /// <summary>
        /// カテゴリ編集クリック
        /// </summary>
        public DelegateCommand EditCategoryClickCommand { set; get; }

        /// <summary>
        /// テンプレート編集クリック
        /// </summary>
        public DelegateCommand EditTemplateClickCommand { set; get; }
        #endregion

        #region Constructor
        public MyLogViewModel(MyLogMainWindow owner) {
            this._owner = owner;
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
            //if (!this.EditMode) {
            //    return false;
            //}
            //if (index < 0) {
            //    return false;
            //}
            //return !this.TemplateData.LogList[index].IsCategory;
            return true;
        }

        /// <summary>
        /// ドロップイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DropDone() {
            // this.SetPriority();
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
                Owner = this._owner
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

        }

        /// <summary>
        /// 空のTODO作成クリック時の処理
        /// </summary>
        private void EmptyTodoClick() {

        }

        /// <summary>
        /// カテゴリ編集クリック時の処理
        /// </summary>
        private void EditCategoryClick() {
            var window = new CategoryWindow() {
                Owner = this._owner
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
                Owner = this._owner
            };
            window.ShowDialog();
        }

        #endregion

        #region Private Method
        /// <summary>
        /// 初期処理
        /// </summary>
        private void Initialize() {
            // コマンドを設定
            this.PrevDayClickCommand = new DelegateCommand(PrevDayClick);
            this.NextDayClickCommand = new DelegateCommand(NextDayClick);
            this.NewTodoClickCommand = new DelegateCommand(NewTodoClick);
            this.EmptyTodoClickCommand = new DelegateCommand(EmptyTodoClick);
            this.CalendarClickCommand = new DelegateCommand(CalendarClick);
            this.EditCategoryClickCommand = new DelegateCommand(EditCategoryClick, () => true);
            this.EditTemplateClickCommand = new DelegateCommand(EditTemplateClick, () => true);

            // 初期データを表示
            this.RecordedOn = DateTime.Now.ToString(DateFormat);
            this.ShowDataByRecordedOn();
        }

        /// <summary>
        /// 指定された日付のログデータを表示
        /// </summary>
        private void ShowDataByRecordedOn() {

        }
        #endregion

        //|9|カテゴリラベル||
        //|10|Todo追加ボタン||
        //||並び替えボタン||
        //|11|完了チェックボタン||
        //|12|Todoテキスト||
        //|13|予定時刻(開始)テキスト||
        //|14|予定時刻(終了)テキスト||
        //|15|予定時間テキスト||
        //|16|実績時刻(開始)テキスト||
        //|17|実績時刻(開始)設定ボタン||
        //|18|実績時刻(終了)テキスト||
        //|19|実績時刻(終了)設定ボタン||
        //|20|実績時間テキスト||
        //|21|メモテキスト||
        //|22|コンテキストメニュー ||
        //|23|未割当Todoリスト||
    }
}