using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLog.Data.Repo.Entity.DataModel {
    /// <summary>
    /// テンプレートリストのアイテム
    /// </summary>
    public class TemplateListItem {

        #region Public Property
        /// <summary>
        /// id
        /// </summary>
        public long Id { set; get; }

        /// <summary>
        /// テンプレート名
        /// </summary>
        public string Name { set; get; }
        #endregion

        #region Public Method
        public override string ToString() {
            return this.Name;
        }
        #endregion
    }
}
