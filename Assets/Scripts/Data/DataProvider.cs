using System.IO;
using System.Linq;
using Core;
using UnityEngine;
using Random = System.Random;

namespace Data {
    public class DataProvider {
        private readonly DistributionSO _distributionData;
        private readonly Random _random;
        private readonly string _distPath;

        private Lineup[] _lineups;
        private int _currentIndex;
        
        private const string DistFileName = "distribution.dat";
        private const string IndexSaveKey = "last_index";
        
        public Lineup CurrentLineup => _lineups[_currentIndex];
        

        public DataProvider(DistributionSO distributionData) {
            _distributionData = distributionData;
            _random = new Random();
            _distPath = Path.Combine(Application.persistentDataPath, DistFileName);
            Initialize();
        }

        private void Initialize() {
            Application.quitting += SaveCurrentIndex;
            LoadCurrentIndex();
            
            if (File.Exists(_distPath)) {
                LoadCurrentLineups();
                return;
            }
            CreateNewDistribution();
            SaveCurrentLineups();
        }

        private void SaveCurrentLineups() {
            using var stream = new FileStream(_distPath, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(stream);
            writer.Write(_lineups.Length);
            foreach (var lineup in _lineups)
            {
                writer.Write((int)lineup.Left);
                writer.Write((int)lineup.Middle);
                writer.Write((int)lineup.Right);
            }
        }

        private void LoadCurrentLineups() {
            using var stream = new FileStream(_distPath, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(stream);
            var count = reader.ReadInt32();
            _lineups = new Lineup[count];
            for (var i = 0; i < count; i++) {
                _lineups[i] = new Lineup
                {
                    Left = (SymbolType)reader.ReadInt32(),
                    Middle = (SymbolType)reader.ReadInt32(),
                    Right = (SymbolType)reader.ReadInt32()
                };
            }
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
            _currentIndex++;
            _currentIndex %= _lineups.Length;
            
            if (_currentIndex > 0) return;
            CreateNewDistribution();
            SaveCurrentLineups();
        }
        
        private void SaveCurrentIndex() {
            PlayerPrefs.SetInt(IndexSaveKey,  _currentIndex);
        }

        private void LoadCurrentIndex() {
            _currentIndex =  PlayerPrefs.GetInt(IndexSaveKey, 0);
        }
    }
}