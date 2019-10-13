using System.Collections.Generic;
using UnityEngine;

namespace Packing_3D.Models
{
    public class InputData
    {
        public Vector3 ContainerSize { get; set; }
        public Dictionary<string, Format> Formats { get; set; }
    }
}