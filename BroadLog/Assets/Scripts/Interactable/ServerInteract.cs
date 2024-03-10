using Gameplay;
using UnityEngine;

namespace Interactable
{
    public class ServerInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject inSight;
        [SerializeField] private QuestStates quests;

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
            quests.serverLogsHave += 1;
        }
    }
}