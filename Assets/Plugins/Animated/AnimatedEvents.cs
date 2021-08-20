using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Plugins.Own.Animated
{
    [ExecuteAlways]
    public abstract class AnimatedEvents : MonoBehaviour
    {
        [SerializeField] private Animated target;
        [ToggleLeft,HorizontalGroup("Settings2"),SerializeField] protected bool Debugging;
        [EnableIf("@false"),ToggleLeft,HorizontalGroup("Settings2"),ShowInInspector] protected bool inited;
        
        protected virtual void OnEnable()
        {
            Unsubscribe();
            Init();
        }

        [Button("Unsubscribe")]
        public void Unsubscribe()
        {
            if (!target || !inited) return;
            target.OnPlayAction -= OnPlay;
            target.OnFinishAction -= OnFinish;
            target.OnReset -= OnReset;
            target.OnTime -= OnTime;
            inited = false;
        }
        [Button("Init",Style = ButtonStyle.FoldoutButton)]
        public virtual bool Init()
        {
            if (!enabled || !target || inited) return false;
        
            target.OnPlayAction += OnPlay;
            target.OnFinishAction += OnFinish;
            target.OnReset += OnReset;
            target.OnTime += OnTime;
            inited = true;
            return true;
        }

        public abstract void OnPlay();
        public abstract void OnFinish();
        public abstract void OnReset();
        public abstract void OnTime();

        [Button("Reset",ButtonSizes.Large)]
        public virtual void Reset()
        {
#if UNITY_EDITOR
            Undo.RecordObject(this,"reset");
            target = target != null ? target : GetComponentInChildren<Animated>(true);
        
            if (!inited) Init();
        
            EditorUtility.SetDirty(this);
#endif
        }


    }
}
