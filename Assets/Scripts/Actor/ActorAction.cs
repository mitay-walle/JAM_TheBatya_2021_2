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
        public bool PlayNextImmediatly;
        [HorizontalGroup("Time",LabelWidth = 60)]public float PreDelay;
        [HorizontalGroup("Time",LabelWidth = 60)]public float TimeOnce = 1;
        [HorizontalGroup("Time",LabelWidth = 60)]public float PostDelay;

        [FoldoutGroup("Events")]public UnityEvent OnAction;
        [FoldoutGroup("Events")]public UnityEvent OnActionEnd;

        public virtual IEnumerator OnActionCoroutine(Actor actor)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(PreDelay);

            OnAction?.Invoke();
            yield return OnOnceActionCoroutine(actor);

            OnActionEnd?.Invoke();

            yield return new WaitForSeconds(PostDelay);

            gameObject.SetActive(false);
        }

        protected abstract IEnumerator OnOnceActionCoroutine(Actor actor);

        public virtual string EditorIconName => "Icons/ActorAction";
        public virtual Color EditorIconBGColor => Color.clear;
        public virtual Type EditorIconBuiltInType => null;
    }
}