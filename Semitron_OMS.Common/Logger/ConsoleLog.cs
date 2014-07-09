using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Semitron_OMS.Common.Logger
{
    public class ConsoleLog : ILog
    {
        private log4net.ILog myLogger;

        public ConsoleLog(log4net.ILog Logger)
        {
            myLogger = Logger;
        }

        public ConsoleLog()
        {
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            this.Write(ConsoleColour.ForeGroundColour.White, message, null);
        }

        public void Debug(string message, Exception exception)
        {
            this.Write(LogLevel.Debug, message, exception);
        }

        /// <summary>
        /// 高亮运行信息
        /// </summary>
        /// <param name="message"></param>
        public void HighInfo(string message)
        {
            this.Write(ConsoleColour.ForeGroundColour.Cyan, message, null);
        }


        /// <summary>
        /// 运行信息
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            this.Write(ConsoleColour.ForeGroundColour.Green, message, null);
        }

        /// <summary>
        /// 运行信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(string message, Exception exception)
        {
            this.Write(ConsoleColour.ForeGroundColour.Red, message, exception);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            this.Write(ConsoleColour.ForeGroundColour.Yellow, message, null);
            if (myLogger != null)
            {
                myLogger.Error(message);
            }
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Warn(string message, Exception exception)
        {
            this.Write(ConsoleColour.ForeGroundColour.Red, message, exception);
            if (myLogger != null)
            {
                myLogger.Error(message, exception);
            }
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            this.Write(ConsoleColour.ForeGroundColour.Red, message, null);
            if (myLogger != null)
            {
                myLogger.Fatal(message);
            }
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Error(string message, Exception exception)
        {
            this.Write(ConsoleColour.ForeGroundColour.Red, message, exception);
            if (myLogger != null)
            {
                myLogger.Fatal(message, exception);
            }
        }


        public void InitName(string name)
        {
        }

        public void InitName(Type type)
        {
        }

        private void Write(string message, Exception exception)
        {
            ConsoleColour.SetForeGroundColour(
            ConsoleColour.ForeGroundColour.Yellow);
            Console.WriteLine(DateTime.Now + ":" + message);
            if (exception != null)
            {
                ConsoleColour.SetForeGroundColour(
               ConsoleColour.ForeGroundColour.Red);
                Console.WriteLine(DateTime.Now + ":" + exception.Message + "||" + exception.StackTrace);
            }
        }

        private void Write(LogLevel level, string message, Exception exception)
        {
            ConsoleColour.SetForeGroundColour(
               ConsoleColour.ForeGroundColour.Yellow);
            Console.WriteLine(DateTime.Now + ":" + message);
            if (exception != null)
            {
                ConsoleColour.SetForeGroundColour(
               ConsoleColour.ForeGroundColour.Red);
                Console.WriteLine(DateTime.Now + ":" + exception.Message + "||" + exception.StackTrace);
            }
        }

        private void Write(ConsoleColour.ForeGroundColour colour, string message, Exception exception)
        {
            ConsoleColour.SetForeGroundColour(colour);
            Console.WriteLine(DateTime.Now + ":" + message);
            if (exception != null)
            {
                ConsoleColour.SetForeGroundColour(
               ConsoleColour.ForeGroundColour.Red);
                Console.WriteLine(DateTime.Now + ":" + exception.Message + "||" + exception.StackTrace);
            }
        }


    }
    /// <summary>
    /// Static class for console colour manipulation.
    /// </summary>
    public class ConsoleColour
    {
        // constants for console streams
        const int STD_INPUT_HANDLE = -10;
        const int STD_OUTPUT_HANDLE = -11;
        const int STD_ERROR_HANDLE = -12;
        [DllImportAttribute("Kernel32.dll")]
        private static extern IntPtr GetStdHandle
        (
            int nStdHandle // input, output, or error device
        );
        [DllImportAttribute("Kernel32.dll")]
        private static extern bool SetConsoleTextAttribute
        (
            IntPtr hConsoleOutput, // handle to screen buffer
            int wAttributes    // text and background colors
        );
        // colours that can be set
        [Flags]
        public enum ForeGroundColour
        {
            Black = 0x0000,
            Blue = 0x0001,
            Green = 0x0002,
            Cyan = 0x0003,
            Red = 0x0004,
            Magenta = 0x0005,
            Yellow = 0x0006,
            Grey = 0x0007,
            White = 0x0008
        }
        // class can not be created, so we can set colours
        // without a variable
        private ConsoleColour()
        {
        }
        public static bool SetForeGroundColour()
        {
            // default to a white-grey
            return SetForeGroundColour(ForeGroundColour.Grey);
        }
        public static bool SetForeGroundColour(
            ForeGroundColour foreGroundColour)
        {
            // default to a bright white-grey
            return SetForeGroundColour(foreGroundColour, true);
        }
        public static bool SetForeGroundColour(
            ForeGroundColour foreGroundColour,
            bool brightColours)
        {
            // get the current console handle
            IntPtr nConsole = GetStdHandle(STD_OUTPUT_HANDLE);
            int colourMap;

            // if we want bright colours OR it with white
            if (brightColours)
                colourMap = (int)foreGroundColour |
                    (int)ForeGroundColour.White;
            else
                colourMap = (int)foreGroundColour;
            // call the api and return the result
            return SetConsoleTextAttribute(nConsole, colourMap);
        }
    }
}
