using JumpKing;
using JumpKing.Level;
using Microsoft.Xna.Framework;
using MomentumStopBlock.Behaviours;

namespace MomentumStopBlock.Blocks
{
    public class BlockMomentumStopScreenSolid : IBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_MOM_STOP_SCREEN_SOLID = new Color(111, 25, 102);

        private readonly Rectangle collider;

        public BlockMomentumStopScreenSolid(Rectangle collider)
        {
            this.collider = collider;
        }

        public Color DebugColor
        {
            get { return BLOCKCODE_MOM_STOP_SCREEN_SOLID; }
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
                if (BehaviourMomentumStopScreen.LastStoppedScreen != Camera.CurrentScreen)
                {
                    return BlockCollisionType.Collision_Blocking;
                }
                return BlockCollisionType.Collision_NonBlocking;
            }
            intersection = Rectangle.Empty;
            return BlockCollisionType.NoCollision;
        }
    }
}

