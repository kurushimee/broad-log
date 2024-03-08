using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class QuestStates : MonoBehaviour
    {
        [SerializeField] private GameObject noteObj;
        [SerializeField] private Text noteNameT;
        [SerializeField] private Text noteDiscrT;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) CloseAllWindows();
        }

        public void SetDiscrToNote(string name, string dscr)
        {
            noteNameT.text = name;
            noteDiscrT.text = dscr;

            noteObj.SetActive(true);
        }

        private void CloseAllWindows()
        {
            noteObj.SetActive(false);
        }
    }
}