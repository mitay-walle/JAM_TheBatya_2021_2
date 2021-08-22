using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Animation.ACV
{
	public class ACV_Slider : AnimatedChangeValueUI<Slider>
	{
		public bool HideOnZeroValue;

		private GameObject cachFillGo;

		public override void SetValue()
		{
			Animated.value = (animatedValue - minValue )  * 1f / maxValue - minValue;
		
			if (HideOnZeroValue)
			{
				if (!cachFillGo) cachFillGo = Animated.fillRect.gameObject;

				cachFillGo.SetActive(Animated.value != 0);
			}
			if (Debugging) Debug.LogError("set slider!",gameObject);
		}

		protected override void Reset()
		{
			base.Reset();
			Animated = GetComponent<Slider>();

#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
#endif
		}
	}
}
