using System.Collections.Generic;
using Packing_3D.Models;
using UnityEngine;

namespace Packing_3D.Algorithms.Algorithm2
{
    public class ContainerLevel
    {
        public Container Container { get; set; }
        public List<Level> Levels { get; set; }

        public ContainerLevel(Vector3 containerSize, Block block)
        {
            Container = new Container
            {
                Position = new Vector3(0, 0, 0),
                Size = containerSize,
                Blocks = new List<Block>()
            };
            Levels = new List<Level>();
            SaveLevel(block);
        }

        public bool LevelFits(Block block)
        {
            var lastLevel = Levels[Levels.Count - 1];
            return
                block.Size.y <= Container.Size.y - (lastLevel.Position.y + lastLevel.Size.y);
        }

        public void SaveLevel(Block block)
        {
            if (Levels.Count > 0)
            {
                var lastLevel = Levels[Levels.Count - 1];
                var newLevel = new Level(block, lastLevel);
                Levels.Add(newLevel);
            }
            else
            {
                var newLevel = new Level(block, Container);
                Levels.Add(newLevel);
            }
        }

        public bool InsertedInLevel(Block block)
        {
            foreach (var level in Levels)
            {
                if (level.InsertedInSubLevel(block))
                {
                    return true;
                }
            }
            if (LevelFits(block))
            {
                SaveLevel(block);
                return true;
            }
            return false;
        }
    }
}
