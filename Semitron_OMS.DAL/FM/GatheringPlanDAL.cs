/**  
* GatheringPlanDAL.cs
*
* 功 能： N/A
* 类 名： GatheringPlanDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:58   童荣辉    初版
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
		/// 增加一条数据
		/// </summary>
		public int Add(Semitron_OMS.Model.FM.GatheringPlanModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GatheringPlan(");
			strSql.Append("ShippingListNo,GatheringPlanDate,ProductCode,Qty,Price,TotalGathering,State,CreateTime,CreateUser,UpdateTime,UpdateUser)");
			strSql.Append(" values (");
			strSql.Append("@ShippingListNo,@GatheringPlanDate,@ProductCode,@Qty,@Price,@TotalGathering,@State,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ShippingListNo", SqlDbType.NVarChar,50),
					new SqlParameter("@GatheringPlanDate", SqlDbType.DateTime),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Qty", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@TotalGathering", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ShippingListNo;
			parameters[1].Value = model.GatheringPlanDate;
			parameters[2].Value = model.ProductCode;
			parameters[3].Value = model.Qty;
			parameters[4].Value = model.Price;
			parameters[5].Value = model.TotalGathering;
			parameters[6].Value = model.State;
			parameters[7].Value = model.CreateTime;
			parameters[8].Value = model.CreateUser;
			parameters[9].Value = model.UpdateTime;
			parameters[10].Value = model.UpdateUser;

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
			strSql.Append("ShippingListNo=@ShippingListNo,");
			strSql.Append("GatheringPlanDate=@GatheringPlanDate,");
			strSql.Append("ProductCode=@ProductCode,");
			strSql.Append("Qty=@Qty,");
			strSql.Append("Price=@Price,");
			strSql.Append("TotalGathering=@TotalGathering,");
			strSql.Append("State=@State,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUser=@UpdateUser");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ShippingListNo", SqlDbType.NVarChar,50),
					new SqlParameter("@GatheringPlanDate", SqlDbType.DateTime),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Qty", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@TotalGathering", SqlDbType.Decimal,9),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ShippingListNo;
			parameters[1].Value = model.GatheringPlanDate;
			parameters[2].Value = model.ProductCode;
			parameters[3].Value = model.Qty;
			parameters[4].Value = model.Price;
			parameters[5].Value = model.TotalGathering;
			parameters[6].Value = model.State;
			parameters[7].Value = model.CreateTime;
			parameters[8].Value = model.CreateUser;
			parameters[9].Value = model.UpdateTime;
			parameters[10].Value = model.UpdateUser;
			parameters[11].Value = model.ID;

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
			strSql.Append("select  top 1 ID,ShippingListNo,GatheringPlanDate,ProductCode,Qty,Price,TotalGathering,State,CreateTime,CreateUser,UpdateTime,UpdateUser from GatheringPlan ");
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
				if(row["ShippingListNo"]!=null)
				{
					model.ShippingListNo=row["ShippingListNo"].ToString();
				}
				if(row["GatheringPlanDate"]!=null && row["GatheringPlanDate"].ToString()!="")
				{
					model.GatheringPlanDate=DateTime.Parse(row["GatheringPlanDate"].ToString());
				}
				if(row["ProductCode"]!=null)
				{
					model.ProductCode=row["ProductCode"].ToString();
				}
				if(row["Qty"]!=null && row["Qty"].ToString()!="")
				{
					model.Qty=int.Parse(row["Qty"].ToString());
				}
				if(row["Price"]!=null && row["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(row["Price"].ToString());
				}
				if(row["TotalGathering"]!=null && row["TotalGathering"].ToString()!="")
				{
					model.TotalGathering=decimal.Parse(row["TotalGathering"].ToString());
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
			strSql.Append("select ID,ShippingListNo,GatheringPlanDate,ProductCode,Qty,Price,TotalGathering,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
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
			strSql.Append(" ID,ShippingListNo,GatheringPlanDate,ProductCode,Qty,Price,TotalGathering,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
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

