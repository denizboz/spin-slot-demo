using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace View {
    public class RewardManager : MonoBehaviour {
        [SerializeField] private ParticleSystem _coinParticles;
        [SerializeField] private float _animDuration;
        [SerializeField] private int _baseParticleRate;

        private ParticleSystem.EmissionModule _emissionModule;
        private Dictionary<SymbolType, int> _particleRates;


        private void Awake() {
            var mainModule = _coinParticles.main;
            mainModule.duration = _animDuration;
            mainModule.startLifetime = _animDuration;
            _emissionModule = _coinParticles.emission;
            SetParticleRates();
        }

        public async UniTask PlayRewardAnimation(SymbolType symbolType) {
            _emissionModule.rateOverTime = _particleRates[symbolType];
            _coinParticles.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(_animDuration));
        }

        private void SetParticleRates() {
            var symbolTypes = Enum.GetValues(typeof(SymbolType)) as SymbolType[];
            _particleRates = new Dictionary<SymbolType, int>(symbolTypes!.Length);

            for (var i = 0; i < symbolTypes.Length; i++) {
                _particleRates.Add(symbolTypes[i], (i + 1) * _baseParticleRate);
            }
        }
    }
}