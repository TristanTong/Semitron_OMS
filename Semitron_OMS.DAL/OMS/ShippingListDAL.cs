/**  
* ShippingListDAL.cs
*
* 功 能： N/A
* 类 名： ShippingListDAL
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
using Semitron_OMS.Common;
using Semitron_OMS.DAL.Common;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:ShippingListDAL
    /// </summary>
    public partial class ShippingListDAL
    {
        public ShippingListDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "ShippingList");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ShippingList");
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
        public int Add(Semitron_OMS.Model.OMS.ShippingListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShippingList(");
            strSql.Append("ShippingListNo,ShippingListDate,OutStockDate,ShippingMethod,ByHandUserID,EstimateArrivedDate,RealArrivedDate,IsApproved,ApprovedUserID,ApprovedTime,Destination,CustomerID,TrackingNo,LogisticsLine,LogisticsFee,State,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@ShippingListNo,@ShippingListDate,@OutStockDate,@ShippingMethod,@ByHandUserID,@EstimateArrivedDate,@RealArrivedDate,@IsApproved,@ApprovedUserID,@ApprovedTime,@Destination,@CustomerID,@TrackingNo,@LogisticsLine,@LogisticsFee,@State,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingListNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ShippingListDate", SqlDbType.DateTime),
					new SqlParameter("@OutStockDate", SqlDbType.DateTime),
					new SqlParameter("@ShippingMethod", SqlDbType.NVarChar,50),
					new SqlParameter("@ByHandUserID", SqlDbType.Int,4),
					new SqlParameter("@EstimateArrivedDate", SqlDbType.DateTime),
					new SqlParameter("@RealArrivedDate", SqlDbType.DateTime),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@ApprovedUserID", SqlDbType.Int,4),
					new SqlParameter("@ApprovedTime", SqlDbType.DateTime),
					new SqlParameter("@Destination", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@TrackingNo", SqlDbType.NVarChar,50),
					new SqlParameter("@LogisticsLine", SqlDbType.NVarChar,50),
					new SqlParameter("@LogisticsFee", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ShippingListNo;
            parameters[1].Value = model.ShippingListDate;
            parameters[2].Value = model.OutStockDate;
            parameters[3].Value = model.ShippingMethod;
            parameters[4].Value = model.ByHandUserID;
            parameters[5].Value = model.EstimateArrivedDate;
            parameters[6].Value = model.RealArrivedDate;
            parameters[7].Value = model.IsApproved;
            parameters[8].Value = model.ApprovedUserID;
            parameters[9].Value = model.ApprovedTime;
            parameters[10].Value = model.Destination;
            parameters[11].Value = model.CustomerID;
            parameters[12].Value = model.TrackingNo;
            parameters[13].Value = model.LogisticsLine;
            parameters[14].Value = model.LogisticsFee;
            parameters[15].Value = model.State;
            parameters[16].Value = model.CreateTime;
            parameters[17].Value = model.CreateUser;
            parameters[18].Value = model.UpdateTime;
            parameters[19].Value = model.UpdateUser;

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
        public bool Update(Semitron_OMS.Model.OMS.ShippingListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShippingList set ");
            strSql.Append("ShippingListNo=@ShippingListNo,");
            strSql.Append("ShippingListDate=@ShippingListDate,");
            strSql.Append("OutStockDate=@OutStockDate,");
            strSql.Append("ShippingMethod=@ShippingMethod,");
            strSql.Append("ByHandUserID=@ByHandUserID,");
            strSql.Append("EstimateArrivedDate=@EstimateArrivedDate,");
            strSql.Append("RealArrivedDate=@RealArrivedDate,");
            strSql.Append("IsApproved=@IsApproved,");
            strSql.Append("ApprovedUserID=@ApprovedUserID,");
            strSql.Append("ApprovedTime=@ApprovedTime,");
            strSql.Append("Destination=@Destination,");
            strSql.Append("CustomerID=@CustomerID,");
            strSql.Append("TrackingNo=@TrackingNo,");
            strSql.Append("LogisticsLine=@LogisticsLine,");
            strSql.Append("LogisticsFee=@LogisticsFee,");
            strSql.Append("State=@State,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingListNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ShippingListDate", SqlDbType.DateTime),
					new SqlParameter("@OutStockDate", SqlDbType.DateTime),
					new SqlParameter("@ShippingMethod", SqlDbType.NVarChar,50),
					new SqlParameter("@ByHandUserID", SqlDbType.Int,4),
					new SqlParameter("@EstimateArrivedDate", SqlDbType.DateTime),
					new SqlParameter("@RealArrivedDate", SqlDbType.DateTime),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@ApprovedUserID", SqlDbType.Int,4),
					new SqlParameter("@ApprovedTime", SqlDbType.DateTime),
					new SqlParameter("@Destination", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@TrackingNo", SqlDbType.NVarChar,50),
					new SqlParameter("@LogisticsLine", SqlDbType.NVarChar,50),
					new SqlParameter("@LogisticsFee", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ShippingListNo;
            parameters[1].Value = model.ShippingListDate;
            parameters[2].Value = model.OutStockDate;
            parameters[3].Value = model.ShippingMethod;
            parameters[4].Value = model.ByHandUserID;
            parameters[5].Value = model.EstimateArrivedDate;
            parameters[6].Value = model.RealArrivedDate;
            parameters[7].Value = model.IsApproved;
            parameters[8].Value = model.ApprovedUserID;
            parameters[9].Value = model.ApprovedTime;
            parameters[10].Value = model.Destination;
            parameters[11].Value = model.CustomerID;
            parameters[12].Value = model.TrackingNo;
            parameters[13].Value = model.LogisticsLine;
            parameters[14].Value = model.LogisticsFee;
            parameters[15].Value = model.State;
            parameters[16].Value = model.CreateTime;
            parameters[17].Value = model.CreateUser;
            parameters[18].Value = model.UpdateTime;
            parameters[19].Value = model.UpdateUser;
            parameters[20].Value = model.ID;

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
            strSql.Append("delete from ShippingList ");
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
            strSql.Append("delete from ShippingList ");
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
        public Semitron_OMS.Model.OMS.ShippingListModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ShippingListNo,ShippingListDate,OutStockDate,ShippingMethod,ByHandUserID,EstimateArrivedDate,RealArrivedDate,IsApproved,ApprovedUserID,ApprovedTime,Destination,CustomerID,TrackingNo,LogisticsLine,LogisticsFee,State,CreateTime,CreateUser,UpdateTime,UpdateUser from ShippingList ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.ShippingListModel model = new Semitron_OMS.Model.OMS.ShippingListModel();
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
        public Semitron_OMS.Model.OMS.ShippingListModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.ShippingListModel model = new Semitron_OMS.Model.OMS.ShippingListModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ShippingListNo"] != null)
                {
                    model.ShippingListNo = row["ShippingListNo"].ToString();
                }
                if (row["ShippingListDate"] != null && row["ShippingListDate"].ToString() != "")
                {
                    model.ShippingListDate = DateTime.Parse(row["ShippingListDate"].ToString());
                }
                if (row["OutStockDate"] != null && row["OutStockDate"].ToString() != "")
                {
                    model.OutStockDate = DateTime.Parse(row["OutStockDate"].ToString());
                }
                if (row["ShippingMethod"] != null)
                {
                    model.ShippingMethod = row["ShippingMethod"].ToString();
                }
                if (row["ByHandUserID"] != null && row["ByHandUserID"].ToString() != "")
                {
                    model.ByHandUserID = int.Parse(row["ByHandUserID"].ToString());
                }
                if (row["EstimateArrivedDate"] != null && row["EstimateArrivedDate"].ToString() != "")
                {
                    model.EstimateArrivedDate = DateTime.Parse(row["EstimateArrivedDate"].ToString());
                }
                if (row["RealArrivedDate"] != null && row["RealArrivedDate"].ToString() != "")
                {
                    model.RealArrivedDate = DateTime.Parse(row["RealArrivedDate"].ToString());
                }
                if (row["IsApproved"] != null && row["IsApproved"].ToString() != "")
                {
                    if ((row["IsApproved"].ToString() == "1") || (row["IsApproved"].ToString().ToLower() == "true"))
                    {
                        model.IsApproved = true;
                    }
                    else
                    {
                        model.IsApproved = false;
                    }
                }
                if (row["ApprovedUserID"] != null && row["ApprovedUserID"].ToString() != "")
                {
                    model.ApprovedUserID = int.Parse(row["ApprovedUserID"].ToString());
                }
                if (row["ApprovedTime"] != null && row["ApprovedTime"].ToString() != "")
                {
                    model.ApprovedTime = DateTime.Parse(row["ApprovedTime"].ToString());
                }
                if (row["Destination"] != null)
                {
                    model.Destination = row["Destination"].ToString();
                }
                if (row["CustomerID"] != null && row["CustomerID"].ToString() != "")
                {
                    model.CustomerID = int.Parse(row["CustomerID"].ToString());
                }
                if (row["TrackingNo"] != null)
                {
                    model.TrackingNo = row["TrackingNo"].ToString();
                }
                if (row["LogisticsLine"] != null)
                {
                    model.LogisticsLine = row["LogisticsLine"].ToString();
                }
                if (row["LogisticsFee"] != null && row["LogisticsFee"].ToString() != "")
                {
                    model.LogisticsFee = decimal.Parse(row["LogisticsFee"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
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
            strSql.Append("select ID,ShippingListNo,ShippingListDate,OutStockDate,ShippingMethod,ByHandUserID,EstimateArrivedDate,RealArrivedDate,IsApproved,ApprovedUserID,ApprovedTime,Destination,CustomerID,TrackingNo,LogisticsLine,LogisticsFee,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM ShippingList ");
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
            strSql.Append(" ID,ShippingListNo,ShippingListDate,OutStockDate,ShippingMethod,ByHandUserID,EstimateArrivedDate,RealArrivedDate,IsApproved,ApprovedUserID,ApprovedTime,Destination,CustomerID,TrackingNo,LogisticsLine,LogisticsFee,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM ShippingList ");
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
            strSql.Append("select count(1) FROM ShippingList ");
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
            strSql.Append(")AS Row, T.*  from ShippingList T ");
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
            parameters[0].Value = "ShippingList";
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
        /// 分页查询出库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>出库单数据</returns>
        public DataSet GetShippingListPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.ShippingList AS G LEFT JOIN Admin AS A1 ON A1.AdminID=G.ByHandUserID LEFT JOIN Admin AS A2 ON A2.AdminID=G.ApprovedUserID ";
            //查询字段
            string strGetFields = " G.ID,State=CASE WHEN G.State=1 THEN '有效' ELSE '无效' END,G.ShippingListNo,IsApproved=CASE WHEN G.IsApproved=1 THEN '已审核' ELSE '未审核' END,OutStockDate=Convert(varchar(20),G.OutStockDate,120),EstimateArrivedDate=Convert(varchar(20),G.EstimateArrivedDate,120),RealArrivedDate=Convert(varchar(20),G.RealArrivedDate,120),ByHandUser=A1.UserName,ApprovedUser=A2.UserName,ApprovedTime=Convert(varchar(20),G.ApprovedTime,120),CreateTime=Convert(varchar(20),G.CreateTime,120) ,UpdateTime=Convert(varchar(20),G.UpdateTime,120),G.CreateUser,G.UpdateUser ";
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
        /// 设置出库单状态
        /// </summary>
        /// <param name="iId">出库单ID</param>
        /// <param name="iStatus">状态ID</param>
        /// <returns>执行结果是否成功</returns>
        public bool SetValid(int iId, int iStatus)
        {
            System.Collections.Generic.List<string> lstSql = new System.Collections.Generic.List<string>();
            lstSql.Add("update ShippingList set State=" + iStatus + " where ID=" + iId);
            lstSql.Add("update ShippingListDetail set AvailFlag=" + iStatus + " where ShippingListID=" + iId);

            int rows = DbHelperSQL.ExecuteSqlTran(lstSql);
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
        /// 验证并审核出库单
        /// </summary>
        public string ApproveShippingList(int iId, int iUser)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@ShippingListId", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Result", SqlDbType.NVarChar,512)
                    };
            parameters[0].Value = iId;
            parameters[1].Value = iUser;
            parameters[2].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ApproveShippingList, parameters, "ds");
            return parameters[2].Value.ToString();
        }
        #endregion  ExtensionMethod
    }
}

