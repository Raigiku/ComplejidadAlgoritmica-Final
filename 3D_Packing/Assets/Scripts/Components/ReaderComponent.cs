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
        
        [SerializeField]
        private GameObject containerPrefab;

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

        public void ReadOutput()
        {
            var output_lines = System.IO.File.ReadAllLines(@"Assets\output.txt");
            ParseOutputBlocks(output_lines);
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

        private void ParseOutputBlocks(string[] output_lines)
        {
            var total_containers = int.Parse(output_lines[0].Split(' ')[2]);
            var containers = new List<GameObject>();
            for (var i = 0; i < total_containers; ++i)
            {
                var newContainer = Instantiate(containerPrefab, transform);
                var newContainerBox = newContainer.transform.GetChild(0);
                newContainerBox.localScale = containerSize;
                containers.Add(newContainer);
            }

            var total_blocks = int.Parse(output_lines[3].Split(' ')[3]);
            for (var i = 0; i < total_blocks; ++i)
            {
                var containerId = 1;
                var formatId = "C";
                var position = new Vector3(2, 2, 2); // Clean
                var orientation = 1;

                var blockTransform = containers[containerId - 1].transform.GetChild(1);
                var storeBlocksComponent = blockTransform.GetComponent<StoreBlocksComponent>();

                var block = new Block
                {
                    Position = position,
                    Container = null,
                    Format = formats[formatId],
                    Orientation = orientation,
                };
                storeBlocksComponent.CreateBlock(block);
            }
        }
    }
}