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
            blockObject.transform.localScale = block.Size;

            var material = new Material(shader);
            material.color = Random.ColorHSV();
            cube.GetComponent<Renderer>().material = material;

            return blockObject;
        }
    }
}