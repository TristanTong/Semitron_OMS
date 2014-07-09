/**  
* ShippingPlanDetailModel.cs
*
* 功 能： N/A
* 类 名： ShippingPlanDetailModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/7 0:14:27   童荣辉    初版
*
* Copyright (c) 2013 SemitronElec Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：森美创（深圳）科技有限公司　　　　　　　　　　　　　　  │
*└──────────────────────────────────┘
*/
using System;
namespace Semitron_OMS.Model.OMS
{
    /// <summary>
    /// ShippingPlanDetailModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ShippingPlanDetailModel
    {
        public ShippingPlanDetailModel()
        { }
        #region Model
        private int _id;
        private int? _shippingplanid;
        private string _planstockcode;
        private string _customerorderno;
        private int? _customerdetailid;
        private string _cpn;
        private string _mpn;
        private string _productcode;
        private int _planqty;
        private bool _availflag = true;
        private DateTime? _createtime = DateTime.Now;
        private string _createuser;
        private DateTime? _updatetime;
        private string _updateuser;
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 出货计划ID
        /// </summary>
        public int? ShippingPlanID
        {
            set { _shippingplanid = value; }
            get { return _shippingplanid; }
        }
        /// <summary>
        /// 计划出货仓编码
        /// </summary>
        public string PlanStockCode
        {
            set { _planstockcode = value; }
            get { return _planstockcode; }
        }
        /// <summary>
        ///  客户单号
        /// </summary>
        public string CustomerOrderNo
        {
            set { _customerorderno = value; }
            get { return _customerorderno; }
        }
        /// <summary>
        /// 产品清单ID
        /// </summary>
        public int? CustomerDetailID
        {
            set { _customerdetailid = value; }
            get { return _customerdetailid; }
        }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string CPN
        {
            set { _cpn = value; }
            get { return _cpn; }
        }
        /// <summary>
        /// 厂商型号
        /// </summary>
        public string MPN
        {
            set { _mpn = value; }
            get { return _mpn; }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 产品出货计划数量
        /// </summary>
        public int PlanQty
        {
            set { _planqty = value; }
            get { return _planqty; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
        }
        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        ///  创建人
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        ///  修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        ///  修改人
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        #endregion Model
        public string InnerOrderNo { get; set; }
        public string Remark { get; set; }
    }

    [Serializable]
    public partial class ShippingPlanDetailDisplayModel
    {

        public int ID { get; set; }

        public bool AvailFlag { get; set; }

        public int ShippingPlanID { get; set; }

        public DateTime? ShippingPlanDate { get; set; }

        public int PlanQty { get; set; }

        public string ShippingPlanNo { get; set; }

        public int CustomerDetailID { get; set; }

        public int InnerOrderNo { get; set; }

        public decimal CustomerOrderNo { get; set; }

        public string ProductCode { get; set; }

        public string Remark { get; set; }
    }
}

