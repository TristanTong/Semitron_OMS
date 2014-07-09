/**  
* WarehouseDAL.cs
*
* 功 能： N/A
* 类 名： WarehouseDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:41:50   童荣辉    初版
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
namespace Semitron_OMS.DAL.Common
{
	/// <summary>
	/// 数据访问类:WarehouseDAL
	/// </summary>
	public partial class WarehouseDAL
	{
		public WarehouseDAL()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Semitron_OMS.Model.Common.WarehouseModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Warehouse(");
			strSql.Append("WCode,WName,WType,PId,WBelongUserID,SK,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
			strSql.Append(" values (");
			strSql.Append("@WCode,@WName,@WType,@PId,@WBelongUserID,@SK,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@WCode", SqlDbType.NVarChar,50),
					new SqlParameter("@WName", SqlDbType.NVarChar,50),
					new SqlParameter("@WType", SqlDbType.Int,4),
					new SqlParameter("@PId", SqlDbType.Int,4),
					new SqlParameter("@WBelongUserID", SqlDbType.Int,4),
					new SqlParameter("@SK", SqlDbType.NVarChar,50),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.WCode;
			parameters[1].Value = model.WName;
			parameters[2].Value = model.WType;
			parameters[3].Value = model.PId;
			parameters[4].Value = model.WBelongUserID;
			parameters[5].Value = model.SK;
			parameters[6].Value = model.AvailFlag;
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
		public bool Update(Semitron_OMS.Model.Common.WarehouseModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Warehouse set ");
			strSql.Append("WCode=@WCode,");
			strSql.Append("WName=@WName,");
			strSql.Append("WType=@WType,");
			strSql.Append("PId=@PId,");
			strSql.Append("WBelongUserID=@WBelongUserID,");
			strSql.Append("SK=@SK,");
			strSql.Append("AvailFlag=@AvailFlag,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUser=@CreateUser,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUser=@UpdateUser");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@WCode", SqlDbType.NVarChar,50),
					new SqlParameter("@WName", SqlDbType.NVarChar,50),
					new SqlParameter("@WType", SqlDbType.Int,4),
					new SqlParameter("@PId", SqlDbType.Int,4),
					new SqlParameter("@WBelongUserID", SqlDbType.Int,4),
					new SqlParameter("@SK", SqlDbType.NVarChar,50),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.WCode;
			parameters[1].Value = model.WName;
			parameters[2].Value = model.WType;
			parameters[3].Value = model.PId;
			parameters[4].Value = model.WBelongUserID;
			parameters[5].Value = model.SK;
			parameters[6].Value = model.AvailFlag;
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
			strSql.Append("delete from Warehouse ");
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
			strSql.Append("delete from Warehouse ");
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
		public Semitron_OMS.Model.Common.WarehouseModel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,WCode,WName,WType,PId,WBelongUserID,SK,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser from Warehouse ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Semitron_OMS.Model.Common.WarehouseModel model=new Semitron_OMS.Model.Common.WarehouseModel();
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
		public Semitron_OMS.Model.Common.WarehouseModel DataRowToModel(DataRow row)
		{
			Semitron_OMS.Model.Common.WarehouseModel model=new Semitron_OMS.Model.Common.WarehouseModel();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["WCode"]!=null)
				{
					model.WCode=row["WCode"].ToString();
				}
				if(row["WName"]!=null)
				{
					model.WName=row["WName"].ToString();
				}
				if(row["WType"]!=null && row["WType"].ToString()!="")
				{
					model.WType=int.Parse(row["WType"].ToString());
				}
				if(row["PId"]!=null && row["PId"].ToString()!="")
				{
					model.PId=int.Parse(row["PId"].ToString());
				}
				if(row["WBelongUserID"]!=null && row["WBelongUserID"].ToString()!="")
				{
					model.WBelongUserID=int.Parse(row["WBelongUserID"].ToString());
				}
				if(row["SK"]!=null)
				{
					model.SK=row["SK"].ToString();
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
			strSql.Append("select ID,WCode,WName,WType,PId,WBelongUserID,SK,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM Warehouse ");
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
			strSql.Append(" ID,WCode,WName,WType,PId,WBelongUserID,SK,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
			strSql.Append(" FROM Warehouse ");
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
			strSql.Append("select count(1) FROM Warehouse ");
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
			strSql.Append(")AS Row, T.*  from Warehouse T ");
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
			parameters[0].Value = "Warehouse";
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
        /// 根据编号获取仓库对象
        /// </summary>
        /// <param name="strCode">编号</param>
        /// <returns>仓库对象</returns>
        public Model.Common.WarehouseModel GetModelByCode(string strCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from Warehouse");
            strSql.Append(" where WCode=@WCode");
            SqlParameter[] parameters = {
					new SqlParameter("@WCode", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = strCode;

            Semitron_OMS.Model.Common.WarehouseModel model = new Semitron_OMS.Model.Common.WarehouseModel();
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
        #endregion  ExtensionMethod

    }
}

