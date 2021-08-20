using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class Pulsating_Base : MonoBehaviour
{
	public enum eType
	{
		Sin = 0,
		Tangent = 1,
		FromTo = 2,
	}

	[SerializeField] protected bool Debugging;
	
	public eType myType;
	
	public float Speed = 5f;
	public float Alpha = 0f;
	public float MinA = 0f;
	public float MaxA = 1f;
	public float Delay;

	public bool EnableOnEnable;

	public abstract void SetAlpha(float alpha);
	public abstract void Animate(float time, float deltaTime);
	public abstract void Enable(bool state = true, float startAlpha = 0f, float disableTimer = float.MaxValue,float finishAlpha = 1f);
	public abstract void Disable(float stayAlpha = 0f);

	[Button]
	public abstract void Toggle();
	public abstract void AlphaApply();

	[Button]
	public abstract void Reset();
}

public abstract class PulsatingUI<T> : Pulsating_Base where T : Component
{
	public T Animated;

	private Coroutine AnimatingCoroutine;

	void OnEnable()
	{
		if (EnableOnEnable) Enable();
	}
	
//	private void Update()
//	{
//		Animate(Time.time);
//	}

	public override void SetAlpha(float alpha)
	{
		Alpha = alpha;
		AlphaApply();
	}

	public override void Animate(float time, float deltaTime)
	{
		switch (myType)
		{
			case eType.Sin:
				Alpha = Mathf.Lerp(MinA,MaxA,(1 + Mathf.Sin(time * Speed))*.5f);
				break;
			case eType.Tangent:
				Alpha = Mathf.Lerp(MinA,MaxA,(1 + Mathf.Tan(time * Speed))*.5f);
				break;
			case eType.FromTo:
				Alpha = Mathf.MoveTowards(Alpha,MaxA,Speed * deltaTime);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		
		if (Debugging) Debug.LogError($"set alpha = {Alpha}");
		
		AlphaApply();
	}

	public override void Enable(bool state = true, float startAlpha = 0f, float disableTimer = float.MaxValue, float finishAlpha = 1f)
	{
		Alpha = startAlpha;
		enabled = state;

		if (state && gameObject.activeInHierarchy)
		{
			if (AnimatingCoroutine != null)
			{
				StopCoroutine(AnimatingCoroutine);
			}
			AnimatingCoroutine = StartCoroutine(Animating(startAlpha,disableTimer,finishAlpha));
		}
		else
		{
			if (AnimatingCoroutine != null) StopCoroutine(AnimatingCoroutine);
		}
		
		AlphaApply();
	}

	IEnumerator Animating(float startAlpha = 0f, float disableTimer = float.MaxValue,float finishAlpha = 1f)
	{
		SetAlpha(startAlpha);
		var time = Mathf.Asin(startAlpha);

		var check = true;
		
		
		switch (myType)
		{
			case eType.Sin:
			case eType.Tangent:
				break;
			case eType.FromTo:
				finishAlpha = MaxA;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		
		while (check)
		{
			switch (myType)
			{
				case eType.Sin:
				case eType.Tangent:
					check = time < disableTimer;
					break;
				case eType.FromTo:
					check = Math.Abs(Alpha - finishAlpha) > .01f;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			Animate(time += Time.deltaTime, Time.deltaTime);
			
			yield return null;
		}
		
		while (Math.Abs(Alpha - finishAlpha) > .01f)
		{
			SetAlpha(Mathf.MoveTowards(Alpha, finishAlpha , Speed * Time.deltaTime));
			yield return null;
		}
	}
	
	
	public override void Disable(float stayAlpha = 0f)
	{
		Enable(false,stayAlpha);
	}

	public override void Toggle()
	{
		Enable(!enabled);
	}

	public abstract override void AlphaApply();

	public override void Reset()
	{
		#if UNITY_EDITOR
		Undo.RecordObject(this,"fill refs PulsatingUI");
		#endif
		Animated = GetComponent<T>();
		#if UNITY_EDITOR
		EditorUtility.SetDirty(this);
		#endif
	}
}
