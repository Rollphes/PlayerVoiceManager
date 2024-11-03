using UdonSharp;

using UnityEngine;

using VRC.SDKBase;

namespace io.github.rollphes.playerVoiceManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class HandMic : BaseVoiceSetting
    {
        public override float Gain { get => this._gain; set => this._gain = value; }
        public override float DistanceNear { get => this._distanceNear; set => this._distanceNear = value; }
        public override float DistanceFar { get => this._distanceFar; set => this._distanceFar = value; }
        public override float VolumetricRadius { get => this._volumetricRadius; set => this._volumetricRadius = value; }
        public override int[] GlobalPlayerIds { get => this._activePlayerIds; set => this._activePlayerIds = value; }
        public override int[] LocalPlayerIds { get; set; }

        [SerializeField] private float _gain = 0.0f;
        [SerializeField] private float _distanceNear = 100.0f;
        [SerializeField] private float _distanceFar = 100.0f;
        [SerializeField] private float _volumetricRadius = 0.0f;
        private int[] _activePlayerIds = new int[0];

        public override void OnPickup() {
            this._activePlayerIds = new int[1] { Networking.LocalPlayer.playerId };
            this.EmitGlobalPlayerIdsChanged();
        }

        public override void OnDrop() {
            this._activePlayerIds = new int[0];
            this.EmitGlobalPlayerIdsChanged();
        }
    }
}