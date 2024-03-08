using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class FlowerConsoleInteract : MonoBehaviour, IActivable
{
    [SerializeField] GameObject inSight;
    [SerializeField] QuestStates quests;
    [SerializeField] FlowerConsoleScr flowerConsole;

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
        flowerConsole.OpenFlowerConsole();
    }
}
