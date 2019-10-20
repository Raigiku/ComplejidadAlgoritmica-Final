using UnityEngine;
using Packing_3D.Models;
using Packing_3D.Interfaces;

namespace Packing_3D.Builders
{
    public class ContainerBuilder : Builder<Container>
    {
        [SerializeField]
        private GameObject containerPrefab = null;

        public override void Build(Container container)
        {
            var containerObject = Instantiate(containerPrefab, transform);
            containerObject.transform.position = container.Position;

            var containerBox = containerObject.transform.GetChild(0);
            containerBox.localScale = container.Size;

            var blocksTransform = containerObject.transform.GetChild(1);
            var containerBoxOrigin = containerBox.GetChild(0).position;
            blocksTransform.position = containerBoxOrigin;

            var blockBuilder = blocksTransform.GetComponent<BlockBuilder>();
            foreach (var block in container.Blocks)
            {
                blockBuilder.Build(block);
            }
        }
    }
}