using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 厂商下包含的项目信息
    /// </summary>
    [Serializable]
    public partial class ProjectModel
    {
        public ProjectModel()
        { }

        #region Model
        private int _projectid;
        private int _custid;
        private string _custcode;
        private string _custname;
        private string _procode;
        private int _feestate = 1;
        private int? _mobilefeemax;
        private float _feerule = 90;
        private DateTime _createtime = DateTime.Now;
        private int _availflag = 1;
        private float _profitsharing = 80;
        private float _salesrule = 0;
        private int _projecttype = -1;
        private string _sk;
        private int _smsstate = 0;
        private string _supportbusiness = ",1,";
        private int _mobilemaxsendcount = 50;
        private int _mobilemaxreissuecount = 50;
        private bool _isopenappoem = false;
        private bool _isopenaddfee = true;
        private int _singleuseraddfeedaymax = 2;
        private int _singleuseraddfeemonthmax = 62;
        private int _SingleUserAddSalesDayMax = 1;
        private int _SingleUserAddSalesMonthMax = 31;
        private string _SupportMsgType;
        private string _description;
        private int _activationrule = 0;
        private int _mobiledaymaxsendcount;
        private int _mobiledaymaxreissuecount;


        /// <summary>
        /// 分成比例
        /// </summary>
        public float ProfitSharing
        {
            set { _profitsharing = value; }
            get { return _profitsharing; }
        }
        /// <summary>
        /// 销量扣量规则 
        /// </summary>
        public float SalesRule
        {
            set { _salesrule = value; }
            get { return _salesrule; }
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 厂商ID
        /// </summary>
        public int CustId
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string CustCode
        {
            set { _custcode = value; }
            get { return _custcode; }
        }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProCode
        {
            set { _procode = value; }
            get { return _procode; }
        }
        /// <summary>
        /// 1,计费 2,可计费不可暗扣  3,不计费
        /// </summary>
        public int FeeState
        {
            set { _feestate = value; }
            get { return _feestate; }
        }
        /// <summary>
        /// 单机扣费限额
        /// </summary>
        public int? MobileFeeMax
        {
            set { _mobilefeemax = value; }
            get { return _mobilefeemax; }
        }
        /// <summary>
        /// 扣量规则(百分比)
        /// </summary>
        public float FeeRule
        {
            set { _feerule = value; }
            get { return _feerule; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 是否有效(0:否1:否)
        /// </summary>
        public int AvailFlag
        {
            set { _availflag = value; }
            get { return _availflag; }
        }

        /// <summary>
        /// 拥有的计费模块（计费方式和计费类型组合）。
        /// </summary>
        public string HasFeeMode { get; set; }

        /// <summary>
        /// 平台类型 0:MTK,1:展讯
        /// </summary>
        public int PlatformType { get; set; }

        /// <summary>
        /// 芯片
        /// </summary>
        public string Chip { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// 不计费地区
        /// </summary>
        public string NoFeeAreas { get; set; }

        /// <summary>
        /// 项目类型：0功能机 1智能机
        /// </summary>
        public int ProjectType
        {
            get
            {
                return _projecttype;
            }
            set
            {
                _projecttype = value;
            }
        }

        /// <summary>
        /// 排序SK拼音首字母
        /// </summary>
        public string SK
        {
            get
            {
                return _sk;
            }
            set
            {
                _sk = value;
            }
        }

        /// <summary>
        /// 短信营销状态
        /// </summary>
        public int SmsState
        {
            set { _smsstate = value; }
            get { return _smsstate; }
        }

        /// <summary>
        /// 支持的业务：1，LBS软件。2，短信营销。3，应用商店。4，更新模块
        /// </summary>
        public string SupportBusiness
        {
            set { _supportbusiness = value; }
            get { return _supportbusiness; }
        }

        /// <summary>
        /// 单机发送月限
        /// </summary>
        public int MobileMaxSendCount
        {
            get
            {
                return _mobilemaxsendcount;
            }
            set
            {
                _mobilemaxsendcount = value;
            }
        }

        /// <summary>
        /// 单机补发月限
        /// </summary>
        public int MobileMaxReissueCount
        {
            get
            {
                return _mobilemaxreissuecount;
            }
            set
            {
                _mobilemaxreissuecount = value;
            }
        }
        /// <summary>
        /// 是否开启APPOEM
        /// </summary>
        public bool IsOpenAppOEM
        {
            get
            {
                return _isopenappoem;
            }
            set
            {
                _isopenappoem = value;
            }
        }
        /// <summary>
        /// 是否开启APPOEM
        /// </summary>
        public bool IsOpenAddFee
        {
            get
            {
                return _isopenaddfee;
            }
            set
            {
                _isopenaddfee = value;
            }
        }

        /// <summary>
        /// 单机补扣日限
        /// </summary>
        public int SingleUserAddFeeDayMax
        {
            get
            {
                return _singleuseraddfeedaymax;
            }
            set
            {
                _singleuseraddfeedaymax = value;
            }
        }

        /// <summary>
        /// 单机补扣月限
        /// </summary>
        public int SingleUserAddFeeMonthMax
        {
            get
            {
                return _singleuseraddfeemonthmax;
            }
            set
            {
                _singleuseraddfeemonthmax = value;
            }
        }

        /// <summary>
        /// 单机补销量日限
        /// </summary>
        public int SingleUserAddSalesDayMax
        {
            get
            {
                return _SingleUserAddSalesDayMax;
            }
            set
            {
                _SingleUserAddSalesDayMax = value;
            }
        }

        /// <summary>
        /// 单机补销量月限
        /// </summary>
        public int SingleUserAddSalesMonthMax
        {
            get
            {
                return _SingleUserAddSalesMonthMax;
            }
            set
            {
                _SingleUserAddSalesMonthMax = value;
            }
        }

        /// <summary>
        /// 支持的信息类型，多个使用逗号隔开。
        /// 0 SMS ，1 闪信 ， 2 MMS，3 WAP
        /// </summary>
        public string SupportMsgType
        {
            get { return _SupportMsgType; }
            set { _SupportMsgType = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 激活扣量率
        /// </summary>
        public int ActivationRule
        {
            get { return _activationrule; }
            set { _activationrule = value; }
        }

        /// <summary>
        /// 单机发送日限
        /// </summary>
        public int MobileDayMaxSendCount
        {
            get { return _mobiledaymaxsendcount; }
            set { _mobiledaymaxsendcount = value; }
        }

        /// <summary>
        /// 单机补发日限
        /// </summary>
        public int MobileDayMaxReissueCount
        {
            get { return _mobiledaymaxreissuecount; }
            set { _mobiledaymaxreissuecount = value; }
        }

        #endregion Model
    }
}
