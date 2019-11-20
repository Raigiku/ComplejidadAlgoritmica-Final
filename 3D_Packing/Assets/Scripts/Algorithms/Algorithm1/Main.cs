using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.Algorithms.Algorithm1;
using Packing_3D.Models;
using UnityEngine;

namespace Packing_3D.Algorithms.Algorithm1
{
    public class Main : Algorithm
    {
        public override List<Container> GetContainers(InputData data)
        {
            Vector3 size = data.ContainerSize;
            List<Block> blocks = data.Blocks;

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var initialMemory = GC.GetTotalMemory(false);

            blocks.Sort((Block a, Block b) =>
            {
                if (a.Volume < b.Volume)
                    return 1;
                else if (a.Volume == b.Volume)
                    return 0;
                else return -1;
            });

            Packer packer = new Packer(size);

            // Algorithm execution
            List<Container> containers = packer.Insert(blocks);

            var finalMemory = GC.GetTotalMemory(false);
            stopWatch.Stop();
            TimeSpan timeElapsedSpan = stopWatch.Elapsed;

            TimeElapsed = timeElapsedSpan.TotalSeconds.ToString();
            MemoryUsed = ((finalMemory - initialMemory) / 1_024).ToString();

            return containers;
        }
    }
}
