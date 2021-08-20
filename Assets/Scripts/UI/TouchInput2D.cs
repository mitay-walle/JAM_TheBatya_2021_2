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
        private RaycastHit2D hit;

        void Start()
        {
            cam = this.GetComponent<Camera>();
        }

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            bool isMousePressing = Input.GetMouseButton(0);
            bool isMouseDown = Input.GetMouseButtonDown(0);
            bool isMouseUp = Input.GetMouseButtonUp(0);

            hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10f, touchInputMask);

            if (isMousePressing || isMouseDown || isMouseUp)
            {
                touchListOld.Clear();
                touchListOld.AddRange(touchList);
                touchList.Clear();

                if (hit.collider != null)
                {
                    GameObject recipent = hit.transform.gameObject;
                    touchList.Add(recipent);

                    if (isMouseDown)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchDown), hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (isMouseUp)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchUp), hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (isMousePressing)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchStay), hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }

                foreach (GameObject g in touchListOld)
                {
                    if (!touchList.Contains(g) && g != null)
                    {
                        g.SendMessage(nameof(ITouchReciever.OnTouchExit), hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            else
            {
                if (hit.collider != null)
                {
                    GameObject recipent = hit.transform.gameObject;

                    if (!touchListOld.Contains(recipent))
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchEntered), hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
#endif

#if UNITY_IOS
             if (Input.touchCount > 0)
             {
                 touchListOld.Clear();
                 touchListOld.AddRange(touchList);
                 touchList.Clear();
                 
                 Touch[] currentTouches = Input.touches;
                 for (int i = 0; i < Input.touchCount; i++)
                 {
                     hit =
 Physics2D.Raycast(cam.ScreenToWorldPoint(currentTouches[i].position), Vector2.zero, 1f, touchInputMask);
                     if (hit.collider != null)
                     {
                         GameObject recipent = hit.transform.gameObject;
                         touchList.Add(recipent);
                         
                         if (currentTouches[i].phase == TouchPhase.Began)
                         {
                             recipent.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                         }
                         if (currentTouches[i].phase == TouchPhase.Ended)
                         {
                             recipent.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                         }
                         if (currentTouches[i].phase == TouchPhase.Stationary || currentTouches[i].phase == TouchPhase.Moved)
                         {
                             recipent.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                         }
                         if (currentTouches[i].phase == TouchPhase.Moved && currentTouches[i].phase != TouchPhase.Began && touchListOld.Contains(recipent) == false)
                         {
                             recipent.SendMessage("OnTouchEntered", hit.point, SendMessageOptions.DontRequireReceiver);
                         }
                         if (currentTouches[i].phase == TouchPhase.Moved)
                         {
                             recipent.SendMessage("OnTouchMoved", hit.point, SendMessageOptions.DontRequireReceiver);
                         }
                         if (currentTouches[i].phase == TouchPhase.Canceled)
                         {
                             recipent.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                         }
                     }
                 }
                 foreach (GameObject g in touchListOld)
                 {
                     if (!touchList.Contains(g) && g != null)
                     {
                         g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                     }
                 }
             }
#endif
        }
    }
}