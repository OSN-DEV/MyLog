using MyLog.AppCommon;
using MyLog.Data.Repo;
using System.Windows;

namespace MyLog {
    /// <summary>
    /// 
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
