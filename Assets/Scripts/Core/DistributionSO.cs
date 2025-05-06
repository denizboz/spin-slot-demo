using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core {
    [CreateAssetMenu(fileName = "Distribution", menuName = "Data/Distribution")]
    public class DistributionSO : ScriptableObject {
        public List<DistEntry> Entries;
        [Space(10), ReadOnly, SerializeField] private int _totalWeight;

        private void OnValidate() {
            _totalWeight = Entries.Sum(entry => entry.Weight);
            if (_totalWeight == 100) return;
            Debug.LogWarning($"Total distribution weight is {_totalWeight}. Should be 100.");
        }
    }

    [System.Serializable]
    public struct DistEntry {
        [HorizontalGroup(0.85f), HideLabel] public Lineup Lineup;
        [HorizontalGroup(0.15f), HideLabel] public int Weight;
    }
}