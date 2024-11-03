
using UdonSharp;

using UnityEngine;

namespace io.github.rollphes.playerVoiceManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PrivateAreaSwitch : UdonSharpBehaviour
    {
        [SerializeField] private PrivateArea _privateArea = null;

        public override void Interact() {
            if (this._privateArea == null) {
                return;
            }

            this._privateArea.State = !this._privateArea.State;
        }
    }
}
