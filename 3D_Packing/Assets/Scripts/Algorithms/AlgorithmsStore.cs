using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Packing_3D.Models;

namespace Packing_3D.Algorithms
{
    public class AlgorithmsStore : MonoBehaviour
    {
        public List<Algorithm> Algorithms { get; private set; }
        public int SelectedAlgorithm { get; private set; }

        [SerializeField]
        private TMP_Text algorithmText = null;

        private void Start()
        {
            SelectedAlgorithm = 0;

            Algorithms = new List<Algorithm>
            {
                new Algorithm1.Main
                {
                    Name = "Algoritmo 1"
                },
                new Algorithm2.Main
                {
                    Name = "Algoritmo 2"
                },
                new Algorithm3.Main
                {
                    Name = "Algoritmo 3"
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
            return Algorithms[SelectedAlgorithm].GetContainers(inputData);
        }
    }
}