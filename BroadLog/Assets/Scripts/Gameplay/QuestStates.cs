using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{

    public class QuestStates : MonoBehaviour
    {
        [SerializeField] GameObject noteObj;
        [SerializeField] Text noteNameT;
        [SerializeField] Text noteDiscrT;

        [SerializeField] Image fadeInOutImage;

        public int day = 1;

        public int[] flowerNeedsLevel;//требуемый уровень воды/удобрений/света по убыванию 3 самая низкая потребность, 1 самая высокая
        public bool stationHaveStorm;//буря на улице?
        public bool stationHaveOxygen;
        public bool stationHaveWater;
        public bool stationHaveElectro;
        public bool stationHavePersonLife;

        public int serverLogsHave;

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

        public void GoNextDay()
        {
            day += 1;
            serverLogsHave = 0;

            StartCoroutine(FadeInOut());
        }

        IEnumerator FadeInOut()
        {
            Color col = fadeInOutImage.color;
            while (col.a < 1f)
            {
                col.a += 0.01f;
                fadeInOutImage.color = col;
                yield return new WaitForSeconds(.02f);
            }
            yield return new WaitForSeconds(2f);
            while (col.a > 0f)
            {
                col.a -= 0.01f;
                fadeInOutImage.color = col;
                yield return new WaitForSeconds(.02f);
            }
        }
    }
}
