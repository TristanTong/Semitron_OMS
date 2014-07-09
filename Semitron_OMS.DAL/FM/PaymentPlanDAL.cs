/**  
* PaymentPlanDAL.cs
*
* 功 能： N/A
* 类 名： PaymentPlanDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:57   童荣辉    初版
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
	/// 数据访问类:PaymentPlanDAL
	/// </summary>
	public partial class PaymentPlanDAL
	{
		public PaymentPlanDAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Semitron_OMS.Model.FM.PaymentPlanModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PaymentPlan(");
			strSql.Append("EntryNo,PaymentPlanDate,ProductCode,MPN,SupplierID,Qty,Price,TotalPayment,State,CreateTime,CreateUser,UpdateTime,UpdateUser)");
			strSql.Append(" values (");
			strSql.Append("@EntryNo,@PaymentPlanDate,@ProductCode,@MPN,@SupplierID,@Qty,@Price,@TotalPayment,@State,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@EntryNo", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentPlanDate", SqlDbType.DateTime),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@MPN", SqlDbType.NVarChar,50),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@Qty", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPayment", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.EntryNo;
			parameters[1].Value = model.PaymentPlanDate;
			parameters[2].Value = model.ProductCode;
			parameters[3].Value = model.MPN;
			parameters[4].Value = model.SupplierID;
			parameters[5].Value = model.Qty;
			parameters[6].Value = model.Price;
			parameters[7].Value = model.TotalPayment;
			parameters[8].Value = model.State;
			parameters[9].Value = model.CreateTime;
			parameters[10].Value = model.CreateUser;
			parameters[11].Value = model.UpdateTime;
			parameters[12].Value = model.UpdateUser;

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
		public bool Update(Semitron_OMS.Model.FM.PaymentPlanModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PaymentPlan set ");
			strSql.Append("EntryNo=@EntryNo,");
			strSql.Append("PaymentPlanDate=@PaymentPlanDate,");
			strSql.Append("ProductCode=@ProductCode,");
			strSql.Append("MPN=@MPN,");
			strSql.Append("SupplierID=@SupplierID,");
			strSql.Append("Qty=@Qty,");
			strSql.Append("Price=@Price,");
			strSql.Append("TotalPayment=@TotalPayment,");
			strSql.Append("State=@State,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUser=@UpdateUser");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@EntryNo", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentPlanDate", SqlDbType.DateTime),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@MPN", SqlDbType.NVarChar,50),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@Qty", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPayment", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.EntryNo;
			parameters[1].Value = model.PaymentPlanDate;
			parameters[2].Value = model.ProductCode;
			parameters[3].Value = model.MPN;
			parameters[4].Value = model.SupplierID;
			parameters[5].Value = model.Qty;
			parameters[6].Value = model.Price;
			parameters[7].Value = model.TotalPayment;
			parameters[8].Value = model.State;
			parameters[9].Value = model.CreateTime;
			parameters[10].Value = model.CreateUser;
			parameters[11].Value = model.UpdateTime;
			parameters[12].Value = model.UpdateUser;
			parameters[13].Value = model.ID;

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
			strSql.Append("delete from PaymentPlan ");
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
			strSql.Append("delete from PaymentPlan ");
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
		public Semitron_OMS.Model.FM.PaymentPlanModel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,EntryNo,PaymentPlanDate,ProductCode,MPN,SupplierID,Qty,Price,TotalPayment,State,CreateTime,CreateUser,UpdateTime,UpdateUser from PaymentPlan ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Semitron_OMS.Model.FM.PaymentPlanModel model=new Semitron_OMS.Model.FM.PaymentPlanModel();
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
		public Semitron_OMS.Model.FM.PaymentPlanModel DataRowToModel(DataRow row)
		{
			Semitron_OMS.Model.FM.PaymentPlanModel model=new Semitron_OMS.Model.FM.PaymentPlanModel();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["EntryNo"]!=null)
				{
					model.EntryNo=row["EntryNo"].ToString();
				}
				if(row["PaymentPlanDate"]!=null && row["PaymentPlanDate"].ToString()!="")
				{
					model.PaymentPlanDate=DateTime.Parse(row["PaymentPlanDate"].ToString());
				}
				if(row["ProductCode"]!=null)
				{
					model.ProductCode=row["ProductCode"].ToString();
				}
				if(row["MPN"]!=null)
				{
					model.MPN=row["MPN"].ToString();
				}
				if(row["SupplierID"]!=null && row["SupplierID"].ToString()!="")
				{
					model.SupplierID=int.Parse(row["SupplierID"].ToString());
				}
				if(row["Qty"]!=null && row["Qty"].ToString()!="")
				{
					model.Qty=int.Parse(row["Qty"].ToString());
				}
				if(row["Price"]!=null && row["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(row["Price"].ToString());
				}
				if(row["TotalPayment"]!=null && row["TotalPayment"].ToString()!="")
				{
					model.TotalPayment=decimal.Parse(row["TotalPayment"].ToString());
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
			strSql.Append("select ID,EntryNo,PaymentPlanDate,ProductCode,MPN,SupplierID,Qty,Price,TotalPayment,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM PaymentPlan ");
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
			strSql.Append(" ID,EntryNo,PaymentPlanDate,ProductCode,MPN,SupplierID,Qty,Price,TotalPayment,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM PaymentPlan ");
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
			strSql.Append("select count(1) FROM PaymentPlan ");
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
			strSql.Append(")AS Row, T.*  from PaymentPlan T ");
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
			parameters[0].Value = "PaymentPlan";
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

