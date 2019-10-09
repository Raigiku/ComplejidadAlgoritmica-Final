using UnityEngine;

namespace Packing_3D.Models
{
    public class Block
    {
        public Vector3 Position { get; set; }
        public Container Container { get; set; }
        public Format Format { get; set; }
        public int Orientation { get; set; }
    }
}
