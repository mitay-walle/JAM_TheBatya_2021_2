using UnityEngine;

namespace UI
{
    public class FollowMouse : MonoBehaviour
    {
        [SerializeField] private Bounds WorldBounds = new Bounds(Vector2.zero, Vector2.one * 100);
        private Camera cam;
        private Vector3 mouseWorldPos;

        private void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {
            Vector3 pos = Input.mousePosition;
            // if (pos.x < 0 || pos.y < 0) return;
            // if (pos.x > Screen.width || pos.y > Screen.height) return;
            pos = cam.ScreenToWorldPoint(pos);
            pos.z = 0;
            mouseWorldPos = pos;
            
            if (!WorldBounds.Contains(pos))
            {
                pos = WorldBounds.ClosestPoint(pos);
            }

            pos.z = transform.position.z;
            transform.position = pos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(WorldBounds.center,WorldBounds.size);
        }
    }
}