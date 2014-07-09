using System;
using System.Data;
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Model.Common;
namespace Semitron_OMS.BLL
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public partial class SystemLog
    {
        private readonly Semitron_OMS.DAL.Common.SystemLogDAL dal = new Semitron_OMS.DAL.Common.SystemLogDAL();
        public SystemLog()
        { }


        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int LogID)
        {
            return dal.Exists(LogID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemLogModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SystemLogModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int LogID)
        {

            return dal.Delete(LogID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string LogIDlist)
        {
            return dal.DeleteList(LogIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemLogModel GetModel(int LogID)
        {

            return dal.GetModel(LogID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemLogModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemLogModel> DataTableToList(DataTable dt)
        {
            List<SystemLogModel> modelList = new List<SystemLogModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SystemLogModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SystemLogModel();
                    if (dt.Rows[n]["LogID"] != null && dt.Rows[n]["LogID"].ToString() != "")
                    {
                        model.LogID = int.Parse(dt.Rows[n]["LogID"].ToString());
                    }
                    if (dt.Rows[n]["LogLevel"] != null && dt.Rows[n]["LogLevel"].ToString() != "")
                    {
                        model.LogLevel = dt.Rows[n]["LogLevel"].ToString();
                    }
                    if (dt.Rows[n]["Msg"] != null && dt.Rows[n]["Msg"].ToString() != "")
                    {
                        model.Msg = dt.Rows[n]["Msg"].ToString();
                    }
                    if (dt.Rows[n]["LogThread"] != null && dt.Rows[n]["LogThread"].ToString() != "")
                    {
                        model.LogThread = dt.Rows[n]["LogThread"].ToString();
                    }
                    if (dt.Rows[n]["Exception"] != null && dt.Rows[n]["Exception"].ToString() != "")
                    {
                        model.Exception = dt.Rows[n]["Exception"].ToString();
                    }
                    if (dt.Rows[n]["Logger"] != null && dt.Rows[n]["Logger"].ToString() != "")
                    {
                        model.Logger = dt.Rows[n]["Logger"].ToString();
                    }
                    if (dt.Rows[n]["CreateTime"] != null && dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method

        /// <summary>
        /// 分页获取系统日志信息
        /// </summary>
        /// <param name="pageSearchInfo">SQL条件过滤器</param>
        /// <param name="o_RowsCount">查询总数</param>
        /// <returns></returns>
        public DataSet GetSystemLogPageData(PageSearchInfo pageSearchInfo, out int o_RowsCount)
        {
            return dal.GetSystemLogPageData(pageSearchInfo, out o_RowsCount);
        }
    }
}

