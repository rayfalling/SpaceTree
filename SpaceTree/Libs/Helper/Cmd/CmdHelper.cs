using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceTree.Libs.Logger;

namespace SpaceTree.Libs.Helper.Cmd {
    internal class CmdHelper {
        // ReSharper disable once StringLiteralTypo
        private static string _pageCommand = @"chcp 437";
        #if WIN32
        private static string _commandName = @"Resources/du/du.exe";
        #elif WIN64
        private static string _commandName = @"Resources/du/du64.exe";
        #endif
        private static string _commandList = @" -q -nobanner -accepteula ";

        /// <summary>
        /// execute du64.exe
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static string ExecDiskUsage(string path) {
            try {
                System.Diagnostics.Process p = new System.Diagnostics.Process {
                    StartInfo = {
                        FileName = _commandName,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        Arguments = _commandList + path
                    }
                };
                if (!p.Start())
                    throw new ArgumentException("Unable to start external tools.");
                p.StandardInput.AutoFlush = true;
                // p.StandardInput.WriteLine(_pageCommand);
                // p.StandardInput.WriteLine(_commandList + path);
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();

                return output;
            } catch (Exception ex) {
                Logger.Logger.GetInstance().Log(LogLevel.Error, ex.Message);
                return "";
            }
        }

        /// <summary>
        /// execute command by cmd
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        internal static string ExeCmd(string command) {
            try {
                System.Diagnostics.Process p = new System.Diagnostics.Process {
                    StartInfo = {
                        FileName = _commandName,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    }
                };
                if (!p.Start())
                    throw new ArgumentException("Unable to start external tools.");
                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine(_pageCommand);
                p.StandardInput.WriteLine(command + "&exit");
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();

                return output;
            } catch (Exception ex) {
                Logger.Logger.GetInstance().Log(LogLevel.Error, ex.Message);
                return "";
            }
        }
    }
}