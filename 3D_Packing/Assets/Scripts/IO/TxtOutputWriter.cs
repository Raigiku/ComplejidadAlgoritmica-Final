using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Packing_3D.Models;
using Packing_3D.Interfaces;

namespace Packing_3D.IO
{
    public class TxtOutputWriter : Writer
    {
        public override void WriteFile()
        {
            /*
                2 3 5
                3
                2 A 1 1 2
                3 B 1 2 3
                1 C 1 2 5
             */

            /*
                Contenedores usados: 2
                Volumen disponible: 60 m3
                Volumen ocupado: 32 m3 (53.33%)
                Cajas a transportar: 6
                Contenedor Formato Coordenadas Orientacion
                1 C (0, 1, 5) 1
                1 A (2, 1, 2) 1
                1 A (2, 1, 4) 1
                1 B (0, 2, 2) 2
                1 B (0, 2, 5) 1

                2 B (0, 1, 3) 1
             */

            // Datos asumidos (por referencias)
            Vector3 containerSize = new Vector3(2, 3, 5);
            List<Format> formats = new List<Format>
            {
                new Format { Id = "A", Size = new Vector3(1, 1, 2) },
                new Format { Id = "B", Size = new Vector3(1, 2, 3) },
                new Format { Id = "C", Size = new Vector3(1, 2, 5) }
            };


            // Datos de entrada
            List<Container> containers = new List<Container> {
                new Container
                {
                    Size = containerSize,
                    Blocks = new List<Block>
                    {
                        new Block
                        {
                            Position = new Vector3(0, 1, 5),
                            Orientation = 1,
                            Format = formats[2]
                        },
                        new Block
                        {
                            Position = new Vector3(2, 1, 2),
                            Orientation = 1,
                            Format = formats[0]
                        },
                        new Block
                        {
                            Position = new Vector3(2, 1, 4),
                            Orientation = 1,
                            Format = formats[0]
                        },
                        new Block
                        {
                            Position = new Vector3(0, 2, 2),
                            Orientation = 2,
                            Format = formats[1]
                        },
                        new Block
                        {
                            Position = new Vector3(0, 2, 5),
                            Orientation = 1,
                            Format = formats[1]
                        }
                    },
                },
                new Container
                {
                    Size = containerSize,
                    Blocks = new List<Block>
                    {
                        new Block
                        {
                            Position = new Vector3(0, 1, 3),
                            Orientation = 1,
                            Format = formats[1]
                        }
                    }
                }
            };


            // Inicio del algoritmo
            Container firstContainer = containers.FirstOrDefault();
            int containerVolume = (int)(firstContainer.Size.x * firstContainer.Size.y * firstContainer.Size.z);
            int usedContainers = containers.Count;

            int totalVolume = containerVolume * usedContainers;
            int usedVolume = 0;
            int totalBlocks = 0;

            foreach (Container item in containers)
            {
                totalBlocks += item.Blocks.Count;

                foreach (Block block in item.Blocks)
                {
                    Vector3 format = block.Format.Size;
                    usedVolume += (int)(format.x * format.y * format.z);
                }
            }

            float usedPercentage = usedVolume / (float)totalVolume * 100f;


            using (System.IO.StreamWriter file = new System.IO.StreamWriter("./Assets/output.txt"))
            {
                file.WriteLine($"Contenedores usados: {usedContainers}");
                file.WriteLine($"Volumen disponible: {totalVolume} m3");
                file.WriteLine($"Volumen ocupado: {usedVolume} m3 ({usedPercentage}%)");
                file.WriteLine($"Cajas a transportar: {totalBlocks}");

                file.WriteLine($"Contenedor\t\tFormato\t\tCoordenadas\t\tOrientación");
                for (int i = 0; i < containers.Count; ++i)
                {
                    foreach (var block in containers[i].Blocks)
                    {
                        file.WriteLine($"{i + 1}\t\t\t\t{block.Format.Id}\t\t\t({block.Position.x}, {block.Position.y}, {block.Position.z})\t\t{block.Orientation}");
                    }
                }
            }
        }
    }
}