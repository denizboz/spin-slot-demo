using System;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Core {
    [Serializable]
    public struct WheelSpinParams {
        [ReadOnly] public SymbolType Target;
        public float Duration;
        public float Delay;
        public Ease Ease;
    }
}