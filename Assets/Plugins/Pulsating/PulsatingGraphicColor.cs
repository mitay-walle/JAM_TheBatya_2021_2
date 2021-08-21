using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Pulsating
{
	public class PulsatingGraphicColor : PulsatingUI<Graphic>
	{
		[SerializeField] private Gradient _gradient;
	
		public override void AlphaApply()
		{
			Animated.color = _gradient.Evaluate(Alpha);
			base.AlphaApply();
		}
	}
}
