using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR

#endif
	
namespace UI.Animation.ACV
{
	public sealed class ACV_ImageLine : AnimatedChangeValueUI<GameObject[]>
	{
		[SerializeField] private string Name;
		[SerializeField] internal bool invert;
		[SerializeField] private HorizontalOrVerticalLayoutGroup lGroup;
	
		[OnInspectorGUI]
		void PreGUISome()
		{
#if UNITY_EDITOR
			EditorGUI.BeginChangeCheck();

			value = EditorGUILayout.Slider(value,minValue,maxValue);

			if (EditorGUI.EndChangeCheck())
			{
				SetValue();
			}
#endif
		}

		protected override void Start()
		{
			base.Start();
			StartCoroutine(StartInternal());
		}

		IEnumerator StartInternal()
		{
			yield return null;
			if (lGroup) lGroup.enabled = false;
		}

		public override void SetValue()
		{
			var count = Animated.Length;
			for (int i = 0; i < count; i++)
			{
				var state = (invert ? count-i : i) * (maxValue-minValue) / count <= value && value > minValue;
			
				if (Animated[i].activeSelf != state)
				{
					Animated[i].SetActive(state);
#if UNITY_EDITOR
					if (Application.isPlaying) EditorUtility.SetDirty(Animated[i]);
#endif
				}
			}
		}

		protected override void Reset()
		{
			base.Reset();

			var images = GetComponentsInChildren<Transform>();

			var targets = new List<GameObject>() ;
		
			for (int i = 0; i < images.Length; i++)
				if (images[i].name.Contains(Name))
					targets.Add(images[i].gameObject);

			Animated = targets.ToArray();
			if (!lGroup) lGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
		
#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
#endif
		}
	}
}
