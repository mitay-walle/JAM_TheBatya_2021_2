using System.Collections;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace UI.Animation.ACV
{

	public abstract class ACV_Base : MonoBehaviour
	{
		public enum AnimateSpeedType
		{
			FixedTime = 0,
			Speed = 1,
		}

		protected const string FillRefsName = "Заполнить ссылки";
	
		public float maxValue = 100;
		public float minValue = 0;
		public bool isAnimating;
		public bool Debugging;
		[SerializeField] protected AnimateSpeedType myType;
	
		[SerializeField] protected float Speed = 1;
		[SerializeField] protected float FixedTime = .35f;
	
		protected float lastValue;
		protected float value;

		protected float animatedValue;
		protected Coroutine AnimatingCoroutine;

		[Header("анимация alpha")]
		public CanvasGroup cGroup;
		[Range(0f, 1f)] public float AlphaOnSet = 1f;
		public float defaultAlpha = 1f;
	
		public abstract void Set(float value, bool animated = true);
		public abstract void SetValue();

		public float GetValue()
		{
			return value;
		}
	}

	public abstract class AnimatedChangeValueUI<T> : ACV_Base
	{
		public T Animated;

		protected virtual void Start()
		{
			if (cGroup) cGroup.alpha = defaultAlpha;
		}

		internal void Animate(float deltaTime)
		{
			animatedValue = Mathf.MoveTowards(animatedValue, value,deltaTime *  (myType == AnimateSpeedType.Speed ? Speed : Mathf.Abs((value - lastValue) / FixedTime) ));
		
#if UNITY_EDITOR
			if (Debugging) Debug.LogError($"animatedValue = {animatedValue} , value = {value}");
#endif
		
			SetValue();
		}

		public override void Set(float value, bool animated = true)
		{
//#if UNITY_EDITOR
			if (Debugging) Debug.LogError($"Value = {value}");
//#endif
			lastValue = this.value;
		
			animatedValue = this.value;
			this.value = value;

			if (isAnimating)
			{
				StopCoroutine(AnimatingCoroutine);
				isAnimating = false;
			}

			SetValue();

			if (animated && gameObject.activeInHierarchy)
			{
				AnimatingCoroutine = StartCoroutine(Animating());
			}
			else
			{
				animatedValue = this.value;
				SetValue();	
			}
		}

		IEnumerator Animating()
		{
			isAnimating = true;

#if UNITY_EDITOR
			if (Debugging) Debug.LogError($"value = {value}");
#endif

			var checkSetAlpha = cGroup && cGroup.alpha != AlphaOnSet;

			var oldAlpha = 1f;
		
			if (checkSetAlpha && cGroup)
			{
				oldAlpha = cGroup.alpha;
				cGroup.alpha = AlphaOnSet;
			}
		
			while (animatedValue != value && isAnimating)
			{
				Animate(Time.deltaTime);
				yield return null;
			}

			if (checkSetAlpha && cGroup)
			{
				cGroup.alpha = oldAlpha;
			}
		
#if UNITY_EDITOR
			if (Debugging) Debug.LogError($"value = {value}");
#endif
		
			isAnimating = false;
		}

		[Button(FillRefsName)]
		protected virtual void Reset()
		{
#if UNITY_EDITOR
			Undo.RecordObject(this,"fill refs AnimatedChangeValueUI");
#endif
		
			if (cGroup) cGroup.alpha = defaultAlpha;
		
#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
#endif
		}
	}
}