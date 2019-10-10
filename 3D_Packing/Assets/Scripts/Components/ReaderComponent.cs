using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Packing_3D.Models;

namespace Packing_3D.Components
{
    public class ReaderComponent : MonoBehaviour
    {
        private Dictionary<string, Format> formats;
        private Vector3 containerSize;

        public void ClearEntities()
        {
            formats = new Dictionary<string, Format>();
        }

        public void ReadInputFile()
        {
            var input_lines = File.ReadAllLines(@"Assets\input.txt");
            ParseInputContainer(input_lines[0].Split(' '));
            ParseInputFormats(input_lines);
        }

        private void ParseInputContainer(string[] dimensions)
        {
            var height = float.Parse(dimensions[0]);
            var width = float.Parse(dimensions[1]);
            var length = float.Parse(dimensions[2]);
            containerSize = new Vector3(width, height, length);
        }

        private void ParseInputFormats(string[] input_lines)
        {
            var total_formats = int.Parse(input_lines[1]);
            for (var i = 2; i < total_formats + 2; ++i)
            {
                var properties = input_lines[i].Split(' ');
                var totalBlocks = int.Parse(properties[0]);
                var id = properties[1];
                var height = float.Parse(properties[2]);
                var width = float.Parse(properties[3]);
                var length = float.Parse(properties[4]);

                formats[id] = new Format
                {
                    Id = id,
                    Size = new Vector3(width, height, length),
                    Blocks = new List<Block>()
                };

                for (var j = 0; j < totalBlocks; ++j)
                {
                    formats[id].Blocks.Add(new Block
                    {
                        Position = Vector3.zero,
                        Container = null,
                        Format = formats[id]
                    });
                }
            }
        }
    }
}