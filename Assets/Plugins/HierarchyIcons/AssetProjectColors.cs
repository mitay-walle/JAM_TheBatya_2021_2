using UnityEngine;

namespace Plugins.mitaywalle.HierarchyIcons
{
    public class AssetProjectColors : ScriptableObject
    {
        protected static readonly Color badColor = new Color(1f, .3f, .3f, 0.1f);

        public virtual  Color color { get; }
    }
}
