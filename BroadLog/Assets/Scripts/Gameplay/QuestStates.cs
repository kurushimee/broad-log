using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{

    public class QuestStates : MonoBehaviour
    {
        [SerializeField] GameObject noteObj;
        [SerializeField] Text noteNameT;
        [SerializeField] Text noteDiscrT;

        int waterLevelNeed;
        int waterLevelHave;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) CloseAllWindows();
        }

        public void SetDiscrToNote(string name, string dscr)
        {
            noteNameT.text = name;
            noteDiscrT.text = dscr;

            noteObj.SetActive(true);
        }

        void CloseAllWindows()
        {
            noteObj.SetActive(false);
        }
    }
}
