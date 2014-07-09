using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Semitron_OMS.Common.Logger
{
    public class Log4net : ILog
    {
        private static log4net.ILog _logger = LogManager.GetLogger(
              System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            _logger.Debug(message, exception);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            _logger.Info(message, exception);
        }

        public void InitName(string name)
        {

        }

        public void InitName(Type type)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }
    }
}
