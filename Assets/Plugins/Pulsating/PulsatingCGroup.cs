using UnityEngine;

namespace Plugins.Pulsating
{
	public class PulsatingCGroup : PulsatingUI<CanvasGroup>
	{
		public override void AlphaApply()
		{
			base.AlphaApply();
			Animated.alpha = Alpha;
		}
	}
}
