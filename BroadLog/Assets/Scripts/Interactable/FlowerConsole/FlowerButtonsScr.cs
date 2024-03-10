using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Interactable.FlowerConsole
{
    public class FlowerButtonsScr : MonoBehaviour
    {
        [SerializeField] private Image buttonCol;
        [SerializeField] private Image triggerCol;
        [SerializeField] private GameObject triggerObj;
        [SerializeField] private Color selfColor;
        [SerializeField] private Color choseColor;

        public Image bar;
        public float fillLoading;

        public bool haveTrigger;
        public bool isTrigger;
        public int triggerPos = 3;

        public void SelectButton(bool sel)
        {
            if (sel)
            {
                if (isTrigger)
                    triggerCol.color = choseColor;
                else buttonCol.color = choseColor;
            }
            else
            {
                if (isTrigger)
                    triggerCol.color = selfColor;
                else buttonCol.color = selfColor;
            }
        }

        public void ChangeTrigger()
        {
            triggerPos -= 1;
            Vector2 pos;
            switch (triggerPos)
            {
                case 1:
                    pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y + 105f);
                    triggerObj.transform.position = pos;
                    break;
                case 2:
                    pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y + 105f);
                    triggerObj.transform.position = pos;
                    break;
                case 3:
                    pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y + 105f);
                    triggerObj.transform.position = pos;
                    break;
                default:
                    triggerPos = 3;
                    pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y - 210f);
                    triggerObj.transform.position = pos;
                    break;
            }
        }

        public void StartLoadFill()
        {
            bar.fillAmount = 0f;
            StartCoroutine(LoadFill());
        }

        private IEnumerator LoadFill()
        {
            while (bar.fillAmount < fillLoading)
            {
                bar.fillAmount += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}