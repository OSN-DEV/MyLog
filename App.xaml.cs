using MyLog.AppCommon;
using MyLog.Data.Repo;
using System.Windows;
using System.Threading;

namespace MyLog {
    /// <summary>
    /// 
    /// </summary>
    public partial class App : Application {

        #region Declaration
        private static readonly string mutexName = "MyLog.AppName";
        private static readonly Mutex mutex = new Mutex(false, mutexName);
        private static bool hasHandle = false;
        #endregion

        #region Event
        protected override void OnStartup(StartupEventArgs e) {
            hasHandle = mutex.WaitOne(0, false);
            if (!hasHandle) {
                MessageBox.Show("already launch");
                this.Shutdown();
                return;
            }
            base.OnStartup(e);

            var settings = AppSettingsRepo.Init(Constants.SettingsFile);
            settings.CreateDatabase(settings.DatabaseFile);
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);
            if (hasHandle) {
                mutex.ReleaseMutex();
            }
            mutex.Close();
        }
        #endregion
    }
}
