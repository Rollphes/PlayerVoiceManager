# PlayerVoiceManager

本 ReadMe は markdown ファイルの為、下記 URL から確認頂く事を推奨致します。<br>
https://github.com/Rollphes/PlayerVoiceManager<br>
VRChat ワールドアセットになります。<br>
VRChat での利用を想定しています。<br>
[こちらにて、最新版を確認できます。](https://github.com/Rollphes/PlayerVoiceManager/releases/latest)
[VCCへの追加はこちらから](https://rollphes.github.io/vpm-repos/)

## 前提アセット

- [うどんマイク](https://booth.pm/ja/items/3038574)
- [Lura's Switch](https://booth.pm/ja/items/1969082)

それぞれモデルデータのみ使用。

## 説明

本アセットはハンドマイク・エリアマイク・プライベートエリア・ミュートエリアの 4 つを内包したアセットになります

- 優先順位をシーン上で設定する事が可能であり、hierarchy の上から順に優先順位が高くなります。
  - 例えばハンドマイクの優先度が高ければプライベートエリアを貫通して音声を流す事が出来、プライベートエリアを用いたイベント等で案内をスムーズに行う事がしやすくなります。
- AreaMicCollider には複数の Collider を設定する事が出来、Collider を組み合わせて複雑な構造にしても動作します。
- ハンドマイクは動作仕様を通常から変更しており、他人の手から奪っても正常に動作します。

## 使用方法

PlayerVoiceManager.prefab をシーンに入れるだけ。<br>
TextMesh Pro を用いている為、案内通りに TextMesh のセットアップをお願いいたします。

HandMic はうどんマイク同様に調整頂いて問題ありません。

AreaMic を参考に各種微調整をご案内します。

1. 対象エリアの調整方法<br>
   PlayerVoiceManager の下にある AreaMic/AreaMicCollider の ColliderComponent を調整ください。<br>
   先述の通りコンポーネントは増やしても問題ありません。
2. スイッチの位置の調整方法<br>
   PlayerVoiceManager の下にある AreaMic/AreaMicSwitch の Transform を調整ください。<br>
   スイッチを増やす場合は別途 AreaMicCollider 側の Area Mic (Script)にある On Objects/Off Objects を参考に追加ください。

## 各種特性

- AreaMic<br>
  collider 内に入ると、入った人の声が全体に届くようになります。<br>
  声の大きさは、AreaMicCollider の Area Mic (Script)から変更可能です。<br>
  スイッチはグローバル動作、音声の設定はローカル動作になります。
- HandMic<br>
  マイクを持つと、持った人の声が全体に届くようになります。<br>
  声の大きさは、HandMic の Hand Mic (Script)から変更可能です。<br>
  すべてグローバル動作になります。
- MuteArea<br>
  collider 内に入ると、入った人の声と外側の人の声の両方が聞こえなくなります。<br>
  また、外部から collider 内の人の声は聞こえません。<br>
  聞こえなくなる側の声の大きさは、AreaMicCollider の Area Mic (Script)から変更可能です。<br>
  スイッチはグローバル動作、音声の設定はローカル動作になります。
- PrivateArea<br>
  collider 内に入ると、collider 内の人の声しか聞こえなくなります。<br>
  また、外部から collider 内の人の声は聞こえません。<br>
  聞こえなくなる側の声の大きさは、AreaMicCollider の Area Mic (Script)から変更可能です。<br>
  スイッチはグローバル動作、音声の設定はローカル動作になります。
  ★今後改良予定★

## 内容物

unitypackage をインポートすると下記ファイルができます。<br>
├── LICENSE<br>
├── PlayerVoiceManager.prefab<br>
├── Prefabs<br>
│ ├── AreaMic.prefab<br>
│ ├── HandMic.prefab<br>
│ ├── MuteArea.prefab<br>
│ └── PrivateArea.prefab<br>
├── README.md<br>
├── SampleColliderMaterial.mat<br>
├── SampleScene.unity<br>
├── Scripts ・・・ (内容中略)<br>
└── package.json<br>

## 動作確認環境
- VRChatSDK-Base@3.7.2
- VRChatSDK-Worlds@3.7.2

## 最後に
質問、バグ報告、要望についてはお気軽にご相談ください。
