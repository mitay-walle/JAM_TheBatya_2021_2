using System;
using UnityEngine;

namespace Plugins.mitaywalle.HierarchyIcons
{
    public interface IHierarchyIconBehaviour
    {
        string EditorIconName { get; }
        Color EditorIconBGColor { get; }
        Type EditorIconBuiltInType { get; }
    }
}