using UnityEngine;

public sealed class PositionPulsatingUI : PulsatingUI<Transform>
{
	public Vector3 MinPosition;
	public Vector3 MaxPosition;
	
	public override void AlphaApply()
	{
		Animated.localPosition = Vector3.Lerp(MinPosition,MaxPosition,Alpha);
	}
}
