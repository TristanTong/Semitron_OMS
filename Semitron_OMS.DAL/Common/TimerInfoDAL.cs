using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Semitron_OMS.DBUtility;
using Semitron_OMS.Model.Common;
using System.Data.SqlClient;
using Semitron_OMS.Common;

namespace Semitron_OMS.DAL.Common
{
    public class TimerInfoDAL
    {
        private OperationsLogDAL _logDal = new OperationsLogDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TimerInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TimerInfo(");
            strSql.Append("AdminId,CreateTime,DateValue,Day,Hour,Minute,Month,ParamValue,Second,Status,TaskId,TimerType,UpdateTime,Year,Result,NextRunTime,IsSubTimer,SubTimerId,TimerTaskName)");
            strSql.Append(" values (");
            strSql.Append("@AdminId,@CreateTime,@DateValue,@Day,@Hour,@Minute,@Month,@ParamValue,@Second,@Status,@TaskId,@TimerType,@UpdateTime,@Year,@Result,@NextRunTime,@IsSubTimer,@SubTimerId,@TimerTaskName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminId", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@DateValue", SqlDbType.Int,4),
					new SqlParameter("@Day", SqlDbType.Int,4),
					new SqlParameter("@Hour", SqlDbType.Int,4),
					new SqlParameter("@Minute", SqlDbType.Int,4),
					new SqlParameter("@Month", SqlDbType.Int,4),
					new SqlParameter("@ParamValue", SqlDbType.VarChar,1024),
					new SqlParameter("@Second", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@TaskId", SqlDbType.Int,4),
					new SqlParameter("@TimerType", SqlDbType.VarChar,32),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@Year", SqlDbType.Int,4) ,
                    new SqlParameter("@Result", SqlDbType.VarChar,512),
                    new SqlParameter("@NextRunTime", SqlDbType.DateTime),
					new SqlParameter("@IsSubTimer", SqlDbType.Int,4),
					new SqlParameter("@SubTimerId", SqlDbType.Int,4),
					new SqlParameter("@TimerTaskName", SqlDbType.VarChar,128)};
            parameters[0].Value = model.AdminId;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.DateValue;
            parameters[3].Value = model.Day;
            parameters[4].Value = model.Hour;
            parameters[5].Value = model.Minute;
            parameters[6].Value = model.Month;
            parameters[7].Value = model.ParamValue;
            parameters[8].Value = model.Second;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.TaskId;
            parameters[11].Value = model.TimerType;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.Year;
            parameters[14].Value = model.Result;
            parameters[15].Value = model.NextRunTime;
            parameters[16].Value = model.IsSubTimer;
            parameters[17].Value = model.SubTimerId;
            parameters[18].Value = model.TimerTaskName;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            int rows = 0;
            try
            {
                //日志
                rows = Convert.ToInt32(obj);
                string logSql = _logDal.Add("TimerInfo", "新增定时操作任务信息：" + strSql.ToString(), parameters, rows.ToString(), (int)OperationsType.Add);
                List<string> lstSql = new List<string>();
                lstSql.Add(logSql);

                //为任务操作对象增加一条操作记录，记录操作人的登陆Id。
                TaskInfoModel taskModel = new TaskInfoDAL().GetModel(model.TaskId);
                string strObjTable = string.Empty;
                string strTaskName = string.Empty;

                if (taskModel != null)
                {
                    strTaskName = taskModel.TaskName;
                    strObjTable = taskModel.ObjectTable;
                }
                string strPkId = string.Empty;
                string[] arr = model.ParamValue.Split('|');
                if (arr.Length > 0)
                {
                    string[] arrTemp = arr[0].Split('^');
                    if (arrTemp.Length == 2)
                    {
                        strPkId = arrTemp[1];
                    }
                }

                logSql = _logDal.Add(strObjTable.ToString(), "定时操作任务！类别：" + strTaskName + "；定时记录Id：" + rows.ToString() + "；定时任务名称：" + model.TimerTaskName + "；下次执行时间：" + model.NextRunTime + "；执行参数：" + model.ParamValue, parameters, strPkId, (int)OperationsType.Add);
                lstSql.Add(logSql);
                DbHelperSQL.ExecuteSqlTran(lstSql);

            }
            catch (Exception)
            {
                return Convert.ToInt32(obj);
            }
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
        /// 根据Id得到定时操作信息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimerInfoModel GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id , AdminId , TaskId , ParamValue , Status , TimerType , DateValue , Year , Month , Day , Hour , Minute , Second ,NextRunTime , Result, CreateTime , UpdateTime,IsSubTimer,SubTimerId,TimerTaskName from TimerInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", id)
};
            TimerInfoModel model = new TimerInfoModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AdminId"] != null && ds.Tables[0].Rows[0]["AdminId"].ToString() != "")
                {
                    model.AdminId = int.Parse(ds.Tables[0].Rows[0]["AdminId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DateValue"] != null && ds.Tables[0].Rows[0]["DateValue"].ToString() != "")
                {
                    model.DateValue = int.Parse(ds.Tables[0].Rows[0]["DateValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Day"] != null && ds.Tables[0].Rows[0]["Day"].ToString() != "")
                {
                    model.Day = int.Parse(ds.Tables[0].Rows[0]["Day"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Hour"] != null && ds.Tables[0].Rows[0]["Hour"].ToString() != "")
                {
                    model.Hour = int.Parse(ds.Tables[0].Rows[0]["Hour"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Minute"] != null && ds.Tables[0].Rows[0]["Minute"].ToString() != "")
                {
                    model.Minute = int.Parse(ds.Tables[0].Rows[0]["Minute"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Month"] != null && ds.Tables[0].Rows[0]["Month"].ToString() != "")
                {
                    model.Month = int.Parse(ds.Tables[0].Rows[0]["Month"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParamValue"] != null && ds.Tables[0].Rows[0]["ParamValue"].ToString() != "")
                {
                    model.ParamValue = ds.Tables[0].Rows[0]["ParamValue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Second"] != null && ds.Tables[0].Rows[0]["Second"].ToString() != "")
                {
                    model.Second = int.Parse(ds.Tables[0].Rows[0]["Second"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"] != null && ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TaskId"] != null && ds.Tables[0].Rows[0]["TaskId"].ToString() != "")
                {
                    model.TaskId = int.Parse(ds.Tables[0].Rows[0]["TaskId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TimerType"] != null && ds.Tables[0].Rows[0]["TimerType"].ToString() != "")
                {
                    model.TimerType = ds.Tables[0].Rows[0]["TimerType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TimerTaskName"] != null && ds.Tables[0].Rows[0]["TimerTaskName"].ToString() != "")
                {
                    model.TimerTaskName = ds.Tables[0].Rows[0]["TimerTaskName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"] != null && ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Year"] != null && ds.Tables[0].Rows[0]["Year"].ToString() != "")
                {
                    model.Year = int.Parse(ds.Tables[0].Rows[0]["Year"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Result"] != null && ds.Tables[0].Rows[0]["Result"].ToString() != "")
                {
                    model.Result = ds.Tables[0].Rows[0]["Result"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NextRunTime"] != null && ds.Tables[0].Rows[0]["NextRunTime"].ToString() != "")
                {
                    model.NextRunTime = DateTime.Parse(ds.Tables[0].Rows[0]["NextRunTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSubTimer"] != null && ds.Tables[0].Rows[0]["IsSubTimer"].ToString() != "")
                {
                    model.IsSubTimer = int.Parse(ds.Tables[0].Rows[0]["IsSubTimer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubTimerId"] != null && ds.Tables[0].Rows[0]["SubTimerId"].ToString() != "")
                {
                    model.SubTimerId = int.Parse(ds.Tables[0].Rows[0]["SubTimerId"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 按条件获取列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT	 Id , AdminId , TaskId , ParamValue , Status , TimerType , DateValue , Year , Month , Day , Hour , Minute , Second ,NextRunTime,Result , CreateTime , UpdateTime,IsSubTimer,SubTimerId,TimerTaskName ");
            strSql.Append(" FROM TimerInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="iStatus">状态值</param>
        public bool UpdateStatus(string strWhere, int iStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE  TimerInfo SET Status = " + iStatus + ",UpdateTime=GETDATE() WHERE " + strWhere);

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
        /// 根据条件更新数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="iStatus">状态值</param>
        public bool UpdateStatusNoTime(string strWhere, int iStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE  TimerInfo SET Status = " + iStatus + " WHERE " + strWhere);

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
        /// 根据条件更新数据
        /// </summary>
        /// <param name="strWhere">根据条件更新数据</param>
        /// <param name="iStatus">条件</param>
        /// <param name="strResult">任务执行结果</param>
        /// <param name="nextRunTime">下一次执行时间</param>
        public bool UpdateStatus(string strWhere, int iStatus, string strResult, DateTime nextRunTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE  TimerInfo SET Status = " + iStatus + ",Result='" + strResult + "',NextRunTime='" + nextRunTime + "',UpdateTime=GETDATE() WHERE " + strWhere);

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
        /// 更新表特定字段值
        /// </summary>
        /// <param name="strSet">语句Set部分</param>
        /// <param name="strWhere">语句Where部分</param>
        public string UpdateField(string strSet, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE  TimerInfo SET " + strSet);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(";");
            return strSql.ToString();

        }

        /// <summary>
        /// 执行UpdateSql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>是否成功</returns>
        public bool ExcuteUpdateSql(string strSql)
        {
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
        /// 分页获取系统日志信息
        /// </summary>
        /// <param name="pageSearchInfo">SQL条件过滤器</param>
        /// <param name="o_RowsCount">查询总数</param>
        /// <returns></returns>
        public DataSet GetTimerInfoPageData(Semitron_OMS.Common.PageSearchInfo pageSearchInfo, out int o_RowsCount)
        {
            //查询表名称
            string strTableName = "TimerInfo LEFT JOIN Admin ON TimerInfo.AdminId = Admin.AdminID LEFT JOIN TaskInfo ON TaskInfo.Id = TaskId";
            //查询列名
            string strGetFields = "TimerInfo.Id , UserName = Name , TaskName ,TimerTaskName, Status = (CASE Status WHEN 0 THEN '等待中' WHEN 1 THEN '执行中' WHEN 2 THEN '已完成' WHEN 3 THEN '用户结束' WHEN 4 THEN '用户删除' END ) ,NextRunTime,IsSubTimer=(CASE IsSubTimer WHEN 0 THEN '主任务' WHEN 1 THEN '子任务' END),SubTimerId,Result , TimerType = ( CASE TimerType WHEN 'DesDate' THEN '指定时间点' WHEN 'EveryDay' THEN '每天' WHEN 'DayOfWeek' THEN '每周' WHEN 'DayOfMonth' THEN '每月' WHEN 'LoopDays' THEN '循环天数' END ), DateValue , TimerDate = ( CASE WHEN Year IS NOT NULL THEN CONVERT(VARCHAR, Year) + '-' + CONVERT(VARCHAR, MONTH) + '-' + CONVERT(VARCHAR, DAY) ELSE '' END ) , TimerTime = CONVERT(VARCHAR, Hour) + ':' + CONVERT(VARCHAR, Minute) + ':' + CONVERT(VARCHAR, Second),ParamValue ,TimerInfo.UpdateTime , TimerInfo.CreateTime";

            //获取查询条件语句
            string strWhere = SQLOperateHelper.GetSQLCondition(pageSearchInfo.ConditionFilter, false);

            //数据查询
            CommonDAL commonDAL = new CommonDAL();
            return commonDAL.GetDataExt(strTableName, strGetFields,
                pageSearchInfo.PageSize,
                pageSearchInfo.PageIndex,
                strWhere,
                pageSearchInfo.OrderByField,
                pageSearchInfo.OrderType,
                out o_RowsCount);
        }
    }
}
