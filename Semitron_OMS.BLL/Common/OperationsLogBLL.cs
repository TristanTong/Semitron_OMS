using System;
using System.Data;
using System.Collections.Generic;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using Semitron_OMS.DAL.Common;
namespace Semitron_OMS.BLL.Common
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    public partial class OperationsLogBLL
    {
        private readonly OperationsLogDAL dal = new OperationsLogDAL();
        public OperationsLogBLL()
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
        public bool Exists(int OperateID)
        {
            return dal.Exists(OperateID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(OperationsLogModel model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="FormObject">模块</param>
        /// <param name="Msg">信息</param>
        /// <param name="PKID">主键</param>
        /// <param name="OperationsType">操作类型</param>
        /// <returns>是否成功，true：成功，false：失败</returns>
        public bool AddExecute(string FormObject, string Msg, string PKID, int OperationsType)
        {
            return dal.AddExecute(FormObject, Msg, PKID, OperationsType);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="FormObject">模块</param>
        /// <param name="OInfo">操作描述</param>
        /// <param name="Msg">信息</param>       
        /// <param name="PKID">主键</param>
        /// <param name="OperationsType">操作类型</param>
        /// <returns>是否成功，true：成功，false：失败</returns>
        public bool AddExecute(string FormObject, string OInfo, string Msg, string PKID, int OperationsType)
        {
            return dal.AddExecute(FormObject, OInfo, Msg, PKID, OperationsType);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(OperationsLogModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int OperateID)
        {

            return dal.Delete(OperateID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string OperateIDlist)
        {
            return dal.DeleteList(OperateIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OperationsLogModel GetModel(int OperateID)
        {

            return dal.GetModel(OperateID);
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
        public List<OperationsLogModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OperationsLogModel> DataTableToList(DataTable dt)
        {
            List<OperationsLogModel> modelList = new List<OperationsLogModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                OperationsLogModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OperationsLogModel();
                    if (dt.Rows[n]["OperateID"] != null && dt.Rows[n]["OperateID"].ToString() != "")
                    {
                        model.OperateID = int.Parse(dt.Rows[n]["OperateID"].ToString());
                    }
                    if (dt.Rows[n]["AdminID"] != null && dt.Rows[n]["AdminID"].ToString() != "")
                    {
                        model.AdminID = int.Parse(dt.Rows[n]["AdminID"].ToString());
                    }
                    if (dt.Rows[n]["OType"] != null && dt.Rows[n]["OType"].ToString() != "")
                    {
                        model.OType = int.Parse(dt.Rows[n]["OType"].ToString());
                    }
                    if (dt.Rows[n]["OMsg"] != null && dt.Rows[n]["OMsg"].ToString() != "")
                    {
                        model.OMsg = dt.Rows[n]["OMsg"].ToString();
                    }
                    if (dt.Rows[n]["CreateTime"] != null && dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["AdminName"] != null && dt.Rows[n]["AdminName"].ToString() != "")
                    {
                        model.AdminName = dt.Rows[n]["AdminName"].ToString();
                    }
                    if (dt.Rows[n]["OInfo"] != null && dt.Rows[n]["OInfo"].ToString() != "")
                    {
                        model.OInfo = dt.Rows[n]["OInfo"].ToString();
                    }
                    if (dt.Rows[n]["NickName"] != null && dt.Rows[n]["NickName"].ToString() != "")
                    {
                        model.NickName = dt.Rows[n]["NickName"].ToString();
                    }
                    if (dt.Rows[n]["PKID"] != null && dt.Rows[n]["PKID"].ToString() != "")
                    {
                        model.PKID = dt.Rows[n]["PKID"].ToString();
                    }
                    if (dt.Rows[n]["FormObject"] != null && dt.Rows[n]["FormObject"].ToString() != "")
                    {
                        model.FormObject = dt.Rows[n]["FormObject"].ToString();
                    }
                    if (dt.Rows[n]["AvailFlag"] != null && dt.Rows[n]["AvailFlag"].ToString() != "")
                    {
                        model.AvailFlag = int.Parse(dt.Rows[n]["AvailFlag"].ToString());
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
        /// 获得分页查询数据
        /// </summary>
        /// <param name="searchInfo">分布查询信息</param>
        /// <param name="o_RowsCount">查询结果行数</param>
        /// <returns>数据集</returns>
        public DataSet GetOperationsLogPageData(PageSearchInfo searchInfo, out int o_RowsCount)
        {
            return dal.GetOperationsLogPageData(searchInfo, out o_RowsCount);
        }
        #endregion  Method
    }
}

