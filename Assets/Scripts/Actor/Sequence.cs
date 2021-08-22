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
        [SerializeField] protected Actor actor;
        public bool IsParent;

        [SerializeField] private bool loop;

        [SerializeField] private List<ActorAction> actions = new List<ActorAction>();

        public bool Contains(Sequence sequence) => actions.Contains(sequence);

        public void Play()
        {
            if (!actor)
            {
                actor = transform.root.GetComponentInChildren<Actor>();
            }

            if (actor) actor.PlaySequence(this);
            else
            {
                Debug.LogError($"[ Sequence ] {name}.Play() Actor null! cant found",this);
            }
        }

        protected override IEnumerator OnOnceActionCoroutine(Actor actor)
        {
            foreach (ActorAction action in actions)
            {
                action.gameObject.SetActive(false);
            }

            while (true)
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    if (actions[i] is Sequence seq && seq.IsParent)
                    {
                        seq.Play();
                    }
                    else
                    {
                        actions[i].gameObject.SetActive(true);
                        if (actions[i].PlayNextImmediatly)
                        {
                            actions[i].StartCoroutine(actions[i].OnActionCoroutine(actor));
                        }
                        else
                        {
                            yield return actions[i].OnActionCoroutine(actor);
                        }

                        if (Debugging) Debug.LogError($"action '{actions[i].name}'");
                    }
                }

                if (!loop) break;

                if (Debugging) Debug.LogError($"break");
            }

            if (Debugging) Debug.LogError($"finish sequence '{name}'");
            if (disableOnFinish) gameObject.SetActive(false);
            yield return null;
        }

        [Button(DirtyOnClick = true)]
        protected void Reset()
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "reset");
#endif
            actions.Clear();
            var sequences = GetComponentsInChildren<Sequence>(true).ToList();
            actions = GetComponentsInChildren<ActorAction>(true)
                .Where(t => t != this && !sequences.Exists(s => s.actions.Contains(t))).ToList();
        }

        public override string EditorIconName => "Icons/Sequence";
    }
}