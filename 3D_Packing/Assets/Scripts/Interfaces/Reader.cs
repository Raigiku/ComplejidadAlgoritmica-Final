using Packing_3D.Models;
using UnityEngine;

namespace Packing_3D.Interfaces
{
    public abstract class Reader : MonoBehaviour
    {
        public abstract InputData GetInputData();
    }
}
