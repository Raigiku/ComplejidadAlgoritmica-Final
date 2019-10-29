using Packing_3D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Algorithms.Algorithm1
{
    class Node
    {
        public Vector3 Position { get; set; }
        public Vector3 Size { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Block Block { get; set; }

        public int X => (int)Position.x;
        public int Y => (int)Position.y;
        public int Z => (int)Position.z;
        public int Width => (int)Size.x;
        public int Length => (int)Size.y;
        public int Height => (int)Size.z;


        public Node()
        {
            Left = null;
            Right = null;
            Block = null;
        }


        public Node(Vector3 position, Vector3 size)
        {
            Left = null;
            Right = null;
            Block = null;
            Position = position;
            Size = size;
        }


        public Node Insert(Block block)
        {
            if (!IsALeaf())
            {
                Node newNode = Left.Insert(block);

                if (newNode != null) return newNode;

                return Right.Insert(block);
            }

            // Si es una hoja y esta llena
            if (Block != null)
            {
                return null;
            }

            FitType fitType = GetFitType(block);

            if (fitType == FitType.Bigger)
            {
                return null;
            }

            if (fitType == FitType.Exact)
            {
                Vector3 position = new Vector3 { x = X, y = Y, z = Z };

                block.Position = position;
                Block = block;

                return this;
            }

            int widthSpace = Width - block.Width;
            int lengthSpace = Length - block.Length;
            int heightSpace = Height - block.Height;

            int maxSpace = Math.Max(widthSpace, Math.Max(lengthSpace, heightSpace));

            if (maxSpace == widthSpace)
            {
                Left = new Node(
                    new Vector3(X, Y, Z),
                    new Vector3(block.Width, Length, Height)
                );

                Right = new Node(
                    new Vector3(X + block.Width, Y, Z),
                    new Vector3(widthSpace, Length, Height)
                );
            }
            else if (maxSpace == lengthSpace)
            {
                Left = new Node(
                    new Vector3(X, Y, Z),
                    new Vector3(Width, block.Length, Height)
                );

                Right = new Node(
                    new Vector3(X, Y + block.Length, Z),
                    new Vector3(Width, lengthSpace, Height)
                );
            }
            else
            {
                Left = new Node(
                    new Vector3(X, Y, Z),
                    new Vector3(Width, Length, block.Height)
                );

                Right = new Node(
                    new Vector3(X, Y, Z + block.Height),
                    new Vector3(Width, Length, heightSpace)
                );
            }

            return Left.Insert(block);
        }


        private FitType GetFitType(Block block)
        {
            if (
                block.Width > Width ||
                block.Length > Length ||
                block.Height > Height
            )
            {
                // Nota, Evaluar posibles rotaciones
                return FitType.Bigger;
            }

            if (
                block.Width == Width &&
                block.Length == Length &&
                block.Height == Height
            )
            {
                return FitType.Exact;
            }

            return FitType.Smaller;
        }


        private bool IsALeaf()
        {
            return Left == null && Right == null;
        }


        public bool HasBlock()
        {
            return Block != null;
        }


        public override string ToString()
        {
            return $"Position: {Position} - Size: {Size}";
        }
    }
}
