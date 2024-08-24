using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace MomentumStopBlock.Blocks
{
    public class BlockMomentumStopScreen : IBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_MOM_STOP_SCREEN = new Color(111, 24, 102);

        private readonly Rectangle collider;

        public BlockMomentumStopScreen(Rectangle collider)
        {
            this.collider = collider;
        }

        public Color DebugColor
        {
            get { return BLOCKCODE_MOM_STOP_SCREEN; }
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
                return BlockCollisionType.Collision_NonBlocking;
            }
            intersection = Rectangle.Empty;
            return BlockCollisionType.NoCollision;
        }
    }
}

