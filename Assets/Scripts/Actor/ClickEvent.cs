using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Actor
{
    public class ClickEvent : MonoBehaviour, ITouchReciever
    {
        [SerializeField] private UnityEvent onClick;
        private bool isDown;
        
        public void OnTouchDown()
        {
            isDown = true;
        }

        public void OnTouchUp()
        {
            if (isDown)
            {
                Debug.Log("clicked!");
                onClick?.Invoke();    
            }

            isDown = false;
        }
        public void OnTouchStay() { }

        public void OnTouchExit()
        {
            isDown = false;
        }
        public void OnTouchEnter() { }
    }
}