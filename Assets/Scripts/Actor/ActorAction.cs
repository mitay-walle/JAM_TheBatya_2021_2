using System;
using System.Collections;
using Plugins.mitaywalle.HierarchyIcons;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Actor
{
    public abstract class ActorAction : MonoBehaviour, IHierarchyIconBehaviour
    {
        [SerializeField] protected bool Debugging;

        public bool PlayNextImmediatly;
        [HorizontalGroup("Time",LabelWidth = 60)]public float PreDelay;
        [HorizontalGroup("Time",LabelWidth = 60)]public float TimeOnce = 1;
        [HorizontalGroup("Time",LabelWidth = 60)]public float PostDelay;

        [FoldoutGroup("Events")]public UnityEvent OnAction;
        [FoldoutGroup("Events")]public UnityEvent OnActionEnd;
        [FoldoutGroup("Events")]public UnityEvent OnActionInterrupt;
        
        [SerializeField] protected bool disableOnFinish = true;
        
        protected bool isActive;
        
        public virtual IEnumerator OnActionCoroutine(Actor actor)
        {
            isActive = true;
            yield return new WaitForSeconds(PreDelay);

            if (Debugging) Debug.Log($"action '{name}' OnAction.Invoke()"); 
            OnAction?.Invoke();
            yield return OnOnceActionCoroutine(actor);

            OnActionEnd?.Invoke();

            yield return new WaitForSeconds(PostDelay);
            isActive = false;
            if (disableOnFinish) gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (isActive)
            {
                if (Debugging) Debug.Log($"action '{name}' interrupt! OnActionInterrupt.Invoke()");
                OnActionInterrupt?.Invoke();
            }
        }

        protected abstract IEnumerator OnOnceActionCoroutine(Actor actor);

        public virtual string EditorIconName => "Icons/ActorAction";
        public virtual Color EditorIconBGColor => Color.clear;
        public virtual Type EditorIconBuiltInType => null;
    }
}