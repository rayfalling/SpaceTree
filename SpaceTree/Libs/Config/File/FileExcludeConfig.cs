using System;
using System.Text.Json;
using System.Threading.Tasks;
using SpaceTree.Libs.Helper.File;

namespace SpaceTree.Libs.Config.File {
    internal class FileExcludeConfig : Config<FileExcludeConfig> {
        #region Protected Property

        protected FileExcludeConfigModel FileExcludeConfigModel { get; private set; } = null!;

        #endregion

        #region Construct

        /// <summary>
        /// default construct
        /// </summary>
        public FileExcludeConfig() {
        }

        #endregion

        #region Override

        /// <summary>
        /// Do not modified config file path
        /// </summary>
        protected override string ConfigPath => "Resources/config/exclude.json";

        /// <summary>
        /// load config file
        /// </summary>
        public sealed override void LoadConfig() {
            var json = FileLoader.ReadFile(ConfigPath);

            var options = new JsonSerializerOptions {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
            FileExcludeConfigModel = JsonSerializer.Deserialize<FileExcludeConfigModel>(json, options) ??
                                     new FileExcludeConfigModel();
        }

        /// <summary>
        /// save config to file
        /// </summary>
        public override void SaveConfig() {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// test if file path contains in exclude list
        /// </summary>
        /// <param name="path"></param>
        /// <returns>false to not include</returns>
        public bool TestFileMatch(string path) {
            if (FileExcludeConfigModel.FileExcludeList.Count == 0) return false;
            var flag = false;
            Parallel.ForEach(FileExcludeConfigModel.FileExcludeList, file => { flag |= path.Contains(file); });
            return flag;
        }

        /// <summary>
        /// test if file path contains in exclude list
        /// </summary>
        /// <param name="path"></param>
        /// <returns>false to not include</returns>
        public bool TestDirectionMatch(string path) {
            if (FileExcludeConfigModel.DirectoryExcludeList.Count == 0) return false;
            var flag = false;
            Parallel.ForEach(FileExcludeConfigModel.DirectoryExcludeList, file => { flag |= path.Contains(file); });
            return flag;
        }

        #endregion
    }
}