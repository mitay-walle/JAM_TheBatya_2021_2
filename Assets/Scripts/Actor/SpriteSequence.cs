using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    [CreateAssetMenu]
    public class SpriteSequence : ScriptableObject
    {
        public List<Sprite> Sprites = new List<Sprite>();
    }
}