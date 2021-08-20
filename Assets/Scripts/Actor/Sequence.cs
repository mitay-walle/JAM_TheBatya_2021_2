using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Actor
{
    public class Sequence : ActorAction
    {
        public bool IsParent;
        [SerializeField] private bool loop;

        [SerializeField] private List<ActorAction> actions = new List<ActorAction>();

        public bool Contains(Sequence sequence) => actions.Contains(sequence);

        public void OnEnable()
        {
            transform.root.GetComponentInChildren<Actor>().PlaySequence(this);
        }

        protected override IEnumerator OnOnceActionCoroutine(Actor actor)
        {
            int counter = 0;
            foreach (ActorAction action in actions)
            {
                action.gameObject.SetActive(false);
            }
            while (loop || counter == 0)
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    actions[i].gameObject.SetActive(true);
                    if (actions[i].PlayNextImmediatly)
                    {
                        StartCoroutine(actions[i].OnActionCoroutine(actor));
                    }
                    else
                    {
                        yield return actions[i].OnActionCoroutine(actor);
                    }
                    
                }

                counter++;
            }
            gameObject.SetActive(false);
        }

        [Button(DirtyOnClick = true)]
        protected void Reset()
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "reset");
#endif
            actions.Clear();
            var sequences = GetComponentsInChildren<Sequence>(true).ToList(); 
            actions = GetComponentsInChildren<ActorAction>(true).Where(t => t != this && !sequences.Exists(s=>s.actions.Contains(t))).ToList();
        }
        
        public override string EditorIconName => "Icons/Sequence";

    }
}