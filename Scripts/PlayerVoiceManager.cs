using UdonSharp;

using UnityEngine;

using VRC.SDK3.Data;
using VRC.SDKBase;

namespace io.github.rollphes.playerVoiceManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerVoiceManager : UdonSharpBehaviour
    {
        [Header("------------------------------------------------------------------")]
        [Header("■■■　プレイヤーボイスマネージャー／Player Voice Manager　■■■")]
        [Header("------------------------------------------------------------------")]

        [Header("デフォルト設定")]
        [SerializeField] private float _defaultGain = 15.0f;
        [SerializeField] private float _defaultDistanceNear = 0.0f;
        [SerializeField] private float _defaultDistanceFar = 25.0f;
        [SerializeField] private float _defaultVolumetricRadius = 0.0f;

        [UdonSynced] private string _json;
        private DataList _playerGlobalSettings = new DataList();
        private BaseVoiceSetting[] _voiceSettings = new BaseVoiceSetting[0];

        private void Start() {
            this._voiceSettings = this.GetComponentsInChildren<BaseVoiceSetting>();

            this.UpdateGlobalSetting();
        }

        public override void OnDeserialization() {
            if (VRCJson.TryDeserializeFromJson(this._json, out var result)) {
                this._playerGlobalSettings = result.DataList;
                this.SetVoiceAllPlayer();
            } else {
                Debug.LogError("Deserialization failed: " + result.ToString());
            }
        }

        public override void OnPlayerJoined(VRCPlayerApi player) {
            if (!Networking.IsMaster && Networking.LocalPlayer == player) {
                this.OnDeserialization();
            }
        }

        public void UpdateGlobalSetting() {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            this.UpdatePlayerGlobalSettings();
            this.RequestSerialization();
            this.OnDeserialization();
        }

        public void UpdateLocalSetting() {
            this.SetVoiceAllPlayer();
        }

        private void SetVoiceAllPlayer() {
            var players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(players);

            foreach (var player in players) {
                if (player == null) {
                    continue;
                }

                this.ApplyVoiceSettings(player);
            }
        }

        private void ApplyVoiceSettings(VRCPlayerApi player) {
            var globalSettingIndex = -1;
            for (var i = 0; i < this._playerGlobalSettings.Count; i++) {
                var setting = this._playerGlobalSettings[i].DataDictionary;
                if (this.GetIntValue(setting["playerId"]) == player.playerId) {
                    globalSettingIndex = this.GetIntValue(setting["settingIndex"]);
                    break;
                }
            }

            var localSettingIndex = this.FindLocalSettingIndex(player.playerId);

            if (localSettingIndex != -1 && (globalSettingIndex == -1 || localSettingIndex < globalSettingIndex)) {
                this.ApplySettingByIndex(player, localSettingIndex);
            } else if (globalSettingIndex != -1) {
                this.ApplySettingByIndex(player, globalSettingIndex);
            } else {
                this.ApplyDefaultSettings(player);
            }
        }

        private int FindLocalSettingIndex(int playerId) {
            for (var i = 0; i < this._voiceSettings.Length; i++) {
                var voiceSetting = this._voiceSettings[i];
                if (voiceSetting.LocalPlayerIds != null) {
                    if (this.FindIndexInArray(voiceSetting.LocalPlayerIds, playerId) != -1) {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int FindIndexInArray(int[] array, int value) {
            for (var i = 0; i < array.Length; i++) {
                if (array[i] == value) {
                    return i;
                }
            }
            return -1;
        }

        private void ApplySettingByIndex(VRCPlayerApi player, int settingIndex) {
            if (settingIndex >= 0 && settingIndex < this._voiceSettings.Length) {
                var voiceSetting = this._voiceSettings[settingIndex];
                player.SetVoiceGain(voiceSetting.Gain);
                player.SetVoiceDistanceNear(voiceSetting.DistanceNear);
                player.SetVoiceDistanceFar(voiceSetting.DistanceFar);
                player.SetVoiceVolumetricRadius(voiceSetting.VolumetricRadius);
            }
        }

        private void ApplyDefaultSettings(VRCPlayerApi player) {
            player.SetVoiceGain(this._defaultGain);
            player.SetVoiceDistanceNear(this._defaultDistanceNear);
            player.SetVoiceDistanceFar(this._defaultDistanceFar);
            player.SetVoiceVolumetricRadius(this._defaultVolumetricRadius);
        }

        private void UpdatePlayerGlobalSettings() {
            this._playerGlobalSettings.Clear();

            for (var i = 0; i < this._voiceSettings.Length; i++) {
                var voiceSetting = this._voiceSettings[i];
                if (voiceSetting.GlobalPlayerIds != null) {
                    foreach (var playerId in voiceSetting.GlobalPlayerIds) {
                        if (!this.ExistPlayerInGlobalSettings(playerId)) {
                            this.AddPlayerGlobalSetting(playerId, i);
                        }
                    }
                }
            }

            var players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(players);

            foreach (var player in players) {
                if (player != null && !this.ExistPlayerInGlobalSettings(player.playerId)) {
                    this.AddPlayerGlobalSetting(player.playerId, -1);
                }
            }

            if (VRCJson.TrySerializeToJson(this._playerGlobalSettings, JsonExportType.Minify, out var result)) {
                this._json = result.String;
            } else {
                Debug.LogError("Serialization failed: " + result.ToString());
            }
        }

        private void AddPlayerGlobalSetting(int playerId, int settingIndex) {
            var newSetting = new DataDictionary();
            newSetting.SetValue("playerId", playerId);
            newSetting.SetValue("settingIndex", settingIndex);

            this._playerGlobalSettings.Add(newSetting);
        }

        private bool ExistPlayerInGlobalSettings(int playerId) {
            for (var i = 0; i < this._playerGlobalSettings.Count; i++) {
                var setting = this._playerGlobalSettings[i].DataDictionary;
                if (this.GetIntValue(setting["playerId"]) == playerId) {
                    return true;
                }
            }
            return false;
        }

        private int GetIntValue(DataToken token) {
            if (token.TokenType == TokenType.Double) {
                return (int)token.Double;
            } else if (token.TokenType == TokenType.Int) {
                return token.Int;
            } else {
                Debug.LogWarning($"Unexpected token type: {token.TokenType}");
                return 0;
            }
        }
    }
}
