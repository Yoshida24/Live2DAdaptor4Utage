using UnityEngine;

namespace Utage
{
    /// <summary>
	/// コマンド：ポーズ変更
	/// </summary>
	public class AdvCommandPose : AdvCommand
	{
		IAnimationTreeLayer layerPose;

		public AdvCommandPose(StringGridRow row):base(row)
		{
			layerPose = new LayerPose();
			this.pose = ParseCell<string>(AdvColumnName.Arg1);
			this.time = ParseCell<float>(AdvColumnName.Arg2);
		}

		public override void DoCommand(AdvEngine engine)
		{
			waitEndTime = engine.Time.Time + (engine.Page.CheckSkip() ? time / engine.Config.SkipSpped : time);
			layerPose.dispatch(pose);
		}

		public override bool Wait(AdvEngine engine)
		{
			return (engine.Time.Time < waitEndTime);
		}

		string pose;
		float time;
		float waitEndTime;
	}
}
