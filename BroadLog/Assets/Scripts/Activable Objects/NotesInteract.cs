using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInteract : MonoBehaviour, IActivable
{
    [SerializeField] GameObject inSight;
    [SerializeField] QuestStates quests;

    [SerializeField][TextArea] string nameText;
    [SerializeField][TextArea] string discrText;

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
