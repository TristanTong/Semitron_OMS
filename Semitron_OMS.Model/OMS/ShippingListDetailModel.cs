/**  
* ShippingListDetailModel.cs
*
* 功 能： N/A
* 类 名： ShippingListDetailModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/7/7 0:14:28   童荣辉    初版
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
    /// 出货计划表
    /// </summary>
    [Serializable]
    public partial class ShippingListDetailModel
    {
        public ShippingListDetailModel()
        { }
        #region Model
        private int _id;
        private int _shippinglistid;
        private string _stockcode;
        private string _shippingplanno;
        private string _productcode;
        private int _outqty;
        private int? _chargeuserid;
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
        ///  出库单ID
        /// </summary>
        public int ShippingListID
        {
            set { _shippinglistid = value; }
            get { return _shippinglistid; }
        }
        /// <summary>
        ///  实际出库仓编码
        /// </summary>
        public string StockCode
        {
            set { _stockcode = value; }
            get { return _stockcode; }
        }
        /// <summary>
        ///  出货计划单号
        /// </summary>
        public string ShippingPlanNo
        {
            set { _shippingplanno = value; }
            get { return _shippingplanno; }
        }
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 出库数量
        /// </summary>
        public int OutQty
        {
            set { _outqty = value; }
            get { return _outqty; }
        }
        /// <summary>
        /// 负责人账号ID
        /// </summary>
        public int? ChargeUserID
        {
            set { _chargeuserid = value; }
            get { return _chargeuserid; }
        }
        /// <summary>
        ///  是否有效
        /// </summary>
        public bool AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
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
        /// 创建人
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        #endregion Model

        public int ShippingPlanDetailID { get; set; }
        public string Remark { get; set; }

    }

    [Serializable]
    public partial class ShippingListDetailDisplayModel
    {
        public DateTime? OutStockDate { get; set; }

        public int ID { get; set; }

        public bool AvailFlag { get; set; }

        public int ShippingListID { get; set; }

        public int OutQty { get; set; }

        public string ShippingListNo { get; set; }

        public int ShippingPlanDetailID { get; set; }

        public string ProductCode { get; set; }

        public string Remark { get; set; }
    }
}

