using System;
using System.IO;
using System.Text.Json;
using SpaceTree.Libs.Helper.File;

namespace SpaceTree.Libs.Config.Disk {
    internal class DiskConfig : Config<DiskConfig> {
        #region Protected Property

        protected DiskConfigModel DiskConfigModel { get; private set; } = null!;

        #endregion

        #region Construct

        /// <summary>
        /// default construct
        /// </summary>
        public DiskConfig() {
        }

        #endregion

        #region Override

        /// <summary>
        /// Do not modified config file path
        /// </summary>
        protected override string ConfigPath => "Resources/config/disk.json";

        /// <summary>
        /// load config file
        /// </summary>
        public override void LoadConfig() {
            var json = FileLoader.ReadFile(ConfigPath);

            var options = new JsonSerializerOptions {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
            DiskConfigModel = JsonSerializer.Deserialize<DiskConfigModel>(json, options) ?? new DiskConfigModel();
        }

        public override void SaveConfig() {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Test if DriveType is in config
        /// </summary>
        /// <param name="drive"></param>
        /// <returns></returns>
        public bool TestDiskType(DriveInfo drive) {
            return DiskConfigModel.DiskTypeInclude.Contains(drive.DriveType.ToString()) && drive.IsReady;
        }

        #endregion
    }
}