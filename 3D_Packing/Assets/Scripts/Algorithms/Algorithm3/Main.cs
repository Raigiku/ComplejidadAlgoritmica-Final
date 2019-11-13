using System.Collections.Generic;
using Packing_3D.Models;
using UnityEngine;
using System.Linq;
using System;
using System.Diagnostics;

namespace Packing_3D.Algorithms.Algorithm3
{
    public class Main : Algorithm
    {
        private BBF algoritmo;
        public override List<Container> GetContainers(InputData data)
        {
            SortBlocks(data.Blocks, data.ContainerSize);
            algoritmo = new BBF(data.ContainerSize, data.Blocks);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var initialMemory = GC.GetTotalMemory(false);

            algoritmo.run();
            // Poner la ejecucion del algoritmo aqui

            var finalMemory = GC.GetTotalMemory(false);
            stopWatch.Stop();
            TimeSpan timeElapsedSpan = stopWatch.Elapsed;

            TimeElapsed = timeElapsedSpan.TotalSeconds.ToString();
            MemoryUsed = ((finalMemory - initialMemory) / 1_024).ToString();

            return algoritmo.Containers;
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
    }
}
