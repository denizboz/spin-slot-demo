using System;
using Core;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Data;
using Util;
using Zenject;

namespace View {
    public class SlotMachineView : MonoBehaviour {
        [SerializeField] private WheelSpinner _leftSpinner;
        [SerializeField] private WheelSpinner _middleSpinner;
        [SerializeField] private WheelSpinner _rightSpinner;
        [Space(10)]
        [SerializeField] private RewardManager _rewardManager;

        private DataProvider _dataProvider;
        private bool _isSpinning;


        [Inject]
        private void Inject(DataProvider dataProvider) {
            _dataProvider = dataProvider;
        }
        
        public async UniTask Spin() {
            if (_isSpinning) return;
            _isSpinning = true;

            var lineup = _dataProvider.CurrentLineup;
            await _leftSpinner.Spin(lineup.Left);
            await _middleSpinner.Spin(lineup.Middle);
            await _rightSpinner.Spin(lineup.Right);
            
            _dataProvider.HandleIteration();
            if (lineup.IsRewardable()) await _rewardManager.PlayRewardAnimation(lineup.Left);
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