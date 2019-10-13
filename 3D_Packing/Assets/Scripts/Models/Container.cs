using System.Collections.Generic;
using UnityEngine;

namespace Packing_3D.Models
{
    public class Container
    {
        public Vector3 Position { get; set; }
        public Vector3 Size { get; set; }
        public List<Block> Blocks { get; set; }
    }
}
