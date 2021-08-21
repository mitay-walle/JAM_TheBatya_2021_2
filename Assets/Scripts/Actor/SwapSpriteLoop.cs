using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.mitaywalle.HierarchyIcons;
using UnityEngine;

namespace Actor
{
    public class SwapSpriteLoop : MonoBehaviour, IHierarchyIconBehaviour
    {
        [SerializeField] private bool Debugging;
        [SerializeField] private bool LoopAnimation = true;
        [SerializeField] private int fps = 6;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private SpriteSequence sprites;
        private float time;

        private void OnEnable()
        {
            if (!sprites)
            {
                Debug.LogError($"[ SwapSpriteLoop ] {name}.OnEnable() SpriteSequence is null!", this);
                return;
            }
            
            if (!_renderer)
                _renderer = transform.root.GetComponentInChildren<SpriteRenderer>();

            if (_renderer)
            {
                StartCoroutine(LoopCoroutine());
            }
            else
            {
                Debug.LogError($"[ SwapSpriteLoop ] {name}.OnEnable() Actor is null! cant find", this);
            }
        }

        IEnumerator LoopCoroutine()
        {
            time = 0;
            int i = 0;
            float deltaTime = 1f / fps;
            while (true)
            {
                if (i < sprites.Sprites.Count)
                {
                    _renderer.sprite = sprites.Sprites[i];
                    if (Debugging)
                    {
                        Debug.Log($"[ SwapSprite] {name} set sprite {sprites.Sprites[i].name}", this);
                    }
                }

                i++;
                if (LoopAnimation && i >= sprites.Sprites.Count) i = 0;
                time += deltaTime;
                yield return new WaitForSeconds(deltaTime);
            }
        }

        public string EditorIconName => string.Empty;
        public Color EditorIconBGColor => Color.clear;
        public Type EditorIconBuiltInType => typeof(SpriteRenderer);
    }
}