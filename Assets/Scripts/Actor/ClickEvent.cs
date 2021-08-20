using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Actor
{
    public class ClickEvent : MonoBehaviour, ITouchReciever
    {
        [SerializeField] private UnityEvent onClick;

        public void OnTouchDown()
        {
            Debug.Log("clicked!");
            onClick?.Invoke();
        }

        public void OnTouchUp() { }
        public void OnTouchStay() { }
        public void OnTouchExit() { }
        public void OnTouchEnter() { }
    }
}