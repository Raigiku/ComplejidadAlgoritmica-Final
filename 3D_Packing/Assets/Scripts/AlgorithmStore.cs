using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Packing_3D.Models;
using Packing_3D.Algorithms;

namespace Packing_3D
{
    public class AlgorithmStore : MonoBehaviour
    {
        public List<Algorithm> Algorithms { get; private set; }
        public int SelectedAlgorithm { get; private set; }

        [SerializeField]
        private TMP_Text algorithmText = null;

        [SerializeField]
        private TMP_Text memoryText = null;

        [SerializeField]
        private TMP_Text timeText = null;

        private void Start()
        {
            SelectedAlgorithm = 0;

            Algorithms = new List<Algorithm>
            {
                new Algorithms.Algorithm1.Main
                {
                    Name = "Binary Tree"
                },
                new Algorithms.Algorithm2.Main
                {
                    Name = "First Fit Decreasing Height"
                },
                new Algorithms.Algorithm3.Main
                {
                    Name = "Bricklaying Best Fit"
                }
            };
        }

        public void ChangeAlgorithm(int direction)
        {
            if (SelectedAlgorithm + direction > -1 && SelectedAlgorithm + direction < Algorithms.Count)
            {
                SelectedAlgorithm += direction;
                algorithmText.text = Algorithms[SelectedAlgorithm].Name;
            }
        }

        public List<Container> GetContainers(InputData inputData)
        {
            var containers = Algorithms[SelectedAlgorithm].GetContainers(inputData);
            
            memoryText.text = "Memoria: " + Algorithms[SelectedAlgorithm].MemoryUsed + " KB";
            timeText.text = "Tiempo: " + Algorithms[SelectedAlgorithm].TimeElapsed + " s";

            return containers;
        }
    }
}