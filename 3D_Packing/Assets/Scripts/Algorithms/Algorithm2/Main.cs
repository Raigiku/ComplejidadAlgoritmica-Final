using System.Collections.Generic;
using Packing_3D.Models;
using UnityEngine;
using System.Linq;
using System;
using System.Diagnostics;

namespace Packing_3D.Algorithms.Algorithm2
{
    public class Main : Algorithm
    {
        private Stopwatch stopwatch;
        private long initialMemory;

        public override List<Container> GetContainers(InputData data)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            initialMemory = GC.GetTotalMemory(false);

            SortBlocks(data.Blocks, data.ContainerSize);
            var containers = CreateContainers(data.Blocks, data.ContainerSize);
            return containers;
        }

        private void SortBlocks(List<Block> blocks, Vector3 containerSize)
        {
            blocks.Sort((block1, block2) =>
            {                
                var blocksToCompare = new List<Block> { block1, block2 };
                foreach (var block in blocksToCompare)
                {
                    if (block.Size.z > block.Size.y 
                        && block.Size.z <= containerSize.y && block.Size.y <= containerSize.z)
                    {
                        block.Size = new Vector3(block.Size.x, block.Size.z, block.Size.y);
                    }
                    if (block.Size.x > block.Size.y
                        && block.Size.x <= containerSize.y && block.Size.y <= containerSize.x)
                    {
                        block.Size = new Vector3(block.Size.y, block.Size.x, block.Size.z);
                    }
                    if (block.Size.x > block.Size.z
                        && block.Size.x <= containerSize.z && block.Size.z <= containerSize.x)
                    {
                        block.Size = new Vector3(block.Size.z, block.Size.y, block.Size.x);
                    }
                    block.Orientation = 5;
                }

                var areaBlock1 = block1.Size.y * block1.Size.z;
                if (block1.Size.x > block1.Size.z)
                {
                    areaBlock1 = block1.Size.y * block1.Size.x;
                }
                var areaBlock2 = block2.Size.y * block2.Size.z;
                if (block2.Size.x > block2.Size.z)
                {
                    areaBlock2 = block2.Size.y * block2.Size.x;
                }

                return areaBlock2.CompareTo(areaBlock1);
            });
        }

        private List<Container> CreateContainers(List<Block> blocks, Vector3 containerSize)
        {
            var containers = new List<ContainerLevel>();
            foreach (var block in blocks)
            {
                bool blockInserted = false;
                foreach (var container in containers)
                {
                    blockInserted = container.InsertedInLevel(block);
                    if (blockInserted)
                    {
                        container.Container.Blocks.Add(block);
                        break;
                    }
                }
                if (!blockInserted)
                {
                    var newContainer = new ContainerLevel(containerSize, block);
                    newContainer.Container.Blocks.Add(block);
                    containers.Add(newContainer);
                }
            }

            var finalMemory = GC.GetTotalMemory(false);
            stopwatch.Stop();
            TimeSpan timeElapsedSpan = stopwatch.Elapsed;

            TimeElapsed = timeElapsedSpan.TotalSeconds.ToString();
            MemoryUsed = ((finalMemory - initialMemory) / 1_024).ToString();

            return containers.Select(container => container.Container).ToList();
        }
    }
}
