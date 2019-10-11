using UnityEngine;
using Packing_3D.Models;

namespace Packing_3D.Components
{
    public class WriterInputComponent : MonoBehaviour, IWriter
    {
        public void WriteFile()
        {
            print("input");
        }
    }
}