using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Data;
using Semitron_OMS.BLL;
using Semitron_OMS.Model;
using System.Web.SessionState;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Semitron_OMS.Model.Common;

namespace Semitron_OMS.UI.Handle.Sys
{
    /// <summary>
    /// Role 的摘要说明
    /// </summary>
    public class Role : IHttpHandler, IRequiresSessionState
    {
        Semitron_OMS.BLL.Common.Role rl = new Semitron_OMS.BLL.Common.Role();
        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Admin"] == null)
            {
                PageResult result = new PageResult();
                context.Response.Write(result.ToString());
                return;
            }
            context.Response.ContentType = "application/json";
            string methStr = string.Empty;
            if (context.Request.Form["meth"] != null)
            {
                methStr = context.Request.Form["meth"];
            }
            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    //根据条件获得角色列表
                    case "GetRole":
                        context.Response.Write(GetRole());
                        break;
                    //新增角色
                    case "AddRole":
                        context.Response.Write(AddRole());
                        break;
                    //修改角色
                    case "UpdateRole":
                        context.Response.Write(UpdateRole());
                        break;
                    //删除角色
                    case "DeleteRole":
                        context.Response.Write(DeleteRole());
                        break;
                    //绑定角色
                    case "Bind":
                        context.Response.Write(Bind());
                        break;
                    //获取所有角色
                    case "GetAllByRole":
                        context.Response.Write(GetAllByRole());
                        break;
                    //修改时查询最新数据
                    case "SelectUpdate":
                        context.Response.Write(SelectUpdate(context));
                        break;
                }
            }
            context.Response.End();
        }
        /// <summary>
        /// 根据条件获得角色列表
        /// </summary>
        /// <returns></returns>
        private string GetRole()
        {
            HttpRequest request = HttpContext.Current.Request;
            //查询条件信息
            PageSearchInfo searchInfo = new PageSearchInfo();
            //返回查询总数总数
            int o_RowsCount = 0;
            //SQL条件过滤器集合
            List<SQLConditionFilter> lstFilter = new List<SQLConditionFilter>();

            //获取表格提交的参数
            //当前查询页码
            searchInfo.PageIndex = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["page"], 1));
            //每页的大小
            searchInfo.PageSize = DataUtility.ToInt(DataUtility.GetPageFormValue(request.Form["rp"], 20));
            //排序字段
            searchInfo.OrderByField = DataUtility.GetPageFormValue(request.Form["sortname"], string.Empty);
            //排序类型
            searchInfo.OrderType = DataUtility.ToStr(request.Form["sortorder"]).ToUpper() == "ASC" ? 0 : 1;

            //角色名
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("RoleName", request.Form["User"], ConditionEnm.Equal));
            //状态
            SQLOperateHelper.AddSQLFilter(lstFilter, SQLOperateHelper.GetSQLFilter("AvailFlag", request.Form["Valid"], ConditionEnm.Equal));

            searchInfo.ConditionFilter = lstFilter;

            //查询数据
            DataSet ds = rl.GetRolePageData(searchInfo, out o_RowsCount);
            //转化JSON格式
            return JsonJqgrid.JsonForJqgrid(ds.Tables[0], searchInfo.PageIndex, o_RowsCount);
        }
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <returns></returns>
        private string AddRole()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            RoleModel rm = new RoleModel();
            if (request.Form["RoleName"] == null || request.Form["Description"] == null)
            {
                result.Info = "系统错误，参数获取异常!";
                return result.ToString();
            }
            //角色名
            SQLOperateHelper.SetEntityFiledValue(rm, "RoleName", request.Form["RoleName"]);
            //角色描述
            SQLOperateHelper.SetEntityFiledValue(rm, "Description", request.Form["Description"]);
            rm.AvailFlag = 1;
            if (!rl.ExistsRoleName(rm.RoleName))
            {
                if (rl.AddRole(rm))
                {
                    result.State = 1;
                    result.Info = "添加成功!";
                }
            }
            else
            {
                result.Info = "角色已经存在,请重新输入!";
            }
            return result.ToString();
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        private string UpdateRole()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            RoleModel rm = new RoleModel();
            if (request.Form["RoleName"] == null || request.Form["Description"] == null)
            {
                result.Info = "系统错误，参数获取异常!";
                return result.ToString();
            }
            //角色名
            SQLOperateHelper.SetEntityFiledValue(rm, "RoleName", request.Form["RoleName"]);
            //角色描述
            SQLOperateHelper.SetEntityFiledValue(rm, "Description", request.Form["Description"]);
            if (request.Form["id"].ToString() != string.Empty)
            {
                rm.RoleID = int.Parse(request.Form["id"].ToString());
            }
            rm.AvailFlag = 1;
            if (!rl.ExistsRoleName(rm.RoleName, rm.RoleID))
            {
                if (rl.Update(rm))
                {
                    result.State = 1;
                    result.Info = "修改成功!";
                }
            }
            else
            {
                result.Info = "角色已存在,请重新输入!";
            }
            return result.ToString();
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        private string DeleteRole()
        {
            PageResult resulr = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            if (request.Form["id"] == null)
            {
                resulr.Info = "系统错误，获取参数失败!";
            }
            if (request.Form["id"].ToString() != string.Empty)
            {
                if (rl.UpdateRl(int.Parse(request.Form["id"])))
                {
                    resulr.State = 1;
                    resulr.Info = "删除成功!";
                }
                else
                {
                    resulr.Info = "删除失败!";
                }
            }
            return resulr.ToString();
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        private string GetAllByRole()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            if (request.Form["CodeId"] == null)
            {
                result.Info = "参赛异常";
                return result.ToString();
            }
            int Id = int.Parse(request.Form["CodeId"].ToString());
            DataSet ds = rl.GetListByFeeCode(Id);
            return JsonZTree.ForRole(ds.Tables[0]);
        }
        /// <summary>
        /// 绑定角色
        /// </summary>
        /// <returns></returns>
        private string Bind()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            if (request.Form["AreaIdList"] == null || request.Form["FeeCodeId"] == null || request.Form["AreaNameList"] == null)
            {
                result.Info = "参数异常。";
                return result.ToString();
            }
            string CodeId = request.Form["FeeCodeId"].ToString();
            string AreaIdList = request.Form["AreaIdList"].ToString();
            string AreaName = request.Form["AreaNameList"].ToString();
            string[] AreasId = AreaIdList.Split(',');
            result.Info = "关联角色操作失败。";
            if (rl.BindFeeCodeInfo(CodeId, AreasId, AreaName))
            {
                result.State = 1;
                result.Info = "关联角色操作成功。";
            }
            return result.ToString();
        }
        //修改时查询最新数据
        private string SelectUpdate(HttpContext context)
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            Semitron_OMS.BLL.Common.Role role = new Semitron_OMS.BLL.Common.Role();
            if (request.Form["id"] == null || request.Form["id"].ToString() == string.Empty)
            {
                result.Info = "ID参数异常。";
                return result.ToString();
            }
            int RoleId = int.Parse(request.Form["id"].ToString());
            RoleModel model = role.GetModel(RoleId);

            result.State = 1;
            result.Remark = JsonConvert.SerializeObject(model, Formatting.Indented, new IsoDateTimeConverter());
            return result.Remark.ToString();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}