using UnityEngine;
using System.Collections.Generic;
using Packing_3D.Models;
using Packing_3D.IO;
using Packing_3D.Algorithms;
using Packing_3D.Builders;
using Packing_3D.Interfaces;

namespace Packing_3D
{
    public class ProductManager : MonoBehaviour
    {
        public IReader InputReader { get; private set; }
        public IWriter OutputWriter { get; private set; }
        public IBuilder<Container> ContainerBuilder { get; private set; }

        [SerializeField]
        private AlgorithmsStore algorithmsStore;

        private void Start()
        {
            InputReader = new InputReader();
            OutputWriter = new OutputWriter();
            ContainerBuilder = GetComponent<ContainerBuilder>();
        }

        private void DeletePreviousContainers()
        {
            var containerObjects = GameObject.FindGameObjectsWithTag("Container");
            foreach (var containerObject in containerObjects)
            {
                Destroy(containerObject);
            }
        }

        public void CreateProduct()
        {
            // Borrar contenedores anteriores
            DeletePreviousContainers();

            // Obtener la data del input
            var inputData = InputReader.GetInputData();

            // Ejecutar el algoritmo seleccionado
            //var containers = algorithmsStore.GetContainers(inputData);

            // EJEMPLO: porque necesito lo que bote los algoritmos
            var containers = new List<Container>
            {
                new Container
                {
                    Position = new Vector3(0, 0, 0),
                    Size = new Vector3(5, 4, 3),
                    Blocks = new List<Block>()
                },
                new Container
                {
                    Position = new Vector3(5, 0, 0),
                    Size = new Vector3(3, 4, 5),
                    Blocks = new List<Block>()
                },
            };
            var formats = new Dictionary<string, Format>
            {
                {
                    "A",
                    new Format
                    {
                        Id = "A",
                        Size = new Vector3(3, 1, 2),
                        Blocks = new List<Block>(new Block[10])
                    }
                },
                {
                    "B",
                    new Format
                    {
                        Id = "B",
                        Size = new Vector3(2, 3, 1),
                        Blocks = new List<Block>(new Block[10])
                    }
                }
            };

            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["A"],
                Position = new Vector3(0, 0, 0),
                Orientation = 2
            });
            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["B"],
                Position = new Vector3(1, 1, 0),
                Orientation = 1
            });

            containers[1].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["A"],
                Position = new Vector3(0, 1, 0),
                Orientation = 1
            });
            containers[1].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["B"],
                Position = new Vector3(0, 0, 2),
                Orientation = 1
            });
            // EJEMPLO: porque necesito lo que bote los algoritmos

            // Graficar los resultados
            foreach (var container in containers)
            {
                ContainerBuilder.Build(container);
            }

            // Escribir archivo de texto con el output
            // Debe recibir la lista de contenedores
            OutputWriter.WriteFile();
        }
    }
}