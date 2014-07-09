using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Common;//Please add references
namespace Semitron_OMS.DAL.Common
{
    /// <summary>
    /// 数据访问类:BusinessPathManageDAL
    /// </summary>
    public partial class BusinessPathManageDAL
    {
        public BusinessPathManageDAL()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "BusinessPathManage");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BusinessPathManage");
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
        public int Add(Semitron_OMS.Model.Common.BusinessPathManageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BusinessPathManage(");
            strSql.Append("SavePathName,PathURL,Remark,AvailFlag,Type,CreateTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@SavePathName,@PathURL,@Remark,@AvailFlag,@Type,@CreateTime,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SavePathName", SqlDbType.NVarChar,32),
                    new SqlParameter("@PathURL",SqlDbType.NVarChar,215),
					new SqlParameter("@Remark", SqlDbType.NVarChar,512),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
                    new SqlParameter("@Type",SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.SavePathName;
            parameters[1].Value = model.PathURL;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.AvailFlag;
            parameters[4].Value = model.Type;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.UpdateTime;

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
        public bool Update(Semitron_OMS.Model.Common.BusinessPathManageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BusinessPathManage set ");
            strSql.Append("SavePathName=@SavePathName,");
            strSql.Append("PathURL=@PathURL,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AvailFlag=@AvailFlag,");
            strSql.Append("Type=@Type,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=GetDate()");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@SavePathName", SqlDbType.NVarChar,32),
                    new SqlParameter("@PathURL",SqlDbType.NVarChar,512),
					new SqlParameter("@Remark", SqlDbType.NVarChar,512),
					new SqlParameter("@AvailFlag", SqlDbType.Int,4),
                    new SqlParameter("@Type",SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.SavePathName;
            parameters[1].Value = model.PathURL;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.AvailFlag;
            parameters[4].Value = model.Type;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.ID;

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
            strSql.Append("delete from BusinessPathManage ");
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
            strSql.Append("delete from BusinessPathManage ");
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
        public Semitron_OMS.Model.Common.BusinessPathManageModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SavePathName,PathURL,Remark,AvailFlag,Type,CreateTime,UpdateTime from BusinessPathManage ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Semitron_OMS.Model.Common.BusinessPathManageModel model = new Semitron_OMS.Model.Common.BusinessPathManageModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SavePathName"] != null && ds.Tables[0].Rows[0]["SavePathName"].ToString() != "")
                {
                    model.SavePathName = ds.Tables[0].Rows[0]["SavePathName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PathURL"] != null && ds.Tables[0].Rows[0]["PathURL"].ToString() != "")
                {
                    model.PathURL = ds.Tables[0].Rows[0]["PathURL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AvailFlag"] != null && ds.Tables[0].Rows[0]["AvailFlag"].ToString() != "")
                {
                    model.AvailFlag = int.Parse(ds.Tables[0].Rows[0]["AvailFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"] != null && ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
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
            strSql.Append("select ID,SavePathName,PathURL,Remark,AvailFlag,Type,CreateTime,UpdateTime ");
            strSql.Append(" FROM BusinessPathManage ");
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
            strSql.Append(" ID,SavePathName,PathURL,Remark,AvailFlag,Type,CreateTime,UpdateTime ");
            strSql.Append(" FROM BusinessPathManage ");
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
            strSql.Append("select count(1) FROM BusinessPathManage ");
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
            strSql.Append(")AS Row, T.*  from BusinessPathManage T ");
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
            parameters[0].Value = "BusinessPathManage";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region Add Method
        /// <summary>
        /// 分页查询业务路径数据
        /// </summary>
        /// <param name="pageSearchInfo">SQL条件过滤器</param>
        /// <param name="o_RowsCount">总查询条数</param>
        /// <returns>DataSet数据集合</returns>
        public DataSet GetBusinnessPathPageData(PageSearchInfo pageSearchInfo, out int o_RowsCount)
        {
            StringBuilder sbGetFields = new StringBuilder();
            sbGetFields.AppendLine("ID,CASE AvailFlag WHEN '0' THEN '无效' WHEN '1' THEN '有效' END AS AvailFlag,");
            sbGetFields.AppendLine("CASE Type WHEN '1' THEN 'SP' WHEN '2' THEN 'CP' END AS Type,");
            sbGetFields.AppendLine("SavePathName,PathURL,Remark,");
            sbGetFields.AppendLine("CONVERT(VARCHAR, CreateTime, 120) AS CreateTime,");
            sbGetFields.AppendLine("CONVERT(VARCHAR, UpdateTime, 120) AS UpdateTime");

            //查询列名
            string strGetFields = sbGetFields.ToString();
            //查询表名称
            string strTableName = "dbo.BusinessPathManage";

            //获取查询条件语句
            string strWhere = SQLOperateHelper.GetSQLCondition(pageSearchInfo.ConditionFilter, false);

            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt("Pro_GetDataExt",
                strTableName,
                strGetFields,
                pageSearchInfo.PageSize,
                pageSearchInfo.PageIndex,
                strWhere,
                pageSearchInfo.OrderByField,
                pageSearchInfo.OrderType,
                out o_RowsCount);
        }

        /// <summary>
        /// 删除业务路径
        /// </summary>
        public bool DelBusinessPath(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE dbo.BusinessPathManage SET AvailFlag=0,UpdateTime=getdate() ");
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
        /// 判断是否存在此业务名称
        /// </summary>
        public bool PathNameExists(string pathName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM dbo.BusinessPathManage");
            strSql.Append(" WHERE SavePathName='" + pathName + "' AND AvailFlag=1");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 判断是否存在此业务路径
        /// </summary>
        public bool PathURLExists(string pathURL)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM dbo.BusinessPathManage");
            strSql.Append(" WHERE PathURL='" + pathURL + "' AND AvailFlag=1");
            return DbHelperSQL.Exists(strSql.ToString());
        }
        #endregion
    }
}

