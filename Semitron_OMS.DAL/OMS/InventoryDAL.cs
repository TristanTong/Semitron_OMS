/**  
* InventoryDAL.cs
*
* 功 能： N/A
* 类 名： InventoryDAL
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
using Semitron_OMS.DBUtility;//Please add references
namespace Semitron_OMS.DAL.OMS
{
	/// <summary>
	/// 数据访问类:InventoryDAL
	/// </summary>
	public partial class InventoryDAL
	{
		public InventoryDAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Semitron_OMS.Model.OMS.InventoryModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Inventory(");
			strSql.Append("ProductCode,WCodeBelong,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser)");
			strSql.Append(" values (");
			strSql.Append("@ProductCode,@WCodeBelong,@OnhandQty,@POQty,@UnInQty,@UnOutQty,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@WCodeBelong", SqlDbType.NVarChar,50),
					new SqlParameter("@OnhandQty", SqlDbType.Int,4),
					new SqlParameter("@POQty", SqlDbType.Int,4),
					new SqlParameter("@UnInQty", SqlDbType.Int,4),
					new SqlParameter("@UnOutQty", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ProductCode;
			parameters[1].Value = model.WCodeBelong;
			parameters[2].Value = model.OnhandQty;
			parameters[3].Value = model.POQty;
			parameters[4].Value = model.UnInQty;
			parameters[5].Value = model.UnOutQty;
			parameters[6].Value = model.CreateTime;
			parameters[7].Value = model.CreateUser;
			parameters[8].Value = model.UpdateTime;
			parameters[9].Value = model.UpdateUser;

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
		public bool Update(Semitron_OMS.Model.OMS.InventoryModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Inventory set ");
			strSql.Append("ProductCode=@ProductCode,");
			strSql.Append("WCodeBelong=@WCodeBelong,");
			strSql.Append("OnhandQty=@OnhandQty,");
			strSql.Append("POQty=@POQty,");
			strSql.Append("UnInQty=@UnInQty,");
			strSql.Append("UnOutQty=@UnOutQty,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUser=@UpdateUser");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@WCodeBelong", SqlDbType.NVarChar,50),
					new SqlParameter("@OnhandQty", SqlDbType.Int,4),
					new SqlParameter("@POQty", SqlDbType.Int,4),
					new SqlParameter("@UnInQty", SqlDbType.Int,4),
					new SqlParameter("@UnOutQty", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductCode;
			parameters[1].Value = model.WCodeBelong;
			parameters[2].Value = model.OnhandQty;
			parameters[3].Value = model.POQty;
			parameters[4].Value = model.UnInQty;
			parameters[5].Value = model.UnOutQty;
			parameters[6].Value = model.CreateTime;
			parameters[7].Value = model.CreateUser;
			parameters[8].Value = model.UpdateTime;
			parameters[9].Value = model.UpdateUser;
			parameters[10].Value = model.ID;

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
			strSql.Append("delete from Inventory ");
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
			strSql.Append("delete from Inventory ");
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
		public Semitron_OMS.Model.OMS.InventoryModel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,ProductCode,WCodeBelong,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser from Inventory ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Semitron_OMS.Model.OMS.InventoryModel model=new Semitron_OMS.Model.OMS.InventoryModel();
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
		public Semitron_OMS.Model.OMS.InventoryModel DataRowToModel(DataRow row)
		{
			Semitron_OMS.Model.OMS.InventoryModel model=new Semitron_OMS.Model.OMS.InventoryModel();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["ProductCode"]!=null)
				{
					model.ProductCode=row["ProductCode"].ToString();
				}
				if(row["WCodeBelong"]!=null)
				{
					model.WCodeBelong=row["WCodeBelong"].ToString();
				}
				if(row["OnhandQty"]!=null && row["OnhandQty"].ToString()!="")
				{
					model.OnhandQty=int.Parse(row["OnhandQty"].ToString());
				}
				if(row["POQty"]!=null && row["POQty"].ToString()!="")
				{
					model.POQty=int.Parse(row["POQty"].ToString());
				}
				if(row["UnInQty"]!=null && row["UnInQty"].ToString()!="")
				{
					model.UnInQty=int.Parse(row["UnInQty"].ToString());
				}
				if(row["UnOutQty"]!=null && row["UnOutQty"].ToString()!="")
				{
					model.UnOutQty=int.Parse(row["UnOutQty"].ToString());
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
			strSql.Append("select ID,ProductCode,WCodeBelong,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM Inventory ");
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
			strSql.Append(" ID,ProductCode,WCodeBelong,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM Inventory ");
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
			strSql.Append("select count(1) FROM Inventory ");
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
			strSql.Append(")AS Row, T.*  from Inventory T ");
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
			parameters[0].Value = "Inventory";
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

