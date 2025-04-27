using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util {
    [Serializable]
    public class SerializedDictionary<TKey, TValue> {
        [SerializeField] private List<SerializedKeyValuePair> _keyValuePairs = new();
        private Dictionary<TKey, TValue> _dictionary;

        public TValue this[TKey key] {
            get {
                EnsureDictionary();
                return _dictionary[key];
            }
        }

        public bool ContainsKey(TKey key) {
            EnsureDictionary();
            return _dictionary.ContainsKey(key);
        }

        private void EnsureDictionary() {
            if (_dictionary != null) return;
            _dictionary = new Dictionary<TKey, TValue>();
            foreach (var pair in _keyValuePairs) {
                _dictionary[pair.Key] = pair.Value;
            }
        }
        
        [Serializable]
        internal class SerializedKeyValuePair {
            public TKey Key;
            public TValue Value;
        }
    }
}