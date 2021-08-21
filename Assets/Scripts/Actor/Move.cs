using System;
using System.Collections;
using UnityEngine;

namespace Actor
{
    public class Move : ActorAction
    {
        public enum ePattern
        {
            Lerp,
            MoveTowards,
        }
        [SerializeField] private Transform From;
        [SerializeField] private Transform To;
        [SerializeField] private ePattern Pattern;
        [SerializeField] private float tolerance = .002f;
        [SerializeField] private float Speed = .01f;
        private float time;
        
        protected override IEnumerator OnOnceActionCoroutine(Actor actor)
        {
            time = 0;
            while (time < TimeOnce) 
            // while (Math.Abs(actor.transform.position.x - To.position.x) > tolerance 
            //        && Math.Abs(actor.transform.position.y - To.position.y) > tolerance
            //        && Math.Abs(actor.transform.position.z - To.position.z) > tolerance)
            {
                switch (Pattern)
                {
                    case ePattern.Lerp:
                        actor.transform.position = Vector3.Lerp(From.position, To.position, time / TimeOnce);
                        break;
                    case ePattern.MoveTowards:
                        actor.transform.position = Vector3.MoveTowards(actor.transform.position, To.position, Speed * Time.deltaTime);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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