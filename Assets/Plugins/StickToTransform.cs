using UnityEngine;

namespace Plugins
{
    [ExecuteAlways]
    public class StickToTransform : MonoBehaviour
    {
        public Transform StickTo;

        [Header("Settings"),SerializeField] protected bool Position = true;
        [SerializeField] protected bool x = true;
        [SerializeField] protected bool y = true;
        [SerializeField] protected bool z = true;
        [SerializeField] protected bool Rotation;
        [SerializeField] protected bool Scale;
        [SerializeField] protected Transform cachTr;
        
        [Header("Update")]
        public bool Simple = true;
        public bool Late;
        public bool Fixed;

        /// <summary>
        /// нужно для UI, чтобы позиция присваивалась, только если менялась
        /// </summary>
        public bool ComparePositionBeforeChange;


        public virtual void FixedUpdate()
        {
            if (Fixed || !StickTo) return;

            UpdateAnchors();
        }

        protected virtual void OnEnable()
        {
            Init();
        }

        public virtual void Init()
        {
            if (!cachTr) cachTr = transform;
        }

        public virtual void Update()
        {
            if (!Simple || !StickTo) return;

            UpdateAnchors();
        }

        public virtual void LateUpdate()
        {
            if (!Late || !StickTo) return;

            UpdateAnchors();
        }

        public virtual void UpdateAnchors()
        {
            if (Position)
            {
                if (ComparePositionBeforeChange)
                {
                    if (cachTr.position != StickTo.position)
                    {
                        ForceStickPosition();
                    }
                }
                else
                {
                    ForceStickPosition();
                }
            }

            if (Rotation) cachTr.rotation = StickTo.rotation;
            if (Scale) cachTr.localScale = StickTo.localScale;
        }

        public virtual void ForceStickPosition()
        {
            Vector3 pos = StickTo.position;
            Vector3 posBefore = cachTr.position;

            if (!x) pos.x = posBefore.x;
            if (!y) pos.y = posBefore.y;
            if (!z) pos.z = posBefore.z;

            cachTr.position = pos;
        }
    }
}