using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Semitron_OMS.DBUtility;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.DAL.Common
{
    public class PreferenceConfigDAL
    {
        private OperationsLogDAL _logDal = new OperationsLogDAL();

        /// <summary>
        /// 根据用户id和页面代码找到对应编号数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageCode"></param>
        /// <returns></returns>
        public Model.Common.PreferenceConfigModel GetModel(int userId, string pageCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,AdminID,CreateTime,UpdateTime,PageCode,ColumnShow,GroupParam,SearchParam,SearchShow from PreferenceConfig ");
            strSql.Append(" where AdminID=@userId and PageCode=@pageCode");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@pageCode", SqlDbType.VarChar)
};
            parameters[0].Value = userId;
            parameters[1].Value = pageCode;

            PreferenceConfigModel model = new PreferenceConfigModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"] != null && ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PageCode"] != null && ds.Tables[0].Rows[0]["PageCode"].ToString() != "")
                {
                    model.PageCode = ds.Tables[0].Rows[0]["PageCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ColumnShow"] != null && ds.Tables[0].Rows[0]["ColumnShow"].ToString() != "")
                {
                    model.ColumnShow = ds.Tables[0].Rows[0]["ColumnShow"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GroupParam"] != null && ds.Tables[0].Rows[0]["GroupParam"].ToString() != "")
                {
                    model.GroupParam = ds.Tables[0].Rows[0]["GroupParam"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SearchParam"] != null && ds.Tables[0].Rows[0]["SearchParam"].ToString() != "")
                {
                    model.SearchParam = ds.Tables[0].Rows[0]["SearchParam"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SearchShow"] != null && ds.Tables[0].Rows[0]["SearchShow"].ToString() != "")
                {
                    model.SearchShow = ds.Tables[0].Rows[0]["SearchShow"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据个人偏好实体新增个人偏好
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddOrUpdatePreferenceConfig(PreferenceConfigModel model)
        {
            PreferenceConfigModel oldModel = GetModel(model.AdminID, model.PageCode);//获取到解码了的实体

            //不存在新增
            if (oldModel == null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into PreferenceConfig(");
                strSql.Append("AdminID,ColumnShow,GroupParam,PageCode,SearchParam,SearchShow,CreateTime,UpdateTime)");
                strSql.Append(" values (");
                strSql.Append("@AdminID,@ColumnShow,@GroupParam,@PageCode,@SearchParam,@SearchShow,@CreateTime,@UpdateTime)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@ColumnShow", SqlDbType.VarChar,1024),
					new SqlParameter("@GroupParam", SqlDbType.VarChar,1024),
					new SqlParameter("@PageCode", SqlDbType.VarChar,128),
                    new SqlParameter("@SearchParam", SqlDbType.VarChar,1024),
                    new SqlParameter("@SearchShow", SqlDbType.VarChar,1024),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
                parameters[0].Value = model.AdminID;
                parameters[1].Value = model.ColumnShow;
                parameters[2].Value = model.GroupParam;
                parameters[3].Value = model.PageCode;
                parameters[4].Value = model.SearchParam;
                parameters[5].Value = model.SearchShow;
                parameters[6].Value = DateTime.Now;
                parameters[7].Value = DateTime.Now;

                int AddID = Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
                if (AddID > 0)
                {
                    //增加操作日志
                    string logSql = _logDal.Add("PreferenceConfig", "新增个人偏好 ：" + strSql.ToString(), parameters, AddID.ToString(), (int)OperationsType.Add);
                    DbHelperSQL.ExecuteSql(logSql);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else//存在则更新
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PreferenceConfig set ");
                strSql.Append("AdminID=@AdminID,");
                strSql.Append("ColumnShow=@ColumnShow,");
                strSql.Append("GroupParam=@GroupParam,");
                strSql.Append("PageCode=@PageCode,");
                strSql.Append("SearchParam=@SearchParam,");
                strSql.Append("SearchShow=@SearchShow,");
                strSql.Append("UpdateTime=@UpdateTime");
                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@ColumnShow", SqlDbType.VarChar,1024),
					new SqlParameter("@GroupParam", SqlDbType.VarChar,1024),
					new SqlParameter("@PageCode", SqlDbType.VarChar,128),
                    new SqlParameter("@SearchParam", SqlDbType.VarChar,1024),
                    new SqlParameter("@SearchShow", SqlDbType.VarChar,1024),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
                parameters[0].Value = model.AdminID;
                parameters[1].Value = model.ColumnShow;
                parameters[2].Value = model.GroupParam;
                parameters[3].Value = model.PageCode;
                parameters[4].Value = model.SearchParam;
                parameters[5].Value = model.SearchShow;
                parameters[6].Value = DateTime.Now;
                parameters[7].Value = oldModel.ID;
                //增加操作日志
                string msg = "修改个人偏好。";
                msg += CommonFunction.ContrastTowObj(oldModel, model);  //获取修改前和修改后的数据。
                strSql.Append(_logDal.Add("PreferenceConfig", msg, oldModel.ID.ToString(), (int)OperationsType.Edit));
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
        }
    }
}
