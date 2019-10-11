using UnityEngine;
using Packing_3D.Models;

namespace Packing_3D.Components
{
    public class StoreBlocksComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject container = null;

        [SerializeField]
        private GameObject blockPrefab = null;

        public void CreateBlock(Block block)
        {
            var blockObject = Instantiate(blockPrefab, transform);
            blockObject.transform.localScale = block.Format.Size;
            blockObject.transform.localPosition = block.Position;
            blockObject.GetComponent<IdComponent>().ChangeIdText(block.Format.Id);
            blockObject.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
        }
    }
}
