using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using View;

namespace Util {
    [CreateAssetMenu(fileName = "Distribution", menuName = "Data/Distribution")]
    public class DistributionSO : ScriptableObject {
        public List<DistEntry> Entries;
        [Space(10), ReadOnly, SerializeField] private int _totalPercentage;

        private void OnValidate() {
            _totalPercentage = Entries.Sum(entry => entry.Percentage);
            if (_totalPercentage == 100) return;
            Debug.LogWarning($"Total distribution percentage is {_totalPercentage}. Should be 100.");
        }
    }

    [System.Serializable]
    public struct DistEntry {
        [HorizontalGroup(0.85f), HideLabel] public Lineup Lineup;
        [HorizontalGroup(0.15f), HideLabel] public int Percentage;
    }
}