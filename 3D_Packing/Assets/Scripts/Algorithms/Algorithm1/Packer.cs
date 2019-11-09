using Packing_3D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Algorithms.Algorithm1
{
    class Packer
    {
        public Vector3 Size { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Container> Containers { get; set; }

        public Packer(Vector3 size)
        {
            Size = size;
            Nodes = new List<Node>();
            Containers = new List<Container>();
            AddNode();
        }


        private void AddNode()
        {
            Nodes.Add(new Node
            {
                Position = new Vector3(0, 0, 0),
                Size = new Vector3(Size.x, Size.y, Size.z)
            });

            Containers.Add(Container.WithBlocksInitialized(Size));
        }


        private void AddNode(Block block)
        {
            Node newNode = new Node
            {
                Position = new Vector3(0, 0, 0),
                Size = new Vector3(Size.x, Size.y, Size.z)
            };
            newNode.Insert(block);
            Nodes.Add(newNode);

            Container newContainer = new Container();
            newContainer.Size = Size;
            newContainer.AddBlock(block);
            Containers.Add(newContainer);
        }


        public List<Container> Insert(List<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                InsertBlock(block);
            }

            return Containers;
        }


        private void InsertBlock(Block block)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Insert(block) != null)
                {
                    Containers[i].Blocks.Add(block);
                    return;
                }
            }

            AddNode(block);
        }
    }
}
