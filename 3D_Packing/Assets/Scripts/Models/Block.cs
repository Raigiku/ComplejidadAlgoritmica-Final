using UnityEngine;

namespace Packing_3D.Models
{
    public class Block
    {
        public Vector3 Position { get; set; }
        public Container Container { get; set; }
        public Format Format { get; set; }
        public Vector3 Size { get; set; }
        public int Orientation { get; set; }
        public int Width => (int)Format.Size.x;
        public int Length => (int)Format.Size.y;
        public int Height => (int)Format.Size.z;
        public int Volume => (int)Width * Length * Height;

        public Block()
        {

        }

        public override string ToString()
        {
            return $"Position:{Position} Container:{Container} Orientation:{Orientation}";
        }
    }
}
