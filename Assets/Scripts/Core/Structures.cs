using System;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Core {
    public enum SymbolType {
        A, Bonus, Seven, Wild, Jackpot,
    }

    [System.Serializable]
    public struct Lineup {
        [HorizontalGroup, HideLabel]
        public SymbolType Left;
        [HorizontalGroup, HideLabel]
        public SymbolType Middle;
        [HorizontalGroup, HideLabel]
        public SymbolType Right;
    }
    
    [Serializable]
    public struct WheelSpinParams {
        [ReadOnly] public SymbolType Target;
        public float Duration;
        public float Delay;
        public Ease Ease;
    }
}