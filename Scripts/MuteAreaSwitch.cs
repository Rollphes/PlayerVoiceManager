
using UdonSharp;

using UnityEngine;

namespace io.github.rollphes.playerVoiceManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MuteAreaSwitch : UdonSharpBehaviour
    {
        [SerializeField] private MuteArea _muteArea = null;

        public override void Interact() {
            if (this._muteArea == null) {
                return;
            }

            this._muteArea.State = !this._muteArea.State;
        }
    }
}
