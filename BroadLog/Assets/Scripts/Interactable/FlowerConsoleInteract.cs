using Gameplay;
using Interactable.FlowerConsole;
using UnityEngine;

namespace Interactable
{
    public class FlowerConsoleInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject inSight;
        [SerializeField] private QuestStates quests;
        [SerializeField] private FlowerConsoleScr flowerConsole;

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
            flowerConsole.OpenFlowerConsole();
        }
    }
}