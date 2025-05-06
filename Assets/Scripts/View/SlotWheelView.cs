using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Util;

namespace View {
    public class SlotWheelView : MonoBehaviour {
        [Header("References")] [SerializeField]
        private SymbolView _symbolPrefab;

        [SerializeField] private SymbolContainerSO _container;

        [Header("Parameters")] [SerializeField]
        private int _sequenceLength = 30;

        [SerializeField] private float _symbolHeight = 2.5f;
        [SerializeField] private int _symbolCount = 5;
        [SerializeField] private int _endOffset = 2;

        private SymbolType[] _sequence;
        private int _rollIndex;

        private SymbolSequenceGenerator _sequenceGenerator;
        private Queue<SymbolView> _symbols;
        private Transform _scroller;
        private Vector3 _scrollerInitialPos;

        private float _updatePointY;
        private float _jumpHeight;

        private int FullSequenceLength => _sequenceLength + _endOffset;


        private void Start() {
            CreateScroller();
            CreateSymbols();
            _sequenceGenerator = new SymbolSequenceGenerator(_endOffset); // TODO: Inject
            _scrollerInitialPos = _scroller.position;
            _updatePointY = _scrollerInitialPos.y - _symbolHeight;
            _jumpHeight = _symbolCount * _symbolHeight;
        }

        public async UniTask Spin(SymbolType targetSymbol, float duration, Ease ease = Ease.Linear) {
            _rollIndex = 0;
            _updatePointY = _scrollerInitialPos.y - _symbolHeight;
            _sequence = _sequenceGenerator.Generate(targetSymbol, _sequenceLength);
            var target = _scrollerInitialPos + FullSequenceLength * _symbolHeight * Vector3.down;
            _symbols.ChangeCollectionParent(_scroller);
            await _scroller.DOMove(target, duration).SetEase(ease).OnUpdate(OnUpdate).ToUniTask();
            _symbols.ChangeCollectionParent(transform);
            _scroller.position = _scrollerInitialPos;
        }

        public async UniTask Spin(WheelSpinParams spinParams) {
            _rollIndex = 0;
            _updatePointY = _scrollerInitialPos.y - _symbolHeight;
            _sequence = _sequenceGenerator.Generate(spinParams.Target, _sequenceLength);
            var target = _scrollerInitialPos + FullSequenceLength * _symbolHeight * Vector3.down;
            _symbols.ChangeCollectionParent(_scroller);
            await UniTask.Delay(TimeSpan.FromSeconds(spinParams.Delay));
            await _scroller.DOMove(target, spinParams.Duration).SetEase(spinParams.Ease).OnUpdate(OnUpdate).ToUniTask();
            _symbols.ChangeCollectionParent(transform);
            _scroller.position = _scrollerInitialPos;
        }

        private void OnUpdate() {
            if (_scroller.position.y > _updatePointY) return;
            var symbol = _symbols.Dequeue();
            var blurred = _rollIndex < _sequenceLength - _symbolCount + _endOffset + 1;
            var sprite = _container.Get(_sequence[_rollIndex++], blurred);
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
            var startPos = transform.position + _symbolCount * _symbolHeight / 2f * Vector3.up -
                           _symbolHeight / 2f * Vector3.up;
            for (var i = _symbolCount - 1; i >= 0; i--) {
                var symbol = Instantiate(_symbolPrefab, transform);
                symbol.transform.position = startPos + i * _symbolHeight * Vector3.down;
                _symbols.Enqueue(symbol);
            }
        }
    }
}