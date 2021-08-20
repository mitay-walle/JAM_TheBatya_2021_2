using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private List<ActorAction> actions = new List<ActorAction>();

        private void OnEnable()
        {
            foreach (ActorAction action in actions)
            {
                action.gameObject.SetActive(false);
            }
            StartCoroutine(EnableCoroutine());
        }

        private IEnumerator EnableCoroutine()
        {
            while (true)
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    yield return actions[i].OnActionCoroutine(this);
                }
            }
        }
    }
}