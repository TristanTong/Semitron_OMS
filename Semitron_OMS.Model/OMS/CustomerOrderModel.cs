/**  
* CustomerOrderModel.cs
*
* 功 能： N/A
* 类 名： CustomerOrderModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/17 19:08:21   童荣辉    初版
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
    /// 客户订单主表
    /// </summary>
    [Serializable]
    public partial class CustomerOrderModel
    {
        public CustomerOrderModel()
        { }
        #region Model
        private int _id;
        private string _innerorderno;
        private string _customerorderno;
        private int _customerid;
        private DateTime? _custorderdate;
        private string _customerbuyer;
        private int? _corporationid;
        private string _innersalesman;
        private int? _paymenttypeid;
        private int? _state = 100;
        private string _createuser;
        private DateTime? _createtime = DateTime.Now;
        private string _updateuser;
        private DateTime? _updatetime;
        private int? _assigntoinnerbuyer;
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司内部单号
        /// </summary>
        public string InnerOrderNO
        {
            set { _innerorderno = value; }
            get { return _innerorderno; }
        }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string CustomerOrderNO
        {
            set { _customerorderno = value; }
            get { return _customerorderno; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime? CustOrderDate
        {
            set { _custorderdate = value; }
            get { return _custorderdate; }
        }
        /// <summary>
        /// 客户采购人员
        /// </summary>
        public string CustomerBuyer
        {
            set { _customerbuyer = value; }
            get { return _customerbuyer; }
        }
        /// <summary>
        /// 接单公司抬头
        /// </summary>
        public int? CorporationID
        {
            set { _corporationid = value; }
            get { return _corporationid; }
        }
        /// <summary>
        /// 公司销售人员
        /// </summary>
        public string InnerSalesMan
        {
            set { _innersalesman = value; }
            get { return _innersalesman; }
        }
        /// <summary>
        /// 客户付款方式ID
        /// </summary>
        public int? PaymentTypeID
        {
            set { _paymenttypeid = value; }
            get { return _paymenttypeid; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int? State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
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
        /// 指定公司采购
        /// </summary>
        public int? AssignToInnerBuyer
        {
            set { _assigntoinnerbuyer = value; }
            get { return _assigntoinnerbuyer; }
        }
        #endregion Model

    }
}

