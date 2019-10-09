using System.Collections.Generic;
using UnityEngine;

namespace Packing_3D.Models
{
    public class Format
    {
        public string Id { get; set; }
        public Vector3 Size { get; set; }
        public List<Block> Blocks { get; set; }
    }
}