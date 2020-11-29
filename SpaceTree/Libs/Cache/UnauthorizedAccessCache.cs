using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpaceTree.Libs.Cache {
    internal class UnauthorizedAccessCache : SingletonFactory<UnauthorizedAccessCache> {
        private readonly Hashtable _hashtable;

        public UnauthorizedAccessCache() {
            _hashtable = new Hashtable();
        }

        #region Public Methods

        /// <summary>
        /// add path which is not accessible when search
        /// </summary>
        /// <param name="parent">key</param>
        /// <param name="path"></param>
        public void InsertMissPath(string parent, string path) {
            if (!_hashtable.ContainsKey(parent))
                _hashtable[parent] = new List<string>();
            (_hashtable[parent] as List<string>)?.Add(path);
        }

        /// <summary>
        /// get count of all inaccessible path with given key
        /// if key is not given, count all
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetMissCount(string key = "") {
            if (key == "") {
                return _hashtable.Keys.Cast<object>()
                                 .Sum(hashtableKey => (_hashtable[hashtableKey] as List<string>)!.Count);
            }

            return (_hashtable[key] as List<string>)?.Count ?? 0;
        }

        /// <summary>
        /// get inaccessible path list with given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetInaccessiblePath(string key) {
            return _hashtable[key] as List<string> ?? new List<string>();
        }

        /// <summary>
        /// get inaccessible path list in all keys
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMissingPath() {
            List<string> missingList = new List<string>();
            foreach (var key in _hashtable.Keys) {
                missingList.AddRange((_hashtable[key] as List<string> ?? new List<string>()));
            }

            return missingList;
        }

        /// <summary>
        /// clear stored values and keys
        /// </summary>
        public void Clear() => _hashtable.Clear();

        #endregion
    }
}