using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpaceTree.Libs.Cache {
    internal class UnauthorizedAccessCache : SingletonFactory<UnauthorizedAccessCache> {
        internal Hashtable Hashtable;

        public UnauthorizedAccessCache() {
            Hashtable = new Hashtable();
        }

        /// <summary>
        /// add path which is not accessible when search
        /// </summary>
        /// <param name="parent">key</param>
        /// <param name="path"></param>
        public void InsertMissPath(string parent, string path) {
            if (!Hashtable.ContainsKey(parent))
                Hashtable[parent] = new List<string>();
            (Hashtable[parent] as List<string>)?.Add(path);
        }

        /// <summary>
        /// get count of all inaccessible path with given key
        /// if key is not given, count all
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetMissCount(string key = "") {
            if (key == "") {
                return Hashtable.Keys.Cast<object>()
                                .Sum(hashtableKey => (Hashtable[hashtableKey] as List<string>)!.Count);
            }

            return (Hashtable[key] as List<string>)?.Count ?? 0;
        }

        /// <summary>
        /// get inaccessible path list with given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetInaccessiblePath(string key) {
            return Hashtable[key] as List<string> ?? new List<string>();
        }
    }
}