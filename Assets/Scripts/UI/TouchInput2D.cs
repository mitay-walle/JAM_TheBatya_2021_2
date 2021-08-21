using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TouchInput2D : MonoBehaviour
    {
        public LayerMask touchInputMask;
        private Camera cam;
        private List<GameObject> touchList = new List<GameObject>();
        private List<GameObject> touchListOld = new List<GameObject>();

        void Start()
        {
            cam = this.GetComponent<Camera>();
        }

        void Update()
        {
            bool isMousePressing = Input.GetMouseButton(0);
            bool isMouseDown = Input.GetMouseButtonDown(0);
            bool isMouseUp = Input.GetMouseButtonUp(0);
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (isMouseDown)
            {
                var ray = cam.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 5);
            }

            SendMessageOptions reciever = SendMessageOptions.DontRequireReceiver;

            var hit = Physics2D.OverlapPoint(mouseWorldPos, touchInputMask,-100,100);

            touchListOld.Clear();
            touchListOld.AddRange(touchList);
            touchList.Clear();


            if (isMouseDown)
            {
                Debug.Log($"OnTouchDown()");   
            }
            
            
            if (isMousePressing || isMouseDown || isMouseUp)
            {
                if (hit != null)
                {
                    GameObject recipent = hit.transform.gameObject;
                    touchList.Add(recipent);

                    Vector3 closest = hit.ClosestPoint(mouseWorldPos);
                    if (isMouseDown)
                    {
                        Debug.Log($"{recipent.name}.OnTouchDown()");
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchDown), closest, reciever);
                    }

                    if (isMouseUp)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchUp), closest, reciever);
                    }

                    if (isMousePressing)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchStay), closest, reciever);
                    }
                }
                else
                {
                    Debug.LogError($"OnTouchDown() MISS-CLICK hit == null!");
                }
                foreach (GameObject g in touchListOld)
                {
                    if (!touchList.Contains(g) && g != null)
                    {
                        g.SendMessage(nameof(ITouchReciever.OnTouchExit), mouseWorldPos, reciever);
                    }
                }
            }
            else
            {
                if (hit != null)
                {
                    GameObject recipent = hit.gameObject;
                    Vector3 closest = hit.ClosestPoint(mouseWorldPos);

                    if (!touchListOld.Contains(recipent))
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchEnter), closest, reciever);
                    }

                    if (!touchList.Contains(recipent)) touchList.Add(recipent);
                }

                foreach (GameObject obj in touchListOld)
                {
                    if (!touchList.Contains(obj))
                    {
                        obj.SendMessage(nameof(ITouchReciever.OnTouchExit), mouseWorldPos, reciever);
                    }
                }
            }
        }
    }
}