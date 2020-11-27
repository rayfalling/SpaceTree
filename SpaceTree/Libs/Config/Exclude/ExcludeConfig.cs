using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceTree.Libs.Cache {
    internal class ExcludeConfig {
        [JsonPropertyName("filename"), JsonInclude]
        public List<string> FileExcludeList;

        [JsonPropertyName("directory"), JsonInclude]
        public List<string> DirectoryExcludeList;

        public ExcludeConfig() {
            FileExcludeList = new List<string>();

            DirectoryExcludeList = new List<string>();
        }
    }
}