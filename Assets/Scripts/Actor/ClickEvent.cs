using UnityEngine;
using UnityEngine.Events;

namespace Actor
{
    public class ClickEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onClick;

        public void OnMouseDown()
        {
            onClick?.Invoke();
        }
    }
}