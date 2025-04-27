using System.Collections.Generic;
using UnityEngine;
using Util;

namespace View {
    public class SymbolFactory : MonoBehaviour {
        [SerializeField] private SymbolView _symbolPrefab;
        [SerializeField] private SymbolContainerSO _container;
        [SerializeField] private int _poolSize = 32;
        
        private Queue<SymbolView> _symbolPool;

        private void Awake() {
            CreatePool();
        }

        public SymbolView Get(SymbolType type, bool blurred = false) {
            var symbol = _symbolPool.Dequeue();
            var sprite = _container.Get(type, blurred);
            symbol.SetSprite(sprite);
            symbol.gameObject.SetActive(true);
            return symbol;
        }

        public void Return(SymbolView symbol) {
            symbol.gameObject.SetActive(false);
            _symbolPool.Enqueue(symbol);
        }
        
        private void CreatePool() {
            _symbolPool = new Queue<SymbolView>(_poolSize);
            for (var i = 0; i < _poolSize; i++) {
                var symbol = Instantiate(_symbolPrefab, transform);
                symbol.gameObject.SetActive(false);
                _symbolPool.Enqueue(symbol);
            }
        }
    }
}