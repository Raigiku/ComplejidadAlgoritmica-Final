using Packing_3D.Models;
using UnityEngine;
using System.Collections.Generic;

namespace Packing_3D.Algorithms.Algorithm2
{
    public class SubLevel
    {
        public Vector3 Position { get; set; }
        public Vector3 Size { get; private set; }
        public List<Block> Blocks { get; private set; }

        public SubLevel(Block block, Level level)
        {
            Position = level.Position;
            Size = new Vector3(level.Size.x, block.Size.y, block.Size.z);
            Blocks = new List<Block>();
            SaveBlock(block);
        }

        public SubLevel(Block block, SubLevel lastSubLevel)
        {
            Position = new Vector3(0, lastSubLevel.Position.y, lastSubLevel.Position.z + lastSubLevel.Size.z);
            Size = new Vector3(lastSubLevel.Size.x, block.Size.y, block.Size.z);
            Blocks = new List<Block>();
            SaveBlock(block);
        }

        public bool BlockFits(Block block)
        {
            var lastBlock = Blocks[Blocks.Count - 1];
            return
                block.Size.x <= Size.x - (lastBlock.Position.x + lastBlock.Size.x)
                && block.Size.y <= lastBlock.Size.y
                && block.Size.z <= lastBlock.Size.z;
        }

        public void SaveBlock(Block block)
        {
            if (Blocks.Count > 0)
            {
                var lastBlock = Blocks[Blocks.Count - 1];
                block.Position = new Vector3(lastBlock.Position.x + lastBlock.Size.x, lastBlock.Position.y, lastBlock.Position.z);
                Blocks.Add(block);
            }
            else
            {
                block.Position = Position;
                Blocks.Add(block);
            }
        }
    }
}