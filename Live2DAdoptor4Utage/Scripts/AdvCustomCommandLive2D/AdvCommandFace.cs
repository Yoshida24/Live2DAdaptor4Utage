using UnityEngine;

namespace Utage
{
    /// <summary>
	/// コマンド：表情変更
	/// </summary>
	public class AdvCommandFace : AdvCommand
	{
		public AdvCommandFace(StringGridRow row)
			:base(row)
		{
			this.face = ParseCell<string>(AdvColumnName.Arg1);
			this.time = ParseCell<float>(AdvColumnName.Arg2);
		}

		public override void DoCommand(AdvEngine engine)
		{
			waitEndTime = engine.Time.Time + (engine.Page.CheckSkip() ? time / engine.Config.SkipSpped : time);
			Debug.Log(face);
		}

		public override bool Wait(AdvEngine engine)
		{
			return (engine.Time.Time < waitEndTime);
		}

		string face;
		float time;
		float waitEndTime;
	}
}
