using System.Collections;

namespace Actor
{
    public class ActorActionBase : ActorAction
    {
        protected override IEnumerator OnOnceActionCoroutine(Actor actor)
        {
            yield break;
        }
    }
}
