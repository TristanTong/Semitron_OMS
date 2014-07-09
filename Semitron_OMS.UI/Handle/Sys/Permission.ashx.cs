using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semitron_OMS.Common;
using System.Data;
using System.Text;
using Semitron_OMS.Model;
using Semitron_OMS.BLL;
using System.Web.SessionState;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Semitron_OMS.Model.Common;
using Semitron_OMS.BLL.Common;
using System.Configuration;

namespace Semitron_OMS.UI.Handle.Sys
{
    /// <summary>
    /// Permission 的摘要说明
    /// </summary>
    public class Permission : IHttpHandler, IRequiresSessionState
    {

        Semitron_OMS.BLL.Common.Permission ps = new Semitron_OMS.BLL.Common.Permission();
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
            if (context.Request.QueryString["meth"] != null)
            {
                methStr = context.Request.QueryString["meth"];
            }

            if (!string.IsNullOrEmpty(methStr))
            {
                switch (methStr)
                {
                    // 新增权限
                    case "InsertPermission":
                        context.Response.Write(InsertPermission());
                        break;
                    //修改权限表
                    case "UpdatePermission":
                        context.Response.Write(UpdatePermission());
                        break;
                    //删除权限
                    case "Delete":
                        context.Response.Write(DeletePermission());
                        break;
                    // 绑定权限
                    case "Bind":
                        context.Response.Write(Bind());
                        break;
                    // 获取所有权限
                    case "GetAllPermission":
                        context.Response.Write(GetAllPermission());
                        break;
                    //获取所有权限信息
                    case "AreaListForJson":
                        context.Response.Write(GetAllAreaForJson());
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
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        private string GetAllPermission()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            if (request.Form["CodeId"] == null)
            {
                result.Info = "参赛异常";
                return result.ToString();
            }
            int Id = int.Parse(request.Form["CodeId"].ToString());
            int ObjType = Convert.ToInt32(request.Form["ObjType"]);
            DataSet ds = ps.GetListByFeeCode(Id, ObjType);
            return JsonZTree.ForOperationsLog(ds.Tables[0]);
        }
        /// <summary>
        /// 获取所有权限信息，转换成json格式（ck）
        /// </summary>
        /// <returns></returns>
        private string GetAllAreaForJson()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            DataSet ds = ps.GetList(" AvailFlag=1");
            return JsonZTree.ForPermission(ds.Tables[0]);
        }
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <returns></returns>
        private string InsertPermission()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            PermissionModel pm = new PermissionModel();
            if (request.Form["Name"] == null || request.Form["Pid"] == null || request.Form["Control"] == null || request.Form["Describe"] == null || request.Form["Site"] == null || request.Form["Type"] == null)
            {
                result.Info = "参数获取错误!";
                return result.ToString();
            }
            //权限名称
            SQLOperateHelper.SetEntityFiledValue(pm, "Name", request.Form["Name"]);
            //权限编码
            SQLOperateHelper.SetEntityFiledValue(pm, "Code", request.Form["Control"]);
            //权限父结点
            SQLOperateHelper.SetEntityFiledValue(pm, "Pid", request.Form["Pid"]);
            //权限描述
            SQLOperateHelper.SetEntityFiledValue(pm, "Description", request.Form["Describe"]);
            //权限链接地址
            SQLOperateHelper.SetEntityFiledValue(pm, "LinkUrl", request.Form["Site"]);
            //权限链接地址
            SQLOperateHelper.SetEntityFiledValue(pm, "Type", request.Form["Type"]);
            //排序SK拼音字母
            SQLOperateHelper.SetEntityFiledValue(pm, "SK", CommonFunction.GetChineseSpell(pm.Name, 4));
            pm.AvailFlag = 1;
            if (ps.AddPermissionManage(pm))
            {
                result.State = 1;
                result.Info = "添加权限成功!";
            }
            else
            {
                result.Info = "添加权限失败!";
            }
            return result.ToString();
        }
        /// <summary>
        /// 修改权限表
        /// </summary>
        /// <returns></returns>
        public string UpdatePermission()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            PermissionModel pm = new PermissionModel();
            if (request.Form["txtName"] == null || request.Form["txtControl"] == null || request.Form["txtSite"] == null || request.Form["SelectType"] == null)
            {
                result.Info = "参数获取错误!";
                return result.ToString();
            }
            //权限ID
            SQLOperateHelper.SetEntityFiledValue(pm, "PermissionID", request.Form["id"]);
            pm = ps.GetModel(pm.PermissionID);
            //权限名称
            SQLOperateHelper.SetEntityFiledValue(pm, "Name", request.Form["txtName"]);
            //权限编码
            SQLOperateHelper.SetEntityFiledValue(pm, "Code", request.Form["txtControl"]);
            //权限描述
            SQLOperateHelper.SetEntityFiledValue(pm, "Description", request.Form["txtDescribe"]);
            //权限链接地址
            SQLOperateHelper.SetEntityFiledValue(pm, "LinkUrl", request.Form["txtSite"]);
            //权限链接地址
            SQLOperateHelper.SetEntityFiledValue(pm, "Type", request.Form["SelectType"]);
            //排序SK拼音字母
            SQLOperateHelper.SetEntityFiledValue(pm, "SK", CommonFunction.GetChineseSpell(pm.Name, 4));
            if (ps.UpdatePermission(pm))
            {
                result.State = 1;
                result.Info = "修改权限成功!";
            }
            else
            {
                result.Info = "修改权限失败!";
            }
            return result.ToString();
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <returns></returns>
        private string DeletePermission()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            if (request.Form["id"] == null)
            {
                result.Info = "获取参数错误!";
                return result.ToString();
            }
            if (request.Form["id"] != null && request.Form["id"] != string.Empty)
            {
                if (ps.DeletePermission(int.Parse(request.Form["id"])))
                {
                    result.State = 1;
                    result.Info = "删除成功!";
                }
            }
            return result.ToString();
        }
        /// <summary>
        /// 绑定权限
        /// </summary>
        /// <returns></returns>
        private string Bind()
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;
            ObjectPermission op = new ObjectPermission();
            if (request.Form["AreaIdList"] == null || request.Form["FeeCodeId"] == null)
            {
                result.Info = "参数异常。";
                return result.ToString();
            }
            string CodeId = request.Form["FeeCodeId"].ToString();
            string AreaIdList = request.Form["AreaIdList"].ToString();
            string AreaNameList = request.Form["AreaNameList"].ToString();
            int ObjType = Convert.ToInt32(request.Form["ObjType"]);
            string[] AreaName = AreaNameList.Split(',');
            string[] AreasId = AreaIdList.Split(',');
            result.Info = "关联权限操作失败。";
            if (op.BindFeeCodeInfo(CodeId, AreasId, AreaName, ObjType))
            {
                result.State = 1;
                result.Info = "关联权限操作成功。";
            }
            return result.ToString();
        }
        /// <summary>
        /// 修改时查询最新数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string SelectUpdate(HttpContext context)
        {
            PageResult result = new PageResult();
            HttpRequest request = HttpContext.Current.Request;

            Semitron_OMS.BLL.Common.Permission permission = new Semitron_OMS.BLL.Common.Permission();

            if (request.Form["id"] == null || request.Form["id"].ToString() == string.Empty)
            {
                result.Info = "ID参数异常。";
                return result.ToString();
            }
            int CodeId = int.Parse(request.Form["id"].ToString());
            PermissionModel model = permission.GetModel(CodeId);
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