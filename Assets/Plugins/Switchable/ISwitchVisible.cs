using UnityEngine;

namespace Plugins.Switchable
{
    public interface ISwitchVisible
    {
        int GetIndex();
        ISwitchVisibleHandler GetHandler();
        GameObject myGo { get; }
    }
}