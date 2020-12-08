using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceTree.Libs.Config.Disk {
    internal class DiskConfigModel {
        [JsonPropertyName("DiskTypeInclude"), JsonInclude]
        public List<string> DiskTypeInclude;

        public DiskConfigModel() {
            DiskTypeInclude = new List<string>();
        }
    }
}