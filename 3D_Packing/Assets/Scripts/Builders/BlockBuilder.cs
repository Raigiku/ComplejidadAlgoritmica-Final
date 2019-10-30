using UnityEngine;
using Packing_3D.Models;
using Packing_3D.Interfaces;
using Packing_3D.UI;

namespace Packing_3D.Builders
{
    public class BlockBuilder : Builder<Block>
    {
        [SerializeField]
        private GameObject blockPrefab = null;

        [SerializeField]
        private Shader shader = null;

        public override GameObject Build(Block block)
        {
            var blockObject = Instantiate(blockPrefab, transform);

            var cube = blockObject.transform.GetChild(0);

            var cubeIdUI = cube.GetComponent<Id>();
            cubeIdUI.ChangeIdText(block.Format.Id);

            blockObject.transform.localPosition = block.Position;
            blockObject.transform.localScale = block.Format.Size;

            var offset = Vector3.zero;
            if (block.Orientation == 2)
            {
                blockObject.transform.Rotate(0, -90, 0);
                offset = Vector3.right * block.Format.Size.z;
            }
            else if (block.Orientation == 3)
            {
                blockObject.transform.Rotate(-90, 0, 0);
                offset = Vector3.forward * block.Format.Size.y;
            }
            else if (block.Orientation == 4)
            {
                blockObject.transform.Rotate(-90, -90, 0);
            }
            else if (block.Orientation == 5)
            {
                blockObject.transform.Rotate(0, 90, 90);
            }
            else if (block.Orientation == 6)
            {
                blockObject.transform.Rotate(0, 0, 90);
                offset = Vector3.right * block.Format.Size.y;
            }
            blockObject.transform.localPosition += offset;

            var material = new Material(shader);
            material.color = Random.ColorHSV();
            cube.GetComponent<Renderer>().material = material;

            return blockObject;
        }
    }
}