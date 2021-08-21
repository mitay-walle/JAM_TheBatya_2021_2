using UnityEngine;
#if UNITY_EDITOR

#endif

namespace Plugins.Pulsating
{
	public sealed class ScalePulsatingUI : PulsatingUI<Transform>
	{
		public override void AlphaApply()
		{
			Animated.localScale = new Vector3(Alpha,Alpha,Alpha);
			base.AlphaApply();
		}
	}
}
