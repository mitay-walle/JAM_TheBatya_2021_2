using UnityEngine;

namespace UI
{
    public class CursorSwapOnMouseEnter : MonoBehaviour
    {
        private static Sprite lastSprite;
        [SerializeField] private Sprite sprite;
        [SerializeField] private bool setOnStart;
        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;


        private void Start()
        {
            if (setOnStart)
            {
                OnTouchEntered();
            }
        }

        void OnTouchEntered()
        {
            lastSprite = sprite;
            
            Debug.Log("onmouseenter");
            
            Cursor.SetCursor(sprite.texture, sprite.pivot, cursorMode);
        }

        void OnTouchExit()
        {
            Cursor.SetCursor(lastSprite.texture, lastSprite.pivot, cursorMode);
        }
    }
}