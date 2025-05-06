using System;
using System.Collections.Generic;

namespace Core {
    public class SymbolSequenceGenerator : ISequenceGenerator<SymbolType> {
        private readonly Random _random;
        private readonly int _endOffset;
        private readonly List<SymbolType> _symbols;
        private SymbolType _lastSymbol;


        public SymbolSequenceGenerator(int endOffset) {
            _random = new Random();
            _endOffset = endOffset;
            
            var symbols = Enum.GetValues(typeof(SymbolType));
            var varietySize = symbols.Length;
            _symbols = new List<SymbolType>(varietySize);
            foreach (SymbolType symbol in symbols) _symbols.Add(symbol);
        }
        
        public SymbolType[] Generate(SymbolType end, int count) {
            var sequence = new SymbolType[count + _endOffset];
            sequence[count - 1] = end;
            
            // Fill from first to target
            _lastSymbol = end;
            for (var i = count - 2; i >= 0; i--) {
                sequence[i] = GetNextRandomSymbol();
            }
            
            // Fill after target
            _lastSymbol = end;
            for (var i = count; i < sequence.Length; i++) {
                sequence[i] = GetNextRandomSymbol();
            }
            
            return sequence;
        }

        private SymbolType GetNextRandomSymbol() {
            _symbols.Remove(_lastSymbol);
            var symbol = _symbols[_random.Next(_symbols.Count - 1)];
            _symbols.Add(_lastSymbol);
            _lastSymbol = symbol;
            return symbol;
        }
    }
}