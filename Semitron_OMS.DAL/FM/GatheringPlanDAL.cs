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
using Semitron_OMS.DBUtility;//Please add references
namespace Semitron_OMS.DAL.FM
{
	/// <summary>
	/// 数据访问类:GatheringPlanDAL
	/// </summary>
	public partial class GatheringPlanDAL
	{
		public GatheringPlanDAL()
		{}
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from GatheringPlan");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Semitron_OMS.Model.FM.GatheringPlanModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GatheringPlan(");
			strSql.Append("CorporationID,GatheringPlanDate,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,FeeBackDate,SalesManProportion,SalesManPay,State,CreateTime,CreateUser,UpdateTime,UpdateUser)");
			strSql.Append(" values (");
			strSql.Append("@CorporationID,@GatheringPlanDate,@InnerOrderNO,@CustomerOrderNO,@CustomerOrderDetailID,@PaymentTypeID,@IsCustomerVATInvoice,@CustomerVATInvoiceNo,@TrackingNumber,@IsCustomerPay,@Qty,@SaleStandardCurrency,@SalePrice,@SaleTotal,@SaleRealCurrency,@SaleExchangeRate,@SaleRealPrice,@SaleRealTotal,@OtherFee,@OtherFeeRemark,@FeeBackDate,@SalesManProportion,@SalesManPay,@State,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@GatheringPlanDate", SqlDbType.DateTime),
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
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,512),
					new SqlParameter("@FeeBackDate", SqlDbType.DateTime),
					new SqlParameter("@SalesManProportion", SqlDbType.Decimal,9),
					new SqlParameter("@SalesManPay", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CorporationID;
			parameters[1].Value = model.GatheringPlanDate;
			parameters[2].Value = model.InnerOrderNO;
			parameters[3].Value = model.CustomerOrderNO;
			parameters[4].Value = model.CustomerOrderDetailID;
			parameters[5].Value = model.PaymentTypeID;
			parameters[6].Value = model.IsCustomerVATInvoice;
			parameters[7].Value = model.CustomerVATInvoiceNo;
			parameters[8].Value = model.TrackingNumber;
			parameters[9].Value = model.IsCustomerPay;
			parameters[10].Value = model.Qty;
			parameters[11].Value = model.SaleStandardCurrency;
			parameters[12].Value = model.SalePrice;
			parameters[13].Value = model.SaleTotal;
			parameters[14].Value = model.SaleRealCurrency;
			parameters[15].Value = model.SaleExchangeRate;
			parameters[16].Value = model.SaleRealPrice;
			parameters[17].Value = model.SaleRealTotal;
			parameters[18].Value = model.OtherFee;
			parameters[19].Value = model.OtherFeeRemark;
			parameters[20].Value = model.FeeBackDate;
			parameters[21].Value = model.SalesManProportion;
			parameters[22].Value = model.SalesManPay;
			parameters[23].Value = model.State;
			parameters[24].Value = model.CreateTime;
			parameters[25].Value = model.CreateUser;
			parameters[26].Value = model.UpdateTime;
			parameters[27].Value = model.UpdateUser;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GatheringPlan set ");
			strSql.Append("CorporationID=@CorporationID,");
			strSql.Append("GatheringPlanDate=@GatheringPlanDate,");
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
			strSql.Append("FeeBackDate=@FeeBackDate,");
			strSql.Append("SalesManProportion=@SalesManProportion,");
			strSql.Append("SalesManPay=@SalesManPay,");
			strSql.Append("State=@State,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUser=@UpdateUser");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CorporationID", SqlDbType.Int,4),
					new SqlParameter("@GatheringPlanDate", SqlDbType.DateTime),
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
					new SqlParameter("@OtherFeeRemark", SqlDbType.NVarChar,512),
					new SqlParameter("@FeeBackDate", SqlDbType.DateTime),
					new SqlParameter("@SalesManProportion", SqlDbType.Decimal,9),
					new SqlParameter("@SalesManPay", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.CorporationID;
			parameters[1].Value = model.GatheringPlanDate;
			parameters[2].Value = model.InnerOrderNO;
			parameters[3].Value = model.CustomerOrderNO;
			parameters[4].Value = model.CustomerOrderDetailID;
			parameters[5].Value = model.PaymentTypeID;
			parameters[6].Value = model.IsCustomerVATInvoice;
			parameters[7].Value = model.CustomerVATInvoiceNo;
			parameters[8].Value = model.TrackingNumber;
			parameters[9].Value = model.IsCustomerPay;
			parameters[10].Value = model.Qty;
			parameters[11].Value = model.SaleStandardCurrency;
			parameters[12].Value = model.SalePrice;
			parameters[13].Value = model.SaleTotal;
			parameters[14].Value = model.SaleRealCurrency;
			parameters[15].Value = model.SaleExchangeRate;
			parameters[16].Value = model.SaleRealPrice;
			parameters[17].Value = model.SaleRealTotal;
			parameters[18].Value = model.OtherFee;
			parameters[19].Value = model.OtherFeeRemark;
			parameters[20].Value = model.FeeBackDate;
			parameters[21].Value = model.SalesManProportion;
			parameters[22].Value = model.SalesManPay;
			parameters[23].Value = model.State;
			parameters[24].Value = model.CreateTime;
			parameters[25].Value = model.CreateUser;
			parameters[26].Value = model.UpdateTime;
			parameters[27].Value = model.UpdateUser;
			parameters[28].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GatheringPlan ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GatheringPlan ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CorporationID,GatheringPlanDate,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,FeeBackDate,SalesManProportion,SalesManPay,State,CreateTime,CreateUser,UpdateTime,UpdateUser from GatheringPlan ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Semitron_OMS.Model.FM.GatheringPlanModel model=new Semitron_OMS.Model.FM.GatheringPlanModel();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
			Semitron_OMS.Model.FM.GatheringPlanModel model=new Semitron_OMS.Model.FM.GatheringPlanModel();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CorporationID"]!=null && row["CorporationID"].ToString()!="")
				{
					model.CorporationID=int.Parse(row["CorporationID"].ToString());
				}
				if(row["GatheringPlanDate"]!=null && row["GatheringPlanDate"].ToString()!="")
				{
					model.GatheringPlanDate=DateTime.Parse(row["GatheringPlanDate"].ToString());
				}
				if(row["InnerOrderNO"]!=null)
				{
					model.InnerOrderNO=row["InnerOrderNO"].ToString();
				}
				if(row["CustomerOrderNO"]!=null)
				{
					model.CustomerOrderNO=row["CustomerOrderNO"].ToString();
				}
				if(row["CustomerOrderDetailID"]!=null && row["CustomerOrderDetailID"].ToString()!="")
				{
					model.CustomerOrderDetailID=int.Parse(row["CustomerOrderDetailID"].ToString());
				}
				if(row["PaymentTypeID"]!=null && row["PaymentTypeID"].ToString()!="")
				{
					model.PaymentTypeID=int.Parse(row["PaymentTypeID"].ToString());
				}
				if(row["IsCustomerVATInvoice"]!=null && row["IsCustomerVATInvoice"].ToString()!="")
				{
					if((row["IsCustomerVATInvoice"].ToString()=="1")||(row["IsCustomerVATInvoice"].ToString().ToLower()=="true"))
					{
						model.IsCustomerVATInvoice=true;
					}
					else
					{
						model.IsCustomerVATInvoice=false;
					}
				}
				if(row["CustomerVATInvoiceNo"]!=null)
				{
					model.CustomerVATInvoiceNo=row["CustomerVATInvoiceNo"].ToString();
				}
				if(row["TrackingNumber"]!=null)
				{
					model.TrackingNumber=row["TrackingNumber"].ToString();
				}
				if(row["IsCustomerPay"]!=null && row["IsCustomerPay"].ToString()!="")
				{
					if((row["IsCustomerPay"].ToString()=="1")||(row["IsCustomerPay"].ToString().ToLower()=="true"))
					{
						model.IsCustomerPay=true;
					}
					else
					{
						model.IsCustomerPay=false;
					}
				}
				if(row["Qty"]!=null && row["Qty"].ToString()!="")
				{
					model.Qty=int.Parse(row["Qty"].ToString());
				}
				if(row["SaleStandardCurrency"]!=null && row["SaleStandardCurrency"].ToString()!="")
				{
					model.SaleStandardCurrency=int.Parse(row["SaleStandardCurrency"].ToString());
				}
				if(row["SalePrice"]!=null && row["SalePrice"].ToString()!="")
				{
					model.SalePrice=decimal.Parse(row["SalePrice"].ToString());
				}
				if(row["SaleTotal"]!=null && row["SaleTotal"].ToString()!="")
				{
					model.SaleTotal=decimal.Parse(row["SaleTotal"].ToString());
				}
				if(row["SaleRealCurrency"]!=null && row["SaleRealCurrency"].ToString()!="")
				{
					model.SaleRealCurrency=int.Parse(row["SaleRealCurrency"].ToString());
				}
				if(row["SaleExchangeRate"]!=null && row["SaleExchangeRate"].ToString()!="")
				{
					model.SaleExchangeRate=decimal.Parse(row["SaleExchangeRate"].ToString());
				}
				if(row["SaleRealPrice"]!=null && row["SaleRealPrice"].ToString()!="")
				{
					model.SaleRealPrice=decimal.Parse(row["SaleRealPrice"].ToString());
				}
				if(row["SaleRealTotal"]!=null && row["SaleRealTotal"].ToString()!="")
				{
					model.SaleRealTotal=decimal.Parse(row["SaleRealTotal"].ToString());
				}
				if(row["OtherFee"]!=null && row["OtherFee"].ToString()!="")
				{
					model.OtherFee=decimal.Parse(row["OtherFee"].ToString());
				}
				if(row["OtherFeeRemark"]!=null)
				{
					model.OtherFeeRemark=row["OtherFeeRemark"].ToString();
				}
				if(row["FeeBackDate"]!=null && row["FeeBackDate"].ToString()!="")
				{
					model.FeeBackDate=DateTime.Parse(row["FeeBackDate"].ToString());
				}
				if(row["SalesManProportion"]!=null && row["SalesManProportion"].ToString()!="")
				{
					model.SalesManProportion=decimal.Parse(row["SalesManProportion"].ToString());
				}
				if(row["SalesManPay"]!=null && row["SalesManPay"].ToString()!="")
				{
					model.SalesManPay=decimal.Parse(row["SalesManPay"].ToString());
				}
				if(row["State"]!=null && row["State"].ToString()!="")
				{
					model.State=int.Parse(row["State"].ToString());
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["CreateUser"]!=null)
				{
					model.CreateUser=row["CreateUser"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["UpdateUser"]!=null)
				{
					model.UpdateUser=row["UpdateUser"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CorporationID,GatheringPlanDate,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,FeeBackDate,SalesManProportion,SalesManPay,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM GatheringPlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,CorporationID,GatheringPlanDate,InnerOrderNO,CustomerOrderNO,CustomerOrderDetailID,PaymentTypeID,IsCustomerVATInvoice,CustomerVATInvoiceNo,TrackingNumber,IsCustomerPay,Qty,SaleStandardCurrency,SalePrice,SaleTotal,SaleRealCurrency,SaleExchangeRate,SaleRealPrice,SaleRealTotal,OtherFee,OtherFeeRemark,FeeBackDate,SalesManProportion,SalesManPay,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM GatheringPlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM GatheringPlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
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

		#endregion  ExtensionMethod
	}
}

