using System;
using System.Linq;
using System.Text.Json;
using SpaceTree.Libs.Helper;

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
        protected override void SaveConfig() { throw new NotImplementedException(); }

        /// <summary>
        /// test if file path contains in exclude list
        /// </summary>
        /// <param name="path"></param>
        public void TestFileMatch(string path) {
            ExcludeConfig.FileExcludeList.AsParallel().Any(item => path.Contains(path));
        }
    }
}