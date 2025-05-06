using UnityEngine;

namespace View {
    public class SymbolView : MonoBehaviour {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSprite(Sprite sprite) {
            _spriteRenderer.sprite = sprite;
        }
    }
}