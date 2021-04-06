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

            var settings = AppSettingsRepo.Init(Constants.SettingsFile);
            settings.CreateDatabase();
        }
        #endregion
    }
}
