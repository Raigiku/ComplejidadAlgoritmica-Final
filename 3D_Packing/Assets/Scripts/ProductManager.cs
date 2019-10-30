using UnityEngine;
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
        private AlgorithmStore algorithmStore = null;

        [SerializeField]
        private ContainerStore containerStore = null;

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
            var containers = algorithmStore.GetContainers(inputData);

            // Graficar los resultados
            for (int i = 0; i < containers.Count; ++i)
            {
                containerStore.ContainerGameObjects.Add(ContainerBuilder.Build(containers[i]));
                if (i == 0)
                {
                    containerStore.ContainerGameObjects[0].SetActive(true);
                }
            }

            // Escribir archivo de texto con el output
            // Debe recibir la lista de contenedores
            txtOutputWriter.WriteFile();
        }
    }
}