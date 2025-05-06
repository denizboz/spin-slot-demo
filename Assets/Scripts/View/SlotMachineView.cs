using System;
using Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View {
    public class SlotMachineView : MonoBehaviour {
        [SerializeField] private WheelSpinner _leftSpinner;
        [SerializeField] private WheelSpinner _middleSpinner;
        [SerializeField] private WheelSpinner _rightSpinner;
        
        [SerializeField] private Lineup _debugLineup;
        
        private Lineup _currentLineup;
        private bool _isSpinning;
        
        
        [Button]
        private void SetRandomLineup() {
            if (!Application.isPlaying) return;
            _debugLineup.Left = (SymbolType)Random.Range(0, 5);
            _debugLineup.Middle = (SymbolType)Random.Range(0, 5);
            _debugLineup.Right = (SymbolType)Random.Range(0, 5);
        }
        
        public UniTask InitiateSpin() {
            return Spin();
        }
        
        public async UniTask Spin() {
            if (_isSpinning) return;
            _isSpinning = true;

            await _leftSpinner.Spin(_debugLineup.Left);
            await _middleSpinner.Spin(_debugLineup.Middle);
            await _rightSpinner.Spin(_debugLineup.Right);
            
            _isSpinning = false;
        }
        
        [Serializable]
        private class WheelSpinner {
            [SerializeField] private SlotWheelView _view;
            [SerializeField] private WheelSpinParams _spinParams;
            
            public async UniTask Spin(SymbolType target) {
                _spinParams.Target = target;
                await _view.Spin(_spinParams);
            }
        }
    }
}