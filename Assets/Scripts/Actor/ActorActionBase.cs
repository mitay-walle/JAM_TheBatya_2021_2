using System.Collections;
using UnityEngine;

namespace Actor
{
    public class ActorActionBase : ActorAction
    {
        protected override IEnumerator OnOnceActionCoroutine(Actor actor)
        {
            yield return new WaitForSeconds(TimeOnce);
        }
    }
}
