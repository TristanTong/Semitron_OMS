using Semitron_OMS.Model.Permission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    public class PermissionUtility
    {
        /**********************************************************
         * 备注：当需要获取多个子系统的权限时,可扩展方法。
         * ********************************************************/
        #region GetPermissionOper
        /// <summary>
        /// 获得单个子系统的权限系统
        /// </summary>
        /// <param name="bp">子系统基本信息实体</param>
        /// <param name="dt">权限数据表</param>
        /// <param name="perModule">需返回权限系统实体</param>
        public static void GetPermissionModuleWithSingleSubSys(BasePer bp, DataTable dt, PermissionModule perModule)
        {
            SubSystemPer subSysPer = new SubSystemPer();
            subSysPer.ID = bp.ID;
            subSysPer.Code = bp.Code;
            subSysPer.Name = bp.Name;

            GetSubSystemPer(dt, subSysPer);
            //权限系统中加入子系统 
            if (!perModule.SubSystemPers.ContainsKey(subSysPer.ID))
            {
                perModule.SubSystemPers.Add(subSysPer.ID, subSysPer);
            }
            else
            {
                perModule.SubSystemPers.Remove(subSysPer.ID);
                perModule.SubSystemPers.Add(subSysPer.ID, subSysPer);
            }
        }

        /// <summary>
        /// 获取单个子系统权限
        /// </summary>
        /// <param name="dt">权限数据表</param>
        /// <param name="subSysPer">需返回子系统权限实体</param>
        public static void GetSubSystemPer(DataTable dt, SubSystemPer subSysPer)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["Type"]) == 1) //模块
                {
                    ModulePer modPer = new ModulePer();
                    modPer.ID = Convert.ToInt32(dr["PermissionID"]);
                    modPer.Code = dr["Code"].ToString();
                    modPer.Name = dr["PName"].ToString();
                    GetModulePer(dt, modPer);
                    if (!subSysPer.ModulePers.ContainsKey(modPer.ID))
                    {
                        subSysPer.ModulePers.Add(modPer.ID, modPer);
                    }
                    else
                    {
                        subSysPer.ModulePers.Remove(modPer.ID);
                        subSysPer.ModulePers.Add(modPer.ID, modPer);
                    }
                }// end if //模块
            }//end foreach (DataRow dr in dt.Rows)
        }

        /// <summary>
        /// 获取单个模块权限
        /// </summary>
        /// <param name="dt">权限数据表</param>
        /// <param name="modPer">需返回模块权限实体</param>
        public static void GetModulePer(DataTable dt, ModulePer modPer)
        {
            foreach (DataRow dw in dt.Rows)
            {
                if (modPer.ID == Convert.ToInt32(dw["Pid"]))//菜单页面
                {
                    PagePer pagePer = new PagePer();
                    pagePer.ID = Convert.ToInt32(dw["PermissionID"]);
                    pagePer.Code = dw["Code"].ToString();
                    pagePer.Name = dw["PName"].ToString();

                    GetPagePer(dt, pagePer);

                    if (!modPer.PagePers.ContainsKey(pagePer.ID))
                    {
                        modPer.PagePers.Add(pagePer.ID, pagePer);
                    }
                    else
                    {
                        modPer.PagePers.Remove(pagePer.ID);
                        modPer.PagePers.Add(pagePer.ID, pagePer);
                    }
                }// end if //菜单页面
            }// end foreach (DataRow dw in dt.Rows)            
        }

        /// <summary>
        /// 获取单个页面权限
        /// </summary>
        /// <param name="dt">权限数据表</param>
        /// <param name="pagePer">需返回页面权限实体</param>
        public static void GetPagePer(DataTable dt, PagePer pagePer)
        {
            foreach (DataRow de in dt.Rows)
            {
                if (pagePer.ID == Convert.ToInt32(de["Pid"]))//按钮、数据集、右键菜单
                {
                    if (Convert.ToInt32(de["Type"]) == 3) //按钮
                    {
                        ButtonPer buttonPer = new ButtonPer();
                        buttonPer.ID = Convert.ToInt32(de["PermissionID"]);
                        buttonPer.Code = de["Code"].ToString();
                        buttonPer.Name = de["PName"].ToString();
                        if (!pagePer.ButtonPers.ContainsKey(buttonPer.ID))
                        {
                            pagePer.ButtonPers.Add(buttonPer.ID, buttonPer);
                        }
                        else
                        {
                            pagePer.ButtonPers.Remove(buttonPer.ID);
                            pagePer.ButtonPers.Add(buttonPer.ID, buttonPer);
                        }
                    }
                    if (Convert.ToInt32(de["Type"]) == 4) //数据集
                    {
                        DataSetPer datasetPer = new DataSetPer();
                        datasetPer.ID = Convert.ToInt32(de["PermissionID"]);
                        datasetPer.Code = de["Code"].ToString();
                        datasetPer.Name = de["PName"].ToString();
                        if (!pagePer.DataSetsPers.ContainsKey(datasetPer.ID))
                        {
                            pagePer.DataSetsPers.Add(datasetPer.ID, datasetPer);
                        }
                        else
                        {
                            pagePer.DataSetsPers.Remove(datasetPer.ID);
                            pagePer.DataSetsPers.Add(datasetPer.ID, datasetPer);
                        }
                    }
                    if (Convert.ToInt32(de["Type"]) == 5) //右键菜单
                    {
                        RightMenuPer rmPer = new RightMenuPer();
                        rmPer.ID = Convert.ToInt32(de["PermissionID"]);
                        rmPer.Code = de["Code"].ToString();
                        rmPer.Name = de["PName"].ToString();
                        if (!pagePer.RightMenuPer.ContainsKey(rmPer.ID))
                        {
                            pagePer.RightMenuPer.Add(rmPer.ID, rmPer);
                        }
                        else
                        {
                            pagePer.RightMenuPer.Remove(rmPer.ID);
                            pagePer.RightMenuPer.Add(rmPer.ID, rmPer);
                        }
                    }
                }//end if //按钮、数据集、右键菜单
            }// end foreach (DataRow de in dt.Rows)
        }
        #endregion GetPermissionOper

        #region JudgeOper

        /// <summary>
        /// 权限系统是否存在指定的页面代码
        /// </summary>
        /// <param name="perModule">当前权限系统</param>
        /// <param name="strPageCode">比对的页面编码</param>
        /// <returns></returns>
        public static bool IsExistPageCode(PermissionModule perModule, string strPageCode)
        {
            foreach (SubSystemPer ssp in perModule.SubSystemPers.Values)
            {
                if (IsExistPageCode(ssp, strPageCode))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 子系统是否存在指定的页面代码
        /// </summary>
        /// <param name="subSystemPer">子系统权限实体</param>
        /// <param name="pageCode">比对的页面编码</param>
        /// <returns>是否存在</returns>
        public static bool IsExistPageCode(SubSystemPer subSystemPer, string strPageCode)
        {
            //遍历子系统下所有模块
            foreach (ModulePer mp in subSystemPer.ModulePers.Values)
            {
                //遍历子所有模块下所有页面
                foreach (PagePer pp in mp.PagePers.Values)
                {
                    if (pp.Code == strPageCode)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 数据权限集列表中是否存在指定编号的数据集权限
        /// </summary>
        /// <param name="list">数据权限集列表</param>
        /// <param name="strDsPerCode">数据集权限编号</param>
        /// <returns>是否存在</returns>
        public static bool IsExistDataSetPer(List<DataSetPer> list, string strDsPerCode)
        {
            foreach (DataSetPer dsPer in list)
            {
                if (dsPer.Code == strDsPerCode)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 权限系统中指定页面是否存在指定编号的数据集权限
        /// </summary>
        /// <param name="permissionModule">权限系统实体</param>
        /// <param name="strPageCode">指定页面编号</param>
        /// <param name="strDsPerCode">数据集权限编号</param>
        /// <returns>是否存在</returns>
        public static bool IsExistDataSetPer(PermissionModule permissionModule, string strPageCode, string strDsPerCode)
        {
            return IsExistDataSetPer(GetDataSetPer(permissionModule, strPageCode), strDsPerCode);
        }
        #endregion JudgeOper

        #region EntityOper

        /// <summary>
        /// 获取当前权限系统实体的所有模块实体
        /// </summary>
        /// <param name="perModule"></param>
        /// <returns>对应模块实体集合</returns>
        public static List<ModulePer> GetModulePerALL(PermissionModule perModule)
        {
            return GetModulePer(perModule, string.Empty);
        }

        /// <summary>
        /// 根据当前权限系统实体和模块编码取得对应模块，模块编码为空时返回所有
        /// </summary>
        /// <param name="perModule">当前权限系统实体</param>
        /// <param name="strModuleCode">模块编码</param>
        /// <returns>对应模块实体集合</returns>
        public static List<ModulePer> GetModulePer(PermissionModule perModule, string strModuleCode)
        {
            List<ModulePer> lstMoPer = new List<ModulePer>();
            foreach (SubSystemPer ssPer in perModule.SubSystemPers.Values)
            {
                foreach (ModulePer bp in GetModulePer(ssPer, strModuleCode))
                {
                    lstMoPer.Add(bp);
                }
            }
            return lstMoPer;
        }

        /// <summary>
        /// 根据子系统实体和模块编码取得对应模块，模块编码为空时返回所有
        /// </summary>
        /// <param name="subSystemPer">子系统实体</param>
        /// <param name="strModuleCode">模块编码</param>
        /// <returns>对应模块实体集合</returns>
        public static List<ModulePer> GetModulePer(SubSystemPer subSystemPer, string strModuleCode)
        {
            List<ModulePer> lstMoPer = new List<ModulePer>();
            foreach (ModulePer mp in subSystemPer.ModulePers.Values)
            {
                if (strModuleCode == string.Empty)
                {
                    lstMoPer.Add(mp);
                }
                else
                {
                    if (strModuleCode == mp.Code)
                    {
                        lstMoPer.Add(mp);
                    }
                }
            }
            return lstMoPer;
        }

        /// <summary>
        /// 根据当前权限系统实体和页面编码取得对应页面编码的按钮权限实体集合
        /// </summary>
        /// <param name="perModule">当前权限系统</param>
        /// <param name="strPageCode"></param>
        /// <returns>按钮权限实体集合</returns>
        public static List<ButtonPer> GetButtonPer(PermissionModule perModule, string strPageCode)
        {
            List<ButtonPer> lstBtnPer = new List<ButtonPer>();
            foreach (SubSystemPer ssPer in perModule.SubSystemPers.Values)
            {
                foreach (ButtonPer bp in GetButtonPer(ssPer, strPageCode))
                {
                    lstBtnPer.Add(bp);
                }
            }
            return lstBtnPer;
        }

        /// <summary>
        /// 根据子系统实体和页面编码取得对应页面编码的按钮权限实体集合
        /// </summary>
        /// <param name="subSystemPer">子系统权限实体</param>
        /// <param name="PageCode">页面编码</param>
        /// <returns>按钮权限实体集合</returns>
        public static List<ButtonPer> GetButtonPer(SubSystemPer subSystemPer, string strPageCode)
        {
            List<ButtonPer> lstBtnPer = new List<ButtonPer>();
            foreach (ModulePer mp in subSystemPer.ModulePers.Values)
            {
                //遍历子所有模块下所有页面
                foreach (PagePer pp in mp.PagePers.Values)
                {
                    if (pp.Code == strPageCode)
                    {
                        foreach (ButtonPer btnPer in pp.ButtonPers.Values)
                        {
                            lstBtnPer.Add(btnPer);
                        }
                    }
                }
            }
            return lstBtnPer;
        }

        /// <summary>
        /// 根据当前权限系统实体和页面编码取得对应页面编码的数据集权限实体集合
        /// </summary>
        /// <param name="perModule">当前权限系统</param>
        /// <param name="strPageCode"></param>
        /// <returns>数据集权限实体集合</returns>
        public static List<DataSetPer> GetDataSetPer(PermissionModule perModule, string strPageCode)
        {
            List<DataSetPer> lstDsPer = new List<DataSetPer>();
            foreach (SubSystemPer ssPer in perModule.SubSystemPers.Values)
            {
                foreach (DataSetPer bp in GetDataSetPer(ssPer, strPageCode))
                {
                    lstDsPer.Add(bp);
                }
            }
            return lstDsPer;
        }

        /// <summary>
        /// 根据子系统实体和页面编码取得对应页面编码的数据集权限实体集合
        /// </summary>
        /// <param name="subSystemPer">子系统权限实体</param>
        /// <param name="strPageCode">页面编码</param>
        /// <returns>数据集权限实体集合</returns>
        public static List<DataSetPer> GetDataSetPer(SubSystemPer subSystemPer, string strPageCode)
        {
            List<DataSetPer> lstDsPer = new List<DataSetPer>();
            foreach (ModulePer mp in subSystemPer.ModulePers.Values)
            {
                //遍历子所有模块下所有页面
                foreach (PagePer pp in mp.PagePers.Values)
                {
                    if (pp.Code == strPageCode)
                    {
                        foreach (DataSetPer dsp in pp.DataSetsPers.Values)
                        {
                            lstDsPer.Add(dsp);
                        }
                    }
                }
            }
            return lstDsPer;
        }

        /// <summary>
        /// 根据当前权限系统实体和页面编码取得对应页面编码的右键菜单权限实体集合
        /// </summary>
        /// <param name="perModule">当前权限系统</param>
        /// <param name="strPageCode"></param>
        /// <returns>右键菜单权限实体集合</returns>
        public static List<RightMenuPer> GetRightMenuPer(PermissionModule perModule, string strPageCode)
        {
            List<RightMenuPer> lstRmPer = new List<RightMenuPer>();
            foreach (SubSystemPer ssPer in perModule.SubSystemPers.Values)
            {
                foreach (RightMenuPer bp in GetRightMenuPer(ssPer, strPageCode))
                {
                    lstRmPer.Add(bp);
                }
            }
            return lstRmPer;
        }

        /// <summary>
        /// 根据子系统实体和页面编码取得对应页面编码的右键菜单权限实体集合
        /// </summary>
        /// <param name="subSystemPer">子系统权限实体</param>
        /// <param name="strPageCode">页面编码</param>
        /// <returns>右键菜单权限实体集合</returns>
        public static List<RightMenuPer> GetRightMenuPer(SubSystemPer subSystemPer, string strPageCode)
        {
            List<RightMenuPer> lstRmPer = new List<RightMenuPer>();
            foreach (ModulePer mp in subSystemPer.ModulePers.Values)
            {
                //遍历子所有模块下所有页面
                foreach (PagePer pp in mp.PagePers.Values)
                {
                    if (pp.Code == strPageCode)
                    {
                        foreach (RightMenuPer rmp in pp.RightMenuPer.Values)
                        {
                            lstRmPer.Add(rmp);
                        }
                    }
                }
            }
            return lstRmPer;
        }

        /// <summary>
        /// 将列表中对象的编号转换成用逗号拼接的字符串
        /// </summary>
        /// <param name="lst">对象列表</param>
        /// <returns>Code逗号字符串</returns>
        public static string ToCodeCommaString(List<ButtonPer> lst)
        {
            string strCommaStr = string.Empty;
            foreach (ButtonPer btnPer in lst)
            {
                strCommaStr += btnPer.Code + ",";
            }
            if (strCommaStr.EndsWith(","))
            {
                strCommaStr = strCommaStr.Remove(strCommaStr.Length - 1);
            }
            return strCommaStr;
        }

        /// <summary>
        /// 将列表中对象的编号转换成用逗号拼接的字符串
        /// </summary>
        /// <param name="lst">对象列表</param>
        /// <returns>Code逗号字符串</returns>
        public static string ToCodeCommaString(List<DataSetPer> lst)
        {
            string strCommaStr = string.Empty;
            foreach (DataSetPer dsPer in lst)
            {
                strCommaStr += dsPer.Code + ",";
            }
            if (strCommaStr.EndsWith(","))
            {
                strCommaStr = strCommaStr.Remove(strCommaStr.Length - 1);
            }
            return strCommaStr;
        }
        #endregion EntityOper
    }
}
