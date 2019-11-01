using Packing_3D.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Packing_3D.Algorithms.Algorithm2
{
    public class Level
    {
        public Vector3 Position { get; private set; }
        public Vector3 Size { get; private set; }
        public List<SubLevel> SubLevels { get; private set; }

        public Level(Block block, Container container)
        {
            Position = container.Position;
            Size = new Vector3(container.Size.x, block.Size.y, container.Size.z);
            SubLevels = new List<SubLevel>();
            SaveSubLevel(block);
        }

        public Level(Block block, Level lastLevel)
        {
            Position = new Vector3(0, lastLevel.Position.y + lastLevel.Size.y, 0);
            Size = new Vector3(lastLevel.Size.x, block.Size.y, lastLevel.Size.z);
            SubLevels = new List<SubLevel>();
            SaveSubLevel(block);
        }

        public bool SubLevelFits(Block block)
        {
            var lastSubLevel = SubLevels[SubLevels.Count - 1];
            return
                block.Size.y <= Size.y
                && block.Size.z <= Size.z - (lastSubLevel.Position.z + lastSubLevel.Size.z);
        }

        public void SaveSubLevel(Block block)
        {
            if (SubLevels.Count > 0)
            {
                var lastSubLevel = SubLevels[SubLevels.Count - 1];
                var newSubLevel = new SubLevel(block, lastSubLevel);
                SubLevels.Add(newSubLevel);
            }
            else
            {
                var newSubLevel = new SubLevel(block, this);
                SubLevels.Add(newSubLevel);
            }
        }

        public bool InsertedInSubLevel(Block block)
        {
            foreach (var subLevel in SubLevels)
            {
                if (subLevel.BlockFits(block))
                {
                    subLevel.SaveBlock(block);
                    return true;
                }
            }
            if (SubLevelFits(block))
            {
                SaveSubLevel(block);
                return true;
            }
            return false;
        }
    }
}
