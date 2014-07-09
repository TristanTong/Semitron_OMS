﻿/**  
* ShippingPlanDetailDAL.cs
*
* 功 能： N/A
* 类 名： ShippingPlanDetailDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/7 0:14:27   童荣辉    初版
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
using System.Collections;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:ShippingPlanDetailDAL
    /// </summary>
    public partial class ShippingPlanDetailDAL
    {
        public ShippingPlanDetailDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "ShippingPlanDetail");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ShippingPlanDetail");
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
        public int Add(Semitron_OMS.Model.OMS.ShippingPlanDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShippingPlanDetail(");
            strSql.Append("ShippingPlanID,PlanStockCode,CustomerOrderNo,CustomerDetailID,CPN,MPN,ProductCode,PlanQty,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@ShippingPlanID,@PlanStockCode,@CustomerOrderNo,@CustomerDetailID,@CPN,@MPN,@ProductCode,@PlanQty,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingPlanID", SqlDbType.Int,4),
					new SqlParameter("@PlanStockCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerOrderNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerDetailID", SqlDbType.Int,4),
					new SqlParameter("@CPN", SqlDbType.NVarChar,50),
					new SqlParameter("@MPN", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PlanQty", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ShippingPlanID;
            parameters[1].Value = model.PlanStockCode;
            parameters[2].Value = model.CustomerOrderNo;
            parameters[3].Value = model.CustomerDetailID;
            parameters[4].Value = model.CPN;
            parameters[5].Value = model.MPN;
            parameters[6].Value = model.ProductCode;
            parameters[7].Value = model.PlanQty;
            parameters[8].Value = model.AvailFlag;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.UpdateUser;

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
        public bool Update(Semitron_OMS.Model.OMS.ShippingPlanDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShippingPlanDetail set ");
            strSql.Append("ShippingPlanID=@ShippingPlanID,");
            strSql.Append("PlanStockCode=@PlanStockCode,");
            strSql.Append("CustomerOrderNo=@CustomerOrderNo,");
            strSql.Append("CustomerDetailID=@CustomerDetailID,");
            strSql.Append("CPN=@CPN,");
            strSql.Append("MPN=@MPN,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("PlanQty=@PlanQty,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingPlanID", SqlDbType.Int,4),
					new SqlParameter("@PlanStockCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerOrderNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerDetailID", SqlDbType.Int,4),
					new SqlParameter("@CPN", SqlDbType.NVarChar,50),
					new SqlParameter("@MPN", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PlanQty", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ShippingPlanID;
            parameters[1].Value = model.PlanStockCode;
            parameters[2].Value = model.CustomerOrderNo;
            parameters[3].Value = model.CustomerDetailID;
            parameters[4].Value = model.CPN;
            parameters[5].Value = model.MPN;
            parameters[6].Value = model.ProductCode;
            parameters[7].Value = model.PlanQty;
            parameters[8].Value = model.AvailFlag;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.UpdateUser;
            parameters[13].Value = model.ID;

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
            strSql.Append("delete from ShippingPlanDetail ");
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
            strSql.Append("delete from ShippingPlanDetail ");
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
        public Semitron_OMS.Model.OMS.ShippingPlanDetailModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ShippingPlanID,PlanStockCode,CustomerOrderNo,CustomerDetailID,CPN,MPN,ProductCode,PlanQty,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser from ShippingPlanDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.ShippingPlanDetailModel model = new Semitron_OMS.Model.OMS.ShippingPlanDetailModel();
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
        public Semitron_OMS.Model.OMS.ShippingPlanDetailModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.ShippingPlanDetailModel model = new Semitron_OMS.Model.OMS.ShippingPlanDetailModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ShippingPlanID"] != null && row["ShippingPlanID"].ToString() != "")
                {
                    model.ShippingPlanID = int.Parse(row["ShippingPlanID"].ToString());
                }
                if (row["PlanStockCode"] != null)
                {
                    model.PlanStockCode = row["PlanStockCode"].ToString();
                }
                if (row["CustomerOrderNo"] != null)
                {
                    model.CustomerOrderNo = row["CustomerOrderNo"].ToString();
                }
                if (row["CustomerDetailID"] != null && row["CustomerDetailID"].ToString() != "")
                {
                    model.CustomerDetailID = int.Parse(row["CustomerDetailID"].ToString());
                }
                if (row["CPN"] != null)
                {
                    model.CPN = row["CPN"].ToString();
                }
                if (row["MPN"] != null)
                {
                    model.MPN = row["MPN"].ToString();
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["PlanQty"] != null && row["PlanQty"].ToString() != "")
                {
                    model.PlanQty = int.Parse(row["PlanQty"].ToString());
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
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["CreateUser"] != null)
                {
                    model.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["UpdateUser"] != null)
                {
                    model.UpdateUser = row["UpdateUser"].ToString();
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
            strSql.Append("select ID,ShippingPlanID,PlanStockCode,CustomerOrderNo,CustomerDetailID,CPN,MPN,ProductCode,PlanQty,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM ShippingPlanDetail ");
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
            strSql.Append(" ID,ShippingPlanID,PlanStockCode,CustomerOrderNo,CustomerDetailID,CPN,MPN,ProductCode,PlanQty,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM ShippingPlanDetail ");
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
            strSql.Append("select count(1) FROM ShippingPlanDetail ");
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
            strSql.Append(")AS Row, T.*  from ShippingPlanDetail T ");
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
            parameters[0].Value = "ShippingPlanDetail";
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
        /// 增加出货计划单明细多条数据
        /// </summary>
        /// <param name="lstShippingPlanDetailModel"></param>
        public void AddList(System.Collections.Generic.List<Model.OMS.ShippingPlanDetailModel> lstShippingPlanDetailModel)
        {
            Hashtable SQLStringList = new Hashtable();
            foreach (Model.OMS.ShippingPlanDetailModel model in lstShippingPlanDetailModel)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ShippingPlanDetail(");
                strSql.Append("ShippingPlanID,PlanStockCode,CustomerOrderNo,CustomerDetailID,CPN,MPN,ProductCode,PlanQty,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
                strSql.Append(" values (");
                strSql.Append("@ShippingPlanID,@PlanStockCode,@CustomerOrderNo,@CustomerDetailID,@CPN,@MPN,@ProductCode,@PlanQty,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
                //strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@ShippingPlanID", SqlDbType.Int,4),
					new SqlParameter("@PlanStockCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerOrderNo", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerDetailID", SqlDbType.Int,4),
					new SqlParameter("@CPN", SqlDbType.NVarChar,50),
					new SqlParameter("@MPN", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PlanQty", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
                parameters[0].Value = model.ShippingPlanID;
                parameters[1].Value = model.PlanStockCode;
                parameters[2].Value = model.CustomerOrderNo;
                parameters[3].Value = model.CustomerDetailID;
                parameters[4].Value = model.CPN;
                parameters[5].Value = model.MPN;
                parameters[6].Value = model.ProductCode;
                parameters[7].Value = model.PlanQty;
                parameters[8].Value = model.AvailFlag;
                parameters[9].Value = model.CreateTime;
                parameters[10].Value = model.CreateUser;
                parameters[11].Value = model.UpdateTime;
                parameters[12].Value = model.UpdateUser;
                SQLStringList.Add(strSql, parameters);
            }
            DbHelperSQL.ExecuteSqlTran(SQLStringList);
        }

        /// <summary>
        /// 分页查询出货计划单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>出货计划单数据</returns>
        public DataSet GetShippingPlanDetailPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.ShippingPlanDetail AS D WITH (NOLOCK) LEFT JOIN ShippingPlan AS G WITH (NOLOCK)  ON D.ShippingPlanId=G.ID LEFT JOIN CustomerOrderDetail AS P WITH (NOLOCK)  ON D.CustomerDetailID=P.ID  LEFT JOIN CustomerOrder AS O WITH (NOLOCK)  ON O.InnerOrderNO=P.InnerOrderNO JOIN Admin AS A1 ON A1.AdminID=G.ByHandUserID LEFT JOIN Admin AS A2 ON A2.AdminID=G.ApprovedUserID ";
            //查询字段
            string strGetFields = " D.ID, AvailFlag=CASE WHEN D.AvailFlag=1 THEN '有效' ELSE '无效' END, G.ShippingPlanNo, D.ProductCode, D.PlanQty,P.InnerOrderNO,O.CustomerOrderNO,P.CPN, P.MPN,P.CustQuantity,G.IsApproved,ShippingPlanDate=Convert(varchar(20),G.ShippingPlanDate,120),D.PlanStockCode,ByHandUser=A1.UserName,ApprovedUser=A2.UserName,CreateTime=Convert(varchar(20),G.CreateTime,120) ,UpdateTime=Convert(varchar(20),G.UpdateTime,120),D.CreateUser,D.UpdateUser ";
            //查询条件
            string strWhere = Semitron_OMS.Common.SQLOperateHelper.GetSQLCondition(searchInfo.ConditionFilter, false);
            //数据查询
            Semitron_OMS.DAL.Common.CommonDAL commonDAL = new Semitron_OMS.DAL.Common.CommonDAL();
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
        /// <summary>
        /// 根据出货计划单Id获得出货计划单明细列表记录
        /// </summary>
        /// <param name="iShippingPlanId">出货计划单Id</param>
        /// <returns>出货计划单显示明细数据</returns>
        public DataTable GetDisplayModelList(int iShippingPlanId)
        {
            //查询字段
            string strGetFields = " D.ID,D.ShippingPlanID,D.POPlanId,P.PONO,P.POQuantity,ShipmentDate=Convert(varchar(20),P.ShipmentDate,120),D.ProductCode,D.InQty,Price=CAST(D.Price AS DECIMAL(18,2)),TotalPrice=CAST(D.TotalPrice AS DECIMAL(18,2)),D.Remark,D.AvailFlag ";
            //查询表名
            string strTableName = " ShippingPlanDetail AS D LEFT JOIN POPlan AS P ON D.POPlanId = P.ID";
            //查询条件
            string strWhere = " D.AvailFlag = 1 AND ShippingPlanID=@ShippingPlanID ";

            SqlParameter[] parameters = {
                    new SqlParameter("@ShippingPlanID", SqlDbType.Int)
                    };
            parameters[0].Value = iShippingPlanId;

            return DbHelperSQL.Query("SELECT " + strGetFields + " FROM " + strTableName + " WHERE " + strWhere, parameters).Tables[0];
        }
        #endregion  ExtensionMethod
    }
}

