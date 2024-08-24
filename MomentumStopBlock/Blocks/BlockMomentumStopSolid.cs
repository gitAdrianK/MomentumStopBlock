using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace MomentumStopBlock.Blocks
{
    public class BlockMomentumStopSolid : IBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_MOM_STOP_SOLID = new Color(111, 25, 101);

        private readonly Rectangle collider;

        public BlockMomentumStopSolid(Rectangle collider)
        {
            this.collider = collider;
        }

        public Color DebugColor
        {
            get { return BLOCKCODE_MOM_STOP_SOLID; }
        }

        public Rectangle GetRect()
        {
            return collider;
        }

        public BlockCollisionType Intersects(Rectangle hitbox, out Rectangle intersection)
        {
            if (collider.Intersects(hitbox))
            {
                intersection = Rectangle.Intersect(hitbox, collider);
                return BlockCollisionType.Collision_Blocking;
            }
            intersection = Rectangle.Empty;
            return BlockCollisionType.NoCollision;
        }
    }
}

