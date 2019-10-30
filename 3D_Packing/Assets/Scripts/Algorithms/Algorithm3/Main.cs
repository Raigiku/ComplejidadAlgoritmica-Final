using System;
using System.Collections.Generic;
using System.Diagnostics;
using Packing_3D.Models;

namespace Packing_3D.Algorithms.Algorithm3
{
    public class Main : Algorithm
    {
        public override List<Container> GetContainers(InputData data)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var initialMemory = GC.GetTotalMemory(false);

            // Poner la ejecucion del algoritmo aqui

            var finalMemory = GC.GetTotalMemory(false);
            stopWatch.Stop();
            TimeSpan timeElapsedSpan = stopWatch.Elapsed;

            TimeElapsed = timeElapsedSpan.TotalSeconds.ToString();
            MemoryUsed = ((finalMemory - initialMemory) / 1_024).ToString();

            throw new System.NotImplementedException();
        }
    }
}
