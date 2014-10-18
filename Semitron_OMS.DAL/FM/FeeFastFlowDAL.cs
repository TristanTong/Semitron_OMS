/**  
* FeeFastFlowDAL.cs
*
* 功 能： N/A
* 类 名： FeeFastFlowDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/10/5 23:43:13   童荣辉    初版
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
	/// 数据访问类:FeeFastFlowDAL
	/// </summary>
	public partial class FeeFastFlowDAL
	{
		public FeeFastFlowDAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Semitron_OMS.Model.FM.FeeFastFlowModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FeeFastFlow(");
			strSql.Append("PayTypeValue,BankAccountID,FeeTypeValue,CurrencyTypeID,Fee,Increase,Decrease,AccountBalance,CustomerID,SupplierID,NetProfitMargin,Remark,ByHandUserID,OccurTime,IsFollowAction,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
			strSql.Append(" values (");
			strSql.Append("@PayTypeValue,@BankAccountID,@FeeTypeValue,@CurrencyTypeID,@Fee,@Increase,@Decrease,@AccountBalance,@CustomerID,@SupplierID,@NetProfitMargin,@Remark,@ByHandUserID,@OccurTime,@IsFollowAction,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PayTypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountID", SqlDbType.Int,4),
					new SqlParameter("@FeeTypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@CurrencyTypeID", SqlDbType.Int,4),
					new SqlParameter("@Fee", SqlDbType.Decimal,9),
					new SqlParameter("@Increase", SqlDbType.Decimal,9),
					new SqlParameter("@Decrease", SqlDbType.Decimal,9),
					new SqlParameter("@AccountBalance", SqlDbType.Decimal,9),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@NetProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1024),
					new SqlParameter("@ByHandUserID", SqlDbType.Int,4),
					new SqlParameter("@OccurTime", SqlDbType.DateTime),
					new SqlParameter("@IsFollowAction", SqlDbType.Bit,1),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.PayTypeValue;
			parameters[1].Value = model.BankAccountID;
			parameters[2].Value = model.FeeTypeValue;
			parameters[3].Value = model.CurrencyTypeID;
			parameters[4].Value = model.Fee;
			parameters[5].Value = model.Increase;
			parameters[6].Value = model.Decrease;
			parameters[7].Value = model.AccountBalance;
			parameters[8].Value = model.CustomerID;
			parameters[9].Value = model.SupplierID;
			parameters[10].Value = model.NetProfitMargin;
			parameters[11].Value = model.Remark;
			parameters[12].Value = model.ByHandUserID;
			parameters[13].Value = model.OccurTime;
			parameters[14].Value = model.IsFollowAction;
			parameters[15].Value = model.AvailFlag;
			parameters[16].Value = model.CreateTime;
			parameters[17].Value = model.CreateUser;
			parameters[18].Value = model.UpdateTime;
			parameters[19].Value = model.UpdateUser;

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
		public bool Update(Semitron_OMS.Model.FM.FeeFastFlowModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FeeFastFlow set ");
			strSql.Append("PayTypeValue=@PayTypeValue,");
			strSql.Append("BankAccountID=@BankAccountID,");
			strSql.Append("FeeTypeValue=@FeeTypeValue,");
			strSql.Append("CurrencyTypeID=@CurrencyTypeID,");
			strSql.Append("Fee=@Fee,");
			strSql.Append("Increase=@Increase,");
			strSql.Append("Decrease=@Decrease,");
			strSql.Append("AccountBalance=@AccountBalance,");
			strSql.Append("CustomerID=@CustomerID,");
			strSql.Append("SupplierID=@SupplierID,");
			strSql.Append("NetProfitMargin=@NetProfitMargin,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("ByHandUserID=@ByHandUserID,");
			strSql.Append("OccurTime=@OccurTime,");
			strSql.Append("IsFollowAction=@IsFollowAction,");
			strSql.Append("AvailFlag=@AvailFlag,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUser=@UpdateUser");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@PayTypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccountID", SqlDbType.Int,4),
					new SqlParameter("@FeeTypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@CurrencyTypeID", SqlDbType.Int,4),
					new SqlParameter("@Fee", SqlDbType.Decimal,9),
					new SqlParameter("@Increase", SqlDbType.Decimal,9),
					new SqlParameter("@Decrease", SqlDbType.Decimal,9),
					new SqlParameter("@AccountBalance", SqlDbType.Decimal,9),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@NetProfitMargin", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1024),
					new SqlParameter("@ByHandUserID", SqlDbType.Int,4),
					new SqlParameter("@OccurTime", SqlDbType.DateTime),
					new SqlParameter("@IsFollowAction", SqlDbType.Bit,1),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.PayTypeValue;
			parameters[1].Value = model.BankAccountID;
			parameters[2].Value = model.FeeTypeValue;
			parameters[3].Value = model.CurrencyTypeID;
			parameters[4].Value = model.Fee;
			parameters[5].Value = model.Increase;
			parameters[6].Value = model.Decrease;
			parameters[7].Value = model.AccountBalance;
			parameters[8].Value = model.CustomerID;
			parameters[9].Value = model.SupplierID;
			parameters[10].Value = model.NetProfitMargin;
			parameters[11].Value = model.Remark;
			parameters[12].Value = model.ByHandUserID;
			parameters[13].Value = model.OccurTime;
			parameters[14].Value = model.IsFollowAction;
			parameters[15].Value = model.AvailFlag;
			parameters[16].Value = model.CreateTime;
			parameters[17].Value = model.CreateUser;
			parameters[18].Value = model.UpdateTime;
			parameters[19].Value = model.UpdateUser;
			parameters[20].Value = model.ID;

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
			strSql.Append("delete from FeeFastFlow ");
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
			strSql.Append("delete from FeeFastFlow ");
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
		public Semitron_OMS.Model.FM.FeeFastFlowModel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,PayTypeValue,BankAccountID,FeeTypeValue,CurrencyTypeID,Fee,Increase,Decrease,AccountBalance,CustomerID,SupplierID,NetProfitMargin,Remark,ByHandUserID,OccurTime,IsFollowAction,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser from FeeFastFlow ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Semitron_OMS.Model.FM.FeeFastFlowModel model=new Semitron_OMS.Model.FM.FeeFastFlowModel();
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
		public Semitron_OMS.Model.FM.FeeFastFlowModel DataRowToModel(DataRow row)
		{
			Semitron_OMS.Model.FM.FeeFastFlowModel model=new Semitron_OMS.Model.FM.FeeFastFlowModel();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["PayTypeValue"]!=null)
				{
					model.PayTypeValue=row["PayTypeValue"].ToString();
				}
				if(row["BankAccountID"]!=null && row["BankAccountID"].ToString()!="")
				{
					model.BankAccountID=int.Parse(row["BankAccountID"].ToString());
				}
				if(row["FeeTypeValue"]!=null)
				{
					model.FeeTypeValue=row["FeeTypeValue"].ToString();
				}
				if(row["CurrencyTypeID"]!=null && row["CurrencyTypeID"].ToString()!="")
				{
					model.CurrencyTypeID=int.Parse(row["CurrencyTypeID"].ToString());
				}
				if(row["Fee"]!=null && row["Fee"].ToString()!="")
				{
					model.Fee=decimal.Parse(row["Fee"].ToString());
				}
				if(row["Increase"]!=null && row["Increase"].ToString()!="")
				{
					model.Increase=decimal.Parse(row["Increase"].ToString());
				}
				if(row["Decrease"]!=null && row["Decrease"].ToString()!="")
				{
					model.Decrease=decimal.Parse(row["Decrease"].ToString());
				}
				if(row["AccountBalance"]!=null && row["AccountBalance"].ToString()!="")
				{
					model.AccountBalance=decimal.Parse(row["AccountBalance"].ToString());
				}
				if(row["CustomerID"]!=null && row["CustomerID"].ToString()!="")
				{
					model.CustomerID=int.Parse(row["CustomerID"].ToString());
				}
				if(row["SupplierID"]!=null && row["SupplierID"].ToString()!="")
				{
					model.SupplierID=int.Parse(row["SupplierID"].ToString());
				}
				if(row["NetProfitMargin"]!=null && row["NetProfitMargin"].ToString()!="")
				{
					model.NetProfitMargin=decimal.Parse(row["NetProfitMargin"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["ByHandUserID"]!=null && row["ByHandUserID"].ToString()!="")
				{
					model.ByHandUserID=int.Parse(row["ByHandUserID"].ToString());
				}
				if(row["OccurTime"]!=null && row["OccurTime"].ToString()!="")
				{
					model.OccurTime=DateTime.Parse(row["OccurTime"].ToString());
				}
				if(row["IsFollowAction"]!=null && row["IsFollowAction"].ToString()!="")
				{
					if((row["IsFollowAction"].ToString()=="1")||(row["IsFollowAction"].ToString().ToLower()=="true"))
					{
						model.IsFollowAction=true;
					}
					else
					{
						model.IsFollowAction=false;
					}
				}
				if(row["AvailFlag"]!=null && row["AvailFlag"].ToString()!="")
				{
					if((row["AvailFlag"].ToString()=="1")||(row["AvailFlag"].ToString().ToLower()=="true"))
					{
						model.AvailFlag=true;
					}
					else
					{
						model.AvailFlag=false;
					}
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
			strSql.Append("select ID,PayTypeValue,BankAccountID,FeeTypeValue,CurrencyTypeID,Fee,Increase,Decrease,AccountBalance,CustomerID,SupplierID,NetProfitMargin,Remark,ByHandUserID,OccurTime,IsFollowAction,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM FeeFastFlow ");
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
			strSql.Append(" ID,PayTypeValue,BankAccountID,FeeTypeValue,CurrencyTypeID,Fee,Increase,Decrease,AccountBalance,CustomerID,SupplierID,NetProfitMargin,Remark,ByHandUserID,OccurTime,IsFollowAction,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM FeeFastFlow ");
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
			strSql.Append("select count(1) FROM FeeFastFlow ");
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
			strSql.Append(")AS Row, T.*  from FeeFastFlow T ");
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
			parameters[0].Value = "FeeFastFlow";
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

