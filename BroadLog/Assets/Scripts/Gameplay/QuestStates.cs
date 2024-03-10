using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gameplay
{
    public class QuestStates : MonoBehaviour
    {
        [FormerlySerializedAs("noteObj")] [SerializeField]
        private GameObject noteObject;

        [FormerlySerializedAs("noteNameT")] [SerializeField]
        private Text noteNameText;

        [FormerlySerializedAs("noteDiscrT")] [SerializeField]
        private Text noteDescriptionText;

        [SerializeField] private Image fadeInOutImage;

        public int day = 1;

        public int[] flowerNeedsLevel;

        [FormerlySerializedAs("stationHaveStorm")]
        public bool stationHasStorm;

        [FormerlySerializedAs("stationHaveOxygen")]
        public bool stationHasAir;

        [FormerlySerializedAs("stationHaveWater")]
        public bool stationHasWater;

        [FormerlySerializedAs("stationHaveElectro")]
        public bool stationHasElectricity;

        [FormerlySerializedAs("stationHavePersonLife")]
        public bool stationHasPersonnel;

        public int serverLogsHave;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) CloseAllNotes();
        }

        public void SetNoteDescription(string noteName, string noteDescription)
        {
            noteNameText.text = noteName;
            noteDescriptionText.text = noteDescription;

            noteObject.SetActive(true);
        }

        private void CloseAllNotes()
        {
            noteObject.SetActive(false);
        }

        public void ToNextDay()
        {
            day += 1;
            serverLogsHave = 0;

            StartCoroutine(FadeInOut());
        }

        private IEnumerator FadeInOut()
        {
            var col = fadeInOutImage.color;
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