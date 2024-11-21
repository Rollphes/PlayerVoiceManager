using UdonSharp;

using UnityEngine;

using VRC.SDK3.Data;
using VRC.SDKBase;
namespace io.github.rollphes.playerVoiceManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class AreaMic : BaseVoiceSetting
    {
        public override float Gain { get => this._gain; set => this._gain = value; }
        public override float DistanceNear { get => this._distanceNear; set => this._distanceNear = value; }
        public override float DistanceFar { get => this._distanceFar; set => this._distanceFar = value; }
        public override float VolumetricRadius { get => this._volumetricRadius; set => this._volumetricRadius = value; }
        public override int[] GlobalPlayerIds { get; set; }
        public override int[] LocalPlayerIds {
            get {
                var result = new int[this._inPlayerIdList.Count];
                for (var i = 0; i < this._inPlayerIdList.Count; i++) {
                    result[i] = this._inPlayerIdList[i].Int;
                }
                return result;
            }
            set => Debug.LogError($"Can't Use Setter in LocalPlayerIds:{value}");
        }

        [Header("---　エリアマイク／Area Mic　---")]

        [Header("マイクON時の設定")]
        [SerializeField] private float _gain = 0.0f;
        [SerializeField] private float _distanceNear = 100.0f;
        [SerializeField] private float _distanceFar = 100.0f;
        [SerializeField] private float _volumetricRadius = 0.0f;
        private readonly DataList _inPlayerIdList = new DataList();

        public bool State {
            set {
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                this._state = value;
                if (this._onObjects != null) {
                    foreach (var OnObject in this._onObjects) {
                        OnObject.SetActive(value);
                    }
                }

                if (this._offObjects != null) {
                    foreach (var OffObject in this._offObjects) {
                        OffObject.SetActive(!value);
                    }
                }

                foreach (var collider in this.GetComponents<Collider>()) {
                    if (collider.isTrigger) {
                        collider.enabled = value;
                    }
                }

                if (!value) {
                    this._inPlayerIdList.Clear();
                    this.EmitLocalPlayerIdsChanged();
                }
                if (!this._isLocal) {
                    this.RequestSerialization();
                }
            }
            get => this._state;
        }
        [Header("スイッチの初期ON/OFF")]
        [SerializeField, UdonSynced, FieldChangeCallback(nameof(State))] private bool _state = false;
        [Header("スイッチONの際のオブジェクト")]
        [SerializeField] private GameObject[] _onObjects;
        [Header("スイッチOFFの際のオブジェクト")]
        [SerializeField] private GameObject[] _offObjects;
        [Header("ローカル化ON/OFF")]
        [SerializeField] private bool _isLocal;

        private void Start() {
            this.State = this._state;
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player) {
            if (this._inPlayerIdList.Contains(player.playerId)) {
                return;
            }

            this._inPlayerIdList.Add(player.playerId);
            this.EmitLocalPlayerIdsChanged();
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player) {
            if (!this._inPlayerIdList.Contains(player.playerId) || this.IsPlayerInsideCombinedArea(player)) {
                return;
            }

            this._inPlayerIdList.Remove(player.playerId);
            this.EmitLocalPlayerIdsChanged();
        }

        private bool IsPlayerInsideCombinedArea(VRCPlayerApi player) {
            var result = false;
            foreach (var collider in this.GetComponents<Collider>()) {
                if (collider.bounds.Contains(player.GetPosition()) && collider.isTrigger) {
                    result = true;
                }
            }
            return result;
        }
    }
}
