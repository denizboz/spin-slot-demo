using System.IO;
using System.Linq;
using Core;
using UnityEngine;
using Random = System.Random;

namespace Data {
    public class DataProvider {
        private readonly DistributionSO _distributionData;
        private readonly Random _random;

        private Lineup[] _lineups;
        private int _currentIndex;
        private string _distPath;
        private string _indexPath;
        
        private const string DistFileName = "distribution.bin";
        private const string IndexFileName = "last_index.bin";
        

        public DataProvider(DistributionSO distributionData) {
            _distributionData = distributionData;
            _random = new Random();
            Initialize();
        }

        public bool IsOkay() {
            return UnityEngine.Random.value < 0.5f;
        }
        
        private void Initialize() {
            _distPath = Path.Combine(Application.persistentDataPath, DistFileName);
            _indexPath = Path.Combine(Application.persistentDataPath, IndexFileName);
        }

        private void CreateNewDistribution() {
            var dataEntries = _distributionData.Entries;
            var lineupCount = dataEntries.Sum(entry => entry.Weight);
            _lineups = new Lineup[lineupCount];
            
            var counter = 0;
            for (var i = 0; i < dataEntries.Count; i++) {
                for (var j = 0; j < dataEntries[i].Weight; j++) {
                    _lineups[counter++] = dataEntries[i].Lineup;
                }
            }

            // TODO: change this simple shuffle -- more naturally spread out
            for (var i = lineupCount - 1; i >= 0; i--) {
                var j = _random.Next(0, i + 1);
                (_lineups[j], _lineups[i]) = (_lineups[i], _lineups[j]);
            }
        }
        
        public void HandleIteration() {
            
        }
    }
}