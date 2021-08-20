using System.Collections;
using UnityEngine;

namespace Actor
{
    public class Move : ActorAction
    {
        [SerializeField] private Transform From;
        [SerializeField] private Transform To;
        private float time;

        protected override IEnumerator OnOnceActionCoroutine(Actor actor)
        {
            time = 0;
            while (time <= TimeOnce)
            {
                actor.transform.position = Vector3.Lerp(From.position, To.position, time / TimeOnce);
                time += Time.deltaTime;
                yield return null;
            }

            actor.transform.position = To.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            if (From) Gizmos.DrawWireCube(From.position,Vector3.one * .1f);
            if (To) Gizmos.DrawWireCube(To.position,Vector3.one* .1f);
            if (From && To)
            {
                Gizmos.DrawLine(From.position,To.position);
            }
        }
        
        public override string EditorIconName => "Icons/ActorActionMove";
    }
}