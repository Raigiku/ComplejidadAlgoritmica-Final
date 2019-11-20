using Packing_3D.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Packing_3D.Interfaces
{
    public abstract class Writer : MonoBehaviour
    {
        public abstract void WriteFile(List<Container> containers);
    }
}