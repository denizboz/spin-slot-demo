using UnityEngine;

namespace Util {
    public class CameraScaler : MonoBehaviour {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2Int _referenceRatio = new(9, 16);
        [SerializeField] private float _referenceSize = 9f;

        private void Awake() {
            UpdateOrthographicSize();
        }

        private void UpdateOrthographicSize() {
            var activeRatio = (float)Screen.width / Screen.height;
            var refRatio = (float)_referenceRatio.x / _referenceRatio.y;
            _camera.orthographicSize = refRatio / Mathf.Clamp(activeRatio, 0f, 1f) * _referenceSize;
        }
    }
}