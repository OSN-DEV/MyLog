﻿using MyLog.AppCommon;
using MyLog.Data.Repo;
using MyLog.Data.Repo.Entity.DataModel;
using MyLog.UI.TemplateSelect;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MyLog.UI.Template {
    /// <summary>
    /// template window view model
    /// </summary>
    internal class TemplateViewModel : BaseBindable {

        #region Declaration
        private readonly TemplateWindow _window;
        private bool _isNew = false;
        #endregion

        #region Public Property
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
        /// リスト情報
        /// </summary>
        private TemplateData _templateData = new TemplateData();
        public TemplateData TemplateData {
            set { base.SetProperty(ref this._templateData, value); }
            get { return this._templateData; }
        }

        /// <summary>
        /// テンプレートリスト
        /// </summary>
        public ObservableCollection<TemplateData> TemplateList { set; get; }

        /// <summary>
        /// テンプレート名のリスト
        /// </summary>
        public ObservableCollection<string> TemplateNameList { set; get; } = new ObservableCollection<string>();

        /// <summary>
        /// 編集モード
        /// </summary>
        private bool _editMode = false;
        public bool EditMode {
            set { 
                base.SetProperty(ref this._editMode, value);
                base.SetProperty(nameof(SearchPanelVisibility));
                base.SetProperty(nameof(EditHeaderPanelVisiblity));
            }
            get { return this._editMode; }
        }

        /// <summary>
        /// テンプレート選択パネルの可視
        /// </summary>
        public Visibility SearchPanelVisibility {
            get { return this._editMode ? Visibility.Collapsed : Visibility.Visible; }
        }

        /// <summary>
        /// 編集ヘッダパネルの可視
        /// </summary>
        public Visibility EditHeaderPanelVisiblity {
            get { return this._editMode ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// テンプレートのリストインデックス
        /// </summary>
        private int _templateIndex = -1;
        public int TemplateIndex {
            set { 
                base.SetProperty(ref this._templateIndex, value);
                base.SetProperty(nameof(this.IsEnabledEdit));
                // 選択した情報を表示
                if (0 <= value) {
                    this.TemplateData = this.TemplateList[value];
                    this.Name = this.TemplateData.Name;
                } else {
                    this.TemplateData = null;
                }
            }
            get { return this._templateIndex; }
        }

        private string _name = "";
        public string Name {
            set {
                base.SetProperty(ref this._name, value);
                if (null != this.TemplateData) {
                    this.TemplateData.Name = this._name;
                }
                base.SetProperty(nameof(this.IsEnabledSave));
            }
            get { return this._name; }
        }

        /// <summary>
        /// 編集・削除ボタンの使用可否
        /// </summary>
        public bool IsEnabledEdit {
            get { return 0 <= this._templateIndex; }
        }

        /// <summary>
        /// 保存ボタンの使用可否
        /// </summary>
        public bool IsEnabledSave {
            get { return 0 < this.TemplateData?.Name?.Length;  }
        }

        /// <summary>
        /// テンプレート追加コマンド
        /// </summary>
        public DelegateCommand AddTemplateCommand { set; get; }

        /// <summary>
        /// 既存のテンプレートから追加コマンド
        /// </summary>
        public DelegateCommand SelectTemplateCommand { set; get; }

        /// <summary>
        /// テンプレート編集コマンド
        /// </summary>
        public DelegateCommand EditTemplateCommand { set; get; }

        /// <summary>
        /// テンプレート削除コマンド
        /// </summary>
        public DelegateCommand DeleteTemplateCommand { set; get; }

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public DelegateCommand SaveTemplateCommand { set; get; }

        /// <summary>
        /// クリアコマンド
        /// </summary>
        public DelegateCommand ClearCommand { set; get; }

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
        public TemplateViewModel(TemplateWindow window) {
            this._window = window;
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
            if (!this.EditMode) {
                return false;
            }
            if (index <0) {
                return false;
            }
            return !this.TemplateData.LogList[index].IsCategory;
        }

        /// <summary>
        /// ドロップイベント
        /// </summary>
        /// <param name="newIndex"></param>
        public void DropDone(int newIndex) {
            this.TemplateData.LogList[newIndex].CategoryId = this.TemplateData.LogList[newIndex-1].CategoryId;
            this.SetPriority();
        }
        #endregion

        #region Private Method(Event)
        /// <summary>
        /// テンプレート追加クリック時の処理
        /// </summary>
        private void AddTemplateClick() {
            this.EditMode = true;
            this._isNew = true;
            this._window.cTemplateName.Focus();

            var repo = new TemplateRepo();
            this.TemplateData = repo.CreateEmptyTemplate();
            this.Name = "";
        }

        /// <summary>
        /// 既存のテンプレートから追加クリック時の処理
        /// </summary>
        private void SelectTemplateClick() {
            var window = new TemplateSelectWindow() {
                Owner = this._window
            };
            if (true != window.ShowDialog()) {
                return;
            }
            this.TemplateData = null;
            this.EditMode = true;
            this._isNew = true;
            this._window.cTemplateName.Focus();

            var repo = new TemplateRepo();
            this.TemplateData = repo.SelectByTemplateId(window.SelectedTemplateId);
            this.Name = "";
        }

        /// <summary>
        /// テンプレート編集クリック時の処理
        /// </summary>
        private void EditTemplateClick() {
            this.EditMode = true;
            this._window.cTemplateName.Focus();
        }

        /// <summary>
        /// テンプレート削除クリック時の処理
        /// </summary>
        private void DeleteTemplateClick() {
            var repo = new TemplateRepo();
            try {
                repo.DeleteByTemplateId(this.TemplateData.Id);
                this.TemplateList.Remove(this.TemplateData);
                this.CreateTemplateNameList();
                this.ClearClick();
            } catch (Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// 保存クリック時の処理
        /// </summary>
        private void SaveTemplateClick() {
            var repo = new TemplateRepo();
            try {
                repo.Update(this.TemplateData, this._isNew);
                if (this._isNew) {
                    this.TemplateList.Add(this.TemplateData);
                }

                this.TemplateList = new ObservableCollection<TemplateData>(this.TemplateList.OrderBy(n => n.Name));

                this.CreateTemplateNameList();
                this.ClearClick();
            } catch(Exception ex) {
                Message.ShowError(this._window, Message.ErrId.Err003, ex.Message);
            }
        }

        /// <summary>
        /// クリアクリック時の処理
        /// </summary>
        private void ClearClick() {
            this.TemplateIndex = -1;
            this.EditMode = false;
            this.TemplateData = null;
            this._isNew = false;

            // 既存データの場合はメモリの情報が書き換わってしまうので便宜上の対処
            // ※データ件数が少ないので通じる手法
            var repo = new TemplateRepo();
            this.TemplateList = repo.Select();
        }

        /// <summary>
        /// Todo追加クリック時の処理
        /// </summary>
        /// <param name="categoryId">カテゴリID</param>
        private void AddLogClick(long categoryId) {
            var foundHeader = false;
            foreach (var (template, index) in this.TemplateData.LogList.Select((template, index) => (template, index))) {
                if (template.IsCategory && template.CategoryId == categoryId) {
                    foundHeader = true;
                    continue;
                }
                if (foundHeader && template.CategoryId != categoryId) {
                    // 該当するカテゴリの末尾に追加する
                    this.TemplateData.LogList.Insert(index,
                        new TemplateDetailData() {
                            CategoryId = categoryId,
                            IsCategory = false
                        });
                    this.SetPriority();
                    return;
                }
            }
            if (foundHeader) {
                // 対象が最後のカテゴリのケース
                this.TemplateData.LogList.Add(new TemplateDetailData() {
                    CategoryId = categoryId,
                    IsCategory = false
                });
            }
            this.SetPriority();
        }

        /// <summary>
        /// Todo削除クリック
        /// </summary>
        /// <param name="priority"></param>
        private void DeleteTodoClick(int priority) {
            // Priorityは一意なので
            foreach (var (template, index) in this.TemplateData.LogList.Select((template, index) => (template, index))) {
                if (priority == template.Priority) {
                    this.TemplateData.LogList.RemoveAt(index);
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
            this.AddTemplateCommand = new DelegateCommand(AddTemplateClick);
            this.SelectTemplateCommand = new DelegateCommand(SelectTemplateClick);
            this.EditTemplateCommand = new DelegateCommand(EditTemplateClick);
            this.DeleteTemplateCommand = new DelegateCommand(DeleteTemplateClick);
            this.SaveTemplateCommand = new DelegateCommand(SaveTemplateClick);
            this.ClearCommand = new DelegateCommand(ClearClick);
            this.AddLogCommand = new DelegateCommandWithParam<long>(AddLogClick);
            this.DeleteTodoCommand = new DelegateCommandWithParam<int>(DeleteTodoClick);

            // 
            var repo = new TemplateRepo();
            this.TemplateList = repo.Select();
            this.CreateTemplateNameList();
        }

        /// <summary>
        /// カテゴリも含めて 並び順を振り直す
        /// </summary>
        /// <remarks>歯抜けが発生することになるが順番は保たれるので問題ないはず</remarks>
        private void SetPriority() {
            foreach (var (template, index) in this.TemplateData.LogList.Select((template, index) => (template, index))) {
                template.Priority = index;
            }
        }

        /// <summary>
        /// コンボボックス用のソースを作成
        /// </summary>
        /// <remarks>TemplateListをソースにすると名称変更が即時反映されないため</remarks>
        private void CreateTemplateNameList() {

            TemplateNameList.Clear();
            foreach(var data in TemplateList) {
                TemplateNameList.Add(data.Name);
            }
        }
        #endregion

    }
}
