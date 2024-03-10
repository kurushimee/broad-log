using Gameplay;
using UnityEngine;

namespace Interactable
{
    public class NotesInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject inSight;
        [SerializeField] private QuestStates quests;

        [SerializeField] [TextArea] private string nameText;
        [SerializeField] [TextArea] private string descriptionText;

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
            quests.SetNoteDescription(nameText, descriptionText);
        }
    }
}