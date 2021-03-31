using OsnCsLib.Data;

namespace MyLog.Data.Repo {
    /// <summary>
    /// app settings repo
    /// </summary>
    class AppSettingsRepo : AppDataBase<AppSettingsRepo> {

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
        #endregion

    }
}
