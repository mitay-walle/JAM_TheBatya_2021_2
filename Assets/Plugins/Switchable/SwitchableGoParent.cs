using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Plugins.Switchable
{
    public class SwitchableGoParent : MonoBehaviour,ISwitchVisibleHandler
    {
        public SwitchableGo[] Gos;

        public bool EnableOnStart = false;

        void Start()
        {
            if (!EnableOnStart) return;
        
            for (int i = 0; i < Gos.Length; i++)
                if (Gos[i] && !Gos[i].gameObject.activeSelf && !Gos[i].inited)
                    Gos[i].gameObject.SetActive(true);
        }
    
        public void Show(int i)
        {
            for (int j = 0; j < Gos.Length; j++)
            {
                if (Gos[j].gameObject.activeSelf != (j == i))
                {
                    Gos[j].gameObject.SetActive(j == i);
#if UNITY_EDITOR
                    EditorUtility.SetDirty(Gos[j]);
#endif    
                }
            }
        }


        public void Show(bool state)
        {
            Show(state ? 1 : 0);
        }

        #region Editor

#if UNITY_EDITOR

        private void PreGUI()
        {
        }

        [ContextMenu("Get 1 lvl Children")]
        protected virtual void GetFirstLvlChidlren()
        {
            Undo.RecordObject(this, "GetFirstLvlChidlren");

            var list = new List<SwitchableGo>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).GetComponent<SwitchableGo>();
                if (child) list.Add(child);
            }

            Gos = list.ToArray();
            
            EditorUtility.SetDirty(this);
        }
        
        [ContextMenu("Reset")]
        protected virtual void Reset()
        {
            Undo.RecordObject(this, "reset");

            Gos = GetComponentsInChildren<SwitchableGo>(true);

            EditorUtility.SetDirty(this);
        }

        [CustomEditor(typeof(SwitchableGoParent), true), CanEditMultipleObjects]
        public class SwitchableGoParentEditor : Editor
        {
            private SwitchableGoParent SwitchableGoParent;

            private void OnEnable()
            {
                SwitchableGoParent = target as SwitchableGoParent;
            }

            public override void OnInspectorGUI()
            {
                SwitchableGoParent.PreGUI();

                base.OnInspectorGUI();
                DrawButton("Get 1 lvl Children", (t) => t.GetFirstLvlChidlren());
                DrawButton("Reset", (t) => t.Reset());
            }

            private void DrawButton(string btnText, Action<SwitchableGoParent> action)
            {
                if (GUILayout.Button(btnText))
                {
                    foreach (var targ in targets)
                    {
                        if (targ is SwitchableGoParent subTarg) action.Invoke(subTarg);
                    }
                }
            }
        }
#endif

        #endregion
    }
}
