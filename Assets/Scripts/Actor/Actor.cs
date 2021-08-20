using UnityEngine;

namespace Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private Sequence DefaultSequence;
        private Sequence current;

        private void OnEnable()
        {
            current = null;
            PlaySequence(DefaultSequence);
        }

        public void PlaySequence(Sequence sequence)
        {
            if (current != null && current != sequence && !current.Contains(sequence) && sequence.IsParent)
            {
                current.gameObject.SetActive(false);
            }
            current = sequence;
            current.StartCoroutine(current.OnActionCoroutine(this));
        }
    }
}