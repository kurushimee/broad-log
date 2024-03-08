using Gameplay;
using UnityEngine;

public class NotesInteract : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject inSight;
    [SerializeField] private QuestStates quests;

    [SerializeField] [TextArea] private string nameText;
    [SerializeField] [TextArea] private string discrText;

    public void ShowMe()
    {
        inSight.SetActive(true);
    }

    public void DontShowMe()
    {
        inSight.SetActive(false);
    }

    public void Interact()
    {
        quests.SetDiscrToNote(nameText, discrText);
    }
}