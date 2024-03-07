using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyConsoleInteract : MonoBehaviour, IActivable
{
    [SerializeField] GameObject inSight;
    [SerializeField] ColonyConsoleScr colonyConsole;
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
        colonyConsole.OpenConsole();
    }
}
