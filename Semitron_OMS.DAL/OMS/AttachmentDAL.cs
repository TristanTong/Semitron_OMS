/**  
* AttachmentDAL.cs
*
* 功 能： N/A
* 类 名： AttachmentDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/21 23:25:58   童荣辉    初版
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
using Semitron_OMS.Common;//Please add references
namespace Semitron_OMS.DAL.OMS
{
    /// <summary>
    /// 数据访问类:AttachmentDAL
    /// </summary>
    public partial class AttachmentDAL
    {
        public AttachmentDAL()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Semitron_OMS.Model.OMS.AttachmentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Attachment(");
            strSql.Append("ObjType,ObjId,PhysicalPath,UrlPath,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            strSql.Append(" values (");
            strSql.Append("@ObjType,@ObjId,@PhysicalPath,@UrlPath,@AvailFlag,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ObjType", SqlDbType.VarChar,32),
					new SqlParameter("@ObjId", SqlDbType.VarChar,128),
					new SqlParameter("@PhysicalPath", SqlDbType.NVarChar,512),
					new SqlParameter("@UrlPath", SqlDbType.NVarChar,512),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,32),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,32)};
            parameters[0].Value = model.ObjType;
            parameters[1].Value = model.ObjId;
            parameters[2].Value = model.PhysicalPath;
            parameters[3].Value = model.UrlPath;
            parameters[4].Value = model.AvailFlag;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.CreateUser;
            parameters[7].Value = model.UpdateTime;
            parameters[8].Value = model.UpdateUser;

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
        public bool Update(Semitron_OMS.Model.OMS.AttachmentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Attachment set ");
            strSql.Append("ObjType=@ObjType,");
            strSql.Append("ObjId=@ObjId,");
            strSql.Append("PhysicalPath=@PhysicalPath,");
            strSql.Append("UrlPath=@UrlPath,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ObjType", SqlDbType.VarChar,32),
					new SqlParameter("@ObjId", SqlDbType.VarChar,128),
					new SqlParameter("@PhysicalPath", SqlDbType.NVarChar,512),
					new SqlParameter("@UrlPath", SqlDbType.NVarChar,512),
					new SqlParameter("@AvailFlag", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.VarChar,32),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUser", SqlDbType.VarChar,32),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ObjType;
            parameters[1].Value = model.ObjId;
            parameters[2].Value = model.PhysicalPath;
            parameters[3].Value = model.UrlPath;
            parameters[4].Value = model.AvailFlag;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.CreateUser;
            parameters[7].Value = model.UpdateTime;
            parameters[8].Value = model.UpdateUser;
            parameters[9].Value = model.ID;

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
            strSql.Append("delete from Attachment ");
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
            strSql.Append("delete from Attachment ");
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
        public Semitron_OMS.Model.OMS.AttachmentModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ObjType,ObjId,PhysicalPath,UrlPath,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser from Attachment ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.OMS.AttachmentModel model = new Semitron_OMS.Model.OMS.AttachmentModel();
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
        public Semitron_OMS.Model.OMS.AttachmentModel DataRowToModel(DataRow row)
        {
            Semitron_OMS.Model.OMS.AttachmentModel model = new Semitron_OMS.Model.OMS.AttachmentModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ObjType"] != null)
                {
                    model.ObjType = row["ObjType"].ToString();
                }
                if (row["ObjId"] != null)
                {
                    model.ObjId = row["ObjId"].ToString();
                }
                if (row["PhysicalPath"] != null)
                {
                    model.PhysicalPath = row["PhysicalPath"].ToString();
                }
                if (row["UrlPath"] != null)
                {
                    model.UrlPath = row["UrlPath"].ToString();
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
            strSql.Append("select ID,ObjType,ObjId,PhysicalPath,UrlPath,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM Attachment ");
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
            strSql.Append(" ID,ObjType,ObjId,PhysicalPath,UrlPath,AvailFlag,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            strSql.Append(" FROM Attachment ");
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
            strSql.Append("select count(1) FROM Attachment ");
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
            strSql.Append(")AS Row, T.*  from Attachment T ");
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
            parameters[0].Value = "Attachment";
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
        /// 批量保存附件文件
        /// </summary>
        /// <param name="strObjType">对象类型</param>
        /// <param name="strObjId">对象Id或编号</param>
        /// <param name="strFileUrl">附件HTTP地址列表</param>
        /// <returns>批量保存是否成功</returns>
        public bool BatchSaveFiles(string strObjType, string strObjId, string strFilePaths, string strUserName)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ObjType", SqlDbType.VarChar,16),
                    new SqlParameter("@ObjId", SqlDbType.VarChar,16),
                    new SqlParameter("@FilePaths", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUser", SqlDbType.VarChar,16)
			};
            parameters[0].Value = strObjType;
            parameters[1].Value = strObjId;
            parameters[2].Value = strFilePaths;
            parameters[3].Value = strUserName;
            int rowsAffected = 0;
            return DbHelperSQL.RunProcedure(ConstantValue.ProcedureNames.AttachmentBatchSaveFiles, parameters, out rowsAffected) >=0 ? true : false;
        }
        #endregion  ExtensionMethod
    }
}

