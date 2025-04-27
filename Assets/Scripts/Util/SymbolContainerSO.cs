using UnityEngine;
using View;

namespace Util {
    [CreateAssetMenu(fileName = "SymbolContainer", menuName = "Data/Symbol Container")]
    public class SymbolContainerSO : ScriptableObject {
        [SerializeField] private SerializedDictionary<SymbolType, SpriteEntry> _symbolDictionary;

        public Sprite Get(SymbolType type, bool blurred) {
            var entry = _symbolDictionary[type];
            return !blurred ? entry.ClearSprite : entry.BlurSprite;
        }
    }

    [System.Serializable]
    public struct SpriteEntry {
        public Sprite ClearSprite;
        public Sprite BlurSprite;
    }
}