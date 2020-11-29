using System;
using System.IO;
using System.Text;
using SpaceTree.Libs.Logger;

namespace SpaceTree.Libs.Helper.File {
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