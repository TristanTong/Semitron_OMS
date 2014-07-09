using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Site;
using System.Data;

namespace Semitron_OMS.BLL.Site
{
    public class SearchBLL
    {
        private readonly Semitron_OMS.DAL.Site.SearchDAL dal = new Semitron_OMS.DAL.Site.SearchDAL();
        /// <summary>
        /// 获取查询结果索引页数据
        /// </summary>
        public System.Data.DataTable GetSearchInIndex(string strLang, string strKey)
        {
            DataTable dt = dal.GetSearchInIndex(strLang, strKey);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取查询结果树
        /// </summary>
        public System.Data.DataTable GetSearchTree(string strLang)
        {
            DataTable dt = dal.GetSearchTree(strLang);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}
