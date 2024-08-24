using JumpKing.API;
using JumpKing.Level;
using JumpKing.Level.Sampler;
using JumpKing.Workshop;
using Microsoft.Xna.Framework;
using MomentumStopBlock.Blocks;
using System;
using System.Collections.Generic;

namespace MomentumStopBlock.Factories
{
    public class FactoryMomentumStop : IBlockFactory
    {
        private static readonly HashSet<Color> supportedBlockCodes = new HashSet<Color> {
            BlockMomentumStop.BLOCKCODE_MOM_STOP,
            BlockMomentumStopSolid.BLOCKCODE_MOM_STOP_SOLID,
            BlockMomentumStopScreen.BLOCKCODE_MOM_STOP_SCREEN,
            BlockMomentumStopScreenSolid.BLOCKCODE_MOM_STOP_SCREEN_SOLID,
        };

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            return supportedBlockCodes.Contains(blockCode);
        }

        public bool IsSolidBlock(Color blockCode)
        {
            return false;
        }

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc, int currentScreen, int x, int y)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BlockMomentumStop.BLOCKCODE_MOM_STOP:
                    return new BlockMomentumStop(blockRect);
                case var _ when blockCode == BlockMomentumStopSolid.BLOCKCODE_MOM_STOP_SOLID:
                    return new BlockMomentumStopSolid(blockRect);
                case var _ when blockCode == BlockMomentumStopScreen.BLOCKCODE_MOM_STOP_SCREEN:
                    return new BlockMomentumStopScreen(blockRect);
                case var _ when blockCode == BlockMomentumStopScreenSolid.BLOCKCODE_MOM_STOP_SCREEN_SOLID:
                    return new BlockMomentumStopScreenSolid(blockRect);
                default:
                    throw new InvalidOperationException($"{typeof(FactoryMomentumStop).Name} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
