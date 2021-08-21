using UnityEngine;

namespace Plugins.Pulsating
{
	public class PulsatingSpriteColor : PulsatingUI<SpriteRenderer>
	{
		[SerializeField] private Gradient _gradient;
	
		public override void AlphaApply()
		{
			Animated.color = _gradient.Evaluate(Alpha);
			base.AlphaApply();
		}
	}
}
