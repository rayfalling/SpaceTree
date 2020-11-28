using System;
using System.IO;
using System.Text;

namespace SpaceTree.Libs.Logger {
    /// <summary>
    /// 一个简单的日志记录类
    /// </summary>
    internal class Logger : SingletonFactory<Logger> {
        // private static Logger? _log;
        private const string LogFilePrefix = "SpaceTree";
        private readonly StreamWriter _fileStreamWriter;
        private LogLevel _logLevel;

        public Logger() {
            _logLevel = LogLevel.Info;
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "\\logs\\";
            _fileStreamWriter = new StreamWriter(new MemoryStream());
            try {
                // 创建日志记录文件
                string logTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                string fileName = LogFilePrefix + "_" + logTime + ".log";
                string logFilePath = logPath + fileName;

                if (Directory.Exists(logPath) == false) {
                    Directory.CreateDirectory(logPath);
                }

                if (File.Exists(logFilePath) == false) {
                    File.Create(logFilePath).Close();
                }

                _fileStreamWriter = new StreamWriter(logFilePath, true, Encoding.UTF8);
            } catch (Exception e) {
                System.Diagnostics.Debug.Print("Log Error:" + e.Message);
            }
        }

        // public static Logger GetInstance() {
        //     if (_log != null) return _log;
        //     _log = new Logger();
        //     return _log;
        // }

        public void Log(LogLevel level, string logString) {
            if (!CheckLogLevel(level))
                return;
            string logTime = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + $" [{LogFilePrefix}] [{level}]: ";
            try {
                // Log to output
                System.Diagnostics.Debug.Print(logTime + logString);

                // Log to file
                _fileStreamWriter.WriteLine(logTime + logString);
                _fileStreamWriter.Flush();
            } catch (Exception ex) {
                System.Diagnostics.Debug.Print("Log Error:" + ex.Message);
            }
        }

        public void SetLogLevel(LogLevel level) => _logLevel = level;

        private bool CheckLogLevel(LogLevel level) => level >= _logLevel;

        ~Logger() {
            _fileStreamWriter.Flush();
            _fileStreamWriter.Close();
            _fileStreamWriter.Dispose();
        }
    }
}