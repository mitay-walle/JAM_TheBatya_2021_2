using UnityEngine;
#if UNITY_EDITOR

#endif

public class PulsatingCGroup : PulsatingUI<CanvasGroup>
{
	public override void AlphaApply()
	{
		Animated.alpha = Alpha;
	}
}
