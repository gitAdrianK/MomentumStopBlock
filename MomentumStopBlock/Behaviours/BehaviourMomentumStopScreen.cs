using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using MomentumStopBlock.Blocks;
using System;

namespace MomentumStopBlock.Behaviours
{
    public class BehaviourMomentumStopScreen : IBlockBehaviour
    {
        public float BlockPriority => 2.0f;

        public bool IsPlayerOnBlock { get; set; }
        public static int LastStoppedScreen { get; set; }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity;
        }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo == null)
            {
                return true;
            }

            AdvCollisionInfo advCollisionInfo = behaviourContext.CollisionInfo.PreResolutionCollisionInfo;
            IsPlayerOnBlock = advCollisionInfo.IsCollidingWith<BlockMomentumStopScreen>()
                || advCollisionInfo.IsCollidingWith<BlockMomentumStopScreenSolid>();

            if (LastStoppedScreen != Camera.CurrentScreen)
            {
                if (IsPlayerOnBlock)
                {
                    BodyComp bodyComp = behaviourContext.BodyComp;
                    bodyComp.Velocity.X = 0;
                    bodyComp.Velocity.Y = Math.Max(0, bodyComp.Velocity.Y);
                    LastStoppedScreen = Camera.CurrentScreen;
                }
                else
                {
                    LastStoppedScreen = -1;
                }
            }

            return true;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }
    }
}
