namespace SpaceTree.Libs {
    /// <summary>
    /// 单例工厂类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonFactory<T> where T : class {
        private static readonly object LockingObject = new object();
        private static T? _instance;

        static SingletonFactory() { }

        protected SingletonFactory() { }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static T GetInstance() {
            if (_instance != null) return _instance;
            lock (LockingObject) {
                _instance ??= default!;
            }

            return _instance;
        }
    }
}