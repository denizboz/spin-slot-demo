using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace View {
    public class SpinButton : MonoBehaviour {
        [SerializeField] private Button _button;
        [SerializeField] private SlotMachineView _slotMachine;
        
        public void InitiateSpin() => Spin().Forget();

        private async UniTask Spin() {
            _button.interactable = false;
            await _slotMachine.Spin();
            _button.interactable = true;
        }
    }
}