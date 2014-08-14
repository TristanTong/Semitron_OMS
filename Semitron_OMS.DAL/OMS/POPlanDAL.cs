/**  
* POPlanDAL.cs
*
* 功 能： N/A
* 类 名： POPlanDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:42:38   童荣辉    初版
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
    /// 数据访问类:POPlanDAL
    /// </summary>
    public partial class POPlanDAL
    {
        public POPlanDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "POPlan");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from POPlan");
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
        public int Add(Semitron_OMS.Model.OMS.POPlanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into POPlan(");
            strSql.Append("PONO,CPN,MPN,MFG,DC,ROHS,VCD,POQuantity,ArrivedQty,StockQty,AlreadyQty,SupplierID,VendorPaymentTypeID,IsPaySupplier,BuyExchangeRate,BuyRealCurrency,BuyRealPrice,BuyStandardCurrency,BuyPrice,BuyCost,OtherFee,OtherFeeRemark,ShipmentDate,State,CustomerOrderDetailID,CreateUser,CreateTime,UpdateUser,UpdateTime,ProductCode)");
            strSql.Append(" values (");
            strSql.Append("@PONO,@CPN,@MPN,@MFG,@DC,@ROHS,@VCD,@POQuantity,@ArrivedQty,@StockQty,@AlreadyQty,@SupplierID,@VendorPaymentTypeID,@IsPaySupplier,@BuyExchangeRate,@BuyRealCurrency,@BuyRealPrice,@BuyStandardCurrency,@BuyPrice,@BuyCost,@OtherFee,@OtherFeeRemark,@ShipmentDate,@State,@CustomerOrderDetailID,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime,@ProductCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PONO", SqlDbType.VarChar,32),
					new SqlParameter("@CPN", SqlDbType.VarChar,32),
					new SqlParameter("@MPN", SqlDbType.VarChar,32),
					new SqlParameter("@MFG", SqlDbType.NVarChar,32),
					new SqlParameter("@DC", SqlDbType.VarChar,16),
					new SqlParameter("@ROHS", SqlDbType.Bit,1),
					new SqlParameter("@VCD", SqlDbType.Date,3),
					new SqlParameter("@POQuantity", SqlDbType.Int,4),
					new SqlParameter("@ArrivedQty", SqlDbType.Int,4),
					new SqlParameter("@StockQty", SqlDbType.Int,4),
					new SqlParameter("@AlreadyQty", SqlDbType.Int,4),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@VendorPaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@IsPaySupplier", SqlDbType.Bit,1),
					new SqlParameter("@BuyExchangeRate", SqlDbType.Decimal,9),
					new SqlParameter("@BuyRealCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@BuyRealPrice", SqlDbType.Decimal,9),
					new SqlParameter("@BuyStandardCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@BuyPrice", SqlDbType.Decimal,9),
					new SqlParameter("@BuyCost", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,128),
					new SqlParameter("@ShipmentDate", SqlDbType.Date,3),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CustomerOrderDetailID", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.PONO;
            parameters[1].Value = model.CPN;
            parameters[2].Value = model.MPN;
            parameters[3].Value = model.MFG;
            parameters[4].Value = model.DC;
            parameters[5].Value = model.ROHS;
            parameters[6].Value = model.VCD;
            parameters[7].Value = model.POQuantity;
            parameters[8].Value = model.ArrivedQty;
            parameters[9].Value = model.StockQty;
            parameters[10].Value = model.AlreadyQty;
            parameters[11].Value = model.SupplierID;
            parameters[12].Value = model.VendorPaymentTypeID;
            parameters[13].Value = model.IsPaySupplier;
            parameters[14].Value = model.BuyExchangeRate;
            parameters[15].Value = model.BuyRealCurrency;
            parameters[16].Value = model.BuyRealPrice;
            parameters[17].Value = model.BuyStandardCurrency;
            parameters[18].Value = model.BuyPrice;
            parameters[19].Value = model.BuyCost;
            parameters[20].Value = model.OtherFee;
            parameters[21].Value = model.OtherFeeRemark;
            parameters[22].Value = model.ShipmentDate;
            parameters[23].Value = model.State;
            parameters[24].Value = model.CustomerOrderDetailID;
            parameters[25].Value = model.CreateUser;
            parameters[26].Value = model.CreateTime;
            parameters[27].Value = model.UpdateUser;
            parameters[28].Value = model.UpdateTime;
            parameters[29].Value = model.ProductCode;

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
        public bool Update(Semitron_OMS.Model.OMS.POPlanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update POPlan set ");
            strSql.Append("PONO=@PONO,");
            strSql.Append("CPN=@CPN,");
            strSql.Append("MPN=@MPN,");
            strSql.Append("MFG=@MFG,");
            strSql.Append("DC=@DC,");
            strSql.Append("ROHS=@ROHS,");
            strSql.Append("VCD=@VCD,");
            strSql.Append("POQuantity=@POQuantity,");
            strSql.Append("ArrivedQty=@ArrivedQty,");
            strSql.Append("StockQty=@StockQty,");
            strSql.Append("AlreadyQty=@AlreadyQty,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("VendorPaymentTypeID=@VendorPaymentTypeID,");
            strSql.Append("IsPaySupplier=@IsPaySupplier,");
            strSql.Append("BuyExchangeRate=@BuyExchangeRate,");
            strSql.Append("BuyRealCurrency=@BuyRealCurrency,");
            strSql.Append("BuyRealPrice=@BuyRealPrice,");
            strSql.Append("BuyStandardCurrency=@BuyStandardCurrency,");
            strSql.Append("BuyPrice=@BuyPrice,");
            strSql.Append("BuyCost=@BuyCost,");
            strSql.Append("OtherFee=@OtherFee,");
            strSql.Append("OtherFeeRemark=@OtherFeeRemark,");
            strSql.Append("ShipmentDate=@ShipmentDate,");
            strSql.Append("State=@State,");
            strSql.Append("CustomerOrderDetailID=@CustomerOrderDetailID,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("ProductCode=@ProductCode");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@PONO", SqlDbType.VarChar,32),
					new SqlParameter("@CPN", SqlDbType.VarChar,32),
					new SqlParameter("@MPN", SqlDbType.VarChar,32),
					new SqlParameter("@MFG", SqlDbType.NVarChar,32),
					new SqlParameter("@DC", SqlDbType.VarChar,16),
					new SqlParameter("@ROHS", SqlDbType.Bit,1),
					new SqlParameter("@VCD", SqlDbType.Date,3),
					new SqlParameter("@POQuantity", SqlDbType.Int,4),
					new SqlParameter("@ArrivedQty", SqlDbType.Int,4),
					new SqlParameter("@StockQty", SqlDbType.Int,4),
					new SqlParameter("@AlreadyQty", SqlDbType.Int,4),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@VendorPaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@IsPaySupplier", SqlDbType.Bit,1),
					new SqlParameter("@BuyExchangeRate", SqlDbType.Decimal,9),
					new SqlParameter("@BuyRealCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@BuyRealPrice", SqlDbType.Decimal,9),
					new SqlParameter("@BuyStandardCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@BuyPrice", SqlDbType.Decimal,9),
					new SqlParameter("@BuyCost", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,128),
					new SqlParameter("@ShipmentDate", SqlDbType.Date,3),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CustomerOrderDetailID", SqlDbType.Int,4),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.PONO;
            parameters[1].Value = model.CPN;
            parameters[2].Value = model.MPN;
            parameters[3].Value = model.MFG;
            parameters[4].Value = model.DC;
            parameters[5].Value = model.ROHS;
            parameters[6].Value = model.VCD;
            parameters[7].Value = model.POQuantity;
            parameters[8].Value = model.ArrivedQty;
            parameters[9].Value = model.StockQty;
            parameters[10].Value = model.AlreadyQty;
            parameters[11].Value = model.SupplierID;
            parameters[12].Value = model.VendorPaymentTypeID;
            parameters[13].Value = model.IsPaySupplier;
            parameters[14].Value = model.BuyExchangeRate;
            parameters[15].Value = model.BuyRealCurrency;
            parameters[16].Value = model.BuyRealPrice;
            parameters[17].Value = model.BuyStandardCurrency;
            parameters[18].Value = model.BuyPrice;
            parameters[19].Value = model.BuyCost;
            parameters[20].Value = model.OtherFee;
            parameters[21].Value = model.OtherFeeRemark;
            parameters[22].Value = model.ShipmentDate;
            parameters[23].Value = model.State;
            parameters[24].Value = model.CustomerOrderDetailID;
            parameters[25].Value = model.CreateUser;
            parameters[26].Value = model.CreateTime;
            parameters[27].Value = model.UpdateUser;
            parameters[28].Value = model.UpdateTime;
            parameters[29].Value = model.ProductCode;
            parameters[30].Value = model.ID;

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
            strSql.Append("delete from POPlan ");
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
            strSql.Append("delete from POPlan ");
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
        public Semitron_OMS.Model.OMS.POPlanModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PONO,CPN,MPN,MFG,DC,ROHS,VCD,POQuantity,ArrivedQty,StockQty,AlreadyQty,SupplierID,VendorPaymentTypeID,IsPaySupplier,BuyExchangeRate,BuyRealCurrency,BuyRealPrice,BuyStandardCurrency,BuyPrice,BuyCost,OtherFee,OtherFeeRemark,ShipmentDate,State,CustomerOrderDetailID,CreateUser,CreateTime,UpdateUser,UpdateTime,ProductCode from POPlan ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.POPlanModel model = new Semitron_OMS.Model.OMS.POPlanModel();
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
        public Semitron_OMS.Model.OMS.POPlanModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.POPlanModel model = new Semitron_OMS.Model.OMS.POPlanModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["PONO"] != null)
                {
                    model.PONO = row["PONO"].ToString();
                }
                if (row["CPN"] != null)
                {
                    model.CPN = row["CPN"].ToString();
                }
                if (row["MPN"] != null)
                {
                    model.MPN = row["MPN"].ToString();
                }
                if (row["MFG"] != null)
                {
                    model.MFG = row["MFG"].ToString();
                }
                if (row["DC"] != null)
                {
                    model.DC = row["DC"].ToString();
                }
                if (row["ROHS"] != null && row["ROHS"].ToString() != "")
                {
                    if ((row["ROHS"].ToString() == "1") || (row["ROHS"].ToString().ToLower() == "true"))
                    {
                        model.ROHS = true;
                    }
                    else
                    {
                        model.ROHS = false;
                    }
                }
                if (row["VCD"] != null && row["VCD"].ToString() != "")
                {
                    model.VCD = DateTime.Parse(row["VCD"].ToString());
                }
                if (row["POQuantity"] != null && row["POQuantity"].ToString() != "")
                {
                    model.POQuantity = int.Parse(row["POQuantity"].ToString());
                }
                if (row["ArrivedQty"] != null && row["ArrivedQty"].ToString() != "")
                {
                    model.ArrivedQty = int.Parse(row["ArrivedQty"].ToString());
                }
                if (row["StockQty"] != null && row["StockQty"].ToString() != "")
                {
                    model.StockQty = int.Parse(row["StockQty"].ToString());
                }
                if (row["AlreadyQty"] != null && row["AlreadyQty"].ToString() != "")
                {
                    model.AlreadyQty = int.Parse(row["AlreadyQty"].ToString());
                }
                if (row["SupplierID"] != null && row["SupplierID"].ToString() != "")
                {
                    model.SupplierID = int.Parse(row["SupplierID"].ToString());
                }
                if (row["VendorPaymentTypeID"] != null && row["VendorPaymentTypeID"].ToString() != "")
                {
                    model.VendorPaymentTypeID = int.Parse(row["VendorPaymentTypeID"].ToString());
                }
                if (row["IsPaySupplier"] != null && row["IsPaySupplier"].ToString() != "")
                {
                    if ((row["IsPaySupplier"].ToString() == "1") || (row["IsPaySupplier"].ToString().ToLower() == "true"))
                    {
                        model.IsPaySupplier = true;
                    }
                    else
                    {
                        model.IsPaySupplier = false;
                    }
                }
                if (row["BuyExchangeRate"] != null && row["BuyExchangeRate"].ToString() != "")
                {
                    model.BuyExchangeRate = decimal.Parse(row["BuyExchangeRate"].ToString());
                }
                if (row["BuyRealCurrency"] != null)
                {
                    model.BuyRealCurrency = row["BuyRealCurrency"].ToString();
                }
                if (row["BuyRealPrice"] != null && row["BuyRealPrice"].ToString() != "")
                {
                    model.BuyRealPrice = decimal.Parse(row["BuyRealPrice"].ToString());
                }
                if (row["BuyStandardCurrency"] != null)
                {
                    model.BuyStandardCurrency = row["BuyStandardCurrency"].ToString();
                }
                if (row["BuyPrice"] != null && row["BuyPrice"].ToString() != "")
                {
                    model.BuyPrice = decimal.Parse(row["BuyPrice"].ToString());
                }
                if (row["BuyCost"] != null && row["BuyCost"].ToString() != "")
                {
                    model.BuyCost = decimal.Parse(row["BuyCost"].ToString());
                }
                if (row["OtherFee"] != null && row["OtherFee"].ToString() != "")
                {
                    model.OtherFee = decimal.Parse(row["OtherFee"].ToString());
                }
                if (row["OtherFeeRemark"] != null)
                {
                    model.OtherFeeRemark = row["OtherFeeRemark"].ToString();
                }
                if (row["ShipmentDate"] != null && row["ShipmentDate"].ToString() != "")
                {
                    model.ShipmentDate = DateTime.Parse(row["ShipmentDate"].ToString());
                }
                if (row["State"] != null && row["State"].ToString() != "")
                {
                    model.State = int.Parse(row["State"].ToString());
                }
                if (row["CustomerOrderDetailID"] != null && row["CustomerOrderDetailID"].ToString() != "")
                {
                    model.CustomerOrderDetailID = int.Parse(row["CustomerOrderDetailID"].ToString());
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
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
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
            strSql.Append("select ID,PONO,CPN,MPN,MFG,DC,ROHS,VCD,POQuantity,ArrivedQty,StockQty,AlreadyQty,SupplierID,VendorPaymentTypeID,IsPaySupplier,BuyExchangeRate,BuyRealCurrency,BuyRealPrice,BuyStandardCurrency,BuyPrice,BuyCost,OtherFee,OtherFeeRemark,ShipmentDate,State,CustomerOrderDetailID,CreateUser,CreateTime,UpdateUser,UpdateTime,ProductCode ");
            strSql.Append(" FROM POPlan ");
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
            strSql.Append(" ID,PONO,CPN,MPN,MFG,DC,ROHS,VCD,POQuantity,ArrivedQty,StockQty,AlreadyQty,SupplierID,VendorPaymentTypeID,IsPaySupplier,BuyExchangeRate,BuyRealCurrency,BuyRealPrice,BuyStandardCurrency,BuyPrice,BuyCost,OtherFee,OtherFeeRemark,ShipmentDate,State,CustomerOrderDetailID,CreateUser,CreateTime,UpdateUser,UpdateTime,ProductCode ");
            strSql.Append(" FROM POPlan ");
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
            strSql.Append("select count(1) FROM POPlan ");
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
            strSql.Append(")AS Row, T.*  from POPlan T ");
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
            parameters[0].Value = "POPlan";
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
        /// 分页查询采购订单数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>采购订单数据</returns>
        public DataSet GetPOPlanPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.POPlan AS P LEFT JOIN dbo.Supplier AS S ON P.SupplierID = S.ID LEFT JOIN dbo.PaymentType AS T ON T.ID=P.VendorPaymentTypeID LEFT JOIN Brand AS B ON B.ID=P.MFG LEFT JOIN CommonTable AS CT ON CT.TableName = 'POPlan' AND CT.FieldID = 'State' AND CT.[KEY] = P.STATE LEFT JOIN (SELECT  ObjId ,FileNum = COUNT(1) FROM dbo.Attachment WITH ( NOLOCK ) WHERE ObjType = 'POPlanQC' AND AvailFlag = 1 GROUP BY ObjId) AS A ON A.ObjId=P.ID";
            //查询字段
            string strGetFields = " P.ID ,P.PONO ,P.ProductCode ,CPN ,MPN ,MFG=B.BrandName ,DC ,ROHS=(CASE ROHS WHEN 1 THEN '是' ELSE '否' END),VCD= CONVERT(VARCHAR(10), VCD, 120) ,POQuantity ,ArrivedQty ,StockQty ,S.SupplierName ,VendorPaymentType=T.PaymentType ,IsPaySupplier =(CASE IsPaySupplier WHEN 1 THEN '是' ELSE '否' END),BuyExchangeRate ,BuyRealCurrency=(SELECT TOP 1 ShortName FROM CurrencyType WHERE CurrencyType.ID=BuyRealCurrency) ,BuyRealPrice ,BuyStandardCurrency=(SELECT TOP 1 ShortName FROM CurrencyType WHERE CurrencyType.ID=BuyStandardCurrency) ,BuyPrice ,BuyCost ,OtherFee ,ShipmentDate= CONVERT(VARCHAR(10), ShipmentDate, 120) ,State = CT.Value ,CreateTime = CONVERT(VARCHAR(20), P.CreateTime, 120) ,UpdateTime = CONVERT(VARCHAR(20), P.UpdateTime, 120),FileNum=ISNULL(A.FileNum,0) ";
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
        /// 取消采购计划
        /// </summary>
        /// <param name="strUser">操作人员</param>
        /// <param name="strIdList">采购计划ID逗号字符串</param>
        /// <returns>操作结果</returns>
        public string ValidateAndCancelPOPlan(string strUser, string strIdList)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@IdList", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = strIdList;
            parameters[1].Value = strUser;
            parameters[2].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ValidateAndCancelPOPlan, parameters, out rowsAffected);
            return parameters[2].Value.ToString();
        }
        /// <summary>
        /// 确认采购计划价格、数量
        /// </summary>
        /// <param name="strUser">操作人员</param>
        /// <param name="strIdList">采购计划ID逗号字符串</param>
        /// <returns>操作结果</returns>
        public string ValidateAndConfirmPrice(string strUser, string strIdList)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@IdList", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = strIdList;
            parameters[1].Value = strUser;
            parameters[2].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ValidateAndConfirmPrice, parameters, out rowsAffected);
            return parameters[2].Value.ToString();
        }
        /// <summary>
        /// QC确认
        /// </summary>
        /// <param name="strUser">操作人员</param>
        /// <param name="strId">采购计划ID</param>
        /// <returns>操作结果</returns>
        public string ValidateAndQCConfirm(string strUser, string strId, string strArrivedQty, string strShipmentDate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
                    new SqlParameter("@ArrivedQty", SqlDbType.VarChar,16),
                    new SqlParameter("@ShipmentDate", SqlDbType.VarChar,16),
                    new SqlParameter("@Result", SqlDbType.NVarChar,2048)
			};
            parameters[0].Value = int.Parse(strId);
            parameters[1].Value = strUser;
            parameters[2].Value = strArrivedQty;
            parameters[3].Value = strShipmentDate;
            parameters[4].Direction = ParameterDirection.Output;
            int rowsAffected = 0;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ValidateAndQCConfirm, parameters, out rowsAffected);
            return parameters[4].Value.ToString();
        }
        /// <summary>
        /// 设置字段各项值
        /// </summary>
        /// <param name="iId">采购计划Id</param>
        /// <param name="bIsCustomerPay">是否已付款 1 已付款 0 未付款</param>
        /// <param name="iVendorPaymentTypeID">供应商付款方式</param>
        /// <returns>是否成功</returns>
        public int SetPOPlanItems(int iId, bool bIsPaySupplier, int iVendorPaymentTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update POPlan set IsPaySupplier=@IsPaySupplier,VendorPaymentTypeID=@VendorPaymentTypeID ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@IsPaySupplier", SqlDbType.Bit),
                    new SqlParameter("@VendorPaymentTypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = iId;
            parameters[1].Value = bIsPaySupplier;
            parameters[2].Value = iVendorPaymentTypeID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        //获取采购计划查找列表 
        /// </summary>
        /// <param name="lstFilter"></param>
        /// <returns></returns>
        public DataTable GetPOPlanLookupList(System.Collections.Generic.List<SQLConditionFilter> lstFilter, string strQueryType)
        {
            //查询字段
            string strGetFields = " POPlanId=P.Id,P.PONo,P.ProductCode,P.MPN,P.POQuantity,P.ArrivedQty,P.StockQty,P.BuyPrice,P.BuyCost,ArrivedDate=CONVERT(varchar(10),P.ShipmentDate,120),P.SupplierID,SupplierCode=S.SCode,S.SupplierName,O.CorporationID,CorporationName=C.CompanyName ";

            //查询表名
            string strTableName = " POPlan AS P WITH (NOLOCK) LEFT JOIN Supplier AS S WITH (NOLOCK) ON P.SupplierID=S.ID LEFT JOIN dbo.PO AS O WITH (NOLOCK) ON O.PONO =P.PONO LEFT JOIN dbo.Corporation AS C ON C.ID=O.CorporationID ";

            //查询条件
            string strWhere = SQLOperateHelper.GetSQLCondition(lstFilter, false) + "AND P.State IN (145,150)";
            if (strQueryType != "2")
                strWhere += " AND P.ArrivedQty>P.StockQty";

            return DbHelperSQL.Query("SELECT " + strGetFields + " FROM " + strTableName + " WHERE " + strWhere).Tables[0];
        }

        /// <summary>
        /// 自动生成采购计划
        /// </summary>
        /// <returns>数据库生成操作结果</returns>
        public string GeneratePOPlan(int iUserId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Result", SqlDbType.NVarChar,512)
                    };
            parameters[0].Value = iUserId;
            parameters[1].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.AutoGeneratePOPlan, parameters, "ds");
            return parameters[1].Value.ToString();
        }
        #endregion  ExtensionMethod
    }
}

