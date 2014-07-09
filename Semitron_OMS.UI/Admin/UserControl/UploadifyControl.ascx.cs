using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Semitron_OMS.UI.Admin.UserControl
{
    public partial class UploadifyControl : System.Web.UI.UserControl
    {
        #region Field
        private string _fileExt = "*.xlsx;*.xls;"; //选择文件类型
        private string _fileDesc = "请选择excel格式文件"; //选择文件内容描述
        private string _buttonText = "Browse File"; //按钮文本,只支持英文
        private bool _isMulti = false; //是否允许上传多个文件
        private bool _isAuto = true; //是否自动上传，true当选择文件后就直接上传了，为false需要点击上传按钮才上传
        private int _simUploadLimit = 1; //允许同时上传的文件个数 
        private int _queueSizeLimit = 999; //单次上传的文件个数 
        private string _folder = "/Upload"; //上传文件夹相对路径
        private bool _isAutoDisappear = false;//完成后是否自动消失
        private long _sizeLimit = 1048576; //1M 上传文件的大小限制

        private string _successFilePath = string.Empty; //成功上传文件的所有路径，用"|"隔开
        private string _errInfo = string.Empty;//上传后的错误信息
        //默认对照值
        private string FileExtDefault = "*.xlsx;*.xls;"; //选择文件类型
        private string FileDescDefault = "请选择excel格式文件"; //选择文件内容描述
        private string ButtonTextDefault = "Browse File"; //按钮文本,只支持英文
        private bool IsMultiDefault = false; //是否允许上传多个文件
        private bool IsAutoDefault = true; //是否自动上传，true当选择文件后就直接上传了，为false需要点击上传按钮才上传
        private int SimUploadLimitDefault = 1; //允许同时上传的文件个数 
        private int QueueSizeLimitDefault = 999; //单次上传的文件个数 
        private string FolderDefault = "/Upload"; //上传文件夹相对路径
        private bool IsAutoDisappearDefault = false;//完成后是否自动消失
        private long SizeLimitDefault = 1048576; //1M 上传文件的大小限制
        #endregion Field
        #region Property
        /// <summary>
        /// 选择文件类型
        /// </summary>
        public string FileExt
        {
            get
            {
                return _fileExt;
            }
            set
            {
                _fileExt = value;
            }
        }
        /// <summary>
        /// 选择文件内容描述
        /// </summary>
        public string FileDesc
        {
            get
            {
                return _fileDesc;
            }
            set
            {
                _fileDesc = value;
            }
        }
        /// <summary>
        /// 按钮文本,只支持英文
        /// </summary>
        public string ButtonText
        {
            get
            {
                return _buttonText;
            }
            set
            {
                _buttonText = value;
            }
        }
        /// <summary>
        /// 是否允许上传多个文件
        /// </summary>
        public bool IsMulti
        {
            get
            {
                return _isMulti;
            }
            set
            {
                _isMulti = value;
            }
        }
        /// <summary>
        /// 是否自动上传，true当选择文件后就直接上传了，为false需要点击上传按钮才上传
        /// </summary>
        public bool IsAuto
        {
            get
            {
                return _isAuto;
            }
            set
            {
                _isAuto = value;
            }
        }
        /// <summary>
        /// 允许同时上传的文件个数
        /// </summary>
        public int SimUploadLimit
        {
            get
            {
                return _simUploadLimit;
            }
            set
            {
                _simUploadLimit = value;
            }
        }
        /// <summary>
        /// 单次上传的文件个数
        /// </summary>
        public int QueueSizeLimit
        {
            get
            {
                return _queueSizeLimit;
            }
            set
            {
                _queueSizeLimit = value;
            }
        }
        /// <summary>
        /// 上传文件夹相对路径
        /// </summary>
        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
            }
        }
        /// <summary>
        /// 完成后是否自动消失
        /// </summary>
        public bool IsAutoDisappear
        {
            get
            {
                return _isAutoDisappear;
            }
            set
            {
                _isAutoDisappear = value;
            }
        }
        /// <summary>
        /// 上传文件的大小限制,1M 
        /// </summary>
        public long SizeLimit
        {
            get
            {
                return _sizeLimit;
            }
            set
            {
                _sizeLimit = value;
            }
        }
        #endregion Property

        /// <summary>
        /// 过滤掉字符串中的特殊字符
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        private string FilterChars(string strSource)
        {
            return strSource.Replace('^', ' ').Replace('|', ' ');
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string strSettingChangeParams = string.Empty;
                if (FileExtDefault != FileExt)
                {
                    strSettingChangeParams += "FileExt^" + FilterChars(FileExt) + "|";
                }
                if (FileDescDefault != FileDesc)
                {
                    strSettingChangeParams += "FileDesc^" + FilterChars(FileDesc) + "|";
                }
                if (ButtonTextDefault != ButtonText)
                {
                    strSettingChangeParams += "ButtonText^" + FilterChars(ButtonText) + "|";
                }
                if (IsMultiDefault != IsMulti)
                {
                    strSettingChangeParams += "IsMulti^" + IsMulti.ToString().ToLower() + "|";
                }
                if (IsAutoDefault != IsAuto)
                {
                    strSettingChangeParams += "IsAuto^" + IsAuto.ToString().ToLower() + "|";
                }
                if (SimUploadLimitDefault != SimUploadLimit)
                {
                    strSettingChangeParams += "SimUploadLimit^" + SimUploadLimit + "|";
                }
                if (QueueSizeLimitDefault != QueueSizeLimit)
                {
                    strSettingChangeParams += "QueueSizeLimit^" + QueueSizeLimit + "|";
                }
                if (FolderDefault != Folder)
                {
                    strSettingChangeParams += "Folder^" + FilterChars(Folder) + "|";
                }
                if (IsAutoDisappearDefault != IsAutoDisappear)
                {
                    strSettingChangeParams += "IsAutoDisappear^" + IsAutoDisappear.ToString().ToLower() + "|";
                }
                if (SizeLimitDefault != SizeLimit)
                {
                    strSettingChangeParams += "SizeLimit^" + SizeLimit + "|";
                }
                if (strSettingChangeParams.EndsWith("|"))
                {
                    strSettingChangeParams = strSettingChangeParams.Remove(strSettingChangeParams.Length - 1, 1);
                }
                this.hfSettingChangeParams.Value = strSettingChangeParams;
            }
        }

        /// <summary>
        /// 上传全部完成时进入后台处理逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHidden_Click(object sender, EventArgs e)
        {
            //保存错误信息到控件
            string strAllErrInfo = this.hfAllErrInfo.Value.Trim();
            if (strAllErrInfo.ToString().EndsWith("|"))
            {
                //移除最后一个逗号
                strAllErrInfo = strAllErrInfo.Remove(strAllErrInfo.ToString().Length - 1, 1);
            }
            if (!string.IsNullOrEmpty(strAllErrInfo))
            {
                this._errInfo = strAllErrInfo;
            }

            //保存上传文件路径到控件
            string strFilePath = this.hfAllFilePath.Value.Trim();
            if (strFilePath.ToString().EndsWith("|"))
            {
                //移除最后一个逗号
                strFilePath = strFilePath.Remove(strFilePath.ToString().Length - 1, 1);
            }
            //保存成功上传的路径
            if (!string.IsNullOrEmpty(strFilePath))
            {
                this._successFilePath = strFilePath;
            }

        }

        /// <summary>
        /// 取得成功上传的文件路径
        /// </summary>
        /// <returns>文件相对路名，多个文件用"|"分隔</returns>
        public string GetSuccessFilePath()
        {
            return this._successFilePath;

        }

        /// <summary>
        /// 取得文件上传后，上传过程中出现的错误信息
        /// </summary>
        /// <returns>错误信息,形如：文件:XX，错误类型:XX,错误描述:XX</returns>
        public string GetErrorInfo()
        {
            return this._errInfo;
        }
    }
}