using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceTree.Libs.Logger;

namespace SpaceTree.Libs.Helper {
    internal class FileLoader {
        public static string ReadFile(string path) {
            try {
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            } catch (FileNotFoundException) {
                Logger.Logger.GetInstance().Log(LogLevel.Error, $"Can't open file: {path}");
            } catch (Exception e) {
                Logger.Logger.GetInstance().Log(LogLevel.Error, e.Message);
            }

            return "";
        }
    }
}