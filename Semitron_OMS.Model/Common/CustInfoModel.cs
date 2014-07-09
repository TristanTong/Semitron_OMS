using System;
namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 厂商信息
    /// </summary>
    [Serializable]
    public partial class CustInfoModel
    {
        public CustInfoModel()
        { }
        #region Model
        private int _custid;
        private string _custname;
        private string _custcode;
        private int _custtype;
        private string _phone;
        private string _telphone;
        private string _linkman;
        private string _address;
        private string _postcode;
        private string _url;
        private string _description;
        private DateTime _createtime = DateTime.Now;
        private int _adminid;
        private int _availflag = 1;
        private string _sk;
        private bool _isopenappoem = false;
        private int _appoemlimit = 10;
        private int _specialoemlimit = 3;
        private int _appstickoemlimit = 5;
        private int _apprecommendoemlimit = 10;
        private int _appbytypeoemlimit = 10;
        private int _appbyspecialoemlimit = 10;
        private int _appbytoplistoemlimit = 10;

        /// <summary>
        /// 厂商ID
        /// </summary>
        public int CustId
        {
            set { _custid = value; }
            get { return _custid; }
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
        /// 厂商编号
        /// </summary>
        public string CustCode
        {
            set { _custcode = value; }
            get { return _custcode; }
        }
        /// <summary>
        /// 类型 1.方案 2.品牌 3.集成商
        /// </summary>
        public int CustType
        {
            set { _custtype = value; }
            get { return _custtype; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string URL
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
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
        /// 业务员ID
        /// </summary>
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 排序SK拼音首字母
        /// </summary>
        public string SK
        {
            set { _sk = value; }
            get { return _sk; }
        }
        /// <summary>
        /// 是否开启APPOEM
        /// </summary>
        public bool IsOpenAppOEM
        {
            set { _isopenappoem = value; }
            get { return _isopenappoem; }
        }
        /// <summary>
        /// 应用OEM限量
        /// </summary>
        public int AppOEMLimit
        {
            set { _appoemlimit = value; }
            get { return _appoemlimit; }
        }
        /// <summary>
        /// 专题OEM限量
        /// </summary>
        public int SpecialOEMLimit
        {
            set { _specialoemlimit = value; }
            get { return _specialoemlimit; }
        }
        /// <summary>
        /// 首页置顶OEM限量
        /// </summary>
        public int AppStickOEMLimit
        {
            set { _appstickoemlimit = value; }
            get { return _appstickoemlimit; }
        }
        /// <summary>
        /// 首页推荐OEM限量
        /// </summary>
        public int AppRecommendOEMLimit
        {
            set { _apprecommendoemlimit = value; }
            get { return _apprecommendoemlimit; }
        }
        /// <summary>
        /// 类别下应用列表OEM限量
        /// </summary>
        public int AppByTypeOEMLimit
        {
            set { _appbytypeoemlimit = value; }
            get { return _appbytypeoemlimit; }
        }
        /// <summary>
        /// 专题下应用列表OEM限量
        /// </summary>
        public int AppBySpecialOEMLimit
        {
            set { _appbyspecialoemlimit = value; }
            get { return _appbyspecialoemlimit; }
        }
        /// <summary>
        /// 排行版下应用列表OEM限量
        /// </summary>
        public int AppByTopListOEMLimit
        {
            set { _appbytoplistoemlimit = value; }
            get { return _appbytoplistoemlimit; }
        }

        #endregion Model

    }
}

