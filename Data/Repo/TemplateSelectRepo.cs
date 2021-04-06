using MyLog.AppCommon;
using MyLog.Data.Repo.Entity;
using MyLog.Data.Repo.Entity.DataModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;

namespace MyLog.Data.Repo {
    /// <summary>
    /// template selct repo
    /// </summary>
    internal class TemplateSelectRepo : BaseRepo {
        #region Declaration 
        private static readonly SolidColorBrush ActiveForeGround = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#333333"));
        private static readonly SolidColorBrush InactiveForeGround = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#AAAAAA"));
        //private static readonly SolidBrush ActiveForeGround = new SolidBrush(ColorTranslator.FromHtml("#FF0000"));
        //private static readonly SolidBrush InactiveForeGround = new SolidBrush(ColorTranslator.FromHtml("#00FF00"));
        #endregion

        #region Internal Method
        /// <summary>
        /// テンプレートのヘッダ一覧を取得する
        /// </summary>
        /// <returns></returns>
        internal List<TemplateListItem> Select() {
            var result = new List<TemplateListItem>();
            using (var database = new MyLogDatabase(Constants.DatabaseFile())) {
                database.Open();

                var entity = new TemplateEntity(database);
                using (var recset = entity.Select()) {
                    while (recset.Read()) {
                        result.Add(new TemplateListItem() {
                            Id = recset.GetLong(TemplateEntity.Cols.Id),
                            Name = recset.GetString(TemplateEntity.Cols.Name),
                            Sun = recset.GetBool(TemplateEntity.Cols.Sun) ? ActiveForeGround : InactiveForeGround,
                            Mon = recset.GetBool(TemplateEntity.Cols.Mon) ? ActiveForeGround : InactiveForeGround,
                            Tue = recset.GetBool(TemplateEntity.Cols.Tue) ? ActiveForeGround : InactiveForeGround,
                            Wed = recset.GetBool(TemplateEntity.Cols.Wed) ? ActiveForeGround : InactiveForeGround,
                            Thu = recset.GetBool(TemplateEntity.Cols.Thu) ? ActiveForeGround : InactiveForeGround,
                            Fri = recset.GetBool(TemplateEntity.Cols.Fri) ? ActiveForeGround : InactiveForeGround,
                            Sat = recset.GetBool(TemplateEntity.Cols.Sat) ? ActiveForeGround : InactiveForeGround
                        });
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
