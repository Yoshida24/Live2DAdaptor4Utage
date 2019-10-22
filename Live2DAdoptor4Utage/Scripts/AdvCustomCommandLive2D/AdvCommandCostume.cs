using UnityEngine;

namespace Utage
{
    /// <summary>
	/// コマンド：コスチューム変更
	/// </summary>
	public class AdvCommandCostume : AdvCommand
	{
		public AdvCommandCostume(StringGridRow row)
			:base(row)
		{
			this.costume = ParseCell<string>(AdvColumnName.Arg1);
			this.time = ParseCell<float>(AdvColumnName.Arg2);
		}

		public override void DoCommand(AdvEngine engine)
		{
			waitEndTime = engine.Time.Time + (engine.Page.CheckSkip() ? time / engine.Config.SkipSpped : time);
			Debug.Log(costume);
		}

		public override bool Wait(AdvEngine engine)
		{
			return (engine.Time.Time < waitEndTime);
		}

		string costume;
		float time;
		float waitEndTime;
	}
}
