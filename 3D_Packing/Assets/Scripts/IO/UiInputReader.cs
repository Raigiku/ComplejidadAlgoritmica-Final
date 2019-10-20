using Packing_3D.Interfaces;
using Packing_3D.Models;
using UnityEngine;

namespace Packing_3D.IO
{
    public class UiInputReader : Reader
    {
        public Vector3 ContainerSize { get; private set; }

        public void ChangeContainerLength(string value)
        {
            float.TryParse(value, out var length);
            ContainerSize = new Vector3(ContainerSize.x, ContainerSize.y, length);
        }

        public void ChangeContainerHeight(string value)
        {
            float.TryParse(value, out var height);
            ContainerSize = new Vector3(ContainerSize.x, height, ContainerSize.z);
        }

        public void ChangeContainerWidth(string value)
        {
            float.TryParse(value, out var width);
            ContainerSize = new Vector3(width, ContainerSize.y, ContainerSize.z);
        }

        public override InputData GetInputData()
        {
            if (ContainerSize.x == 0 || ContainerSize.y == 0 || ContainerSize.z == 0)
            {
                return null;
            }
            else
            {
                return new InputData
                {
                    ContainerSize = ContainerSize,
                    Formats = null
                };
            }
        }
    }
}