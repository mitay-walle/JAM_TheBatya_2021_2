using UnityEngine.UI;

public class PulsatingGraphic : PulsatingUI<Graphic>
{
	public override void AlphaApply()
	{
		var color = Animated.color;
		color.a = Alpha;
		Animated.color = color;
	}
}
