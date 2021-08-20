using UnityEngine;

namespace Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private Sequence DefaultSequence;
        private Sequence current;

        private void OnEnable()
        {
            PlaySequence(DefaultSequence);
        }

        public void PlaySequence(Sequence sequence)
        {
            if (current != null)
            {
                StopCoroutine(current.OnActionCoroutine(this));
            }
            current = sequence;
            current.StartCoroutine(sequence.OnActionCoroutine(this));
        }
    }
}