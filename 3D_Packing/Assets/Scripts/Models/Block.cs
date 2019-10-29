using UnityEngine;

namespace Packing_3D.Models
{
    public class Block
    {
        public Vector3 Position { get; set; }
        public Container Container { get; set; }
        public Format Format { get; set; }
        public Vector3 Size 
        {
            get
            {
                switch (Orientation)
                {
                    case 1:
                        return Format.Size;
                    case 2:
                        return new Vector3(Format.Size.z, Format.Size.y, Format.Size.x);
                    case 3:
                        return new Vector3(Format.Size.x, Format.Size.z, Format.Size.y);
                    case 4:
                        return new Vector3(Format.Size.y, Format.Size.z, Format.Size.x);
                    case 5:
                        return new Vector3(Format.Size.z, Format.Size.x, Format.Size.y);
                    case 6:
                        return new Vector3(Format.Size.y, Format.Size.x, Format.Size.z);
                    default:
                        return Vector3.one;
                }
            }
        }
        public int Orientation { get; set; }
        public override string ToString()
        {
            return $"Position:{Position} Container:{Container} Orientation:{Orientation}";
        }
    }
}
