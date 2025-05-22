using System;
using Core;

namespace Util {
    public static class Utility {
        private static readonly Random _random;
        private static readonly SymbolType[] _symbols;
        
        static Utility() {
            _random = new Random();
            _symbols = Enum.GetValues(typeof(SymbolType)) as SymbolType[];
        }

        public static SymbolType GetRandomSymbol() {
            return _symbols[_random.Next(_symbols.Length)];
        }
    }
}