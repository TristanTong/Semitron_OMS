/**  
* GodownEntryDetailDAL.cs
*
* 功 能： N/A
* 类 名： GodownEntryDetailDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:18   童荣辉    初版
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
    /// 数据访问类:GodownEntryDetailDAL
    /// </summary>
    public partial class GodownEntryDetailDAL
    {
        public GodownEntryDetailDAL()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.GodownEntryDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GodownEntryDetail(");
            strSql.Append("GodownEntryID,PONo,POPlanId,ProductCode,InQty,Price,TotalPrice,Remark,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@GodownEntryID,@PONo,@POPlanId,@ProductCode,@InQty,@Price,@TotalPrice,@Remark,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GodownEntryID", SqlDbType.Int,4),
					new SqlParameter("@PONo", SqlDbType.NVarChar,50),
					new SqlParameter("@POPlanId", SqlDbType.Int,4),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InQty", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1024),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.GodownEntryID;
            parameters[1].Value = model.PONo;
            parameters[2].Value = model.POPlanId;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.InQty;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.TotalPrice;
            parameters[7].Value = model.Remark;
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
        public bool Update(Semitron_OMS.Model.OMS.GodownEntryDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GodownEntryDetail set ");
            strSql.Append("GodownEntryID=@GodownEntryID,");
            strSql.Append("PONo=@PONo,");
            strSql.Append("POPlanId=@POPlanId,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("InQty=@InQty,");
            strSql.Append("Price=@Price,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@GodownEntryID", SqlDbType.Int,4),
					new SqlParameter("@PONo", SqlDbType.NVarChar,50),
					new SqlParameter("@POPlanId", SqlDbType.Int,4),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InQty", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1024),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.GodownEntryID;
            parameters[1].Value = model.PONo;
            parameters[2].Value = model.POPlanId;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.InQty;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.TotalPrice;
            parameters[7].Value = model.Remark;
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
            strSql.Append("delete from GodownEntryDetail ");
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
            strSql.Append("delete from GodownEntryDetail ");
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
        public Semitron_OMS.Model.OMS.GodownEntryDetailModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,GodownEntryID,PONo,POPlanId,ProductCode,InQty,Price,TotalPrice,Remark,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser from GodownEntryDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.GodownEntryDetailModel model = new Semitron_OMS.Model.OMS.GodownEntryDetailModel();
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
        public Semitron_OMS.Model.OMS.GodownEntryDetailModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.GodownEntryDetailModel model = new Semitron_OMS.Model.OMS.GodownEntryDetailModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["GodownEntryID"] != null && row["GodownEntryID"].ToString() != "")
                {
                    model.GodownEntryID = int.Parse(row["GodownEntryID"].ToString());
                }
                if (row["PONo"] != null)
                {
                    model.PONo = row["PONo"].ToString();
                }
                if (row["POPlanId"] != null && row["POPlanId"].ToString() != "")
                {
                    model.POPlanId = int.Parse(row["POPlanId"].ToString());
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["InQty"] != null && row["InQty"].ToString() != "")
                {
                    model.InQty = int.Parse(row["InQty"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["TotalPrice"] != null && row["TotalPrice"].ToString() != "")
                {
                    model.TotalPrice = decimal.Parse(row["TotalPrice"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
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
            strSql.Append("select ID,GodownEntryID,PONo,POPlanId,ProductCode,InQty,Price,TotalPrice,Remark,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM GodownEntryDetail ");
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
            strSql.Append(" ID,GodownEntryID,PONo,POPlanId,ProductCode,InQty,Price,TotalPrice,Remark,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM GodownEntryDetail ");
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
            strSql.Append("select count(1) FROM GodownEntryDetail ");
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
            strSql.Append(")AS Row, T.*  from GodownEntryDetail T ");
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
            parameters[0].Value = "GodownEntryDetail";
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
        /// 增加入库单明细多条数据
        /// </summary>
        /// <param name="lstGodownEntryDetailModel"></param>
        public void AddList(System.Collections.Generic.List<Model.OMS.GodownEntryDetailModel> lstGodownEntryDetailModel)
        {
            Hashtable SQLStringList = new Hashtable();
            foreach (Model.OMS.GodownEntryDetailModel model in lstGodownEntryDetailModel)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into GodownEntryDetail(");
                strSql.Append("GodownEntryID,PONo,POPlanId,ProductCode,InQty,Price,TotalPrice,Remark,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
                strSql.Append(" values (");
                strSql.Append("@GodownEntryID,@PONo,@POPlanId,@ProductCode,@InQty,@Price,@TotalPrice,@Remark,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
                //strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@GodownEntryID", SqlDbType.Int,4),
					new SqlParameter("@PONo", SqlDbType.NVarChar,50),
					new SqlParameter("@POPlanId", SqlDbType.Int,4),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InQty", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1024),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
                parameters[0].Value = model.GodownEntryID;
                parameters[1].Value = model.PONo;
                parameters[2].Value = model.POPlanId;
                parameters[3].Value = model.ProductCode;
                parameters[4].Value = model.InQty;
                parameters[5].Value = model.Price;
                parameters[6].Value = model.TotalPrice;
                parameters[7].Value = model.Remark;
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
        /// 分页查询入库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>入库单数据</returns>
        public DataSet GetGodownEntryDetailPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.GodownEntryDetail AS D LEFT JOIN GodownEntry AS G ON D.GodownEntryId=G.ID LEFT JOIN POPlan AS P ON D.POPlanId=P.ID LEFT JOIN Supplier AS S ON S.ID=P.SupplierID LEFT JOIN Admin AS A1 ON A1.AdminID=G.ByHandUserID LEFT JOIN Admin AS A2 ON A2.AdminID=G.ApprovedUser LEFT JOIN dbo.PO AS O WITH(NOLOCK) ON O.PONO=P.PONO LEFT JOIN dbo.Corporation AS C ON C.ID=O.CorporationID";
            //查询字段
            string strGetFields = " D.ID, AvailFlag=CASE WHEN D.AvailFlag=1 THEN '有效' ELSE '无效' END,C.CompanyName, G.EntryNo, D.ProductCode, D.InQty, D.Price, D.TotalPrice, P.PONo, S.SupplierName, P.MPN,P.POQuantity,G.IsApproved,InStockDate=Convert(varchar(20),G.InStockDate,120),G.InWarehouseCode,ByHandUser=A1.UserName,ApprovedUser=A2.UserName,CreateTime=Convert(varchar(20),G.CreateTime,120) ,UpdateTime=Convert(varchar(20),G.UpdateTime,120),D.CreateUser,D.UpdateUser ";
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
        /// 根据入库单Id获得入库单明细列表记录
        /// </summary>
        /// <param name="iEntryId">入库单Id</param>
        /// <returns>入库单显示明细数据</returns>
        public DataTable GetDisplayModelList(int iEntryId)
        {
            //查询字段
            string strGetFields = " D.ID,D.GodownEntryID,D.POPlanId,P.PONO,P.POQuantity,ShipmentDate=Convert(varchar(20),P.ShipmentDate,120),D.ProductCode,D.InQty,Price=CAST(D.Price AS DECIMAL(18,2)),TotalPrice=CAST(D.TotalPrice AS DECIMAL(18,2)),D.Remark,D.AvailFlag ";
            //查询表名
            string strTableName = " GodownEntryDetail AS D LEFT JOIN POPlan AS P ON D.POPlanId = P.ID";
            //查询条件
            string strWhere = " D.AvailFlag = 1 AND GodownEntryID=@GodownEntryID ";

            SqlParameter[] parameters = {
                    new SqlParameter("@GodownEntryID", SqlDbType.Int)
                    };
            parameters[0].Value = iEntryId;

            return DbHelperSQL.Query("SELECT " + strGetFields + " FROM " + strTableName + " WHERE " + strWhere, parameters).Tables[0];
        }

        /// <summary>
        /// 获取入库单查找明细，得历史入库单价
        /// </summary>
        public DataTable GetGodownEntryDetailLookupList(System.Collections.Generic.List<Semitron_OMS.Common.SQLConditionFilter> lstFilter)
        {
            //查询字段
            string strGetFields = " GodownEntryID=E.ID,GodownEntryDetailID=D.ID,E.EntryNo,I.MPN,ProductCodes=I.ProductCode,POPrice=D.Price,E.InWarehouseCode,W.WName,S.SCode,S.SupplierName,InStockDate=CONVERT(NVARCHAR(10),E.InStockDate,120) ";

            //查询表名
            string strTableName = " dbo.GodownEntry AS E WITH(NOLOCK) INNER JOIN dbo.GodownEntryDetail AS D WITH(NOLOCK) ON E.ID=D.GodownEntryID INNER JOIN dbo.ProductInfo AS I WITH(NOLOCK) ON I.ProductCode=D.ProductCode LEFT JOIN dbo.Warehouse AS W ON W.WCode=E.InWarehouseCode LEFT JOIN dbo.Supplier AS S WITH(NOLOCK) ON S.ID=I.SupplierID ";

            //查询条件
            string strWhere = "  E.State=1 AND D.AvailFlag=1 AND I.AvailFlag=1 AND W.AvailFlag=1 AND S.AvailFlag=1 AND I.MPN IN (SELECT MPN FROM dbo.CustomerOrderDetail AS COD WITH(NOLOCK) WHERE COD.ID=" + lstFilter.Find(r => r.FiledName == "CustomerOrderDetailId").Value + " AND COD.AvailFlag=1)";

            //分组条件
            string strOrderBy = " E.InStockDate DESC ";

            return DbHelperSQL.Query("SELECT " + strGetFields + " FROM " + strTableName + " WHERE " + strWhere + " ORDER BY " + strOrderBy).Tables[0];
        }
        #endregion  ExtensionMethod
    }
}

