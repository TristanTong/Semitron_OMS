using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Semitron_OMS.DAL.Common;
using Semitron_OMS.Common;
using Semitron_OMS.Model.Common;
using System.Data.SqlClient;
using System.Web;

namespace Semitron_OMS.BLL.Common
{
    public class CommonBLL
    {
        CommonDAL dal = new CommonDAL();
        /// <summary>
        /// 调用通用存储过程
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="intPageCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string tablename, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int intPageCount)
        {
            if (string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(strGetFields) || string.IsNullOrEmpty(strOrder))
            {
                intPageCount = 0;
                return null;
            }

            return dal.GetData(tablename, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  intPageCount);
        }

        /// <summary>
        /// 调用通用存储过程
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="intPageCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetDataP(string tablename, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int intPageCount)
        {
            if (string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(strGetFields) || string.IsNullOrEmpty(strOrder))
            {
                intPageCount = 0;
                return null;
            }

            return dal.GetDataP(tablename, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  intPageCount);
        }

        /// <summary>
        /// 开启事务。
        /// </summary>
        public void BeginTransaction()
        {
            dal.BeginTransaction();
        }

        /// <summary>
        /// 提交事务。
        /// </summary>
        public void CommitTransaction()
        {
            dal.CommitTransaction();
        }

        /// <summary>
        /// 回滚事务。
        /// </summary>
        public void RollbackTransaction()
        {
            dal.RollbackTransaction();
        }


        public DataSet Query(string strSql)
        {
            return dal.Query(strSql);
        }
        /// <summary>
        /// 根据表名获取索引
        /// </summary>
        /// <param name="tblName"></param>
        /// <returns></returns>
        public int[] GetNumIndex(string tblName, int size)
        {
            return dal.GetNumIndex(tblName, size);
        }


        /// <summary>
        /// 使用事务执行
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        public bool ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            return dal.ExecuteSqlTran(cmdList);
        }

        /// <summary>
        /// 使用事务执行
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        //public bool ExecuteSqlTran(string connectionstring, List<CommandInfo> cmdList)
        //{
        //    return dal.ExecuteSqlTran(connectionstring, cmdList);
        //}

        public bool UpdateTable(DataTable dt)
        {
            return dal.UpdateTable(dt);
        }

        public DataSet ExecProcedure(string sql)
        {
            return dal.ExecProcedure(sql);
        }

        /// <summary>
        /// 执行支持分页的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="iRowsCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetDataExt(string proName, string tableName, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }

            return dal.GetDataExt(proName, tableName, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的存储过程
        /// </summary>
        /// <param name="strConnectStringName">数据库连接字符串名称</param>
        /// <param name="proName">存储过程名</param>
        /// <param name="tablename">表名</param>
        /// <param name="strGetFields">查询列名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序列名</param>
        /// <param name="OrderType">排序类型，0为升序，默认降序</param>
        /// <param name="iRowsCount">返回记录总数</param>
        /// <returns>数据集</returns>
        public DataSet GetDataExt(string strConnectStringName, string proName, string tableName, string strGetFields, int pageSize, int PageIndex,
            string strWhere, string strOrder, int OrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }

            return dal.GetDataExt(strConnectStringName, proName, tableName, strGetFields, pageSize, PageIndex,
             strWhere, strOrder, OrderType, out  iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的参数可动态组建的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="dicSearch">查询条件键值对字典</param>
        /// <param name="dicGroup">组条件键值对字典</param>
        /// <param name="iPageSize">每页条数</param>
        /// <param name="iPageIndex">页索引</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="iOrderType">排序类型 0:升序 1:降序</param>
        /// <param name="iRowsCount">数据总数</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string proName, Dictionary<string, string> dicSearch, Dictionary<string, string> dicGroup, int iPageSize, int iPageIndex, string strOrderField, int iOrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }
            return dal.GetData(proName, dicSearch, dicGroup, iPageSize, iPageIndex, strOrderField, iOrderType, out iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的参数可动态组建的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="dicSearch">查询条件键值对字典</param>
        /// <param name="dicGroup">组条件键值对字典</param>
        /// <param name="iPageSize">每页条数</param>
        /// <param name="iPageIndex">页索引</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="iOrderType">排序类型 0:升序 1:降序</param>
        /// <param name="iRowsCount">数据总数</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string proName, string strvalue, Dictionary<string, string> dicSearch, Dictionary<string, string> dicGroup, int iPageSize, int iPageIndex, string strOrderField, int iOrderType, out int iRowsCount)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }
            return dal.GetData(proName, strvalue, dicSearch, dicGroup, iPageSize, iPageIndex, strOrderField, iOrderType, out iRowsCount);
        }

        /// <summary>
        /// 执行支持分页的参数可动态组建的存储过程
        /// </summary>
        /// <param name="proName">存储过程名</param>
        /// <param name="dicSearch">查询条件键值对字典</param>
        /// <param name="dicGroup">组条件键值对字典</param>
        /// <param name="iPageSize">每页条数</param>
        /// <param name="iPageIndex">页索引</param>
        /// <param name="strOrderField">排序字段</param>
        /// <param name="iOrderType">排序类型 0:升序 1:降序</param>
        /// <param name="iRowsCount">数据总数</param>
        /// <param name="strConnectStringName">数据库查询字符串</param>
        /// <returns>数据集</returns>
        public DataSet GetData(string proName, Dictionary<string, string> dicSearch, Dictionary<string, string> dicGroup, int iPageSize, int iPageIndex, string strOrderField, int iOrderType, out int iRowsCount, string strConnectStringName)
        {
            if (string.IsNullOrEmpty(proName))
            {
                iRowsCount = 0;
                return null;
            }
            return dal.GetData(proName, dicSearch, dicGroup, iPageSize, iPageIndex, strOrderField, iOrderType, out iRowsCount, strConnectStringName);
        }

        /// <summary>
        /// 分页获取商务交易总表
        /// </summary>
        /// <returns></returns>
        public string GetReportCommon(HttpRequest request, string proName, string strTableName)
        {
            int iPageIndex = 1;          //页码
            int iPageSize = 1;           //页大小
            int iRowsCount = 0;
            string strOrderField = string.Empty;
            int iOrderType = 0;

            string strSearchParam = string.Empty;
            string strGroupParam = string.Empty;
            Dictionary<string, string> dicSearch = new Dictionary<string, string>();
            Dictionary<string, string> dicGroup = new Dictionary<string, string>();
            DataTable dt = new DataTable();
            DataTable dtCurrentPage = new DataTable();
            //获取表格提交的参数
            if (request.Form["page"] != null)
            {
                iPageIndex = int.Parse(request.Form["page"].ToString().Trim());
            }
            if (request.Form["rp"] != null)
            {
                iPageSize = int.Parse(request.Form["rp"].ToString().Trim());
            }
            if (request.Form["sortname"] != null)
            {
                strOrderField = request.Form["sortname"].ToString().Trim();
            }
            if (request.Form["sortorder"] != null)
            {
                iOrderType = request.Form["sortorder"].ToString().ToLower() == "asc" ? 0 : 1;
            }

            string strType = DataUtility.GetPageFormValue(request.Form["Type"], string.Empty);
            dicSearch.Add("SearchType", strType);
            //获取查询与分组的参数字符串键值对形式，用“，”分开，值用':'分开。
            //如：SpID:,ServiceCodeID:,FeeCodeID:,RouterInfoID:,BeginTime:,EndTime:,TimeType:1,AreaId:1,OperatesType:,FeeType:
            //Province:1,City:1,Cp:1,Sp:1,ServiceCode:1,FeeCode:1,Router:1,OperateType:1,FeeType:1,Date:1

            if (request.Form["SearchParam"] != null && request.Form["GroupParam"] != null)
            {
                strSearchParam = request.Form["SearchParam"].ToString().Trim();
                strGroupParam = request.Form["GroupParam"].ToString().Trim();
                string[] arrStrSearch = strSearchParam.Split(',');
                if (strSearchParam.Length > 0)
                {
                    foreach (string strSearch in arrStrSearch)
                    {
                        string[] arrStrTemp = strSearch.Split(':');
                        if (arrStrTemp.Length == 2)
                        {
                            dicSearch.Add(arrStrTemp[0], arrStrTemp[1]);
                        }
                        else if (arrStrTemp.Length > 2)
                        {
                            int index = strSearch.IndexOf(':');
                            dicSearch.Add(arrStrTemp[0], strSearch.Substring(index + 1));
                        }
                    }
                }
                string[] arrStrGroup = strGroupParam.Split(',');
                if (arrStrGroup.Length > 0)
                {
                    foreach (string strGroup in arrStrGroup)
                    {
                        string[] arrStrTemp = strGroup.Split(':');
                        if (arrStrTemp.Length == 2)
                        {
                            dicGroup.Add(arrStrTemp[0], arrStrTemp[1]);
                        }
                    }
                }

                try
                {
                    DataSet ds = new DataSet();
                    CommonBLL bll = new CommonBLL();
                    ds = bll.GetData(proName, dicSearch, dicGroup, 0, 0, strOrderField, iOrderType, out iRowsCount);//使用从DataTable提取分页数据
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        iRowsCount = dt.Rows.Count;
                        //SumDataTable(dtCurrentPage, string.Empty, "本页小计");
                        //dt = SumTotalDataTable(dt, dtCurrentPage, string.Empty, "合计");
                    }
                }
                catch (Exception ex)
                {
                    dt = new DataTable();
                }
            }

            if (string.IsNullOrEmpty(strType))
            {
                dtCurrentPage = CommonFunction.GetCurrentPageTable(dt, iPageSize, iPageIndex);
                string strCols = DataUtility.GetPageFormValue(request.Form["colNames"], string.Empty);
                return JsonJqgrid.JsonForJqgrid(dtCurrentPage.SortDataTableCols(strCols), iPageIndex, iRowsCount);
            }

            //导出Excel
            PageResult result = new PageResult();
            if (strType == "ExportExcel")
            {
                try
                {
                    FileExcelDAl fileDLL = new FileExcelDAl();
                    string filename = strTableName + "导出" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    fileDLL.CreateFile(strTableName + "导出", filename, dt);
                    string filepath = "/file_system/ExportExcelFile/" + filename;
                    //增加操作日志
                    OperationsLogBLL bllOL = new OperationsLogBLL();
                    bool bA = bllOL.AddExecute(proName, filename, "", (int)OperationsType.Export);
                    if (bA)
                    {
                        result.State = 1;
                        result.Remark = filepath;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = 0;
                    result.Info = "数据量过大，无法生成相应Excel文件。请细化地区查询后导出。";
                }
                catch (Exception e)
                {
                    result.State = 0;
                    result.Info = "无法生成相应Excel文件。出现异常：" + e.Message;
                }
            }
            return result.ToString();
        }
    }
}
