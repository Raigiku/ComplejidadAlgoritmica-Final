using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Packing_3D
{
    public class ContainerStore : MonoBehaviour
    {
        public List<GameObject> ContainerGameObjects { get; set; }
        public int SelectedContainer { get; set; }

        [SerializeField]
        private TMP_Text containerText = null;

        [SerializeField]
        private TMP_Text totalContainersText = null;

        private void Start()
        {
            SelectedContainer = 0;
            ContainerGameObjects = new List<GameObject>();
        }

        public void ChangeContainer(int direction)
        {
            int totalContainers = ContainerGameObjects.Count;
            if (totalContainers > 0)
            {
                if (SelectedContainer + direction > -1 && SelectedContainer + direction < totalContainers)
                {
                    ContainerGameObjects[SelectedContainer].SetActive(false);
                    SelectedContainer += direction;
                    ContainerGameObjects[SelectedContainer].SetActive(true);
                    containerText.text = "Contenedor " + (SelectedContainer + 1).ToString();
                }
            }
        }

        public void ResetSelectedContainer()
        {
            SelectedContainer = 0;
            ContainerGameObjects[SelectedContainer].SetActive(true);
            containerText.text = "Contenedor " + (SelectedContainer + 1).ToString();
        }

        public void ShowTotalContainers()
        {
            totalContainersText.text = ContainerGameObjects.Count + " contenedores";
        }
    }
}