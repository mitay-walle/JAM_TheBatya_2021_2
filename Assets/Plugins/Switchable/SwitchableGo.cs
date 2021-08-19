using System;
using System.Linq;
using UnityEngine;

namespace Plugins.Switchable
{
    public class SwitchableGo : MonoBehaviour,ISwitchVisible
    {
        [NonSerialized] public bool inited;
        public bool RuntimeOnly;
        void Start()
        {
            inited = true;
        }
    
        public int GetIndex()
        {
            var found = GetComponentsInParent<SwitchableGoParent>(true);
            
            if (found == null || found.Length == 0) return -1;

            var canvases = found.ToList();
            var index = canvases.FindIndex(t => t.Gos.ToList().Contains(this));
            
            if (index < 0)
            {
                return -1;
            }

            var canvas = canvases[index];
            
            if (!canvas) return -1;
        
            var Gos = canvas.Gos;

            for (int i = 0; i < Gos.Length; i++)
                if (Gos[i] == this) return i;

            return -1;
        }

        public ISwitchVisibleHandler GetHandler()
        {
#if UNITY_EDITOR
            if (RuntimeOnly && !Application.isPlaying) return null;
#endif

            var found = GetComponentsInParent<SwitchableGoParent>(true);
            
            if (found == null || found.Length == 0) return null;

            var canvases = found.ToList();
            var index = canvases.FindIndex(t => t.Gos.ToList().Contains(this));
            
            if (index < 0)
            {
                return null;
            }
            
            return canvases[index];
        }

        public GameObject myGo => gameObject;
    }
}
