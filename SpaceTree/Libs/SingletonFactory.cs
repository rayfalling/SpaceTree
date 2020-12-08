using System;
using System.Diagnostics;
using System.Linq;

namespace SpaceTree.Libs {
    /// <summary>
    /// singleton factory class
    /// because the limit of C#, subclass must have public
    /// construct method, but we will check callee when init
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonFactory<T> where T : class, new() {
        private static readonly object LockingObject = new object();
        private static T? _instance;

        static SingletonFactory() {
        }

        protected SingletonFactory() {
            CheckCallee();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static T GetInstance() {
            if (_instance != null) return _instance;
            lock (LockingObject) {
                _instance = new T();
            }

            return _instance!;
        }

        private static void CheckCallee() {
            StackTrace trace = new StackTrace();

            var callCount = trace.GetFrames().AsParallel()
                                 .Count(stackFrame => stackFrame.GetMethod()?.Name == nameof(GetInstance));
            if (callCount == 0) {
                throw new Exception("Try to init a singleton class by new operator");
            }
        }
    }
}