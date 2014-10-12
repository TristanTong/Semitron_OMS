/**  
* CommonTableDAL.cs
*
* 功 能： N/A
* 类 名： CommonTableDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 12:05:52   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Common;
using Semitron_OMS.DAL.Common;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:CommonTableDAL
    /// </summary>
    public partial class CommonTableDAL
    {
        public CommonTableDAL()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.CommonTableModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CommonTable(");
            strSql.Append("TableName,FieldID,[Key],Value,[Desc],CreateUser,CreateTime,UpdateUser,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@TableName,@FieldID,@Key,@Value,@Desc,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,32),
					new SqlParameter("@FieldID", SqlDbType.VarChar,32),
					new SqlParameter("@Key", SqlDbType.NVarChar,128),
					new SqlParameter("@Value", SqlDbType.NVarChar,128),
					new SqlParameter("@Desc", SqlDbType.NVarChar,128),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.TableName;
            parameters[1].Value = model.FieldID;
            parameters[2].Value = model.Key;
            parameters[3].Value = model.Value;
            parameters[4].Value = model.Desc;
            parameters[5].Value = model.CreateUser;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.UpdateUser;
            parameters[8].Value = model.UpdateTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.OMS.CommonTableModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CommonTable set ");
            strSql.Append("TableName=@TableName,");
            strSql.Append("FieldID=@FieldID,");
            strSql.Append("[Key]=@Key,");
            strSql.Append("Value=@Value,");
            strSql.Append("[Desc]=@Desc,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,32),
					new SqlParameter("@FieldID", SqlDbType.VarChar,32),
					new SqlParameter("@Key", SqlDbType.NVarChar,128),
					new SqlParameter("@Value", SqlDbType.NVarChar,128),
					new SqlParameter("@Desc", SqlDbType.NVarChar,128),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.TableName;
            parameters[1].Value = model.FieldID;
            parameters[2].Value = model.Key;
            parameters[3].Value = model.Value;
            parameters[4].Value = model.Desc;
            parameters[5].Value = model.CreateUser;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.UpdateUser;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CommonTable ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CommonTable ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Semitron_OMS.Model.OMS.CommonTableModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,TableName,FieldID,[Key],Value,[Desc],CreateUser,CreateTime,UpdateUser,UpdateTime from CommonTable ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.CommonTableModel model = new Semitron_OMS.Model.OMS.CommonTableModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Semitron_OMS.Model.OMS.CommonTableModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.CommonTableModel model = new Semitron_OMS.Model.OMS.CommonTableModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["TableName"] != null)
                {
                    model.TableName = row["TableName"].ToString();
                }
                if (row["FieldID"] != null)
                {
                    model.FieldID = row["FieldID"].ToString();
                }
                if (row["Key"] != null)
                {
                    model.Key = row["Key"].ToString();
                }
                if (row["Value"] != null)
                {
                    model.Value = row["Value"].ToString();
                }
                if (row["Desc"] != null)
                {
                    model.Desc = row["Desc"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    model.CreateUser = row["CreateUser"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["UpdateUser"] != null)
                {
                    model.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TableName,FieldID,[Key],Value,[Desc],CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM CommonTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,TableName,FieldID,[Key],Value,[Desc],CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM CommonTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CommonTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from CommonTable T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "CommonTable";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获得指定条件的数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strField">字段名</param>
        /// <param name="strKey">字段值</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="strOrder">降序还是升序</param>
        /// <returns>数据集合</returns>
        public DataSet GetListByWhere(string strTableName, string strField, string strKey, string strOrderField, string strOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SELECT [ID],[TableName],[FieldID],[Key],[Value] FROM CommonTable WHERE 1=1");
            if (!string.IsNullOrEmpty(strTableName))
            {
                strSql.Append(" AND TableName='" + strTableName + "'");
            }
            if (!string.IsNullOrEmpty(strTableName))
            {
                strSql.Append(" AND FieldID='" + strField + "'");
            }
            if (!string.IsNullOrEmpty(strTableName))
            {
                strSql.Append(" AND Key='" + strKey + "'");
            }
            if (!string.IsNullOrEmpty(strOrderField))
            {
                strSql.Append(" ORDER BY " + strOrderField);
            }
            else
            {
                strSql.Append(" ORDER BY Desc ");
            }
            if (!string.IsNullOrEmpty(strOrder))
            {
                strSql.Append(strOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据数据库缓存依赖获得数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <returns>数据集合</returns>
        public DataTable GetDataTableByCache(string strTableName)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@TableName", SqlDbType.VarChar, 32)
                    };
            parameters[0].Value = strTableName;
            string strSql = ConstantValue.SQLNotifierDepObj.CommonTableDepSql.Replace("@TableName", "'" + strTableName + "'");
            return Semitron_OMS.DAL.SQLNotifier.GetDataTable(ConstantValue.SQLNotifierDepObj.CommonTableDepSql, strSql, ConstantValue.TableNames.CommonTable, parameters);
        }

        /// <summary>
        /// 分页查询记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>记录数据</returns>
        public DataSet GetCommonTablePageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.CommonTable AS P WITH(NOLOCK) ";
            //查询字段
            string strGetFields = " ID,TableName,FieldID,[Key],Value,[Desc], P.CreateUser , CreateTime = CONVERT(VARCHAR(20), P.CreateTime, 120) , P.UpdateUser , UpdateTime = CONVERT(VARCHAR(20), P.UpdateTime, 120) ";
            //查询条件
            string strWhere = SQLOperateHelper.GetSQLCondition(searchInfo.ConditionFilter, false);
            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt(Semitron_OMS.Common.ConstantValue.ProcedureNames.PageProcedureName,
                strTableName,
                strGetFields,
                searchInfo.PageSize,
                searchInfo.PageIndex,
                strWhere,
                searchInfo.OrderByField,
                searchInfo.OrderType,
                out o_RowsCount);
        }
        #endregion  ExtensionMethod

    }
}

