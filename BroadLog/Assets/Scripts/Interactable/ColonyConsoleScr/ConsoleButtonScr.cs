using UnityEngine;
using UnityEngine.UI;

namespace Interactable.ColonyConsoleScr
{
    public class ConsoleButtonScr : MonoBehaviour
    {
        [SerializeField] private Image frame;
        [SerializeField] private GameObject chosed;
        [SerializeField] private GameObject contentObj;
        public bool isGame;
        public bool isLogs;
        private bool fadeIn;

        private void Update()
        {
            if (fadeIn)
            {
                var tempColor = frame.color;
                tempColor.a = Mathf.PingPong(Time.time * 3f, 1f);
                frame.color = tempColor;
            }
        }

        public void StartBlinking()
        {
            fadeIn = true;
        }

        public void StopBlinking()
        {
            var tempColor = frame.color;
            tempColor.a = 1f;
            frame.color = tempColor;
            fadeIn = false;
        }

        public void OpenPage(bool bb)
        {
            chosed.SetActive(bb);
            contentObj.SetActive(bb);
        }
    }
}