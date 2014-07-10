/**  
* InventoryAuditDAL.cs
*
* 功 能： N/A
* 类 名： InventoryAuditDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:19   童荣辉    初版
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
	/// 数据访问类:InventoryAuditDAL
	/// </summary>
	public partial class InventoryAuditDAL
	{
		public InventoryAuditDAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Semitron_OMS.Model.OMS.InventoryAuditModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into InventoryAudit(");
			strSql.Append("ActionType,ActionTime,ID,ProductCode,WCodeLocation,WCodeBelong,WBelongUserID,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser)");
			strSql.Append(" values (");
			strSql.Append("@ActionType,@ActionTime,@ID,@ProductCode,@WCodeLocation,@WCodeBelong,@WBelongUserID,@OnhandQty,@POQty,@UnInQty,@UnOutQty,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ActionType", SqlDbType.NVarChar,50),
					new SqlParameter("@ActionTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@WCodeLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@WCodeBelong", SqlDbType.NVarChar,50),
					new SqlParameter("@WBelongUserID", SqlDbType.Int,4),
					new SqlParameter("@OnhandQty", SqlDbType.Int,4),
					new SqlParameter("@POQty", SqlDbType.Int,4),
					new SqlParameter("@UnInQty", SqlDbType.Int,4),
					new SqlParameter("@UnOutQty", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ActionType;
			parameters[1].Value = model.ActionTime;
			parameters[2].Value = model.ID;
			parameters[3].Value = model.ProductCode;
			parameters[4].Value = model.WCodeLocation;
			parameters[5].Value = model.WCodeBelong;
			parameters[6].Value = model.WBelongUserID;
			parameters[7].Value = model.OnhandQty;
			parameters[8].Value = model.POQty;
			parameters[9].Value = model.UnInQty;
			parameters[10].Value = model.UnOutQty;
			parameters[11].Value = model.CreateTime;
			parameters[12].Value = model.CreateUser;
			parameters[13].Value = model.UpdateTime;
			parameters[14].Value = model.UpdateUser;

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
		public bool Update(Semitron_OMS.Model.OMS.InventoryAuditModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update InventoryAudit set ");
			strSql.Append("ActionType=@ActionType,");
			strSql.Append("ActionTime=@ActionTime,");
			strSql.Append("ID=@ID,");
			strSql.Append("ProductCode=@ProductCode,");
			strSql.Append("WCodeLocation=@WCodeLocation,");
			strSql.Append("WCodeBelong=@WCodeBelong,");
			strSql.Append("WBelongUserID=@WBelongUserID,");
			strSql.Append("OnhandQty=@OnhandQty,");
			strSql.Append("POQty=@POQty,");
			strSql.Append("UnInQty=@UnInQty,");
			strSql.Append("UnOutQty=@UnOutQty,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUser=@UpdateUser");
			strSql.Append(" where ActionID=@ActionID");
			SqlParameter[] parameters = {
					new SqlParameter("@ActionType", SqlDbType.NVarChar,50),
					new SqlParameter("@ActionTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@WCodeLocation", SqlDbType.NVarChar,50),
					new SqlParameter("@WCodeBelong", SqlDbType.NVarChar,50),
					new SqlParameter("@WBelongUserID", SqlDbType.Int,4),
					new SqlParameter("@OnhandQty", SqlDbType.Int,4),
					new SqlParameter("@POQty", SqlDbType.Int,4),
					new SqlParameter("@UnInQty", SqlDbType.Int,4),
					new SqlParameter("@UnOutQty", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ActionID", SqlDbType.Int,4)};
			parameters[0].Value = model.ActionType;
			parameters[1].Value = model.ActionTime;
			parameters[2].Value = model.ID;
			parameters[3].Value = model.ProductCode;
			parameters[4].Value = model.WCodeLocation;
			parameters[5].Value = model.WCodeBelong;
			parameters[6].Value = model.WBelongUserID;
			parameters[7].Value = model.OnhandQty;
			parameters[8].Value = model.POQty;
			parameters[9].Value = model.UnInQty;
			parameters[10].Value = model.UnOutQty;
			parameters[11].Value = model.CreateTime;
			parameters[12].Value = model.CreateUser;
			parameters[13].Value = model.UpdateTime;
			parameters[14].Value = model.UpdateUser;
			parameters[15].Value = model.ActionID;

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
		public bool Delete(int ActionID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from InventoryAudit ");
			strSql.Append(" where ActionID=@ActionID");
			SqlParameter[] parameters = {
					new SqlParameter("@ActionID", SqlDbType.Int,4)
			};
			parameters[0].Value = ActionID;

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
		public bool DeleteList(string ActionIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from InventoryAudit ");
			strSql.Append(" where ActionID in ("+ActionIDlist + ")  ");
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
		public Semitron_OMS.Model.OMS.InventoryAuditModel GetModel(int ActionID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ActionID,ActionType,ActionTime,ID,ProductCode,WCodeLocation,WCodeBelong,WBelongUserID,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser from InventoryAudit ");
			strSql.Append(" where ActionID=@ActionID");
			SqlParameter[] parameters = {
					new SqlParameter("@ActionID", SqlDbType.Int,4)
			};
			parameters[0].Value = ActionID;

			Semitron_OMS.Model.OMS.InventoryAuditModel model=new Semitron_OMS.Model.OMS.InventoryAuditModel();
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
		public Semitron_OMS.Model.OMS.InventoryAuditModel DataRowToModel(DataRow row)
		{
			Semitron_OMS.Model.OMS.InventoryAuditModel model=new Semitron_OMS.Model.OMS.InventoryAuditModel();
			if (row != null)
			{
				if(row["ActionID"]!=null && row["ActionID"].ToString()!="")
				{
					model.ActionID=int.Parse(row["ActionID"].ToString());
				}
				if(row["ActionType"]!=null)
				{
					model.ActionType=row["ActionType"].ToString();
				}
				if(row["ActionTime"]!=null && row["ActionTime"].ToString()!="")
				{
					model.ActionTime=DateTime.Parse(row["ActionTime"].ToString());
				}
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["ProductCode"]!=null)
				{
					model.ProductCode=row["ProductCode"].ToString();
				}
				if(row["WCodeLocation"]!=null)
				{
					model.WCodeLocation=row["WCodeLocation"].ToString();
				}
				if(row["WCodeBelong"]!=null)
				{
					model.WCodeBelong=row["WCodeBelong"].ToString();
				}
				if(row["WBelongUserID"]!=null && row["WBelongUserID"].ToString()!="")
				{
					model.WBelongUserID=int.Parse(row["WBelongUserID"].ToString());
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
			strSql.Append("select ActionID,ActionType,ActionTime,ID,ProductCode,WCodeLocation,WCodeBelong,WBelongUserID,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM InventoryAudit ");
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
			strSql.Append(" ActionID,ActionType,ActionTime,ID,ProductCode,WCodeLocation,WCodeBelong,WBelongUserID,OnhandQty,POQty,UnInQty,UnOutQty,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM InventoryAudit ");
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
			strSql.Append("select count(1) FROM InventoryAudit ");
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
				strSql.Append("order by T.ActionID desc");
			}
			strSql.Append(")AS Row, T.*  from InventoryAudit T ");
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
			parameters[0].Value = "InventoryAudit";
			parameters[1].Value = "ActionID";
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
        /// 分页查询入库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>数据</returns>
        public DataSet GetInventoryPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.InventoryAudit AS G WITH (NOLOCK) LEFT JOIN Warehouse AS W ON G.WCodeBelong=W.WCode LEFT JOIN ProductInfo AS P WITH (NOLOCK) ON P.ProductCode=G.ProductCode";
            //查询字段
            string strGetFields = "G.ActionID,G.ActionType,ActionTime=Convert(varchar(20),G.ActionTime,120), G.ID,G.ProductCode,P.MPN,G.WCodeBelong,W.WName,G.OnHandQty,G.POQty,G.UnInQty,G.UnOutQty,UpdateTime=Convert(varchar(20),G.UpdateTime,120),G.UpdateUser ";
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

