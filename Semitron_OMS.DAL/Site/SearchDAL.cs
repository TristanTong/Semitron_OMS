using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Common;
using System.Data;

namespace Semitron_OMS.DAL.Site
{
    public class SearchDAL
    {
        /// <summary>
        /// 获取查询结果索引页数据
        /// </summary>
        public System.Data.DataTable GetSearchTree(string strLang)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@lang", SqlDbType.VarChar,16)
			};
            parameters[0].Value = strLang;
            DataSet ds = DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GetSearchTree, parameters, "ds");
            if (ds.IsExsitData())
                return ds.Tables[0];
            else
                return new DataTable();
        }
        /// <summary>
        /// 获取查询结果树
        /// </summary>
        public System.Data.DataTable GetSearchInIndex(string strLang, string strKey)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@lang", SqlDbType.VarChar,16),
                    new SqlParameter("@key", SqlDbType.NVarChar,256)
			};
            parameters[0].Value = strLang;
            parameters[1].Value = strKey;
            DataSet ds = DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GetSearchInIndex, parameters, "ds");
            if (ds.IsExsitData())
                return ds.Tables[0];
            else
                return new DataTable();
        }
    }
}
