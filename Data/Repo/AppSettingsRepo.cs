using MyLog.AppCommon;
using MyLog.Data.Repo.Entity;
using OsnCsLib.Data;
using System;
using System.IO;

namespace MyLog.Data.Repo {
    /// <summary>
    /// app settings repo
    /// </summary>
    public class AppSettingsRepo : AppDataBase<AppSettingsRepo> {

        #region Declaration
        private static string _file;
        #endregion

        #region Public Property
        /// <summary>
        /// window x position
        /// </summary>
        public double X { set; get; }

        /// <summary>
        /// windows y position
        /// </summary>
        public double Y { set; get; }

        /// <summary>
        /// window width
        /// </summary>
        public double Width { set; get; }

        /// <summary>
        /// window height
        /// </summary>
        public double Height { set; get; }

        /// <summary>
        /// show window top
        /// </summary>
        public bool Topmost { set; get; }

        /// <summary>
        /// DatabaseFile
        /// </summary>
        public string DatabaseFile { set; get; } = OsnCsLib.Common.Util.GetAppPath() + @"app.data";
        #endregion

        #region Public Method
        /// <summary>
        /// initialize repo
        /// </summary>
        /// <param name="file">setting file</param>
        /// <returns>instance</returns>
        public static AppSettingsRepo Init(string file) {
            _file = file;
            GetInstanceBase(file);
            if (!System.IO.File.Exists(file)) {
                _instance.Save();
            }
            return _instance;
        }

        /// <summary>
        /// get instance
        /// </summary>
        /// <returns>instance</returns>
        public static AppSettingsRepo GetInstance() {
            return GetInstanceBase();
        }

        /// <summary>
        /// save data
        /// </summary>
        public void Save() {
            GetInstanceBase().SaveToXml(_file);
        }

        /// <summary>
        /// set database
        /// </summary>
        /// <param name="database"></param>
        public void SetDatabaseFile(string database) {
            CreateDatabase(database);
            this.DatabaseFile = database;
            this.Save();

        }

        /// <summary>
        /// Create Database File
        /// </summary>
        /// <param name="file">database file</param>
        public void CreateDatabase(string file = "") {
            if (0 == file.Length) {
                file = OsnCsLib.Common.Util.GetAppPath() + @"app.data";
            }

            if (System.IO.File.Exists(file)) {
                this.BackupData(file);
            } else {
                using (var database = new MyLogDatabase(file)) {
                    try {
                        database.Open();
                        database.BeginTrans();

                        new CategoryEntity(database).Create();
                        new LogEntity(database).Create();
                        new LogDetailEntity(database).Create();
                        new TemplateEntity(database).Create();
                        new TemplateDetailEntity(database).Create();

                        database.CommitTrans();
                    } catch (Exception ex) {
                        Message.ShowError(null, Message.ErrId.Err002, ex.Message);
                    }
                }
                this.DatabaseFile = file;
                this.Save();
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// バックアップ処理
        /// </summary>
        /// <param name="file"></param>
        private void BackupData(string file) {
            var root = new DirectoryInfo(file);
            var backupDir =$@"{root.Parent.FullName}\.logdatabackup";
            if (!Directory.Exists(backupDir)) {
                Directory.CreateDirectory(backupDir);
            }
            var backup = System.DateTime.Now.ToString("yyyyMMdd");
            if (File.Exists($@"{backupDir}\{backup}")) {
                return;
            }
            File.Copy(file, $@"{backupDir}\{backup}");
            var files = Directory.GetFiles(backupDir);
            var baseDate = System.DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            foreach(var f in files) {
                if (new FileInfo(f).Name.CompareTo(baseDate) <= 0) {
                    File.Delete(f);
                }
            }
        }
        #endregion
    }
}
