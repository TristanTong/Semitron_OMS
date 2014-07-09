/**  
* ProductTypeDAL.cs
*
* 功 能： N/A
* 类 名： ProductTypeDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/9 19:15:46   童荣辉    初版
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
namespace Semitron_OMS.DAL.Site
{
    /// <summary>
    /// 数据访问类:ProductTypeDAL
    /// </summary>
    public partial class ProductTypeDAL
    {
        public ProductTypeDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "ProductType");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProductType");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Semitron_OMS.Model.Site.ProductTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProductType(");
            strSql.Append("Pid,Code,TypeName,Lang,SK,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@Pid,@Code,@TypeName,@Lang,@SK,@AvailFlag,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            SqlParameter[] parameters = {					
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.VarChar,36),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,128),
					new SqlParameter("@Lang", SqlDbType.VarChar,16),
					new SqlParameter("@SK", SqlDbType.VarChar,16),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,36),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Pid;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.TypeName;
            parameters[3].Value = model.Lang;
            parameters[4].Value = model.SK;
            parameters[5].Value = model.AvailFlag;
            parameters[6].Value = model.CreateUser;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.UpdateUser;
            parameters[9].Value = model.UpdateTime;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Semitron_OMS.Model.Site.ProductTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProductType set ");
            strSql.Append("Pid=@Pid,");
            strSql.Append("Code=@Code,");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("Lang=@Lang,");
            strSql.Append("SK=@SK,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.VarChar,36),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,128),
					new SqlParameter("@Lang", SqlDbType.VarChar,16),
					new SqlParameter("@SK", SqlDbType.VarChar,16),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,36),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Pid;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.TypeName;
            parameters[3].Value = model.Lang;
            parameters[4].Value = model.SK;
            parameters[5].Value = model.AvailFlag;
            parameters[6].Value = model.CreateUser;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.UpdateUser;
            parameters[9].Value = model.UpdateTime;
            parameters[10].Value = model.ID;

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
            strSql.Append("delete from ProductType ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
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
            strSql.Append("delete from ProductType ");
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
        public Semitron_OMS.Model.Site.ProductTypeModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Pid,Code,TypeName,Lang,SK,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime from ProductType ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.Site.ProductTypeModel model = new Semitron_OMS.Model.Site.ProductTypeModel();
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
        public Semitron_OMS.Model.Site.ProductTypeModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.Site.ProductTypeModel model = new Semitron_OMS.Model.Site.ProductTypeModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Pid"] != null && row["Pid"].ToString() != "")
                {
                    model.Pid = int.Parse(row["Pid"].ToString());
                }
                if (row["Code"] != null)
                {
                    model.Code = row["Code"].ToString();
                }
                if (row["TypeName"] != null)
                {
                    model.TypeName = row["TypeName"].ToString();
                }
                if (row["Lang"] != null)
                {
                    model.Lang = row["Lang"].ToString();
                }
                if (row["SK"] != null)
                {
                    model.SK = row["SK"].ToString();
                }
                if (row["AvailFlag"] != null && row["AvailFlag"].ToString() != "")
                {
                    if ((row["AvailFlag"].ToString() == "1") || (row["AvailFlag"].ToString().ToLower() == "true"))
                    {
                        model.AvailFlag = true;
                    }
                    else
                    {
                        model.AvailFlag = false;
                    }
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
            strSql.Append("select ID,Pid,Code,TypeName,Lang,SK,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM ProductType ");
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
            strSql.Append(" ID,Pid,Code,TypeName,Lang,SK,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM ProductType ");
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
            strSql.Append("select count(1) FROM ProductType ");
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
            strSql.Append(")AS Row, T.*  from ProductType T ");
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
            parameters[0].Value = "ProductType";
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
        /// 获取框架导航菜单产品分类
        /// </summary>
        public DataTable GetProductTypeInMain(string strLang)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Pid,Code,TypeName,Lang,SK,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM ProductType ");
            strSql.Append(" WHERE  AvailFlag = 1  " + (string.IsNullOrEmpty(strLang) ? (" AND lang='en'") : (" AND lang='" + strLang + "'")) + " ORDER BY SK ASC,CreateTime DESC");

            return SQLNotifier.GetDataTable(strSql.ToString(), strSql.ToString(), ConstantValue.TableNames.ProductType, null);
        }

        /// <summary>
        /// 分页查询解决方案明细记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>记录数据</returns>
        public DataSet GetProductTypePageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " ProductType ";
            //查询字段
            string strGetFields = " ID , Code , Name=TypeName , SK,Lang , AvailFlag = CASE AvailFlag WHEN 1 THEN '有效' ELSE '无效' END , CreateUser , CreateTime = CONVERT(VARCHAR(20), CreateTime, 120) , UpdateUser , UpdateTime = CONVERT(VARCHAR(20), UpdateTime, 120) ";
            //查询条件
            string strWhere = SQLOperateHelper.GetSQLCondition(searchInfo.ConditionFilter, false);
            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt(ConstantValue.ProcedureNames.PageProcedureName,
                strTableName,
                strGetFields,
                searchInfo.PageSize,
                searchInfo.PageIndex,
                strWhere,
                searchInfo.OrderByField,
                searchInfo.OrderType,
                out o_RowsCount);
        }
        /// <summary>
        /// 根据编号判断是否存在
        /// </summary>
        public bool ExistsByCode(string strCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProductType");
            strSql.Append(" where Code=@Code");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.VarChar,32)
			};
            parameters[0].Value = strCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 设置记录为无效
        /// </summary>
        public bool SetValid(int iId, int iStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProductType set AvailFlag=" + iStatus);
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = iId;

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
        #endregion  ExtensionMethod
    }
}

