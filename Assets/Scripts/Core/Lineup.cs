using System;
using Sirenix.OdinInspector;

namespace Core {
    public enum SymbolType {
        A, Bonus, Seven, Wild, Jackpot,
    }

    [Serializable]
    public struct Lineup : IEquatable<Lineup> {
        [HorizontalGroup, HideLabel]
        public SymbolType Left;
        [HorizontalGroup, HideLabel]
        public SymbolType Middle;
        [HorizontalGroup, HideLabel]
        public SymbolType Right;
        
        public bool Equals(Lineup other) {
            return Left == other.Left && Middle == other.Middle && Right == other.Right;
        }

        public override bool Equals(object obj) {
            return obj is Lineup other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine((int)Left, (int)Middle, (int)Right);
        }
    }
}