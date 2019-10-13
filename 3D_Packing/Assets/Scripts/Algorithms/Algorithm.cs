using System.Collections.Generic;
using Packing_3D.Models;

namespace Packing_3D.Algorithms
{
    public abstract class Algorithm
    {
        public string Name { get; set; }
        public string TimeElapsed { get; set; }
        public string MemoryUsed { get; set; }
        public abstract List<Container> GetContainers(InputData data);
    }
}
