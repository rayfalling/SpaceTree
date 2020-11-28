namespace SpaceTree.Libs.Config {
    internal abstract class Config<T> : SingletonFactory<T> where T : Config<T> {
        protected abstract string ConfigPath { get; }

        protected Config() { }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        protected abstract void LoadConfig();

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        protected abstract void SaveConfig();
    }
}