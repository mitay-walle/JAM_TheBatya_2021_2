using UnityEngine;

namespace Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private Sequence DefaultSequence;
        [SerializeField] private bool Debugging;
        private Sequence current;

        private void OnEnable()
        {
            current = null;
            PlayDefaultSequence();
        }

        public void PlayDefaultSequence()
        {
            PlaySequence(DefaultSequence);
        }
        public void PlaySequence(Sequence sequence)
        {
            if (Debugging) Debug.Log($"PlaySequence '{sequence.name}'");

            if (current != null && current != sequence && (!current.Contains(sequence) || current.IsParent))
            {
                current.gameObject.SetActive(false);
                if (Debugging) Debug.Log($"disabled '{current.name}'");
            }
            else
            {
                if (Debugging)
                {
                    Debug.LogError("ERROR");
                    Debug.LogError($"{current != null}");
                    Debug.LogError($"{current != sequence}");
                    if (current) Debug.LogError($"{!current.Contains(sequence)}");
                    if (current) Debug.LogError($"{current.IsParent}");
                }
            }

            current = sequence;

            current.gameObject.SetActive(true);

            if (Debugging) Debug.Log($"enabled '{current.name}'");

            current.StartCoroutine(current.OnActionCoroutine(this));
        }
    }
}