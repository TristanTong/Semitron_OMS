/**  
* ShippingListDetailDAL.cs
*
* 功 能： N/A
* 类 名： ShippingListDetailDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/7 0:14:28   童荣辉    初版
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
    /// 数据访问类:ShippingListDetailDAL
    /// </summary>
    public partial class ShippingListDetailDAL
    {
        public ShippingListDetailDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "ShippingListDetail");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ShippingListDetail");
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
        public int Add(Semitron_OMS.Model.OMS.ShippingListDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShippingListDetail(");
            strSql.Append("ShippingListID,StockCode,ShippingPlanNo,ProductCode,OutQty,ChargeUserID,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@ShippingListID,@StockCode,@ShippingPlanNo,@ProductCode,@OutQty,@ChargeUserID,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingListID", SqlDbType.Int,4),
					new SqlParameter("@StockCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ShippingPlanNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OutQty", SqlDbType.Int,4),
					new SqlParameter("@ChargeUserID", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ShippingListID;
            parameters[1].Value = model.StockCode;
            parameters[2].Value = model.ShippingPlanNo;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.OutQty;
            parameters[5].Value = model.ChargeUserID;
            parameters[6].Value = model.AvailFlag;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.CreateUser;
            parameters[9].Value = model.UpdateTime;
            parameters[10].Value = model.UpdateUser;

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
        public bool Update(Semitron_OMS.Model.OMS.ShippingListDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShippingListDetail set ");
            strSql.Append("ShippingListID=@ShippingListID,");
            strSql.Append("StockCode=@StockCode,");
            strSql.Append("ShippingPlanNo=@ShippingPlanNo,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("OutQty=@OutQty,");
            strSql.Append("ChargeUserID=@ChargeUserID,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingListID", SqlDbType.Int,4),
					new SqlParameter("@StockCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ShippingPlanNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OutQty", SqlDbType.Int,4),
					new SqlParameter("@ChargeUserID", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ShippingListID;
            parameters[1].Value = model.StockCode;
            parameters[2].Value = model.ShippingPlanNo;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.OutQty;
            parameters[5].Value = model.ChargeUserID;
            parameters[6].Value = model.AvailFlag;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.CreateUser;
            parameters[9].Value = model.UpdateTime;
            parameters[10].Value = model.UpdateUser;
            parameters[11].Value = model.ID;

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
            strSql.Append("delete from ShippingListDetail ");
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
            strSql.Append("delete from ShippingListDetail ");
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
        public Semitron_OMS.Model.OMS.ShippingListDetailModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ShippingListID,StockCode,ShippingPlanNo,ProductCode,OutQty,ChargeUserID,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser from ShippingListDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.ShippingListDetailModel model = new Semitron_OMS.Model.OMS.ShippingListDetailModel();
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
        public Semitron_OMS.Model.OMS.ShippingListDetailModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.ShippingListDetailModel model = new Semitron_OMS.Model.OMS.ShippingListDetailModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ShippingListID"] != null && row["ShippingListID"].ToString() != "")
                {
                    model.ShippingListID = int.Parse(row["ShippingListID"].ToString());
                }
                if (row["StockCode"] != null)
                {
                    model.StockCode = row["StockCode"].ToString();
                }
                if (row["ShippingPlanNo"] != null)
                {
                    model.ShippingPlanNo = row["ShippingPlanNo"].ToString();
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["OutQty"] != null && row["OutQty"].ToString() != "")
                {
                    model.OutQty = int.Parse(row["OutQty"].ToString());
                }
                if (row["ChargeUserID"] != null && row["ChargeUserID"].ToString() != "")
                {
                    model.ChargeUserID = int.Parse(row["ChargeUserID"].ToString());
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
            strSql.Append("select ID,ShippingListID,StockCode,ShippingPlanNo,ProductCode,OutQty,ChargeUserID,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM ShippingListDetail ");
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
            strSql.Append(" ID,ShippingListID,StockCode,ShippingPlanNo,ProductCode,OutQty,ChargeUserID,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM ShippingListDetail ");
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
            strSql.Append("select count(1) FROM ShippingListDetail ");
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
            strSql.Append(")AS Row, T.*  from ShippingListDetail T ");
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
            parameters[0].Value = "ShippingListDetail";
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
        /// 增加出库单明细多条数据
        /// </summary>
        /// <param name="lstShippingListDetailModel"></param>
        public void AddList(System.Collections.Generic.List<Model.OMS.ShippingListDetailModel> lstShippingListDetailModel)
        {
            Hashtable SQLStringList = new Hashtable();
            foreach (Model.OMS.ShippingListDetailModel model in lstShippingListDetailModel)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ShippingListDetail(");
                strSql.Append("ShippingListID,StockCode,ShippingPlanNo,ProductCode,OutQty,ChargeUserID,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
                strSql.Append(" values (");
                strSql.Append("@ShippingListID,@StockCode,@ShippingPlanNo,@ProductCode,@OutQty,@ChargeUserID,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@ShippingListID", SqlDbType.Int,4),
					new SqlParameter("@StockCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ShippingPlanNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OutQty", SqlDbType.Int,4),
					new SqlParameter("@ChargeUserID", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
                parameters[0].Value = model.ShippingListID;
                parameters[1].Value = model.StockCode;
                parameters[2].Value = model.ShippingPlanNo;
                parameters[3].Value = model.ProductCode;
                parameters[4].Value = model.OutQty;
                parameters[5].Value = model.ChargeUserID;
                parameters[6].Value = model.AvailFlag;
                parameters[7].Value = model.CreateTime;
                parameters[8].Value = model.CreateUser;
                parameters[9].Value = model.UpdateTime;
                parameters[10].Value = model.UpdateUser;
                SQLStringList.Add(strSql, parameters);
            }
            DbHelperSQL.ExecuteSqlTran(SQLStringList);
        }

        /// <summary>
        /// 分页查询出库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>出库单数据</returns>
        public DataSet GetShippingListDetailPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.ShippingListDetail AS D WITH(NOLOCK) LEFT JOIN ShippingList AS G WITH(NOLOCK) ON D.ShippingListId=G.ID LEFT JOIN ShippingPlanDetail AS P WITH(NOLOCK) ON D.ShippingPlanDetailID=P.ID LEFT JOIN ShippingPlan AS S WITH(NOLOCK) ON P.ShippingPlanID=S.ID JOIN Admin AS A1 ON A1.AdminID=G.ByHandUserID LEFT JOIN Admin AS A2 ON A2.AdminID=G.ApprovedUserID ";
            //查询字段
            string strGetFields = " D.ID, AvailFlag=CASE WHEN D.AvailFlag=1 THEN '有效' ELSE '无效' END, G.ShippingListNo, D.ProductCode, D.OutQty, S.ShippingPlanNo , P.CPN, P.MPN,P.PlanQty,G.IsApproved,OutStockDate=Convert(varchar(20),G.OutStockDate,120),D.StockCode,ByHandUser=A1.UserName,ApprovedUser=A2.UserName,CreateTime=Convert(varchar(20),G.CreateTime,120) ,UpdateTime=Convert(varchar(20),G.UpdateTime,120),D.CreateUser,D.UpdateUser ";
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
        /// 根据出库单Id获得出库单明细列表记录
        /// </summary>
        /// <param name="iShippingListId">出库单Id</param>
        /// <returns>出库单显示明细数据</returns>
        public DataTable GetDisplayModelList(int iShippingListId)
        {
            //查询字段
            string strGetFields = " D.ID,D.ShippingListID,D.POPlanId,P.PONO,P.POQuantity,ShipmentDate=Convert(varchar(20),P.ShipmentDate,120),D.ProductCode,D.InQty,Price=CAST(D.Price AS DECIMAL(18,2)),TotalPrice=CAST(D.TotalPrice AS DECIMAL(18,2)),D.Remark,D.AvailFlag ";
            //查询表名
            string strTableName = " ShippingListDetail AS D LEFT JOIN POPlan AS P ON D.POPlanId = P.ID";
            //查询条件
            string strWhere = " D.AvailFlag = 1 AND ShippingListID=@ShippingListID ";

            SqlParameter[] parameters = {
                    new SqlParameter("@ShippingListID", SqlDbType.Int)
                    };
            parameters[0].Value = iShippingListId;

            return DbHelperSQL.Query("SELECT " + strGetFields + " FROM " + strTableName + " WHERE " + strWhere, parameters).Tables[0];
        }
        #endregion  ExtensionMethod
    }
}

