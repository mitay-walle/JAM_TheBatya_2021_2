using UnityEngine;

public sealed class RotationPulsatingUI : PulsatingUI<Transform>
{
	public Vector3 MinPosition;
	public Vector3 MaxPosition;
	
	public override void AlphaApply()
	{
		Animated.localRotation = Quaternion.Euler(Vector3.Lerp(MinPosition,MaxPosition,Alpha));
	}
}
