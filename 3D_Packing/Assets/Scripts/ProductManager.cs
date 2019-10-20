using UnityEngine;
using System.Collections.Generic;
using Packing_3D.Models;
using Packing_3D.Algorithms;
using Packing_3D.Builders;
using Packing_3D.Interfaces;

namespace Packing_3D
{
    public class ProductManager : MonoBehaviour
    {
        [SerializeField]
        private ContainerBuilder ContainerBuilder = null;

        [SerializeField]
        private Writer txtOutputWriter = null;

        [SerializeField]
        private Reader txtInputReader = null;

        [SerializeField]
        private Reader uiInputReader = null;

        [SerializeField]
        private AlgorithmsStore algorithmsStore = null;

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
            var inputData = txtInputReader.GetInputData();
            var uiInputData = uiInputReader.GetInputData();
            if (uiInputData != null)
            {
                inputData.ContainerSize = uiInputData.ContainerSize;
            }
            // Ejecutar el algoritmo seleccionado
            //var containers = algorithmsStore.GetContainers(inputData);

            // EJEMPLO: porque necesito lo que bote los algoritmos
            var containers = new List<Container>
            {
                new Container
                {
                    Position = new Vector3(0, 0, 0),
                    Size = inputData.ContainerSize,
                    Blocks = new List<Block>()
                },
            };
            var formats = inputData.Formats;

            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["A"],
                Position = new Vector3(0, 0, 0),
                Orientation = 1
            });
            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["B"],
                Position = new Vector3(10, 10, 10),
                Orientation = 2
            });
            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["C"],
                Position = new Vector3(20, 20, 20),
                Orientation = 3
            });
            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["D"],
                Position = new Vector3(30, 30, 30),
                Orientation = 4
            });
            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["E"],
                Position = new Vector3(40, 40, 40),
                Orientation = 5
            });
            containers[0].Blocks.Add(new Block
            {
                Container = containers[0],
                Format = formats["F"],
                Position = new Vector3(50, 50, 50),
                Orientation = 6
            });
            // EJEMPLO: porque necesito lo que bote los algoritmos

            // Graficar los resultados
            foreach (var container in containers)
            {
                ContainerBuilder.Build(container);
            }

            // Escribir archivo de texto con el output
            // Debe recibir la lista de contenedores
            txtOutputWriter.WriteFile();
        }
    }
}