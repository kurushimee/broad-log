using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FlowerConsoleScr : MonoBehaviour
{
    [SerializeField] QuestStates questStates;
    [SerializeField] GameObject console;
    [SerializeField] PlayerController playerController;

    [SerializeField] FlowerButtonsScr[] buttons;

    bool consoleIsOpen;
    int choseInput;

    int waterLevelNeed;

    private void Update()
    {
        if (consoleIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseConsole();
            }

            SelectButtons();
        }
    }

    public void OpenFlowerConsole()
    {
        if (!consoleIsOpen)
        {
            console.SetActive(true);
            consoleIsOpen = true;
            buttons[0].SelectButton(true);
            choseInput = 0;
            playerController.TakeControll(false);
            CheckNeeds(0);
            CheckNeeds(1);
            CheckNeeds(2);
        }
    }

    void CloseConsole()
    {
        buttons[choseInput].SelectButton(false);
        buttons[choseInput].isTrigger = false;
        console.SetActive(false);
        consoleIsOpen = false;
        playerController.TakeControll(true);
    }

    void SelectButtons()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (buttons[choseInput].haveTrigger)
            {
                buttons[choseInput].SelectButton(false);
                buttons[choseInput].isTrigger = false;
            }
            buttons[choseInput].SelectButton(false);

            choseInput -= 1;
            if (choseInput < 0)
                choseInput = buttons.Length - 1;

            buttons[choseInput].SelectButton(true);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (buttons[choseInput].haveTrigger)
            {
                buttons[choseInput].SelectButton(false);
                buttons[choseInput].isTrigger = false;

            }
            buttons[choseInput].SelectButton(false);

            choseInput += 1;
            if (choseInput > buttons.Length - 1)
                choseInput = 0;

            buttons[choseInput].SelectButton(true);
        }

        if (buttons[choseInput].haveTrigger)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttons[choseInput].SelectButton(false);
                buttons[choseInput].isTrigger = true;
                buttons[choseInput].SelectButton(true);
            }
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttons[choseInput].SelectButton(false);
                buttons[choseInput].isTrigger = false;
                buttons[choseInput].SelectButton(true);
            }

            if (buttons[choseInput].isTrigger)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {

                    buttons[choseInput].ChangeTrigger();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    CheckNeeds(choseInput);
                }
            }
        }
    }

    private void CheckNeeds(int button)
    {

        if (!questStates.stationHaveWater && button == 0)
        {
            buttons[button].fillLoading = 0f;
            buttons[button].StartLoadFill();
            return;
        }

        if (!questStates.stationHaveElectro && button == 2)
        {
            buttons[button].fillLoading = 0f;
            buttons[button].StartLoadFill();
            return;
        }

        if (buttons[button].triggerPos > questStates.flowerNeedsLevel[button])
        {
            buttons[button].fillLoading = 0.15f;
            buttons[button].StartLoadFill();
        }
        if (buttons[button].triggerPos == questStates.flowerNeedsLevel[button])
        {
            buttons[button].fillLoading = 0.5f;
            buttons[button].StartLoadFill();
        }
        if (buttons[button].triggerPos < questStates.flowerNeedsLevel[button])
        {
            buttons[button].fillLoading = 0.9f;
            buttons[button].StartLoadFill();
        }
    }
}
