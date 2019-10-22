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