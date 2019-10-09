using System.Collections.Generic;
using UnityEngine;

namespace Packing_3D.Models
{
    public class Container
    {
        public Vector3 Size { get; set; }
        public List<Block> Blocks { get; set; }
    }
}
