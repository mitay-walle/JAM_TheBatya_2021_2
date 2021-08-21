using System;
using UnityEngine;

namespace Plugins.Pulsating
{
	public class PositionPulsatingUI : PulsatingUI<Transform>
	{
		[SerializeField] private eLerpType lerpType;
		public Vector3 MinPosition;
		public Vector3 MaxPosition;

	
		public override void AlphaApply()
		{
			switch (lerpType)
			{
				case eLerpType.Lerp:
					Animated.localPosition = Vector3.Lerp(MinPosition,MaxPosition,Alpha);
					break;
				case eLerpType.LerpUnclamped:
					Animated.localPosition = Vector3.LerpUnclamped(MinPosition,MaxPosition,Alpha);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			base.AlphaApply();
		}

		private enum eLerpType
		{
			Lerp = 0,
			LerpUnclamped = 1,
		}
	}
}
