using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common.Logger
{
    public enum LoggerType
    {
        Nlog,
        LogNet,
        ConSole
    }
    [Flags]
    public enum LogLevel
    {
        All = 8,
        Debug = 0,
        Error = 1,
        Info = 2,
        Warn = 4,
        Fatal = 16
    }
}
