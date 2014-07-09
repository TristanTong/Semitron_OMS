using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Semitron_OMS.Common
{
    /// <summary>
    /// 针对ZTree的JSON类
    /// </summary>
    public class JsonZTree
    {
        /// <summary>
        /// 获取所有的业务人员转成json格式
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static string ForBusinessList(DataTable dt, string fuzeren)
        {


            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (fuzeren.Contains(row["AdminId"].ToString()))
                {
                    string node = string.Format("{{\"id\":\"" + row["AdminID"] + "\",\"pId\":\"1\",\"name\":\"" + row["AdminName"] + "\",\"checked\":\"true\"}}");
                    treenodes.Add(node);
                }
                else
                {
                    string node = string.Format("{{\"id\":\"" + row["AdminID"] + "\",\"pId\":\"1\",\"name\":\"" + row["AdminName"] + "\",\"checked\":\"false\"}}");
                    treenodes.Add(node);

                }
            }
            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }



        /// <summary>
        /// 通道所关联的地区
        /// </summary>
        public static string ForAreaListByFeeCode(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string node = string.Format("{{ \"id\":{0}, \"pId\":{1}, \"name\":\"{2}\",\"isParent\":false,\"Code\":\"{3}\"}}",
                  dr["AreaId"].ToString(), dr["Pid"].ToString(), dr["AreaName"].ToString(), dr["AreaCode"].ToString());
                if (dr["pId"].ToString() == "0")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ", \"open\":true }";
                }
                if (dr["Checked"] != null && dr["Checked"].ToString() != "0")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ",\"checked\":true }";
                }
                treenodes.Add(node);
            }

            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }

        /// <summary>
        /// Sapayy
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ForSppayList(DataRow[] dr)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();
            for (int i = 0; i < dr.Count(); i++)
            {
                string AreaType = "";
                string AreaId = "";
                string AreaName = "";

                if (dr[i].Table.Columns.Contains("AreaId"))
                {
                    AreaType = "城市";
                    AreaId = dr[i]["AreaId"].ToString();
                    AreaName = dr[i]["City"].ToString();
                }
                else
                {
                    if (dr[i].Table.Columns.Contains("AreaPid"))
                    {
                        AreaType = "省份";
                        AreaId = dr[i]["AreaPid"].ToString();
                        AreaName = dr[i]["Province"].ToString();
                    }
                }

                string node = string.Format("{{ \"Addr\":\"{0}\", \"Povince\":\"{1}\",\"FeeCodeSuccessRate\":\"{2}\",\"sortYouxianji\":\"{3}\",\"Priority\":\"{4}\",\"AreaType\":\"{5}\",\"AreaId\":\"{6}\",\"CodeId\":\"{7}\"}}", dr[i]["Addr"].ToString(), AreaName, dr[i]["FeeCodeSuccessRate"], i + 1 > 5 ? 5 : i + 1, dr[i]["Priority"].ToString(), AreaType, AreaId, dr[i]["CodeId"].ToString());
                treenodes.Add(node);
            }
            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }

        /// <summary>
        /// 地区转换
        /// </summary>
        public static string ForAreaList(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string node = string.Format("{{ \"id\":{0}, \"pId\":{1}, \"name\":\"{2}\",\"isParent\":false, \"Code\":\"{3}\", \"YDSMSC\":\"{4}\",\"LTSMSC\":\"{5}\",\"DXSMSC\":\"{6}\",\"UserFeeMax\":\"{7}\",\"UserFirstFeeMax\":\"{8}\",\"UserSecondFeeMax\":\"{9}\",\"AvailFlag\":\"{10}\"}}",
                  dr["AreaId"].ToString(), dr["Pid"].ToString(), dr["AreaName"].ToString(), dr["AreaCode"].ToString(), dr["YDSMSC"].ToString(), dr["LTSMSC"].ToString(), dr["DXSMSC"].ToString(), dr["UserFeeMax"].ToString(), dr["UserFirstFeeMax"].ToString(), dr["UserSecondFeeMax"].ToString(), dr["AvailFlag"].ToString());
                if (dr["pId"].ToString() == "0")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ", \"open\":true }";
                }
                treenodes.Add(node);
            }

            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }

        //模板例
        public static string JsonForZTree(string PId, DataTable dt, int page, int total)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string node = string.Format("{{ \"id\":{0}, \"pId\":{1}, \"name\":\"{2}\",\"isParent\":false}}",
                  dr["GroupCode"].ToString(), dr["ParentCode"].ToString(), dr["Name"].ToString());
                treenodes.Add(node);
            }
            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";

        }

        /// <summary>
        /// 权限转换
        /// </summary>
        public static string ForPermission(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string node = string.Format("{{ \"id\":{0}, \"pId\":{1}, \"name\":\"{2}\",\"isParent\":false,\"Code\":\"{3}\",\"Description\":\"{4}\",\"Type\":\"{5}\",\"LinkUrl\":\"{6}\",\"AvailFlag\":{7}}}",
                  dr["PermissionID"].ToString(), dr["ParentSystem"].ToString(), dr["Name"].ToString(), dr["Code"].ToString(), dr["Description"].ToString(), dr["Type"].ToString(), dr["LinkUrl"].ToString(), dr["AvailFlag"].ToString());
                if (dr["PermissionID"].ToString() == "-1")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ", \"open\":true }";
                }
                treenodes.Add(node);
            }
            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }
        /// <summary>
        /// 用户角色关联
        /// </summary>
        public static string ForRole(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string node = string.Format("{{ \"id\":{0}, \"pId\":0, \"name\":\"{1}\",\"isParent\":false,\"Code\":\"{2}\"}}",
                  dr["RoleID"].ToString(), dr["RoleName"].ToString(), dr["Description"].ToString());

                if (dr["BID"].ToString() != "")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ",\"checked\":true }";
                }
                treenodes.Add(node);
            }

            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }
        /// <summary>
        /// 角色权限关联
        /// </summary>
        public static string ForOperationsLog(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string node = string.Format("{{ \"id\":{0}, \"pId\":{1}, \"name\":\"{2}\",\"isParent\":false,\"BID\":\"{3}\"}}",
                  dr["PermissionID"].ToString(), dr["ParentSystem"].ToString(), dr["Name"].ToString(), dr["BID"].ToString());
                if (dr["ParentSystem"].ToString() == "-1")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ", \"open\":true }";
                }
                if (dr["BID"].ToString() != "")
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ",\"checked\":true }";
                }
                treenodes.Add(node);
            }

            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }

        /// <summary>
        /// (树形勾选与否及打开与否Json串)
        /// </summary>
        public static string ForTreeList(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            List<string> treenodes = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                string node = string.Format("{{ \"id\":\"{0}\", \"pId\":\"{1}\", \"name\":\"{2}\",\"isParent\":false }}",
                  dr["id"].ToString(), dr["pId"].ToString(), dr["name"].ToString());
                if (dt.Columns.Contains("open"))
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ", \"open\":" + dr["open"].ToString() + " }";
                }
                else
                {
                    if (dr["pId"].ToString() == "0")
                    {
                        node = node.Substring(0, node.Length - 1);
                        node += ", \"open\":true }";
                    }
                }
                if (dt.Columns.Contains("checked"))
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ",\"checked\":" + dr["checked"].ToString() + " }";
                }
                if (dt.Columns.Contains("chkDisabled"))
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ",\"chkDisabled\":" + dr["chkDisabled"].ToString() + " }";
                }
                if (dt.Columns.Contains("fullname"))
                {
                    node = node.Substring(0, node.Length - 1);
                    node += ",\"fullname\":\"" + dr["fullname"].ToString() + "\" }";
                }

                treenodes.Add(node);
            }

            string strs = string.Join(",", treenodes.ToArray());
            return "[" + strs + "]";
        }
    }
}
