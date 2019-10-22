# Live2DAdaptor4Utage

## 背景
宴のコマンドからLive2Dモーションを制御したい、というシチュエーションはよくある。
だが、宴3はLive2D公式で紹介されているモデルをMotionTreeで制御する方法を使う方法をサポートしていない。
そのため、いちいちカスタムコードを書く必要があり、非常にめんどくさい。
この問題に対処するため、カスタムコマンドを手っ取り早く実装できるようにしてみたのが本スクリプト。
ちなみに公式のLive2DモデルをMotionTreeで制御する方法は下記。

**公式の方法**
https://docs.live2d.com/cubism-sdk-tutorials/blendexpression/?locale=ja


## こんな時に便利
- 複数レイヤでアニメーションを並列再生している
- あるレイヤのアニメーションに対して宴のカスタムスクリプトでアニメーションを切り替えたい

## 使用イメージ

|Command | Arg1 | Arg2 |
|Pose | Default | 1 |

本スクリプトを入れると上記でWeit付きで対応しているPose切り替えスクリプトを呼べるようになる。
また、Pose切り替えのロジック部分は宴のスクリプトとは切り離されているので、モデルが完成していなくても代わりにテストコードを使うことで開発を進めることができる。

## 使い方
1. Utage3を入れる
2. https://docs.live2d.com/cubism-sdk-tutorials/blendexpression/?locale=ja こちらの方法を使ったAnimationControllerを作る。
3. 本プロジェクトを入れる。
4. Utage3のAdvシーンでHierarchyからAdvEngineを探し、Live2DCustomCommand.csをアタッチ
5. 下記を書き込む。シナリオを実行し、ログが表示されれば成功
|Command | Arg1 | Arg2 |
|Pose | Default | 1 |

## カスタマイズ

### View側のロジック変更
ConcreateAnimationLayerMapper以下のスクリプトに、AnimationTree操作メソッドを記述する。
例えば下記は一例。

```
using UnityEngine;
 
public class BlendExpression : MonoBehaviour
{
    private Animator _blendTree;
    private int _expressionIndex;
 
    [SerializeField, Range(0f, 1f)]
    public float Blending = 0f;
 
    [SerializeField, Range(0f, 1f)]
    public float ExpressionWeight = 1f;
 
    void Start()
    {
        _blendTree = GetComponent<Animator>();
 
        _expressionIndex = _blendTree.GetLayerIndex("Expression");
    }
 
    void Update()
    {
        //Fail getting animator.
        if (_blendTree == null)
        {
            return;
        }
 
 
        //Setting Blend Param and Weights.
        _blendTree.SetFloat("Blend", Blending);
 
        if (_expressionIndex != -1)
            _blendTree.SetLayerWeight(_expressionIndex, ExpressionWeight);
 
    }
}
```

### カスタムコマンドの追記
Live2DCustomCommand.csを編集する。

```
using UnityEngine;

namespace Utage
{
	public class Live2DCustomCommand : AdvCustomCommandManager
	{
		public override void OnBootInit()
		{
			Utage.AdvCommandParser.OnCreateCustomCommandFromID += CreateCustomCommand;
		}

		//AdvEnginのクリア処理のときに呼ばれる
		public override void OnClear()
		{
		}
 		
		//カスタムコマンドの作成用コールバック
		public void CreateCustomCommand(string id, StringGridRow row, AdvSettingDataManager dataManager, ref AdvCommand command )
		{
			switch (id)
			{
				case "Face":
					command = new AdvCommandFace(row);
					break;
				case "Costume":
					command = new AdvCommandCostume(row);
					break;
				case "Pose":
					command = new AdvCommandPose(row);
					break;
			}
		}
	}
}
```

### ビュー側のメソッドを追加する
ConcreateAnimationLayerMapper以下nスクリプトを複数し、dispatchメソッドをoverrideする。

### テスト
IFを切っているので、基本的にViewと宴のロジックは別々にテストが可能。
また、Zenjectを使っているなら依存性注入でスタブを書き換えることも可能。
