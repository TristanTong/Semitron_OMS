/**  
* OrderFeeDAL.cs
*
* 功 能： N/A
* 类 名： OrderFeeDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 11:12:29   童荣辉    初版
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
    /// 数据访问类:OrderFeeDAL
    /// </summary>
    public partial class OrderFeeDAL
    {
        public OrderFeeDAL()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.OrderFeeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderFee(");
            strSql.Append("CustomerOrderDetailID,POPlanID,TotalFeeCurrencyUnit,CustomerFeeIn,IncomeRate,IncomeStandardCurrency,StandardCustomerFeeIn,RealInCurrencyUnit,CustomerRealPayFee,StandardRealInCurrencyUnit,StandardCustomerRealPayFee,StandardIncomeRealDiff,GrossProfits,OtherFee,OtherFeeRemark,NetProfit,ProfitMargin,OtherRemark,IsCustomerVATInvoice,CustomerVATInvoiceNo,IsSupplierVATInvoice,SupplierVATInvoice,TraceNumber,FeeBackDate,SalesManProportion,SalesManPay,BuyerProportion,BuyerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@CustomerOrderDetailID,@POPlanID,@TotalFeeCurrencyUnit,@CustomerFeeIn,@IncomeRate,@IncomeStandardCurrency,@StandardCustomerFeeIn,@RealInCurrencyUnit,@CustomerRealPayFee,@StandardRealInCurrencyUnit,@StandardCustomerRealPayFee,@StandardIncomeRealDiff,@GrossProfits,@OtherFee,@OtherFeeRemark,@NetProfit,@ProfitMargin,@OtherRemark,@IsCustomerVATInvoice,@CustomerVATInvoiceNo,@IsSupplierVATInvoice,@SupplierVATInvoice,@TraceNumber,@FeeBackDate,@SalesManProportion,@SalesManPay,@BuyerProportion,@BuyerPay,@AvailFlag,@CreateUser,@CreateTime,@UpdateUser,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerOrderDetailID", SqlDbType.Int,4),
					new SqlParameter("@POPlanID", SqlDbType.Int,4),
					new SqlParameter("@TotalFeeCurrencyUnit", SqlDbType.VarChar,16),
					new SqlParameter("@CustomerFeeIn", SqlDbType.Decimal,9),
					new SqlParameter("@IncomeRate", SqlDbType.Decimal,9),
					new SqlParameter("@IncomeStandardCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@StandardCustomerFeeIn", SqlDbType.Decimal,9),
					new SqlParameter("@RealInCurrencyUnit", SqlDbType.VarChar,16),
					new SqlParameter("@CustomerRealPayFee", SqlDbType.Decimal,9),
					new SqlParameter("@StandardRealInCurrencyUnit", SqlDbType.VarChar,16),
					new SqlParameter("@StandardCustomerRealPayFee", SqlDbType.Decimal,9),
					new SqlParameter("@StandardIncomeRealDiff", SqlDbType.Decimal,9),
					new SqlParameter("@GrossProfits", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,256),
					new SqlParameter("@NetProfit", SqlDbType.Decimal,9),
					new SqlParameter("@ProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@OtherRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@IsCustomerVATInvoice", SqlDbType.Bit,1),
					new SqlParameter("@CustomerVATInvoiceNo", SqlDbType.NVarChar,128),
					new SqlParameter("@IsSupplierVATInvoice", SqlDbType.Bit,1),
					new SqlParameter("@SupplierVATInvoice", SqlDbType.NVarChar,128),
					new SqlParameter("@TraceNumber", SqlDbType.NVarChar,128),
					new SqlParameter("@FeeBackDate", SqlDbType.DateTime),
					new SqlParameter("@SalesManProportion", SqlDbType.Decimal,9),
					new SqlParameter("@SalesManPay", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerProportion", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerPay", SqlDbType.Decimal,9),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.CustomerOrderDetailID;
            parameters[1].Value = model.POPlanID;
            parameters[2].Value = model.TotalFeeCurrencyUnit;
            parameters[3].Value = model.CustomerFeeIn;
            parameters[4].Value = model.IncomeRate;
            parameters[5].Value = model.IncomeStandardCurrency;
            parameters[6].Value = model.StandardCustomerFeeIn;
            parameters[7].Value = model.RealInCurrencyUnit;
            parameters[8].Value = model.CustomerRealPayFee;
            parameters[9].Value = model.StandardRealInCurrencyUnit;
            parameters[10].Value = model.StandardCustomerRealPayFee;
            parameters[11].Value = model.StandardIncomeRealDiff;
            parameters[12].Value = model.GrossProfits;
            parameters[13].Value = model.OtherFee;
            parameters[14].Value = model.OtherFeeRemark;
            parameters[15].Value = model.NetProfit;
            parameters[16].Value = model.ProfitMargin;
            parameters[17].Value = model.OtherRemark;
            parameters[18].Value = model.IsCustomerVATInvoice;
            parameters[19].Value = model.CustomerVATInvoiceNo;
            parameters[20].Value = model.IsSupplierVATInvoice;
            parameters[21].Value = model.SupplierVATInvoice;
            parameters[22].Value = model.TraceNumber;
            parameters[23].Value = model.FeeBackDate;
            parameters[24].Value = model.SalesManProportion;
            parameters[25].Value = model.SalesManPay;
            parameters[26].Value = model.BuyerProportion;
            parameters[27].Value = model.BuyerPay;
            parameters[28].Value = model.AvailFlag;
            parameters[29].Value = model.CreateUser;
            parameters[30].Value = model.CreateTime;
            parameters[31].Value = model.UpdateUser;
            parameters[32].Value = model.UpdateTime;

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
        public bool Update(Semitron_OMS.Model.OMS.OrderFeeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderFee set ");
            strSql.Append("CustomerOrderDetailID=@CustomerOrderDetailID,");
            strSql.Append("POPlanID=@POPlanID,");
            strSql.Append("TotalFeeCurrencyUnit=@TotalFeeCurrencyUnit,");
            strSql.Append("CustomerFeeIn=@CustomerFeeIn,");
            strSql.Append("IncomeRate=@IncomeRate,");
            strSql.Append("IncomeStandardCurrency=@IncomeStandardCurrency,");
            strSql.Append("StandardCustomerFeeIn=@StandardCustomerFeeIn,");
            strSql.Append("RealInCurrencyUnit=@RealInCurrencyUnit,");
            strSql.Append("CustomerRealPayFee=@CustomerRealPayFee,");
            strSql.Append("StandardRealInCurrencyUnit=@StandardRealInCurrencyUnit,");
            strSql.Append("StandardCustomerRealPayFee=@StandardCustomerRealPayFee,");
            strSql.Append("StandardIncomeRealDiff=@StandardIncomeRealDiff,");
            strSql.Append("GrossProfits=@GrossProfits,");
            strSql.Append("OtherFee=@OtherFee,");
            strSql.Append("OtherFeeRemark=@OtherFeeRemark,");
            strSql.Append("NetProfit=@NetProfit,");
            strSql.Append("ProfitMargin=@ProfitMargin,");
            strSql.Append("OtherRemark=@OtherRemark,");
            strSql.Append("IsCustomerVATInvoice=@IsCustomerVATInvoice,");
            strSql.Append("CustomerVATInvoiceNo=@CustomerVATInvoiceNo,");
            strSql.Append("IsSupplierVATInvoice=@IsSupplierVATInvoice,");
            strSql.Append("SupplierVATInvoice=@SupplierVATInvoice,");
            strSql.Append("TraceNumber=@TraceNumber,");
            strSql.Append("FeeBackDate=@FeeBackDate,");
            strSql.Append("SalesManProportion=@SalesManProportion,");
            strSql.Append("SalesManPay=@SalesManPay,");
            strSql.Append("BuyerProportion=@BuyerProportion,");
            strSql.Append("BuyerPay=@BuyerPay,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerOrderDetailID", SqlDbType.Int,4),
					new SqlParameter("@POPlanID", SqlDbType.Int,4),
					new SqlParameter("@TotalFeeCurrencyUnit", SqlDbType.VarChar,16),
					new SqlParameter("@CustomerFeeIn", SqlDbType.Decimal,9),
					new SqlParameter("@IncomeRate", SqlDbType.Decimal,9),
					new SqlParameter("@IncomeStandardCurrency", SqlDbType.VarChar,16),
					new SqlParameter("@StandardCustomerFeeIn", SqlDbType.Decimal,9),
					new SqlParameter("@RealInCurrencyUnit", SqlDbType.VarChar,16),
					new SqlParameter("@CustomerRealPayFee", SqlDbType.Decimal,9),
					new SqlParameter("@StandardRealInCurrencyUnit", SqlDbType.VarChar,16),
					new SqlParameter("@StandardCustomerRealPayFee", SqlDbType.Decimal,9),
					new SqlParameter("@StandardIncomeRealDiff", SqlDbType.Decimal,9),
					new SqlParameter("@GrossProfits", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFee", SqlDbType.Decimal,9),
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,256),
					new SqlParameter("@NetProfit", SqlDbType.Decimal,9),
					new SqlParameter("@ProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@OtherRemark", SqlDbType.NVarChar,1024),
					new SqlParameter("@IsCustomerVATInvoice", SqlDbType.Bit,1),
					new SqlParameter("@CustomerVATInvoiceNo", SqlDbType.NVarChar,128),
					new SqlParameter("@IsSupplierVATInvoice", SqlDbType.Bit,1),
					new SqlParameter("@SupplierVATInvoice", SqlDbType.NVarChar,128),
					new SqlParameter("@TraceNumber", SqlDbType.NVarChar,128),
					new SqlParameter("@FeeBackDate", SqlDbType.DateTime),
					new SqlParameter("@SalesManProportion", SqlDbType.Decimal,9),
					new SqlParameter("@SalesManPay", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerProportion", SqlDbType.Decimal,9),
					new SqlParameter("@BuyerPay", SqlDbType.Decimal,9),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,16),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.CustomerOrderDetailID;
            parameters[1].Value = model.POPlanID;
            parameters[2].Value = model.TotalFeeCurrencyUnit;
            parameters[3].Value = model.CustomerFeeIn;
            parameters[4].Value = model.IncomeRate;
            parameters[5].Value = model.IncomeStandardCurrency;
            parameters[6].Value = model.StandardCustomerFeeIn;
            parameters[7].Value = model.RealInCurrencyUnit;
            parameters[8].Value = model.CustomerRealPayFee;
            parameters[9].Value = model.StandardRealInCurrencyUnit;
            parameters[10].Value = model.StandardCustomerRealPayFee;
            parameters[11].Value = model.StandardIncomeRealDiff;
            parameters[12].Value = model.GrossProfits;
            parameters[13].Value = model.OtherFee;
            parameters[14].Value = model.OtherFeeRemark;
            parameters[15].Value = model.NetProfit;
            parameters[16].Value = model.ProfitMargin;
            parameters[17].Value = model.OtherRemark;
            parameters[18].Value = model.IsCustomerVATInvoice;
            parameters[19].Value = model.CustomerVATInvoiceNo;
            parameters[20].Value = model.IsSupplierVATInvoice;
            parameters[21].Value = model.SupplierVATInvoice;
            parameters[22].Value = model.TraceNumber;
            parameters[23].Value = model.FeeBackDate;
            parameters[24].Value = model.SalesManProportion;
            parameters[25].Value = model.SalesManPay;
            parameters[26].Value = model.BuyerProportion;
            parameters[27].Value = model.BuyerPay;
            parameters[28].Value = model.AvailFlag;
            parameters[29].Value = model.CreateUser;
            parameters[30].Value = model.CreateTime;
            parameters[31].Value = model.UpdateUser;
            parameters[32].Value = model.UpdateTime;
            parameters[33].Value = model.ID;

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
            strSql.Append("delete from OrderFee ");
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
            strSql.Append("delete from OrderFee ");
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
        public Semitron_OMS.Model.OMS.OrderFeeModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CustomerOrderDetailID,POPlanID,TotalFeeCurrencyUnit,CustomerFeeIn,IncomeRate,IncomeStandardCurrency,StandardCustomerFeeIn,RealInCurrencyUnit,CustomerRealPayFee,StandardRealInCurrencyUnit,StandardCustomerRealPayFee,StandardIncomeRealDiff,GrossProfits,OtherFee,OtherFeeRemark,NetProfit,ProfitMargin,OtherRemark,IsCustomerVATInvoice,CustomerVATInvoiceNo,IsSupplierVATInvoice,SupplierVATInvoice,TraceNumber,FeeBackDate,SalesManProportion,SalesManPay,BuyerProportion,BuyerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime from OrderFee ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.OrderFeeModel model = new Semitron_OMS.Model.OMS.OrderFeeModel();
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
        public Semitron_OMS.Model.OMS.OrderFeeModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.OrderFeeModel model = new Semitron_OMS.Model.OMS.OrderFeeModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["CustomerOrderDetailID"] != null && row["CustomerOrderDetailID"].ToString() != "")
                {
                    model.CustomerOrderDetailID = int.Parse(row["CustomerOrderDetailID"].ToString());
                }
                if (row["POPlanID"] != null && row["POPlanID"].ToString() != "")
                {
                    model.POPlanID = int.Parse(row["POPlanID"].ToString());
                }
                if (row["TotalFeeCurrencyUnit"] != null)
                {
                    model.TotalFeeCurrencyUnit = row["TotalFeeCurrencyUnit"].ToString();
                }
                if (row["CustomerFeeIn"] != null && row["CustomerFeeIn"].ToString() != "")
                {
                    model.CustomerFeeIn = decimal.Parse(row["CustomerFeeIn"].ToString());
                }
                if (row["IncomeRate"] != null && row["IncomeRate"].ToString() != "")
                {
                    model.IncomeRate = decimal.Parse(row["IncomeRate"].ToString());
                }
                if (row["IncomeStandardCurrency"] != null)
                {
                    model.IncomeStandardCurrency = row["IncomeStandardCurrency"].ToString();
                }
                if (row["StandardCustomerFeeIn"] != null && row["StandardCustomerFeeIn"].ToString() != "")
                {
                    model.StandardCustomerFeeIn = decimal.Parse(row["StandardCustomerFeeIn"].ToString());
                }
                if (row["RealInCurrencyUnit"] != null)
                {
                    model.RealInCurrencyUnit = row["RealInCurrencyUnit"].ToString();
                }
                if (row["CustomerRealPayFee"] != null && row["CustomerRealPayFee"].ToString() != "")
                {
                    model.CustomerRealPayFee = decimal.Parse(row["CustomerRealPayFee"].ToString());
                }
                if (row["StandardRealInCurrencyUnit"] != null)
                {
                    model.StandardRealInCurrencyUnit = row["StandardRealInCurrencyUnit"].ToString();
                }
                if (row["StandardCustomerRealPayFee"] != null && row["StandardCustomerRealPayFee"].ToString() != "")
                {
                    model.StandardCustomerRealPayFee = decimal.Parse(row["StandardCustomerRealPayFee"].ToString());
                }
                if (row["StandardIncomeRealDiff"] != null && row["StandardIncomeRealDiff"].ToString() != "")
                {
                    model.StandardIncomeRealDiff = decimal.Parse(row["StandardIncomeRealDiff"].ToString());
                }
                if (row["GrossProfits"] != null && row["GrossProfits"].ToString() != "")
                {
                    model.GrossProfits = decimal.Parse(row["GrossProfits"].ToString());
                }
                if (row["OtherFee"] != null && row["OtherFee"].ToString() != "")
                {
                    model.OtherFee = decimal.Parse(row["OtherFee"].ToString());
                }
                if (row["OtherFeeRemark"] != null)
                {
                    model.OtherFeeRemark = row["OtherFeeRemark"].ToString();
                }
                if (row["NetProfit"] != null && row["NetProfit"].ToString() != "")
                {
                    model.NetProfit = decimal.Parse(row["NetProfit"].ToString());
                }
                if (row["ProfitMargin"] != null && row["ProfitMargin"].ToString() != "")
                {
                    model.ProfitMargin = decimal.Parse(row["ProfitMargin"].ToString());
                }
                if (row["OtherRemark"] != null)
                {
                    model.OtherRemark = row["OtherRemark"].ToString();
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
                if (row["IsSupplierVATInvoice"] != null && row["IsSupplierVATInvoice"].ToString() != "")
                {
                    if ((row["IsSupplierVATInvoice"].ToString() == "1") || (row["IsSupplierVATInvoice"].ToString().ToLower() == "true"))
                    {
                        model.IsSupplierVATInvoice = true;
                    }
                    else
                    {
                        model.IsSupplierVATInvoice = false;
                    }
                }
                if (row["SupplierVATInvoice"] != null)
                {
                    model.SupplierVATInvoice = row["SupplierVATInvoice"].ToString();
                }
                if (row["TraceNumber"] != null)
                {
                    model.TraceNumber = row["TraceNumber"].ToString();
                }
                if (row["FeeBackDate"] != null && row["FeeBackDate"].ToString() != "")
                {
                    model.FeeBackDate = DateTime.Parse(row["FeeBackDate"].ToString());
                }
                if (row["SalesManProportion"] != null && row["SalesManProportion"].ToString() != "")
                {
                    model.SalesManProportion = decimal.Parse(row["SalesManProportion"].ToString());
                }
                if (row["SalesManPay"] != null && row["SalesManPay"].ToString() != "")
                {
                    model.SalesManPay = decimal.Parse(row["SalesManPay"].ToString());
                }
                if (row["BuyerProportion"] != null && row["BuyerProportion"].ToString() != "")
                {
                    model.BuyerProportion = decimal.Parse(row["BuyerProportion"].ToString());
                }
                if (row["BuyerPay"] != null && row["BuyerPay"].ToString() != "")
                {
                    model.BuyerPay = decimal.Parse(row["BuyerPay"].ToString());
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
            strSql.Append("select ID,CustomerOrderDetailID,POPlanID,TotalFeeCurrencyUnit,CustomerFeeIn,IncomeRate,IncomeStandardCurrency,StandardCustomerFeeIn,RealInCurrencyUnit,CustomerRealPayFee,StandardRealInCurrencyUnit,StandardCustomerRealPayFee,StandardIncomeRealDiff,GrossProfits,OtherFee,OtherFeeRemark,NetProfit,ProfitMargin,OtherRemark,IsCustomerVATInvoice,CustomerVATInvoiceNo,IsSupplierVATInvoice,SupplierVATInvoice,TraceNumber,FeeBackDate,SalesManProportion,SalesManPay,BuyerProportion,BuyerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM OrderFee ");
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
            strSql.Append(" ID,CustomerOrderDetailID,POPlanID,TotalFeeCurrencyUnit,CustomerFeeIn,IncomeRate,IncomeStandardCurrency,StandardCustomerFeeIn,RealInCurrencyUnit,CustomerRealPayFee,StandardRealInCurrencyUnit,StandardCustomerRealPayFee,StandardIncomeRealDiff,GrossProfits,OtherFee,OtherFeeRemark,NetProfit,ProfitMargin,OtherRemark,IsCustomerVATInvoice,CustomerVATInvoiceNo,IsSupplierVATInvoice,SupplierVATInvoice,TraceNumber,FeeBackDate,SalesManProportion,SalesManPay,BuyerProportion,BuyerPay,AvailFlag,CreateUser,CreateTime,UpdateUser,UpdateTime ");
            strSql.Append(" FROM OrderFee ");
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
            strSql.Append("select count(1) FROM OrderFee ");
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
            strSql.Append(")AS Row, T.*  from OrderFee T ");
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
            parameters[0].Value = "OrderFee";
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
        /// 分页查询订单费用明细记录数据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>采购订单记录数据</returns>
        public DataSet GetOrderFeePageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " OrderFee AS O LEFT JOIN dbo.POPlan AS P ON P.ID = O.POPlanID LEFT JOIN CustomerOrderDetail AS C ON C.ID = O.CustomerOrderDetailID LEFT JOIN dbo.CustomerOrder AS CO ON CO.InnerOrderNO=C.InnerOrderNO LEFT JOIN Corporation AS COR ON COR.ID=CO.CorporationID  LEFT JOIN (SELECT  ObjId ,FileNum = COUNT(1) FROM dbo.Attachment WITH ( NOLOCK ) WHERE ObjType = 'OrderFee' AND AvailFlag = 1 GROUP BY ObjId) AS A ON A.ObjId=O.ID";
            //查询字段
            string strGetFields = " O.ID ,COR.CompanyName,IsCustomerPay=CASE WHEN C.IsCustomerPay=1 THEN '是' ELSE '否' END,IsCustomerVATInvoice=CASE WHEN O.IsCustomerVATInvoice=1 THEN '是' ELSE '否' END,CustomerName=(SELECT TOP 1 CustomerName FROM dbo.Customer WHERE ID=CO.CustomerID),C.InnerOrderNO ,CO.CustomerOrderNO,C.CPN,C.CustQuantity,IsPaySupplier=CASE WHEN P.IsPaySupplier=1 THEN '是' ELSE '否' END,IsSupplierVATInvoice=CASE WHEN O.IsSupplierVATInvoice=1 THEN '是' ELSE '否' END,SupplierName=(SELECT TOP 1 SupplierName FROM dbo.Supplier WHERE ID=P.SupplierID),P.PONO ,P.MPN,P.POQuantity,TotalFeeCurrencyUnit=(SELECT TOP 1 ShortName FROM CurrencyType WHERE CurrencyType.ID=TotalFeeCurrencyUnit) ,CustomerFeeIn ,IncomeRate ,IncomeStandardCurrency=(SELECT TOP 1 ShortName FROM CurrencyType WHERE CurrencyType.ID=IncomeStandardCurrency) ,StandardCustomerFeeIn ,RealInCurrencyUnit=(SELECT TOP 1 ShortName FROM CurrencyType WHERE CurrencyType.ID=RealInCurrencyUnit) ,CustomerRealPayFee ,StandardRealInCurrencyUnit=(SELECT TOP 1 ShortName FROM CurrencyType WHERE CurrencyType.ID=StandardRealInCurrencyUnit) ,StandardCustomerRealPayFee ,StandardIncomeRealDiff ,GrossProfits , O.OtherFee ,NetProfit ,ProfitMargin ,AvailFlag = ( CASE O.AvailFlag WHEN 1 THEN '有效' ELSE '无效' END ),FeeBackDate = CONVERT(VARCHAR(10), O.FeeBackDate, 120),CustomerInStockDate = CONVERT(VARCHAR(10), C.CustomerInStockDate, 120) ,CreateTime = CONVERT(VARCHAR(20), O.CreateTime, 120) ,UpdateTime = CONVERT(VARCHAR(20), O.UpdateTime, 120),FileNum=ISNULL(A.FileNum,0) ";
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
        #endregion  ExtensionMethod
    }
}

