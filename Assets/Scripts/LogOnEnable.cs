using UnityEngine;

public class LogOnEnable : MonoBehaviour
{
    void OnEnable()
    {
        Debug.LogError($"{name} enabled");
    }
    void OnDisable()
    {
        Debug.LogError($"{name} disabled");
    }
}
