/**  
* POBindCustomerOrderDAL.cs
*
* 功 能： N/A
* 类 名： POBindCustomerOrderDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/4 20:47:48   童荣辉    初版
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
using Semitron_OMS.Common;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:POBindCustomerOrderDAL
    /// </summary>
    public partial class POBindCustomerOrderDAL
    {
        public POBindCustomerOrderDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "POBindCustomerOrder");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from POBindCustomerOrder");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.POBindCustomerOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into POBindCustomerOrder(");
            strSql.Append("POID,CustomerOrderID,CreateUser,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@POID,@CustomerOrderID,@CreateUser,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@POID", SqlDbType.Int,4),
					new SqlParameter("@CustomerOrderID", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.POID;
            parameters[1].Value = model.CustomerOrderID;
            parameters[2].Value = model.CreateUser;
            parameters[3].Value = model.CreateTime;

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
        public bool Update(Semitron_OMS.Model.OMS.POBindCustomerOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update POBindCustomerOrder set ");
            strSql.Append("POID=@POID,");
            strSql.Append("CustomerOrderID=@CustomerOrderID,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@POID", SqlDbType.Int,4),
					new SqlParameter("@CustomerOrderID", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.POID;
            parameters[1].Value = model.CustomerOrderID;
            parameters[2].Value = model.CreateUser;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.ID;

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
            strSql.Append("delete from POBindCustomerOrder ");
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
            strSql.Append("delete from POBindCustomerOrder ");
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
        public Semitron_OMS.Model.OMS.POBindCustomerOrderModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,POID,CustomerOrderID,CreateUser,CreateTime from POBindCustomerOrder ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.POBindCustomerOrderModel model = new Semitron_OMS.Model.OMS.POBindCustomerOrderModel();
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
        public Semitron_OMS.Model.OMS.POBindCustomerOrderModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.POBindCustomerOrderModel model = new Semitron_OMS.Model.OMS.POBindCustomerOrderModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["POID"] != null && row["POID"].ToString() != "")
                {
                    model.POID = int.Parse(row["POID"].ToString());
                }
                if (row["CustomerOrderID"] != null && row["CustomerOrderID"].ToString() != "")
                {
                    model.CustomerOrderID = int.Parse(row["CustomerOrderID"].ToString());
                }
                if (row["CreateUser"] != null)
                {
                    model.CreateUser = row["CreateUser"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
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
            strSql.Append("select ID,POID,CustomerOrderID,CreateUser,CreateTime ");
            strSql.Append(" FROM POBindCustomerOrder ");
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
            strSql.Append(" ID,POID,CustomerOrderID,CreateUser,CreateTime ");
            strSql.Append(" FROM POBindCustomerOrder ");
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
            strSql.Append("select count(1) FROM POBindCustomerOrder ");
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
            strSql.Append(")AS Row, T.*  from POBindCustomerOrder T ");
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
            parameters[0].Value = "POBindCustomerOrder";
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
        /// 通过采购订单Id获取相关联及待关联的客户订单树
        /// </summary>
        /// <param name="iPOId">采购订单Id</param>
        /// <returns>数据集合</returns>
        public DataSet GetCustomerOrderByPoId(int iPOId, int iAdminID)
        {
            //#1 加入权限控制,根据指定采购员获取客户订单 20140406 修改
            SqlParameter[] parameters = {
					new SqlParameter("@POId", SqlDbType.Int,4),
                   new SqlParameter("@AdminID", SqlDbType.Int,4) //#1
			};
            parameters[0].Value = iPOId;
            parameters[1].Value = iAdminID;
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.GetCustomerOrderByPoId, parameters, "BindCustomerOrder");
        }
        #endregion  ExtensionMethod


        /// <summary>
        /// 关联客户订单
        /// </summary>
        /// <param name="iPOId">采购订单Id</param>
        /// <param name="strCustomerOrderIdList">所选择要关联的客户订单Id</param>
        /// <param name="strCreateUser">操作用户</param>
        /// <returns>是否成功 >0: 成功</returns>
        public int BindCustiomerOrder(int iPOId, string strCustomerOrderIdList, string strCreateUser)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@POId", SqlDbType.Int,4),
                    new SqlParameter("@OrderIds", SqlDbType.VarChar),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16)
			};
            parameters[0].Value = iPOId;
            parameters[1].Value = strCustomerOrderIdList;
            parameters[2].Value = strCreateUser;
            int rowsAffected = 0;
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.BindCustiomerOrder, parameters, out rowsAffected);
        }
    }
}

