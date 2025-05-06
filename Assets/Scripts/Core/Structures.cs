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
}