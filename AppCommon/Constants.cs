using MyLog.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLog.AppCommon {
    /// <summary>
    /// constant definition
    /// </summary>
    internal class Constants {
        /// <summary>
        /// アプリの設定関連情報
        /// </summary>
        public static readonly string SettingsFile = OsnCsLib.Common.Util.GetAppPath() + @"app.settings";

        /// <summary>
        /// アプリデータベース
        /// </summary>
        //public static readonly string DatabaseFile = AppSettingsRepo.GetInstance().DatabaseFile;
        public static string DatabaseFile() {
            return AppSettingsRepo.GetInstance().DatabaseFile;
        }

        /// <summary>
        /// カテゴリの数
        /// </summary>
        public static readonly int CategoryCount = 5;
    }
}
