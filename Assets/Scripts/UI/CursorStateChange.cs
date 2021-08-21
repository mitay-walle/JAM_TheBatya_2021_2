using System.Runtime.InteropServices;
using UnityEngine;

namespace UI
{
    public class CursorStateChange : MonoBehaviour
    {
        [SerializeField] private bool Lock;
        
#if UNITY_STANDALONE_WIN
        [DllImport("user32.dll")]
#endif
        static extern bool SetCursorPos(int X, int Y);

        void OnEnable()
        {
            if (Lock)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }

        private void Update()
        {
            if (Lock)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}