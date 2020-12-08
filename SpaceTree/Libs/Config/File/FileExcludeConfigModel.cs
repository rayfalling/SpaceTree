using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceTree.Libs.Config.File {
    internal class FileExcludeConfigModel {
        [JsonPropertyName("Filename"), JsonInclude]
        public List<string> FileExcludeList;

        [JsonPropertyName("Directory"), JsonInclude]
        public List<string> DirectoryExcludeList;

        public FileExcludeConfigModel() {
            FileExcludeList = new List<string>();

            DirectoryExcludeList = new List<string>();
        }
    }
}