using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Packing_3D.Components
{
    public class IdComponent : MonoBehaviour
    {
        [SerializeField]
        private List<TMP_Text> idTexts = new List<TMP_Text>();

        public void ChangeIdText(string id)
        {
            idTexts.ForEach(idText => idText.text = id);
        }
    }
}
