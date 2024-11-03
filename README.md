# PlayerVoiceManager
本ReadMeはmarkdownファイルの為、下記URLから確認頂く事を推奨致します。
https://github.com/Rollphes/PlayerVoiceManager
VRChatワールドアセットになります。
VRChatでの利用を想定しています。
[こちらにて、最新版を確認できます。](https://github.com/Rollphes/genshin-manager/releases/tag/latest)

## 前提アセット
- [うどんマイク](https://booth.pm/ja/items/3038574)
- [Lura's Switch](https://booth.pm/ja/items/1969082)

それぞれモデルデータのみ使用。

## 説明
本アセットはハンドマイク・エリアマイク・プライベートエリア・ミュートエリアの4つを内包したアセットになります
- 優先順位をシーン上で設定する事が可能であり、hierarchyの上から順に優先順位が高くなります。
  - 例えばハンドマイクの優先度が高ければプライベートエリアを貫通して音声を流す事が出来、プライベートエリアを用いたイベント等で案内をスムーズに行う事がしやすくなります。
- AreaMicColliderには複数のColliderを設定する事が出来、Colliderを組み合わせて複雑な構造にしても動作します。
- ハンドマイクは動作仕様を通常から変更しており、他人の手から奪っても正常に動作します。

## 使用方法
PlayerVoiceManager.prefabをシーンに入れるだけ。
TextMesh Proを用いている為、案内通りにTextMeshのセットアップをお願いいたします。

HandMicはうどんマイク同様に調整頂いて問題ありません。

AreaMicを参考に各種微調整をご案内します。
1. 対象エリアの調整方法
   PlayerVoiceManagerの下にあるAreaMic/AreaMicColliderのColliderComponentを調整ください。先述の通りコンポーネントは増やしても問題ありません。
2. スイッチの位置の調整方法
   PlayerVoiceManagerの下にあるAreaMic/AreaMicSwitchのTransformを調整ください。
   スイッチを増やす場合は別途AreaMicCollider側のArea Mic (Script)にあるOn Objects/Off Objectsを参考に追加ください。

## 各種特性
- AreaMic
  collider内に入ると、入った人の声が全体に届くようになります。
  声の大きさは、AreaMicColliderのArea Mic (Script)から変更可能です。
  スイッチはグローバル動作、音声の設定はローカル動作になります。
- HandMic
  マイクを持つと、持った人の声が全体に届くようになります。
  声の大きさは、HandMicのHand Mic (Script)から変更可能です。
  すべてグローバル動作になります。
- MuteArea
  collider内に入ると、入った人の声と外側の人の声の両方が聞こえなくなります。
  また、外部からcollider内の人の声は聞こえません。
  聞こえなくなる側の声の大きさは、AreaMicColliderのArea Mic (Script)から変更可能です。
  スイッチはグローバル動作、音声の設定はローカル動作になります。
- PrivateArea
  collider内に入ると、collider内の人の声しか聞こえなくなります。
  また、外部からcollider内の人の声は聞こえません。
  聞こえなくなる側の声の大きさは、AreaMicColliderのArea Mic (Script)から変更可能です。
  スイッチはグローバル動作、音声の設定はローカル動作になります。

## 内容物
unitypackageをインポートすると下記ファイルができます。
├── LICENSE
├── PlayerVoiceManager.prefab
├── Prefabs
│   ├── AreaMic.prefab
│   ├── HandMic.prefab
│   ├── MuteArea.prefab
│   └── PrivateArea.prefab
├── README.md
├── SampleColliderMaterial.mat
├── SampleScene.unity
├── Scripts - (内容中略)
└── package.json