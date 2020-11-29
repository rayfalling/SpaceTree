using System;
using System.Text.Json;
using System.Threading.Tasks;
using SpaceTree.Libs.Helper.File;

namespace SpaceTree.Libs.Config.Exclude {
    internal class ExcludeMatch : Config<ExcludeMatch> {
        protected ExcludeConfig ExcludeConfig { get; private set; } = null!;

        /// <summary>
        /// Do not modified config file path
        /// </summary>
        protected override string ConfigPath => "Resources/exclude.json";

        /// <summary>
        /// default construct
        /// </summary>
        public ExcludeMatch() {
        }

        /// <summary>
        /// load config file
        /// </summary>
        public sealed override void LoadConfig() {
            var json = FileLoader.ReadFile(ConfigPath);

            var options = new JsonSerializerOptions {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
            ExcludeConfig = JsonSerializer.Deserialize<ExcludeConfig>(json, options) ?? new ExcludeConfig();
        }

        /// <summary>
        /// save config to file
        /// </summary>
        public override void SaveConfig() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// test if file path contains in exclude list
        /// </summary>
        /// <param name="path"></param>
        /// <returns>false to not include</returns>
        public bool TestFileMatch(string path) {
            if (ExcludeConfig.FileExcludeList.Count == 0) return false;
            var flag = false;
            Parallel.ForEach(ExcludeConfig.FileExcludeList, file => { flag |= path.Contains(file); });
            return flag;
        }

        /// <summary>
        /// test if file path contains in exclude list
        /// </summary>
        /// <param name="path"></param>
        /// <returns>false to not include</returns>
        public bool TestDirectionMatch(string path) {
            if (ExcludeConfig.DirectoryExcludeList.Count == 0) return false;
            var flag = false;
            Parallel.ForEach(ExcludeConfig.DirectoryExcludeList, file => {
                flag |= path.Contains(file);
            });
            return flag;
        }
    }
}