using System.Text.Json;
using System.Threading.Tasks;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Helper;

namespace SpaceTree.Libs.Config {
    internal class ConfigLoader {
        private const string ExcludeConfigFile = "Resources/exclude.json";

        /// <summary>
        /// Load Exclude Config
        /// </summary>
        /// <param name="excludeConfig"></param>
        public static ExcludeConfig LoadExcludeConfig() {
            var json = FileLoader.ReadFile(ExcludeConfigFile);

            var options = new JsonSerializerOptions {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<ExcludeConfig>(json, options) ?? new ExcludeConfig();
        }
    }
}