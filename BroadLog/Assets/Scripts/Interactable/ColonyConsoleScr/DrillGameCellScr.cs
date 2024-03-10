using UnityEngine;
using UnityEngine.UI;

namespace Interactable.ColonyConsoleScr
{
    public class DrillGameCellScr : MonoBehaviour
    {
        public bool isIce;
        [SerializeField] private Sprite defSpr;

        private Image thisImage;

        private void Start()
        {
            thisImage = GetComponent<Image>();
        }

        private void Update()
        {
            if (!isIce) thisImage.sprite = defSpr;
        }
    }
}