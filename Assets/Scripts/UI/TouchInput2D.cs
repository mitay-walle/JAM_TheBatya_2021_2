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

            touchListOld.Clear();
            touchListOld.AddRange(touchList);
            touchList.Clear();

            if (isMousePressing || isMouseDown || isMouseUp)
            {
                if (hit.collider != null)
                {
                    GameObject recipent = hit.transform.gameObject;
                    touchList.Add(recipent);

                    if (isMouseDown)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchDown), hit.point,
                            SendMessageOptions.DontRequireReceiver);
                    }

                    if (isMouseUp)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchUp), hit.point,
                            SendMessageOptions.DontRequireReceiver);
                    }

                    if (isMousePressing)
                    {
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchStay), hit.point,
                            SendMessageOptions.DontRequireReceiver);
                    }
                }

                foreach (GameObject g in touchListOld)
                {
                    if (!touchList.Contains(g) && g != null)
                    {
                        g.SendMessage(nameof(ITouchReciever.OnTouchExit), hit.point,
                            SendMessageOptions.DontRequireReceiver);
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
                        recipent.SendMessage(nameof(ITouchReciever.OnTouchEnter), hit.point,
                            SendMessageOptions.DontRequireReceiver);
                    }

                    if (!touchList.Contains(recipent)) touchList.Add(recipent);
                }

                foreach (GameObject obj in touchListOld)
                {
                    if (!touchList.Contains(obj))
                    {
                        obj.SendMessage(nameof(ITouchReciever.OnTouchExit), hit.point,
                            SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
#endif
        }
    }
}