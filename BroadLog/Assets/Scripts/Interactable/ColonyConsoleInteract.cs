using UnityEngine;

namespace Interactable
{
    public class ColonyConsoleInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject inSight;
        [SerializeField] private ColonyConsoleScr.ColonyConsoleScr colonyConsole;

        public void ShowMe()
        {
            inSight.SetActive(true);
        }

        public void HideMe()
        {
            inSight.SetActive(false);
        }

        public void Interact()
        {
            colonyConsole.OpenConsole();
        }
    }
}