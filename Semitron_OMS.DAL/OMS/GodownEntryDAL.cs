/**  
* GodownEntryDAL.cs
*
* 功 能： N/A
* 类 名： GodownEntryDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:40:17   童荣辉    初版
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
    /// 数据访问类:GodownEntryDAL
    /// </summary>
    public partial class GodownEntryDAL
    {
        public GodownEntryDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "GodownEntry");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GodownEntry");
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
        public int Add(Semitron_OMS.Model.OMS.GodownEntryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GodownEntry(");
            strSql.Append("EntryNo,ByHandUserID,InStockDate,InWarehouseCode,IsApproved,ApprovedUser,ApprovedTime,Description,State,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@EntryNo,@ByHandUserID,@InStockDate,@InWarehouseCode,@IsApproved,@ApprovedUser,@ApprovedTime,@Description,@State,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@EntryNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ByHandUserID", SqlDbType.Int,4),
					new SqlParameter("@InStockDate", SqlDbType.DateTime),
					new SqlParameter("@InWarehouseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@ApprovedUser", SqlDbType.Int,4),
					new SqlParameter("@ApprovedTime", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,1024),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.EntryNo;
            parameters[1].Value = model.ByHandUserID;
            parameters[2].Value = model.InStockDate;
            parameters[3].Value = model.InWarehouseCode;
            parameters[4].Value = model.IsApproved;
            parameters[5].Value = model.ApprovedUser;
            parameters[6].Value = model.ApprovedTime;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.State;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.UpdateUser;

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
        public bool Update(Semitron_OMS.Model.OMS.GodownEntryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GodownEntry set ");
            strSql.Append("EntryNo=@EntryNo,");
            strSql.Append("ByHandUserID=@ByHandUserID,");
            strSql.Append("InStockDate=@InStockDate,");
            strSql.Append("InWarehouseCode=@InWarehouseCode,");
            strSql.Append("IsApproved=@IsApproved,");
            strSql.Append("ApprovedUser=@ApprovedUser,");
            strSql.Append("ApprovedTime=@ApprovedTime,");
            strSql.Append("Description=@Description,");
            strSql.Append("State=@State,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@EntryNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ByHandUserID", SqlDbType.Int,4),
					new SqlParameter("@InStockDate", SqlDbType.DateTime),
					new SqlParameter("@InWarehouseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsApproved", SqlDbType.Bit,1),
					new SqlParameter("@ApprovedUser", SqlDbType.Int,4),
					new SqlParameter("@ApprovedTime", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.NVarChar,1024),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.EntryNo;
            parameters[1].Value = model.ByHandUserID;
            parameters[2].Value = model.InStockDate;
            parameters[3].Value = model.InWarehouseCode;
            parameters[4].Value = model.IsApproved;
            parameters[5].Value = model.ApprovedUser;
            parameters[6].Value = model.ApprovedTime;
            parameters[7].Value = model.Description;
            parameters[8].Value = model.State;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.UpdateUser;
            parameters[13].Value = model.ID;

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
            strSql.Append("delete from GodownEntry ");
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
            strSql.Append("delete from GodownEntry ");
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
        public Semitron_OMS.Model.OMS.GodownEntryModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,EntryNo,ByHandUserID,InStockDate,InWarehouseCode,IsApproved,ApprovedUser,ApprovedTime,Description,State,CreateTime,CreateUser,UpdateTime,UpdateUser from GodownEntry ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.GodownEntryModel model = new Semitron_OMS.Model.OMS.GodownEntryModel();
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
        public Semitron_OMS.Model.OMS.GodownEntryModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.GodownEntryModel model = new Semitron_OMS.Model.OMS.GodownEntryModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["EntryNo"] != null)
                {
                    model.EntryNo = row["EntryNo"].ToString();
                }
                if (row["ByHandUserID"] != null && row["ByHandUserID"].ToString() != "")
                {
                    model.ByHandUserID = int.Parse(row["ByHandUserID"].ToString());
                }
                if (row["InStockDate"] != null && row["InStockDate"].ToString() != "")
                {
                    model.InStockDate = DateTime.Parse(row["InStockDate"].ToString());
                }
                if (row["InWarehouseCode"] != null)
                {
                    model.InWarehouseCode = row["InWarehouseCode"].ToString();
                }
                if (row["IsApproved"] != null && row["IsApproved"].ToString() != "")
                {
                    if ((row["IsApproved"].ToString() == "1") || (row["IsApproved"].ToString().ToLower() == "true"))
                    {
                        model.IsApproved = true;
                    }
                    else
                    {
                        model.IsApproved = false;
                    }
                }
                if (row["ApprovedUser"] != null && row["ApprovedUser"].ToString() != "")
                {
                    model.ApprovedUser = int.Parse(row["ApprovedUser"].ToString());
                }
                if (row["ApprovedTime"] != null && row["ApprovedTime"].ToString() != "")
                {
                    model.ApprovedTime = DateTime.Parse(row["ApprovedTime"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
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
            strSql.Append("select ID,EntryNo,ByHandUserID,InStockDate,InWarehouseCode,IsApproved,ApprovedUser,ApprovedTime,Description,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM GodownEntry ");
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
            strSql.Append(" ID,EntryNo,ByHandUserID,InStockDate,InWarehouseCode,IsApproved,ApprovedUser,ApprovedTime,Description,State,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM GodownEntry ");
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
            strSql.Append("select count(1) FROM GodownEntry ");
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
            strSql.Append(")AS Row, T.*  from GodownEntry T ");
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
            parameters[0].Value = "GodownEntry";
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
        /// 分页查询入库单据
        /// </summary>
        /// <param name="searchInfo">SQL辅助类对象</param>
        /// <param name="o_RowsCount">总查询数</param>
        /// <returns>入库单数据</returns>
        public DataSet GetGodownEntryPageData(Semitron_OMS.Common.PageSearchInfo searchInfo, out int o_RowsCount)
        {
            //查询表名
            string strTableName = " dbo.GodownEntry AS G LEFT JOIN Admin AS A1 ON A1.AdminID=G.ByHandUserID LEFT JOIN Admin AS A2 ON A2.AdminID=G.ApprovedUser";
            //查询字段
            string strGetFields = " G.ID,State=CASE WHEN G.State=1 THEN '有效' ELSE '无效' END,G.EntryNo,IsApproved=CASE WHEN G.IsApproved=1 THEN '已审核' ELSE '未审核' END,InStockDate=Convert(varchar(20),G.InStockDate,120),G.InWarehouseCode,ByHandUser=A1.UserName,ApprovedUser=A2.UserName,ApprovedTime=Convert(varchar(20),G.ApprovedTime,120),CreateTime=Convert(varchar(20),G.CreateTime,120) ,UpdateTime=Convert(varchar(20),G.UpdateTime,120),G.CreateUser,G.UpdateUser ";
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
        /// 设置入库单状态
        /// </summary>
        /// <param name="iId">入库单ID</param>
        /// <param name="iStatus">状态ID</param>
        /// <returns>执行结果是否成功</returns>
        public bool SetValid(int iId, int iStatus)
        {
            System.Collections.Generic.List<string> lstSql = new System.Collections.Generic.List<string>();
            lstSql.Add("update GodownEntry set State=" + iStatus + " where ID=" + iId);
            lstSql.Add("update GodownEntryDetail set AvailFlag=" + iStatus + " where GodownEntryID=" + iId);

            int rows = DbHelperSQL.ExecuteSqlTran(lstSql);
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
        /// 验证并审核入库单
        /// </summary>
        public string ApproveGodownEntry(int iId, int iUser)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@EntryId", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Result", SqlDbType.NVarChar,512)
                    };
            parameters[0].Value = iId;
            parameters[1].Value = iUser;
            parameters[2].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.ApproveGodownEntry, parameters, "ds");
            return parameters[2].Value.ToString();
        }
        #endregion  ExtensionMethod

    }
}

