using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tilemap
{
    [CreateAssetMenu]
    public class WallRuleTile : RuleTile<WallRuleTile.Neighbor> {
        public bool customField;

        public class Neighbor : RuleTile.TilingRuleOutput.Neighbor {
            public const int Null = 3;
            public const int NotNull = 4;
        }

        public override bool RuleMatch(int neighbor, TileBase tile) {
            switch (neighbor) {
                case Neighbor.Null: return tile == null;
                case Neighbor.NotNull: return tile != null;
            }
            return base.RuleMatch(neighbor, tile);
        }
    }
}