using Gameplay;
using UnityEngine;

namespace Interactable.FlowerConsole
{
    public class FlowerConsoleScr : MonoBehaviour
    {
        [SerializeField] private QuestStates questStates;
        [SerializeField] private GameObject console;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private FlowerButtonsScr[] buttons;

        private int choseInput;
        private bool isConsoleOpen;
        private int waterLevelNeeded;

        private void Update()
        {
            if (!isConsoleOpen) return;

            if (Input.GetKeyDown(KeyCode.Escape)) CloseConsole();

            SelectButtons();
        }

        public void OpenFlowerConsole()
        {
            if (isConsoleOpen) return;

            console.SetActive(true);
            isConsoleOpen = true;
            buttons[0].SelectButton(true);
            choseInput = 0;
            playerController.TakeControll(false);
            CheckNeeds(0);
            CheckNeeds(1);
            CheckNeeds(2);
        }

        private void CloseConsole()
        {
            buttons[choseInput].SelectButton(false);
            buttons[choseInput].isTrigger = false;
            console.SetActive(false);
            isConsoleOpen = false;
            playerController.TakeControll(true);
        }

        private void SelectButtons()
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

            if (!buttons[choseInput].haveTrigger) return;

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
                    buttons[choseInput].ChangeTrigger();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    CheckNeeds(choseInput);
            }
        }

        private void CheckNeeds(int button)
        {
            var needsWater = !questStates.stationHasWater && button == 0;
            var needsPower = !questStates.stationHasElectricity && button == 2;

            if (needsWater || needsPower)
                buttons[button].fillLoading = 0f;
            else if (buttons[button].triggerPos > questStates.flowerNeedsLevel[button])
                buttons[button].fillLoading = 0.15f;
            else if (buttons[button].triggerPos == questStates.flowerNeedsLevel[button])
                buttons[button].fillLoading = 0.5f;
            else if (buttons[button].triggerPos < questStates.flowerNeedsLevel[button])
                buttons[button].fillLoading = 0.9f;

            buttons[button].StartLoadFill();
        }
    }
}