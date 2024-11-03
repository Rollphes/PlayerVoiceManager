using UdonSharp;

using UnityEngine;

namespace io.github.rollphes.playerVoiceManager
{
    public abstract class BaseVoiceSetting : UdonSharpBehaviour
    {
        public abstract float Gain { get; set; }
        public abstract float DistanceNear { get; set; }
        public abstract float DistanceFar { get; set; }
        public abstract float VolumetricRadius { get; set; }
        public abstract int[] GlobalPlayerIds { get; set; }
        public abstract int[] LocalPlayerIds { get; set; }

        [SerializeField] private PlayerVoiceManager _playerVoiceManager;

        protected void EmitGlobalPlayerIdsChanged() {
            if (this._playerVoiceManager != null) {
                this._playerVoiceManager.UpdateGlobalSetting();
            } else {
                Debug.LogError("PlayerVoiceManager が設定されていません。");
            }
        }
        protected void EmitLocalPlayerIdsChanged() {
            if (this._playerVoiceManager != null) {
                this._playerVoiceManager.UpdateLocalSetting();
            } else {
                Debug.LogError("PlayerVoiceManager が設定されていません。");
            }
        }
    }
}