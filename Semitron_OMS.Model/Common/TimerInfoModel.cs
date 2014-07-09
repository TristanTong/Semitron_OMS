using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Model.Common
{
    /// <summary>
    /// 定时操作信息类
    /// TaskId 任务Id
    /// TimerType   类型：EveryDay(每天),DayOfWeek(每周),DayOfMonth(每月),DesDate(指定日期),LoopDays(循环天数)
    /// DateValue 日期值：TimerType="DayOfWeek"时,值为1-7表示周一到周日;TimerType="DayOfMonth"时,值为1-31表示1号到31号,
    ///                   TimerType="LoopDays"时,值为要循环的天数,TimerType为其它值时,此值无效
    /// Year   年：TimerType="DesDate"时,此值有效
    /// Month  月：TimerType="DesDate"时,此值有效
    /// Day    日：TimerType="DesDate"时,此值有效
    /// Hour   时：]
    /// Minute 分： > 设置的执行时间
    /// Second 秒：]
    [Serializable]
    public class TimerInfoModel
    {
        private int _id;
        private int _adminid;
        private int _taskid;
        private string _timertaskname;
        private string _paramvalue;
        private int _status;
        private string _timertype;
        private int? _datevalue;
        private int? _year;
        private int? _month;
        private int? _day;
        private int _hour = 00;
        private int _minute = 00;
        private int _second = 00;
        private DateTime? _nextruntime;
        public string _result;
        private int _issubtimer = 0;
        private int? _subtimerid;
        private DateTime _createtime = DateTime.Now;
        private DateTime? _updatetime;

        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 定时操作用户Id
        /// </summary>
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 操作任务Id编号
        /// </summary>
        public int TaskId
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 定时操作任务名称
        /// </summary>
        public string TimerTaskName
        {
            set { _timertaskname = value; }
            get { return _timertaskname; }
        }
        /// <summary>
        /// 操作参数值，存入调用方法时的所需参数
        /// </summary>
        public string ParamValue
        {
            set { _paramvalue = value; }
            get { return _paramvalue; }
        }
        /// <summary>
        /// 任务状态：0 等待中 1执行中 2执行完毕 3用户中止 4用户删除
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 定时类型：EveryDay(每天),DayOfWeek(每周),DayOfMonth(每月),DesDate(指定日期),LoopDays(循环天数)
        /// </summary>
        public string TimerType
        {
            set { _timertype = value; }
            get { return _timertype; }
        }
        /// <summary>
        /// 日期值：TimerType="DayOfWeek"时,值为1-7表示周一到周日;TimerType="DayOfMonth"时,值为1-31表示1号到31号, TimerType="LoopDays"时,值为要循环的天数,TimerType为其它值时,此值无效
        /// </summary>
        public int? DateValue
        {
            set { _datevalue = value; }
            get { return _datevalue; }
        }
        /// <summary>
        /// 年，TimerType="DesDate"时,此值有效
        /// </summary>
        public int? Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 月，TimerType="DesDate"时,此值有效
        /// </summary>
        public int? Month
        {
            set { _month = value; }
            get { return _month; }
        }
        /// <summary>
        /// 日，TimerType="DesDate"时,此值有效
        /// </summary>
        public int? Day
        {
            set { _day = value; }
            get { return _day; }
        }
        /// <summary>
        /// 时
        /// </summary>
        public int Hour
        {
            set { _hour = value; }
            get { return _hour; }
        }
        /// <summary>
        /// 分
        /// </summary>
        public int Minute
        {
            set { _minute = value; }
            get { return _minute; }
        }
        /// <summary>
        /// 秒
        /// </summary>
        public int Second
        {
            set { _second = value; }
            get { return _second; }
        }
        /// <summary>
        /// 任务下一次执行时间
        /// </summary>
        public DateTime? NextRunTime
        {
            set { _nextruntime = value; }
            get { return _nextruntime; }
        }
        /// <summary>
        /// 任务执行结果说明
        /// </summary>
        public string Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 是否为子任务
        /// </summary>
        public int IsSubTimer
        {
            set { _issubtimer = value; }
            get { return _issubtimer; }
        }
        /// <summary>
        /// 子任务ID
        /// </summary>
        public int? SubTimerId
        {
            set { _subtimerid = value; }
            get { return _subtimerid; }
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
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
    }
}
