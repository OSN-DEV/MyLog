using OsnLib.Data.Sqlite;
using System.Collections.Generic;

namespace MyLog.Data.Repo.Entity {
    /// <summary>
    /// My log database
    /// </summary>
    internal class MyLogDatabase : Database {

        #region Declaration
        private enum Ver : int {
            Ver00 = 0,
            Ver01 = 1,
            Current = Ver01
        }
        private delegate List<SqlBuilder> CreateSqls();
        #endregion

        #region Publi Property
        internal static string Password { private set; get; } = "";
        #endregion

        #region Constructor
        internal MyLogDatabase(string database) : base(database, (int)Ver.Current) {
        }
        #endregion

        #region Public Method
        public override void Open() {
            base.Open(Password);
        }
        #endregion

        #region Protected Method
        protected override void UpgradeDatabase(int currentVersion, int newVersion, Database database) {
            switch ((Ver)currentVersion) {
                case Ver.Ver00:
                    if ((Ver)newVersion == Ver.Ver01) {
                        this.Update00To01();
                    }
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region Private Method
        /// <summary>
        /// ver00 → ver01へのマイグレーション
        /// </summary>
        private void Update00To01() {
            new TempLogEntity(this).Create();
        }
        #endregion


    }
}
