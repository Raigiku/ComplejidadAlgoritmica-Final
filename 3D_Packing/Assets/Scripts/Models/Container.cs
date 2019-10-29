using System.Collections.Generic;
using UnityEngine;

namespace Packing_3D.Models
{
    public class Container
    {
        public Vector3 Position { get; set; }
        public Vector3 Size { get; set; }
        public List<Block> Blocks { get; set; }

        public Container() { }

        public static Container WithBlocksInitialized()
        {
            Container container = new Container();
            container.Blocks = new List<Block>();
            return container;
        }

        public void AddBlock(Block block)
        {
            if (Blocks == null)
                Blocks = new List<Block>();

            Blocks.Add(block);
        }
    }
}
