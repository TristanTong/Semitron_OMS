/**  
* POPlanModel.cs
*
* 功 能： N/A
* 类 名： POPlanModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/6 17:42:38   童荣辉    初版
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
    /// 采购订单明细表(采购计划)
    /// </summary>
    [Serializable]
    public partial class POPlanModel
    {
        public POPlanModel()
        { }
        #region Model
        private int _id;
        private string _pono;
        private string _cpn;
        private string _mpn;
        private string _mfg;
        private string _dc;
        private bool _rohs;
        private DateTime? _vcd;
        private int? _poquantity = 0;
        private int? _arrivedqty = 0;
        private int? _stockqty = 0;
        private int? _alreadyqty = 0;
        private int? _supplierid;
        private int? _vendorpaymenttypeid;
        private bool _ispaysupplier = false;
        private decimal? _buyexchangerate;
        private string _buyrealcurrency;
        private decimal? _buyrealprice = 0.0000M;
        private string _buystandardcurrency = "1";
        private decimal? _buyprice = 0.0000M;
        private decimal? _buycost = 0.0000M;
        private decimal? _otherfee = 0.0000M;
        private string _otherfeeremark;
        private DateTime? _shipmentdate;
        private int? _state = 100;
        private int? _customerorderdetailid;
        private string _createuser;
        private DateTime? _createtime = DateTime.Now;
        private string _updateuser;
        private DateTime? _updatetime;
        private string _productcode;
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string PONO
        {
            set { _pono = value; }
            get { return _pono; }
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
        /// 厂家标准型号
        /// </summary>
        public string MPN
        {
            set { _mpn = value; }
            get { return _mpn; }
        }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string MFG
        {
            set { _mfg = value; }
            get { return _mfg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DC
        {
            set { _dc = value; }
            get { return _dc; }
        }
        /// <summary>
        /// 是否环保
        /// </summary>
        public bool ROHS
        {
            set { _rohs = value; }
            get { return _rohs; }
        }
        /// <summary>
        /// 供应商答复的交期
        /// </summary>
        public DateTime? VCD
        {
            set { _vcd = value; }
            get { return _vcd; }
        }
        /// <summary>
        /// 采购订单数量
        /// </summary>
        public int? POQuantity
        {
            set { _poquantity = value; }
            get { return _poquantity; }
        }
        /// <summary>
        /// 已到货数量
        /// </summary>
        public int? ArrivedQty
        {
            set { _arrivedqty = value; }
            get { return _arrivedqty; }
        }
        /// <summary>
        /// Semitron库存数量
        /// </summary>
        public int? StockQty
        {
            set { _stockqty = value; }
            get { return _stockqty; }
        }
        /// <summary>
        /// 已发货数量
        /// </summary>
        public int? AlreadyQty
        {
            set { _alreadyqty = value; }
            get { return _alreadyqty; }
        }
        /// <summary>
        /// 供应商ID (冗余)
        /// </summary>
        public int? SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 供应商付款方式ID
        /// </summary>
        public int? VendorPaymentTypeID
        {
            set { _vendorpaymenttypeid = value; }
            get { return _vendorpaymenttypeid; }
        }
        /// <summary>
        /// 是否向供应商付款
        /// </summary>
        public bool IsPaySupplier
        {
            set { _ispaysupplier = value; }
            get { return _ispaysupplier; }
        }
        /// <summary>
        /// 买汇率
        /// </summary>
        public decimal? BuyExchangeRate
        {
            set { _buyexchangerate = value; }
            get { return _buyexchangerate; }
        }
        /// <summary>
        /// 实际买货币
        /// </summary>
        public string BuyRealCurrency
        {
            set { _buyrealcurrency = value; }
            get { return _buyrealcurrency; }
        }
        /// <summary>
        /// 实际买价
        /// </summary>
        public decimal? BuyRealPrice
        {
            set { _buyrealprice = value; }
            get { return _buyrealprice; }
        }
        /// <summary>
        /// 标准买货币
        /// </summary>
        public string BuyStandardCurrency
        {
            set { _buystandardcurrency = value; }
            get { return _buystandardcurrency; }
        }
        /// <summary>
        /// 买价
        /// </summary>
        public decimal? BuyPrice
        {
            set { _buyprice = value; }
            get { return _buyprice; }
        }
        /// <summary>
        /// 买货成本
        /// </summary>
        public decimal? BuyCost
        {
            set { _buycost = value; }
            get { return _buycost; }
        }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal? OtherFee
        {
            set { _otherfee = value; }
            get { return _otherfee; }
        }
        /// <summary>
        /// 其他费用备注
        /// </summary>
        public string OtherFeeRemark
        {
            set { _otherfeeremark = value; }
            get { return _otherfeeremark; }
        }
        /// <summary>
        /// 出货日期
        /// </summary>
        public DateTime? ShipmentDate
        {
            set { _shipmentdate = value; }
            get { return _shipmentdate; }
        }
        /// <summary>
        /// 状态(100 采购计划草稿,110 完成数量价格,120 一级审核价格,130 二级审核计划,140 QC确认,150 完成采购,160 取消采购)
        /// </summary>
        public int? State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 对应采购的产品清单ID
        /// </summary>
        public int? CustomerOrderDetailID
        {
            set { _customerorderdetailid = value; }
            get { return _customerorderdetailid; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        #endregion Model

    }

    /// <summary>
    /// 采购计划未入库实体
    /// </summary>
    [Serializable]
    public partial class POPlanUnInStockModel
    {
        public int POPlanId { get; set; }

        public string PONo { get; set; }

        public string MPN { get; set; }

        public string ProductCode { get; set; }

        public int POQuantity { get; set; }

        public decimal BuyPrice { get; set; }

        public string ArrivedDate { get; set; }

        public decimal BuyCost { get; set; }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public int ArrivedQty { get; set; }

        public int StockQty { get; set; }

        public int UnInStockQty { get; set; }
    }
}

