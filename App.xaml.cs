using MyLog.AppCommon;
using MyLog.Data.Repo;
using MyLog.Data.Repo.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyLog {
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application {

        #region Event
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            // create a database file if need
            if (!System.IO.File.Exists(Constants.DatabaseFile)) {
                using (var database = new MyLogDatabase(Constants.DatabaseFile)) {
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
            }
        }
        #endregion
    }
}
