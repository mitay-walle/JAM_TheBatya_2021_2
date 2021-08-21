using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class SwapSprite : ActorAction
    {
        [SerializeField] private bool Debugging;
        [SerializeField] private bool LoopAnimation = true;
        [SerializeField] private bool flipX;
        [SerializeField] private bool flipY;
        [SerializeField] private int fps = 30;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private List<Sprite> sprites = new List<Sprite>();
        private float time;

        protected override IEnumerator OnOnceActionCoroutine(Actor actor)
        {
            _renderer.flipX = flipX;
            _renderer.flipY = flipY;
            time = 0;
            int i = 0;
            float deltaTime = 1f / fps;
            while (time < TimeOnce)
            {
                if (i < sprites.Count)
                {
                    _renderer.sprite = sprites[i];
                    if (Debugging)
                    {
                        Debug.Log($"[ SwapSprite] {name} set sprite {sprites[i].name}",this);
                    }
                }

                i++;
                if (LoopAnimation && i >= sprites.Count) i = 0;
                time += deltaTime;
                yield return new WaitForSeconds(deltaTime);
            }
        }

        public override Type EditorIconBuiltInType => typeof(SpriteRenderer);
    }
}