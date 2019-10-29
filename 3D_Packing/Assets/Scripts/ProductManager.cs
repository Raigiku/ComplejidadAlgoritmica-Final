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
            var containers = algorithmsStore.GetContainers(inputData);

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