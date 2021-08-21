using UnityEngine;

namespace Plugins.Pulsating
{
	public class PulsatingSprite : PulsatingUI<SpriteRenderer>
	{
		public override void AlphaApply()
		{
			var color = Animated.color;
			color.a = Alpha;
			Animated.color = color;
		}
	}
}
