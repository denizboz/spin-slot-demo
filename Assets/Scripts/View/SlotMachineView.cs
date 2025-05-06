using System;
using Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View {
    public class SlotMachineView : MonoBehaviour {
        [Header("Wheels")]
        [SerializeField] private SlotWheelView _leftWheel;
        [SerializeField] private SlotWheelView _middleWheel;
        [SerializeField] private SlotWheelView _rightWheel;

        [SerializeField] private float _leftSpinTime = 1f;
        [SerializeField] private float _middleSpinTime = 1f;
        [SerializeField] private float _rightSpinTime = 2f;
        
        [Header("Spin Delays")]
        [SerializeField] private float _leftSpinDelay = 0.1f;
        [SerializeField] private float _middleSpinDelay = 0.1f;
        [SerializeField] private float _rightSpinDelay = 0.2f;
        
        [Header("Debug")]
        [SerializeField] private Lineup _debugLineup;
        [SerializeField] private bool _setRandomLineup;
        
        private bool _isSpinning;
        

        [Button(ButtonSizes.Large)]
        private void InitiateSpin() {
            if (!Application.isPlaying) return;
            Spin().Forget();
        }

        private async UniTask Spin() {
            if (_isSpinning) return;
            _isSpinning = true;

            if (_setRandomLineup) {
                _debugLineup.Left = (SymbolType)Random.Range(0, 5);
                _debugLineup.Middle = (SymbolType)Random.Range(0, 5);
                _debugLineup.Right = (SymbolType)Random.Range(0, 5);
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(_leftSpinDelay));
            await _leftWheel.Spin(_debugLineup.Left, _leftSpinTime);
            await UniTask.Delay(TimeSpan.FromSeconds(_middleSpinDelay));
            await _middleWheel.Spin(_debugLineup.Middle, _middleSpinTime);
            await UniTask.Delay(TimeSpan.FromSeconds(_rightSpinDelay));
            await _rightWheel.Spin(_debugLineup.Right, _rightSpinTime);
            
            _isSpinning = false;
        }
    }
}