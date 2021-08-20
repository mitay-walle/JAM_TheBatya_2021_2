using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Actor
{
    public abstract class ActorAction : MonoBehaviour
    {
        public float PreDelay;
        public bool Loop;
        [ShowIf(nameof(Loop))]public int LoopCount = 1;
        public float TimeOnce = 1;
        public float PostDelay;
        
        public UnityEvent OnAction;
        public UnityEvent OnActionEnd;

        public virtual IEnumerator OnActionCoroutine(Actor actor)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(PreDelay);

            OnAction?.Invoke();
            if (Loop)
            {
                int count = 0;
                while (LoopCount > count)
                {
                    yield return OnOnceActionCoroutine(actor);
                    count++;
                }
            }
            else
            {
                yield return OnOnceActionCoroutine(actor);
            }

            OnActionEnd?.Invoke();
                
            yield return new WaitForSeconds(PostDelay);
            
            gameObject.SetActive(false);
        }

        protected abstract IEnumerator OnOnceActionCoroutine(Actor actor);
    }
}