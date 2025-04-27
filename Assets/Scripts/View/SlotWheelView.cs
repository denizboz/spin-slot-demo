using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Util;

namespace View {
    public class SlotWheelView : MonoBehaviour {
        [SerializeField] private SymbolView _symbolPrefab;
        [SerializeField] private SymbolContainerSO _container;
        [SerializeField] private float _symbolHeight = 2.5f;
        [SerializeField] private int _symbolCount = 5;

        [SerializeField] private int _debugSequenceLength = 30;
        [SerializeField] private float _debugRollDuration = 3f;
        private SymbolType[] _debugSequence;
        private int _debugIndex;
        
        private Queue<SymbolView> _symbols;
        private Transform _scroller;
        private Vector3 _scrollerInitialPos;
        
        private float _updatePointY;
        private float _jumpHeight;
        private bool _isSpinning;


        private void Start() {
            CreateScroller();
            CreateDebugSequence();
            CreateSymbols();
            _scrollerInitialPos = _scroller.position;
            _updatePointY = _scrollerInitialPos.y - _symbolHeight;
            _jumpHeight = _symbolCount *  _symbolHeight;
        }
        
        [Button(ButtonSizes.Large)]
        private void InitiateSpin() {
            Spin().Forget();
        }

        private async UniTask Spin() {
            if (_isSpinning) return;
            _isSpinning = true;
            _debugIndex = 0;
            _updatePointY = _scrollerInitialPos.y - _symbolHeight;
            var target = _scrollerInitialPos + _debugSequenceLength * _symbolHeight * Vector3.down;
            _symbols.ChangeCollectionParent(_scroller);
            await _scroller.DOMove(target, _debugRollDuration).OnUpdate(OnUpdate).ToUniTask();
            _symbols.ChangeCollectionParent(transform);
            _scroller.position = _scrollerInitialPos;
            _isSpinning = false;
        }

        private void OnUpdate() {
            if (_scroller.position.y > _updatePointY) return;
            var symbol = _symbols.Dequeue();
            var sprite = _container.Get(_debugSequence[_debugIndex++], false);
            symbol.SetSprite(sprite);
            symbol.transform.position += _jumpHeight * Vector3.up;
            _symbols.Enqueue(symbol);
            _updatePointY -= _symbolHeight;
        }
        
        private void CreateScroller() {
            _scroller = new GameObject("Scroller").transform;
            _scroller.SetParent(transform);
            _scroller.localPosition = Vector3.zero;
        }

        private void CreateSymbols() {
            _symbols = new Queue<SymbolView>(_symbolCount);
            var startPos = transform.position + _symbolCount * _symbolHeight / 2f * Vector3.up;
            for (var i = _symbolCount - 1; i >= 0; i--) {
                var symbol = Instantiate(_symbolPrefab, transform);
                symbol.transform.position = startPos + i *  _symbolHeight * Vector3.down;
                _symbols.Enqueue(symbol);
            }
        }
        
        private void CreateDebugSequence() {
            _debugSequence = new SymbolType[_debugSequenceLength];
            for (var i = 0; i < _debugSequenceLength; i++) {
                _debugSequence[i] = (SymbolType)Random.Range(0, 5);
            }
        }
    }
}