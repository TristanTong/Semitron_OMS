using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Semitron_OMS.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.DAL.Common
{
    public class TaskInfoDAL
    {
        public DataSet GetList(string strWhere, string strOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  Id , TaskCode ,ObjectTable , TaskName , PageCode , Valid , CreateTime , UpdateTime ");
            strSql.Append(" FROM TaskInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (strOrder.Trim() != "")
            {
                strSql.Append(" order by " + strOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到记录的一个字段的值
        /// </summary>
        /// <param name="strField">字段名称</param>
        /// <param name="Id">任务id</param>
        /// <returns>字段的值</returns>
        public object GetField(string strField, int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + strField + " FROM TaskInfo");
            strSql.Append(" WHERE Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.ExecuteSqlGet(strSql.ToString(), strField);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TaskInfoModel GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id , TaskCode ,ObjectTable , TaskName , PageCode , Valid , CreateTime , UpdateTime ");
            strSql.Append(" FROM TaskInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            TaskInfoModel model = new TaskInfoModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TaskCode"] != null && ds.Tables[0].Rows[0]["TaskCode"].ToString() != "")
                {
                    model.TaskCode = ds.Tables[0].Rows[0]["TaskCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ObjectTable"] != null && ds.Tables[0].Rows[0]["ObjectTable"].ToString() != "")
                {
                    model.ObjectTable = ds.Tables[0].Rows[0]["ObjectTable"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TaskName"] != null && ds.Tables[0].Rows[0]["TaskName"].ToString() != "")
                {
                    model.TaskName = ds.Tables[0].Rows[0]["TaskName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PageCode"] != null && ds.Tables[0].Rows[0]["PageCode"].ToString() != "")
                {
                    model.PageCode = ds.Tables[0].Rows[0]["PageCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Valid"] != null && ds.Tables[0].Rows[0]["Valid"].ToString() != "")
                {
                    model.Valid = int.Parse(ds.Tables[0].Rows[0]["Valid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"] != null && ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
