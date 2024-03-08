using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
<<<<<<< Updated upstream
    public class QuestStates : MonoBehaviour
=======
    [SerializeField] GameObject noteObj;
    [SerializeField] Text noteNameT;
    [SerializeField] Text noteDiscrT;

    int waterLevelNeed;
    int waterLevelHave;


    private void Update()
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
}
=======

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
>>>>>>> Stashed changes
