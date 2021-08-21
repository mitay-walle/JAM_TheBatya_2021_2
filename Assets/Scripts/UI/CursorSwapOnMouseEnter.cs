using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI
{
    public class CursorSwapOnMouseEnter : MonoBehaviour, ITouchReciever
    {
        private static Texture2D lastSprite;
        private static Vector2 lastPivot;

        [Serializable]
        private class CursorState
        {
            public Texture2D sprite;
            public Vector2 pivot = Vector2.up;
        }

        [SerializeField] private Texture2D sprite;
        [SerializeField] private Vector2 pivot = Vector2.up;
        [SerializeField] private bool setOnStart;
        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

        [SerializeField] private SerializedDictionary<ITouchReciever.eTouchState, CursorState> data =
            new SerializedDictionary<ITouchReciever.eTouchState, CursorState>();


        private bool isHovered;
        private bool isClicked;


        private void Start()
        {
            if (setOnStart)
            {
                OnTouchExit();
                lastSprite = sprite;
                lastPivot = pivot;
            }
        }

        private void TrySetState(ITouchReciever.eTouchState state)
        {
            if (state == ITouchReciever.eTouchState.Exit && !data.ContainsKey(ITouchReciever.eTouchState.Exit))
            {
                Cursor.SetCursor(lastSprite, lastPivot, cursorMode);
                Debug.Log($"{state} {name}", this);
            }
            else
            {
                if (data.ContainsKey(state))
                {
                    Cursor.SetCursor(data[state].sprite, pivot, cursorMode);
                }
            }
        }

        public void OnTouchExit()
        {
            TrySetState(ITouchReciever.eTouchState.Exit);
            isHovered = false;
            isClicked = false;
        }

        public void OnTouchEnter()
        {
            TrySetState(ITouchReciever.eTouchState.Enter);
            isHovered = true;
        }

        public void OnTouchDown()
        {
            TrySetState(ITouchReciever.eTouchState.Down);
            isClicked = true;
        }

        public void OnTouchUp()
        {
            TrySetState(ITouchReciever.eTouchState.Up);
            isClicked = false;
        }

        public void OnTouchStay()
        {
            if (!isHovered) OnTouchEnter();
            if (!isClicked) OnTouchDown();

            TrySetState(ITouchReciever.eTouchState.Stay);
        }

        private void OnDisable()
        {
            if (isHovered)
            {
                OnTouchExit();
            }

            if (isClicked)
            {
                OnTouchUp();
            }
        }
    }
}