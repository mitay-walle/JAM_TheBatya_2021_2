using System;
using UnityEngine.UI;

public class PulsatingImage : PulsatingUI<Image>
{
	public enum FillType
	{
		Alpha = 0,
		Fill = 1,
	}

	public FillType type;
	
	public override void AlphaApply()
	{
		switch (type)
		{
			case FillType.Alpha:
				var color = Animated.color;
				color.a = Alpha;
				Animated.color = color;
				break;
			case FillType.Fill:
				Animated.fillAmount = Alpha;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
