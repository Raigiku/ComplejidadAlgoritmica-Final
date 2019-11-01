using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Packing_3D.Models;
using Packing_3D.Interfaces;

namespace Packing_3D.IO
{
    public class TxtInputReader : Reader
    {
        public override InputData GetInputData()
        {
            var input_lines = File.ReadAllLines(@"Assets\input.txt");
            var containerSize = ParseContainer(input_lines[0].Split(' '));
            var blocks = ParseBlocks(input_lines);

            return new InputData
            {
                ContainerSize = containerSize,
                Blocks = blocks
            };
        }

        private Vector3 ParseContainer(string[] dimensions)
        {
            var height = float.Parse(dimensions[0]);
            var width = float.Parse(dimensions[1]);
            var length = float.Parse(dimensions[2]);

            return new Vector3(width, height, length);
        }

        private List<Block> ParseBlocks(string[] input_lines)
        {
            List<Block> blocks = new List<Block>();
            Dictionary<string, Format> formats = new Dictionary<string, Format>();

            var total_formats = int.Parse(input_lines[1]);
            for (var i = 2; i < total_formats + 2; ++i)
            {
                var properties = input_lines[i].Split(' ');
                var totalBlocks = int.Parse(properties[0]);
                var id = properties[1];
                var height = float.Parse(properties[2]);
                var width = float.Parse(properties[3]);
                var length = float.Parse(properties[4]);

                formats.Add(
                    id,
                    new Format
                    {
                        Id = id,
                        Size = new Vector3(width, height, length),
                        Blocks = new List<Block>()
                    }
                );

                for (var j = 0; j < totalBlocks; ++j)
                {
                    formats[id].Blocks.Add(new Block
                    {
                        Position = Vector3.zero,
                        Container = null,
                        Format = formats[id],
                        Orientation = 1,
                        Size = formats[id].Size
                    });
                }
                blocks.AddRange(formats[id].Blocks);
            }

            return blocks;
        }
    }
}