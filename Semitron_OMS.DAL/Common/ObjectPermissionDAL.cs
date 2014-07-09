using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using System.Web;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.DAL.Common
{
    /// <summary>
    /// 数据访问类:ObjectPermissionDAL
    /// </summary>
    public partial class ObjectPermissionDAL
    {
        public ObjectPermissionDAL()
        { }
        #region  Method
        OperationsLogDAL logDal = new OperationsLogDAL();
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "ObjectPermission");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ObjectPermission");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ObjectPermissionModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ObjectPermission(");
            strSql.Append("ID,PermissionID,ObjType,ObjID,AvailFlag)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PermissionID,@ObjType,@ObjID,@AvailFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@PermissionID", SqlDbType.Int,4),
					new SqlParameter("@ObjType", SqlDbType.Int,4),
					new SqlParameter("@ObjID", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PermissionID;
            parameters[2].Value = model.ObjType;
            parameters[3].Value = model.ObjID;
            parameters[4].Value = model.AvailFlag;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(ObjectPermissionModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ObjectPermission set ");
            strSql.Append("PermissionID=@PermissionID,");
            strSql.Append("ObjType=@ObjType,");
            strSql.Append("ObjID=@ObjID,");
            strSql.Append("AvailFlag=@AvailFlag");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PermissionID", SqlDbType.Int,4),
					new SqlParameter("@ObjType", SqlDbType.Int,4),
					new SqlParameter("@ObjID", SqlDbType.Int,4),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.PermissionID;
            parameters[1].Value = model.ObjType;
            parameters[2].Value = model.ObjID;
            parameters[3].Value = model.AvailFlag;
            parameters[4].Value = model.ID;

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
            strSql.Append("delete from ObjectPermission ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
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
            strSql.Append("delete from ObjectPermission ");
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
        public ObjectPermissionModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PermissionID,ObjType,ObjID,AvailFlag from ObjectPermission ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            ObjectPermissionModel model = new ObjectPermissionModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PermissionID"] != null && ds.Tables[0].Rows[0]["PermissionID"].ToString() != "")
                {
                    model.PermissionID = int.Parse(ds.Tables[0].Rows[0]["PermissionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ObjType"] != null && ds.Tables[0].Rows[0]["ObjType"].ToString() != "")
                {
                    model.ObjType = int.Parse(ds.Tables[0].Rows[0]["ObjType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ObjID"] != null && ds.Tables[0].Rows[0]["ObjID"].ToString() != "")
                {
                    model.ObjID = int.Parse(ds.Tables[0].Rows[0]["ObjID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AvailFlag"] != null && ds.Tables[0].Rows[0]["AvailFlag"].ToString() != "")
                {
                    model.AvailFlag = int.Parse(ds.Tables[0].Rows[0]["AvailFlag"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PermissionID,ObjType,ObjID,AvailFlag ");
            strSql.Append(" FROM ObjectPermission ");
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
            strSql.Append(" ID,PermissionID,ObjType,ObjID,AvailFlag ");
            strSql.Append(" FROM ObjectPermission ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 绑定角色关系
        /// </summary>
        /// <param name="ObjID">代码ID</param>
        /// <param name="PermissionIDList">地区ID集合</param>
        /// <param name="Key1">关键字1</param>
        /// <param name="Key2">关键字2</param>
        /// <param name="RContent">内容</param>
        /// <param name="SuccessRate">成功率</param>
        /// <returns></returns>
        public bool BindObjectPermission(string ObjID, string[] PermissionIDList, string[] PermissionNameList, int ObjType)
        {
            if (PermissionIDList.Length > 0)
            {
                string PermissionNames = null;
                string PermissionIds = string.Empty;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from ObjectPermission where ObjID=" + ObjID + "; ");
                for (int i = 0; i < PermissionIDList.Length; i++)
                {
                    if (PermissionIDList[i] != "")
                    {
                        if (i == 0)
                        {
                            strSql.Append("insert into ObjectPermission ");
                        }
                        strSql.Append(" select " + PermissionIDList[i] + "," + ObjType + "," + ObjID + ",1");
                        if (i != PermissionIDList.Length - 1)
                        {
                            strSql.Append(" union all ");
                        }
                    }
                }
                //for (int i = 0; i < PermissionNameList.Length; i++)
                //{
                //    PermissionNames += PermissionNameList[i] + ",";
                //}
                for (int i = 0; i < PermissionIDList.Length; i++)
                {
                    PermissionIds += PermissionIDList[i] + ",";
                }

                //增加操作日志
                string logSql = logDal.Add("ObjectPermission", "修改角色权限：" + PermissionIds, "", (int)OperationsType.Edit);

                strSql.Append(logSql);
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
            return false;
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
            parameters[0].Value = "ObjectPermission";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}

