/**  
* AdminBindSupplierDAL.cs
*
* 功 能： N/A
* 类 名： AdminBindSupplierDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/24 22:27:48   童荣辉    初版
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
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Model.Common;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:AdminBindSupplierDAL
    /// </summary>
    public partial class AdminBindSupplierDAL
    {
        public AdminBindSupplierDAL()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.AdminBindSupplierModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AdminBindSupplier(");
            strSql.Append("ServiceType,AdminID,SupplierID,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@ServiceType,@AdminID,@SupplierID,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ServiceType", SqlDbType.SmallInt,2),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ServiceType;
            parameters[1].Value = model.AdminID;
            parameters[2].Value = model.SupplierID;
            parameters[3].Value = model.CreateTime;

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
        public bool Update(Semitron_OMS.Model.OMS.AdminBindSupplierModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AdminBindSupplier set ");
            strSql.Append("ServiceType=@ServiceType,");
            strSql.Append("AdminID=@AdminID,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where BindID=@BindID");
            SqlParameter[] parameters = {
					new SqlParameter("@ServiceType", SqlDbType.SmallInt,2),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@BindID", SqlDbType.Int,4)};
            parameters[0].Value = model.ServiceType;
            parameters[1].Value = model.AdminID;
            parameters[2].Value = model.SupplierID;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.BindID;

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
        public bool Delete(int BindID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AdminBindSupplier ");
            strSql.Append(" where BindID=@BindID");
            SqlParameter[] parameters = {
					new SqlParameter("@BindID", SqlDbType.Int,4)
			};
            parameters[0].Value = BindID;

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
        public bool DeleteList(string BindIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AdminBindSupplier ");
            strSql.Append(" where BindID in (" + BindIDlist + ")  ");
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
        public Semitron_OMS.Model.OMS.AdminBindSupplierModel GetModel(int BindID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BindID,ServiceType,AdminID,SupplierID,CreateTime from AdminBindSupplier ");
            strSql.Append(" where BindID=@BindID");
            SqlParameter[] parameters = {
					new SqlParameter("@BindID", SqlDbType.Int,4)
			};
            parameters[0].Value = BindID;

            Semitron_OMS.Model.OMS.AdminBindSupplierModel model = new Semitron_OMS.Model.OMS.AdminBindSupplierModel();
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
        public Semitron_OMS.Model.OMS.AdminBindSupplierModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.AdminBindSupplierModel model = new Semitron_OMS.Model.OMS.AdminBindSupplierModel();
            if (row != null)
            {
                if (row["BindID"] != null && row["BindID"].ToString() != "")
                {
                    model.BindID = int.Parse(row["BindID"].ToString());
                }
                if (row["ServiceType"] != null && row["ServiceType"].ToString() != "")
                {
                    model.ServiceType = int.Parse(row["ServiceType"].ToString());
                }
                if (row["AdminID"] != null && row["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(row["AdminID"].ToString());
                }
                if (row["SupplierID"] != null && row["SupplierID"].ToString() != "")
                {
                    model.SupplierID = int.Parse(row["SupplierID"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
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
            strSql.Append("select BindID,ServiceType,AdminID,SupplierID,CreateTime ");
            strSql.Append(" FROM AdminBindSupplier ");
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
            strSql.Append(" BindID,ServiceType,AdminID,SupplierID,CreateTime ");
            strSql.Append(" FROM AdminBindSupplier ");
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
            strSql.Append("select count(1) FROM AdminBindSupplier ");
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
                strSql.Append("order by T.BindID desc");
            }
            strSql.Append(")AS Row, T.*  from AdminBindSupplier T ");
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
            parameters[0].Value = "AdminBindSupplier";
            parameters[1].Value = "BindID";
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
        /// 分页获取采购人员
        /// </summary>
        /// <param name="pageSearchInfo">SQL条件过滤器</param>
        /// <param name="iSupplierId">供应商Id</param>
        /// <param name="o_RowsCount">查询总数</param>
        /// <returns></returns>
        public DataSet GetAdminBindSupplierPageData(PageSearchInfo pageSearchInfo, int iSupplierId, out int o_RowsCount)
        {
            //查询表名称
            string strTableName = "dbo.Admin AS A LEFT JOIN AdminBindSupplier AS F ON A.AdminID = F.AdminID AND F.ServiceType = 1 AND F.SupplierId =" + iSupplierId;
            //查询列名
            string strGetFields = " CASE WHEN BindID IS NULL THEN '未关联' ELSE '已关联' END AS Valid ,A.AdminID , A.Name , CASE F.ServiceType WHEN 1 THEN '关联采购负责人' ELSE '' END AS ServiceType , F.CreateTime , F.BindID IndexID2 , CASE WHEN F.BindID IS NULL THEN 'false' ELSE 'true' END AS checked999";

            //获取查询条件语句
            string strWhere = SQLOperateHelper.GetSQLCondition(pageSearchInfo.ConditionFilter, false)
                + " AND EXISTS ( SELECT 1 FROM   dbo.UserRole AS U INNER JOIN dbo.Role AS R ON U.RoleID = R.RoleID WHERE  U.AdminID = A.AdminID AND R.RoleID IN ( "
                + (int)Semitron_OMS.Common.Enum.EnumRoleID.InnerBuyer + ","
                + (int)Semitron_OMS.Common.Enum.EnumRoleID.BuyerManager + " ) )";
            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt(strTableName,
                strGetFields,
                pageSearchInfo.PageSize,
                pageSearchInfo.PageIndex,
                strWhere,
                pageSearchInfo.OrderByField,
                pageSearchInfo.OrderType,
                out o_RowsCount);
        }

        /// <summary>
        /// 供应商关联采购
        /// </summary>
        /// <param name="iSupplierId">供应商Id</param>
        /// <param name="IdList">选中的Id串</param>
        /// <param name="IdListAll">页面中所有的Id串</param>
        /// <returns></returns>
        public bool AdminBindSupplier(int iSupplierId, string IdList, string IdListAll)
        {
            List<string> lstSql = new List<string>();
            if (string.IsNullOrEmpty(IdListAll))
            {
                IdListAll = "''";
            }
            lstSql.Add("DELETE FROM AdminBindSupplier WHERE SupplierId='" + iSupplierId + "' AND AdminID IN (" + IdListAll + ")");
            lstSql.Add("INSERT  INTO AdminBindSupplier ( ServiceType , SupplierId , AdminID , CreateTime ) SELECT  ServiceType = 1 , SupplierId = '" + iSupplierId + "' , T.AdminID , CreateTime = GETDATE() FROM    ( SELECT    AdminID = value FROM      dbo.SplitToTable('" + IdList + "', ',') ) AS T WHERE  AdminID IN (" + IdListAll + ")");
            if (DbHelperSQL.ExecuteSqlTran(lstSql) > 0)
            {
                new DAL.Common.OperationsLogDAL().AddExecute("AdminBindSupplier", "供应商关联采购,供应商ID:" + iSupplierId + ",采购ID串：" + IdList, iSupplierId.ToString(), (int)OperationsType.Add);
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

