using Gameplay;
using UnityEngine;

public class ServerInteract : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject inSight;
    [SerializeField] private QuestStates quests;

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
        quests.serverLogsHave += 1;
    }
}
