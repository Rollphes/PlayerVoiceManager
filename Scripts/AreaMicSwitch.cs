
using UdonSharp;

using UnityEngine;

namespace io.github.rollphes.playerVoiceManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class AreaMicSwitch : UdonSharpBehaviour
    {
        [SerializeField] private AreaMic _areaMic = null;

        public override void Interact() {
            if (this._areaMic == null) {
                return;
            }

            this._areaMic.State = !this._areaMic.State;
        }
    }
}
