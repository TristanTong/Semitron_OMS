/**  
* GatheringPlanDAL.cs
*
* 功 能： N/A
* 类 名： GatheringPlanDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/21 22:00:31   童荣辉    初版
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
namespace Semitron_OMS.DAL.FM
{
    /// <summary>
    /// 数据访问类:GatheringPlanDAL
    /// </summary>
    public partial class GatheringPlanDAL
    {
        public GatheringPlanDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "GatheringPlan");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GatheringPlan");
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
        public int Add(Semitron_OMS.Model.FM.GatheringPlanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GatheringPlan(");
            strSql.Append("CorporationID,GatheringPlanDate,CustomerID,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,ChannelFee,ChannelFeeRemark,LogisticsFee,LogisticsFeeRemark,OperatingFee,OperatingFeeRemark,StandardIncomeRealDiff,SalesMan,SalesManProportion,SalesManPay,BuyerMan,BuyerProportion,BuyerPay,POPrice,GrossProfits,ProfitMargin,NetProfit,NetProfitMargin,RealNetProfit,FeeBackDate,ProductCodes,State,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@CorporationID,@GatheringPlanDate,@CustomerID,@InnerOrderNO,@CustomerOrderNO,@CustomerOrderDetailID,@PaymentTypeID,@IsCustomerVATInvoice,@CustomerVATInvoiceNo,@TrackingNumber,@IsCustomerPay,@Qty,@SaleStandardCurrency,@SalePrice,@SaleTotal,@SaleRealCurrency,@SaleExchangeRate,@SaleRealPrice,@SaleRealTotal,@OtherFee,@OtherFeeRemark,@ChannelFee,@ChannelFeeRemark,@LogisticsFee,@LogisticsFeeRemark,@OperatingFee,@OperatingFeeRemark,@StandardIncomeRealDiff,@SalesMan,@SalesManProportion,@SalesManPay,@BuyerMan,@BuyerProportion,@BuyerPay,@POPrice,@GrossProfits,@ProfitMargin,@NetProfit,@NetProfitMargin,@RealNetProfit,@FeeBackDate,@ProductCodes,@State,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@GatheringPlanDate", SqlDbType.DateTime),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@InnerOrderNO", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerOrderNO", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerOrderDetailID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@IsCustomerVATInvoice", SqlDbType.Bit,1),
					new SqlParameter("@CustomerVATInvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@TrackingNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@IsCustomerPay", SqlDbType.Bit,1),
					new SqlParameter("@Qty", SqlDbType.Int,4),
					new SqlParameter("@SaleStandardCurrency", SqlDbType.Int,4),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,9),
					new SqlParameter("@SaleTotal", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealCurrency", SqlDbType.Int,4),
					new SqlParameter("@SaleExchangeRate", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealTotal", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@ChannelFee", SqlDbType.Decimal,9),
					new SqlParameter("@ChannelFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@LogisticsFee", SqlDbType.Decimal,9),
					new SqlParameter("@LogisticsFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@OperatingFee", SqlDbType.Decimal,9),
					new SqlParameter("@OperatingFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@StandardIncomeRealDiff", SqlDbType.Decimal,9),
					new SqlParameter("@SalesMan", SqlDbType.NVarChar,50),
					new SqlParameter("@SalesManProportion", SqlDbType.Decimal,9),
					new SqlParameter("@SalesManPay", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerMan", SqlDbType.NVarChar,50),
					new SqlParameter("@BuyerProportion", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerPay", SqlDbType.Decimal,9),
					new SqlParameter("@POPrice", SqlDbType.Decimal,9),
					new SqlParameter("@GrossProfits", SqlDbType.Decimal,9),
					new SqlParameter("@ProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@NetProfit", SqlDbType.Decimal,9),
					new SqlParameter("@NetProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@RealNetProfit", SqlDbType.Decimal,9),
					new SqlParameter("@FeeBackDate", SqlDbType.DateTime),
					new SqlParameter("@ProductCodes", SqlDbType.NVarChar,50),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.CorporationID;
            parameters[1].Value = model.GatheringPlanDate;
            parameters[2].Value = model.CustomerID;
            parameters[3].Value = model.InnerOrderNO;
            parameters[4].Value = model.CustomerOrderNO;
            parameters[5].Value = model.CustomerOrderDetailID;
            parameters[6].Value = model.PaymentTypeID;
            parameters[7].Value = model.IsCustomerVATInvoice;
            parameters[8].Value = model.CustomerVATInvoiceNo;
            parameters[9].Value = model.TrackingNumber;
            parameters[10].Value = model.IsCustomerPay;
            parameters[11].Value = model.Qty;
            parameters[12].Value = model.SaleStandardCurrency;
            parameters[13].Value = model.SalePrice;
            parameters[14].Value = model.SaleTotal;
            parameters[15].Value = model.SaleRealCurrency;
            parameters[16].Value = model.SaleExchangeRate;
            parameters[17].Value = model.SaleRealPrice;
            parameters[18].Value = model.SaleRealTotal;
            parameters[19].Value = model.OtherFee;
            parameters[20].Value = model.OtherFeeRemark;
            parameters[21].Value = model.ChannelFee;
            parameters[22].Value = model.ChannelFeeRemark;
            parameters[23].Value = model.LogisticsFee;
            parameters[24].Value = model.LogisticsFeeRemark;
            parameters[25].Value = model.OperatingFee;
            parameters[26].Value = model.OperatingFeeRemark;
            parameters[27].Value = model.StandardIncomeRealDiff;
            parameters[28].Value = model.SalesMan;
            parameters[29].Value = model.SalesManProportion;
            parameters[30].Value = model.SalesManPay;
            parameters[31].Value = model.BuyerMan;
            parameters[32].Value = model.BuyerProportion;
            parameters[33].Value = model.BuyerPay;
            parameters[34].Value = model.POPrice;
            parameters[35].Value = model.GrossProfits;
            parameters[36].Value = model.ProfitMargin;
            parameters[37].Value = model.NetProfit;
            parameters[38].Value = model.NetProfitMargin;
            parameters[39].Value = model.RealNetProfit;
            parameters[40].Value = model.FeeBackDate;
            parameters[41].Value = model.ProductCodes;
            parameters[42].Value = model.State;
            parameters[43].Value = model.CreateTime;
            parameters[44].Value = model.CreateUser;
            parameters[45].Value = model.UpdateTime;
            parameters[46].Value = model.UpdateUser;

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
        public bool Update(Semitron_OMS.Model.FM.GatheringPlanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GatheringPlan set ");
            strSql.Append("CorporationID=@CorporationID,");
            strSql.Append("GatheringPlanDate=@GatheringPlanDate,");
            strSql.Append("CustomerID=@CustomerID,");
            strSql.Append("InnerOrderNO=@InnerOrderNO,");
            strSql.Append("CustomerOrderNO=@CustomerOrderNO,");
            strSql.Append("CustomerOrderDetailID=@CustomerOrderDetailID,");
            strSql.Append("PaymentTypeID=@PaymentTypeID,");
            strSql.Append("IsCustomerVATInvoice=@IsCustomerVATInvoice,");
            strSql.Append("CustomerVATInvoiceNo=@CustomerVATInvoiceNo,");
            strSql.Append("TrackingNumber=@TrackingNumber,");
            strSql.Append("IsCustomerPay=@IsCustomerPay,");
            strSql.Append("Qty=@Qty,");
            strSql.Append("SaleStandardCurrency=@SaleStandardCurrency,");
            strSql.Append("SalePrice=@SalePrice,");
            strSql.Append("SaleTotal=@SaleTotal,");
            strSql.Append("SaleRealCurrency=@SaleRealCurrency,");
            strSql.Append("SaleExchangeRate=@SaleExchangeRate,");
            strSql.Append("SaleRealPrice=@SaleRealPrice,");
            strSql.Append("SaleRealTotal=@SaleRealTotal,");
            strSql.Append("OtherFee=@OtherFee,");
            strSql.Append("OtherFeeRemark=@OtherFeeRemark,");
            strSql.Append("ChannelFee=@ChannelFee,");
            strSql.Append("ChannelFeeRemark=@ChannelFeeRemark,");
            strSql.Append("LogisticsFee=@LogisticsFee,");
            strSql.Append("LogisticsFeeRemark=@LogisticsFeeRemark,");
            strSql.Append("OperatingFee=@OperatingFee,");
            strSql.Append("OperatingFeeRemark=@OperatingFeeRemark,");
            strSql.Append("StandardIncomeRealDiff=@StandardIncomeRealDiff,");
            strSql.Append("SalesMan=@SalesMan,");
            strSql.Append("SalesManProportion=@SalesManProportion,");
            strSql.Append("SalesManPay=@SalesManPay,");
            strSql.Append("BuyerMan=@BuyerMan,");
            strSql.Append("BuyerProportion=@BuyerProportion,");
            strSql.Append("BuyerPay=@BuyerPay,");
            strSql.Append("POPrice=@POPrice,");
            strSql.Append("GrossProfits=@GrossProfits,");
            strSql.Append("ProfitMargin=@ProfitMargin,");
            strSql.Append("NetProfit=@NetProfit,");
            strSql.Append("NetProfitMargin=@NetProfitMargin,");
            strSql.Append("RealNetProfit=@RealNetProfit,");
            strSql.Append("FeeBackDate=@FeeBackDate,");
            strSql.Append("ProductCodes=@ProductCodes,");
            strSql.Append("State=@State,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@GatheringPlanDate", SqlDbType.DateTime),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@InnerOrderNO", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerOrderNO", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerOrderDetailID", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeID", SqlDbType.Int,4),
					new SqlParameter("@IsCustomerVATInvoice", SqlDbType.Bit,1),
					new SqlParameter("@CustomerVATInvoiceNo", SqlDbType.NVarChar,50),
					new SqlParameter("@TrackingNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@IsCustomerPay", SqlDbType.Bit,1),
					new SqlParameter("@Qty", SqlDbType.Int,4),
					new SqlParameter("@SaleStandardCurrency", SqlDbType.Int,4),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,9),
					new SqlParameter("@SaleTotal", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealCurrency", SqlDbType.Int,4),
					new SqlParameter("@SaleExchangeRate", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SaleRealTotal", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@ChannelFee", SqlDbType.Decimal,9),
					new SqlParameter("@ChannelFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@LogisticsFee", SqlDbType.Decimal,9),
					new SqlParameter("@LogisticsFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@OperatingFee", SqlDbType.Decimal,9),
					new SqlParameter("@OperatingFeeRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@StandardIncomeRealDiff", SqlDbType.Decimal,9),
					new SqlParameter("@SalesMan", SqlDbType.NVarChar,50),
					new SqlParameter("@SalesManProportion", SqlDbType.Decimal,9),
					new SqlParameter("@SalesManPay", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerMan", SqlDbType.NVarChar,50),
					new SqlParameter("@BuyerProportion", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerPay", SqlDbType.Decimal,9),
					new SqlParameter("@POPrice", SqlDbType.Decimal,9),
					new SqlParameter("@GrossProfits", SqlDbType.Decimal,9),
					new SqlParameter("@ProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@NetProfit", SqlDbType.Decimal,9),
					new SqlParameter("@NetProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@RealNetProfit", SqlDbType.Decimal,9),
					new SqlParameter("@FeeBackDate", SqlDbType.DateTime),
					new SqlParameter("@ProductCodes", SqlDbType.NVarChar,50),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.CorporationID;
            parameters[1].Value = model.GatheringPlanDate;
            parameters[2].Value = model.CustomerID;
            parameters[3].Value = model.InnerOrderNO;
            parameters[4].Value = model.CustomerOrderNO;
            parameters[5].Value = model.CustomerOrderDetailID;
            parameters[6].Value = model.PaymentTypeID;
            parameters[7].Value = model.IsCustomerVATInvoice;
            parameters[8].Value = model.CustomerVATInvoiceNo;
            parameters[9].Value = model.TrackingNumber;
            parameters[10].Value = model.IsCustomerPay;
            parameters[11].Value = model.Qty;
            parameters[12].Value = model.SaleStandardCurrency;
            parameters[13].Value = model.SalePrice;
            parameters[14].Value = model.SaleTotal;
            parameters[15].Value = model.SaleRealCurrency;
            parameters[16].Value = model.SaleExchangeRate;
            parameters[17].Value = model.SaleRealPrice;
            parameters[18].Value = model.SaleRealTotal;
            parameters[19].Value = model.OtherFee;
            parameters[20].Value = model.OtherFeeRemark;
            parameters[21].Value = model.ChannelFee;
            parameters[22].Value = model.ChannelFeeRemark;
            parameters[23].Value = model.LogisticsFee;
            parameters[24].Value = model.LogisticsFeeRemark;
            parameters[25].Value = model.OperatingFee;
            parameters[26].Value = model.OperatingFeeRemark;
            parameters[27].Value = model.StandardIncomeRealDiff;
            parameters[28].Value = model.SalesMan;
            parameters[29].Value = model.SalesManProportion;
            parameters[30].Value = model.SalesManPay;
            parameters[31].Value = model.BuyerMan;
            parameters[32].Value = model.BuyerProportion;
            parameters[33].Value = model.BuyerPay;
            parameters[34].Value = model.POPrice;
            parameters[35].Value = model.GrossProfits;
            parameters[36].Value = model.ProfitMargin;
            parameters[37].Value = model.NetProfit;
            parameters[38].Value = model.NetProfitMargin;
            parameters[39].Value = model.RealNetProfit;
            parameters[40].Value = model.FeeBackDate;
            parameters[41].Value = model.ProductCodes;
            parameters[42].Value = model.State;
            parameters[43].Value = model.CreateTime;
            parameters[44].Value = model.CreateUser;
            parameters[45].Value = model.UpdateTime;
            parameters[46].Value = model.UpdateUser;
            parameters[47].Value = model.ID;

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
            strSql.Append("delete from GatheringPlan ");
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
            strSql.Append("delete from GatheringPlan ");
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
        public Semitron_OMS.Model.FM.GatheringPlanModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CorporationID,GatheringPlanDate,CustomerID,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,ChannelFee,ChannelFeeRemark,LogisticsFee,LogisticsFeeRemark,OperatingFee,OperatingFeeRemark,StandardIncomeRealDiff,SalesMan,SalesManProportion,SalesManPay,BuyerMan,BuyerProportion,BuyerPay,POPrice,GrossProfits,ProfitMargin,NetProfit,NetProfitMargin,RealNetProfit,FeeBackDate,ProductCodes,State,CreateTime,CreateUser,UpdateTime,UpdateUser from GatheringPlan ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.FM.GatheringPlanModel model = new Semitron_OMS.Model.FM.GatheringPlanModel();
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
        public Semitron_OMS.Model.FM.GatheringPlanModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.FM.GatheringPlanModel model = new Semitron_OMS.Model.FM.GatheringPlanModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["CorporationID"] != null && row["CorporationID"].ToString() != "")
                {
                    model.CorporationID = int.Parse(row["CorporationID"].ToString());
                }
                if (row["GatheringPlanDate"] != null && row["GatheringPlanDate"].ToString() != "")
                {
                    model.GatheringPlanDate = DateTime.Parse(row["GatheringPlanDate"].ToString());
                }
                if (row["CustomerID"] != null && row["CustomerID"].ToString() != "")
                {
                    model.CustomerID = int.Parse(row["CustomerID"].ToString());
                }
                if (row["InnerOrderNO"] != null)
                {
                    model.InnerOrderNO = row["InnerOrderNO"].ToString();
                }
                if (row["CustomerOrderNO"] != null)
                {
                    model.CustomerOrderNO = row["CustomerOrderNO"].ToString();
                }
                if (row["CustomerOrderDetailID"] != null && row["CustomerOrderDetailID"].ToString() != "")
                {
                    model.CustomerOrderDetailID = int.Parse(row["CustomerOrderDetailID"].ToString());
                }
                if (row["PaymentTypeID"] != null && row["PaymentTypeID"].ToString() != "")
                {
                    model.PaymentTypeID = int.Parse(row["PaymentTypeID"].ToString());
                }
                if (row["IsCustomerVATInvoice"] != null && row["IsCustomerVATInvoice"].ToString() != "")
                {
                    if ((row["IsCustomerVATInvoice"].ToString() == "1") || (row["IsCustomerVATInvoice"].ToString().ToLower() == "true"))
                    {
                        model.IsCustomerVATInvoice = true;
                    }
                    else
                    {
                        model.IsCustomerVATInvoice = false;
                    }
                }
                if (row["CustomerVATInvoiceNo"] != null)
                {
                    model.CustomerVATInvoiceNo = row["CustomerVATInvoiceNo"].ToString();
                }
                if (row["TrackingNumber"] != null)
                {
                    model.TrackingNumber = row["TrackingNumber"].ToString();
                }
                if (row["IsCustomerPay"] != null && row["IsCustomerPay"].ToString() != "")
                {
                    if ((row["IsCustomerPay"].ToString() == "1") || (row["IsCustomerPay"].ToString().ToLower() == "true"))
                    {
                        model.IsCustomerPay = true;
                    }
                    else
                    {
                        model.IsCustomerPay = false;
                    }
                }
                if (row["Qty"] != null && row["Qty"].ToString() != "")
                {
                    model.Qty = int.Parse(row["Qty"].ToString());
                }
                if (row["SaleStandardCurrency"] != null && row["SaleStandardCurrency"].ToString() != "")
                {
                    model.SaleStandardCurrency = int.Parse(row["SaleStandardCurrency"].ToString());
                }
                if (row["SalePrice"] != null && row["SalePrice"].ToString() != "")
                {
                    model.SalePrice = decimal.Parse(row["SalePrice"].ToString());
                }
                if (row["SaleTotal"] != null && row["SaleTotal"].ToString() != "")
                {
                    model.SaleTotal = decimal.Parse(row["SaleTotal"].ToString());
                }
                if (row["SaleRealCurrency"] != null && row["SaleRealCurrency"].ToString() != "")
                {
                    model.SaleRealCurrency = int.Parse(row["SaleRealCurrency"].ToString());
                }
                if (row["SaleExchangeRate"] != null && row["SaleExchangeRate"].ToString() != "")
                {
                    model.SaleExchangeRate = decimal.Parse(row["SaleExchangeRate"].ToString());
                }
                if (row["SaleRealPrice"] != null && row["SaleRealPrice"].ToString() != "")
                {
                    model.SaleRealPrice = decimal.Parse(row["SaleRealPrice"].ToString());
                }
                if (row["SaleRealTotal"] != null && row["SaleRealTotal"].ToString() != "")
                {
                    model.SaleRealTotal = decimal.Parse(row["SaleRealTotal"].ToString());
                }
                if (row["OtherFee"] != null && row["OtherFee"].ToString() != "")
                {
                    model.OtherFee = decimal.Parse(row["OtherFee"].ToString());
                }
                if (row["OtherFeeRemark"] != null)
                {
                    model.OtherFeeRemark = row["OtherFeeRemark"].ToString();
                }
                if (row["ChannelFee"] != null && row["ChannelFee"].ToString() != "")
                {
                    model.ChannelFee = decimal.Parse(row["ChannelFee"].ToString());
                }
                if (row["ChannelFeeRemark"] != null)
                {
                    model.ChannelFeeRemark = row["ChannelFeeRemark"].ToString();
                }
                if (row["LogisticsFee"] != null && row["LogisticsFee"].ToString() != "")
                {
                    model.LogisticsFee = decimal.Parse(row["LogisticsFee"].ToString());
                }
                if (row["LogisticsFeeRemark"] != null)
                {
                    model.LogisticsFeeRemark = row["LogisticsFeeRemark"].ToString();
                }
                if (row["OperatingFee"] != null && row["OperatingFee"].ToString() != "")
                {
                    model.OperatingFee = decimal.Parse(row["OperatingFee"].ToString());
                }
                if (row["OperatingFeeRemark"] != null)
                {
                    model.OperatingFeeRemark = row["OperatingFeeRemark"].ToString();
                }
                if (row["StandardIncomeRealDiff"] != null && row["StandardIncomeRealDiff"].ToString() != "")
                {
                    model.StandardIncomeRealDiff = decimal.Parse(row["StandardIncomeRealDiff"].ToString());
                }
                if (row["SalesMan"] != null)
                {
                    model.SalesMan = row["SalesMan"].ToString();
                }
                if (row["SalesManProportion"] != null && row["SalesManProportion"].ToString() != "")
                {
                    model.SalesManProportion = decimal.Parse(row["SalesManProportion"].ToString());
                }
                if (row["SalesManPay"] != null && row["SalesManPay"].ToString() != "")
                {
                    model.SalesManPay = decimal.Parse(row["SalesManPay"].ToString());
                }
                if (row["BuyerMan"] != null)
                {
                    model.BuyerMan = row["BuyerMan"].ToString();
                }
                if (row["BuyerProportion"] != null && row["BuyerProportion"].ToString() != "")
                {
                    model.BuyerProportion = decimal.Parse(row["BuyerProportion"].ToString());
                }
                if (row["BuyerPay"] != null && row["BuyerPay"].ToString() != "")
                {
                    model.BuyerPay = decimal.Parse(row["BuyerPay"].ToString());
                }
                if (row["POPrice"] != null && row["POPrice"].ToString() != "")
                {
                    model.POPrice = decimal.Parse(row["POPrice"].ToString());
                }
                if (row["GrossProfits"] != null && row["GrossProfits"].ToString() != "")
                {
                    model.GrossProfits = decimal.Parse(row["GrossProfits"].ToString());
                }
                if (row["ProfitMargin"] != null && row["ProfitMargin"].ToString() != "")
                {
                    model.ProfitMargin = decimal.Parse(row["ProfitMargin"].ToString());
                }
                if (row["NetProfit"] != null && row["NetProfit"].ToString() != "")
                {
                    model.NetProfit = decimal.Parse(row["NetProfit"].ToString());
                }
                if (row["NetProfitMargin"] != null && row["NetProfitMargin"].ToString() != "")
                {
                    model.NetProfitMargin = decimal.Parse(row["NetProfitMargin"].ToString());
                }
                if (row["RealNetProfit"] != null && row["RealNetProfit"].ToString() != "")
                {
                    model.RealNetProfit = decimal.Parse(row["RealNetProfit"].ToString());
                }
                if (row["FeeBackDate"] != null && row["FeeBackDate"].ToString() != "")
                {
                    model.FeeBackDate = DateTime.Parse(row["FeeBackDate"].ToString());
                }
                if (row["ProductCodes"] != null)
                {
                    model.ProductCodes = row["ProductCodes"].ToString();
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
            strSql.Append("select ID,CorporationID,GatheringPlanDate,CustomerID,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,ChannelFee,ChannelFeeRemark,LogisticsFee,LogisticsFeeRemark,OperatingFee,OperatingFeeRemark,StandardIncomeRealDiff,SalesMan,SalesManProportion,SalesManPay,BuyerMan,BuyerProportion,BuyerPay,POPrice,GrossProfits,ProfitMargin,NetProfit,NetProfitMargin,RealNetProfit,FeeBackDate,ProductCodes,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM GatheringPlan ");
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
            strSql.Append(" ID,CorporationID,GatheringPlanDate,CustomerID,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,ChannelFee,ChannelFeeRemark,LogisticsFee,LogisticsFeeRemark,OperatingFee,OperatingFeeRemark,StandardIncomeRealDiff,SalesMan,SalesManProportion,SalesManPay,BuyerMan,BuyerProportion,BuyerPay,POPrice,GrossProfits,ProfitMargin,NetProfit,NetProfitMargin,RealNetProfit,FeeBackDate,ProductCodes,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM GatheringPlan ");
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
            strSql.Append("select count(1) FROM GatheringPlan ");
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
            strSql.Append(")AS Row, T.*  from GatheringPlan T ");
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
            parameters[0].Value = "GatheringPlan";
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
        public DataSet GetGatheringPlanPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.GatheringPlan AS P WITH(NOLOCK) LEFT JOIN dbo.Customer AS S WITH(NOLOCK) ON S.ID=P.CustomerID LEFT JOIN dbo.Corporation AS C ON C.ID=P.CorporationID LEFT JOIN dbo.PaymentType AS T ON T.ID=P.PaymentTypeID LEFT JOIN dbo.CurrencyType AS CT ON CT.ID=P.SaleRealCurrency LEFT JOIN dbo.CurrencyType AS CT1 ON CT1.ID=P.SaleStandardCurrency  LEFT JOIN (SELECT  ObjId ,FileNum = COUNT(1) FROM dbo.Attachment WITH ( NOLOCK ) WHERE ObjType = 'GatheringPlanFilePath' AND AvailFlag = 1 GROUP BY ObjId) AS A ON A.ObjId=P.ID";
            //查询字段
            string strGetFields = " P.ID,State=CASE WHEN P.State=1 THEN '有效' ELSE '无效' END ,GatheringPlanDate=CONVERT(VARCHAR(10),P.GatheringPlanDate,120),FeeBackDate=CONVERT(VARCHAR(10),P.FeeBackDate,120),CorporationID=C.CompanyName,P.InnerOrderNO,P.CustomerOrderNO,S.CustomerName,IsCustomerPay=CASE WHEN P.IsCustomerPay=1 THEN '是' ELSE '否' END ,IsCustomerVATInvoice=CASE WHEN P.IsCustomerVATInvoice=1 THEN '是' ELSE '否' END ,P.Qty,PaymentTypeID=T.PaymentType,P.SaleExchangeRate,SaleRealCurrency=CT.CurrencyName,P.SaleRealPrice,P.SaleRealTotal,SaleStandardCurrency=CT1.CurrencyName,P.SalePrice,P.SaleTotal,P.OtherFee,P.CreateUser,CreateTime = CONVERT(VARCHAR(20), P.CreateTime, 120) , P.UpdateUser , UpdateTime = CONVERT(VARCHAR(20), P.UpdateTime, 120),FileNum=ISNULL(A.FileNum,0) ";
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

        /// <summary>
        /// 设置状态
        /// </summary>
        public bool SetValid(int iId, int iState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GatheringPlan set State=@State");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@State", SqlDbType.Int,4)
			};
            parameters[0].Value = iId;
            parameters[1].Value = iState;
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

