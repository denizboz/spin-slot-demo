using Sirenix.OdinInspector;
using UnityEngine;

namespace View {
    public class SymbolView : MonoBehaviour {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSprite(Sprite sprite) {
            _spriteRenderer.sprite = sprite;
        }
    }

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