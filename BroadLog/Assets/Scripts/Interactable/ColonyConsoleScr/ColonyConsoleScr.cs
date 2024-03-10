using System.Collections;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Interactable.ColonyConsoleScr
{
    public class ColonyConsoleScr : MonoBehaviour
    {
        [SerializeField] private GameObject console;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject loadingWindow;
        [SerializeField] private GameObject mainWindow;
        [SerializeField] private QuestStates questStates;

        [SerializeField] private ConsoleButtonScr[] buttons;

        public Image bar;
        [SerializeField] private bool playingGame;
        [SerializeField] private bool analizeLogs;

        [SerializeField] private GameObject drillGameGrid;
        [SerializeField] private GameObject drillGameObj;
        [SerializeField] private InputField drillInputObj;
        [SerializeField] private InputField logInputObj;
        [SerializeField] private Text logInputTextObj;
        [SerializeField] private int drillX = 11;
        [SerializeField] private int drillY = 11;
        [SerializeField] private bool canDrill;

        public int choseInput;

        [SerializeField] private Text[] stationHave;
        [SerializeField] [TextArea] private string[] logText;

        private bool consoleIsOpen;

        private int drillDirection;
        private string drillInputText;
        private float fillLoading;
        private DrillGameCellScr iceBlockDrilling;
        private string logInputText;
        private bool mainIsOpen;

        private void Start()
        {
            buttons[choseInput].StartBlinking();
        }

        private void Update()
        {
            if (consoleIsOpen)
            {
                if (playingGame || analizeLogs)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        playingGame = false;
                        analizeLogs = false;


                        for (var i = 0; i < buttons.Length; i++)
                        {
                            buttons[i].OpenPage(false);
                            buttons[i].StopBlinking();
                        }

                        choseInput = 0;

                        buttons[choseInput].StartBlinking();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        for (var i = 0; i < buttons.Length; i++)
                        {
                            buttons[i].OpenPage(false);
                            buttons[i].StopBlinking();
                        }

                        CloseConsole();
                    }

                    bar.fillAmount = fillLoading;
                }
            }

            if (mainIsOpen)
            {
                if (playingGame)
                {
                    PlayDrillGame();
                    drillInputObj.ActivateInputField();
                }
                else if (analizeLogs)
                {
                    AnalizeLogsGame();
                    logInputObj.ActivateInputField();
                }
                else
                {
                    SelectMenu();
                }
            }
        }

        private void FixedUpdate()
        {
            if (playingGame)
                DrillRayCast();
        }

        public void OpenConsole()
        {
            if (!consoleIsOpen)
            {
                console.SetActive(true);
                consoleIsOpen = true;
                playerController.TakeControll(false);
                loadingWindow.SetActive(true);
                fillLoading = 0f;
                choseInput = 0;
                buttons[choseInput].StartBlinking();
                StartCoroutine(LoadingConsole());
            }
        }

        private void CloseConsole()
        {
            consoleIsOpen = false;
            mainIsOpen = false;
            playerController.TakeControll(true);
            CloseAllWindows();
            console.SetActive(false);
        }

        private void CloseAllWindows()
        {
            loadingWindow.SetActive(false);
            mainWindow.SetActive(false);
        }

        private void MainWindow()
        {
            CloseAllWindows();
            mainWindow.SetActive(true);
            mainIsOpen = true;
            choseInput = 0;
            CheckStatusBase();
        }

        private void SelectMenu()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttons[choseInput].StopBlinking();

                choseInput -= 1;
                if (choseInput < 0)
                    choseInput = buttons.Length - 1;

                buttons[choseInput].StartBlinking();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttons[choseInput].StopBlinking();

                choseInput += 1;
                if (choseInput > buttons.Length - 1)
                    choseInput = 0;

                buttons[choseInput].StartBlinking();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                for (var i = 0; i < buttons.Length; i++) buttons[i].OpenPage(false);

                buttons[choseInput].OpenPage(true);

                if (choseInput == buttons.Length - 1)
                {
                    buttons[buttons.Length - 1].OpenPage(false);
                    buttons[buttons.Length - 1].StopBlinking();
                    CloseConsole();
                }

                if (buttons[choseInput].isGame) playingGame = true;

                if (buttons[choseInput].isLogs)
                {
                    analizeLogs = true;
                    logInputObj.readOnly = false;
                    logInputTextObj.text = "Ëîãè íå íàéäåíû\r\nÏðåäîñòàâüòå ëîãè äëÿ àíàëèçà\r\n\r\nÂâåäèòå start";
                }

                CheckStatusBase();
            }
        }

        private void DrillRayCast()
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(new Vector2(drillGameObj.transform.position.x, drillGameObj.transform.position.y),
                Vector3.up, 38f);
            if (hit.collider != null)
                if (hit.transform.TryGetComponent(out DrillGameCellScr drillCell))
                {
                    if (drillCell.isIce)
                    {
                        //Debug.Log("ÒÓÒ Ë¨Ä!");
                        //drillCell.isIce = false;
                        iceBlockDrilling = drillCell;
                        canDrill = true;
                    }
                    else
                    {
                        canDrill = false;
                    }
                }
        }

        private void PlayDrillGame()
        {
            if (drillInputText == "move forward")
            {
                Vector2 pos;
                switch (drillDirection)
                {
                    case 0:
                        if (drillY > 1)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x,
                                drillGameGrid.transform.position.y - 76f);
                            drillGameGrid.transform.position = pos;
                            drillY -= 1;
                        }

                        break;
                    case 1:
                        if (drillX < 21)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x - 76f,
                                drillGameGrid.transform.position.y);
                            drillGameGrid.transform.position = pos;
                            drillX += 1;
                        }

                        break;
                    case 2:
                        if (drillY < 20)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x,
                                drillGameGrid.transform.position.y + 76f);
                            drillGameGrid.transform.position = pos;
                            drillY += 1;
                        }

                        break;
                    case 3:
                        if (drillX > 1)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x + 76f,
                                drillGameGrid.transform.position.y);
                            drillGameGrid.transform.position = pos;
                            drillX -= 1;
                        }

                        break;
                }

                drillInputText = "";
            }

            if (drillInputText == "move back")
            {
                Vector2 pos;
                switch (drillDirection)
                {
                    case 0:
                        if (drillY < 20)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x,
                                drillGameGrid.transform.position.y + 76f);
                            drillGameGrid.transform.position = pos;
                            drillY += 1;
                        }

                        break;
                    case 1:
                        if (drillX > 1)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x + 76f,
                                drillGameGrid.transform.position.y);
                            drillGameGrid.transform.position = pos;
                            drillX -= 1;
                        }

                        break;
                    case 2:
                        if (drillY > 1)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x,
                                drillGameGrid.transform.position.y - 76f);
                            drillGameGrid.transform.position = pos;
                            drillY -= 1;
                        }

                        break;
                    case 3:
                        if (drillX < 21)
                        {
                            pos = new Vector2(drillGameGrid.transform.position.x - 76f,
                                drillGameGrid.transform.position.y);
                            drillGameGrid.transform.position = pos;
                            drillX += 1;
                        }

                        break;
                }

                drillInputText = "";
            }

            if (drillInputText == "turn right")
            {
                drillGameObj.transform.Rotate(0, 0, drillGameObj.transform.rotation.z - 90f);
                drillDirection += 1;
                if (drillDirection > 3)
                    drillDirection = 0;

                drillInputText = "";
            }

            if (drillInputText == "turn left")
            {
                drillGameObj.transform.Rotate(0, 0, drillGameObj.transform.rotation.z + 90f);
                drillDirection -= 1;
                if (drillDirection < 0)
                    drillDirection = 3;

                drillInputText = "";
            }

            if (drillInputText == "exit")
            {
                drillInputText = "";
                playingGame = false;

                for (var i = 0; i < buttons.Length; i++)
                {
                    buttons[i].OpenPage(false);
                    buttons[i].StopBlinking();
                }

                choseInput = 0;

                buttons[choseInput].StartBlinking();
            }

            if (drillInputText == "start drill")
                if (canDrill)
                {
                    drillInputText = "";
                    playingGame = false;

                    for (var i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].OpenPage(false);
                        buttons[i].StopBlinking();
                    }

                    choseInput = 0;

                    buttons[choseInput].StartBlinking();
                    questStates.stationHasWater = true;
                }
        }

        private void AnalizeLogsGame()
        {
            if (logInputText == "start")
            {
                logInputObj.text = "";
                logInputText = "";
                if (questStates.serverLogsHave > 0)
                {
                    logInputObj.readOnly = true;
                    StartCoroutine(AnalizeLogsLoading());
                }
            }
        }

        public void ReadDrillInput(string st)
        {
            drillInputText = st;
            //Debug.Log(st);
        }

        public void ReadLogInput(string st)
        {
            logInputText = st;
            //Debug.Log(st);
        }

        private void CheckStatusBase()
        {
            if (questStates.stationHasStorm)
                stationHave[0].text = "Ïîãîäà: Ïûëåâàÿ áóðÿ";
            else stationHave[0].text = "Ïîãîäà: Ñîëíå÷íî";

            if (questStates.stationHasAir)
                stationHave[1].text = "Êèñëîðîä: Íîðìà";
            else stationHave[1].text = "Êèñëîðîä: Êðèòè÷åñêèé óðîâåíü";

            if (questStates.stationHasWater)
                stationHave[2].text = "Âûðàáîòêà âîäû: Íîðìà";
            else stationHave[2].text = "Âûðàáîòêà âîäû: Ïðåêðàùåíà";

            if (questStates.stationHasElectricity)
                stationHave[3].text = "Ýëåêòðè÷åñòâî: Íîðìà";
            else stationHave[3].text = "Ýëåêòðè÷åñòâî: Êðèòè÷åñêèé óðîâåíü";

            if (questStates.stationHasPersonnel)
                stationHave[4].text = "Ñîñòîÿíèå ïåðñîíàëà: Íîðìà";
            else stationHave[4].text = "Ñîñòîÿíèå ïåðñîíàëà: Ïîãèá";
        }

        private IEnumerator LoadingConsole()
        {
            while (fillLoading < 0.4f)
            {
                fillLoading += 0.01f;
                yield return new WaitForSeconds(.02f);
            }

            while (fillLoading < 0.5f)
            {
                fillLoading += 0.04f;
                yield return new WaitForSeconds(0.5f);
            }

            while (fillLoading < 1f)
            {
                fillLoading += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }

            MainWindow();
        }

        private IEnumerator AnalizeLogsLoading()
        {
            logInputTextObj.text = "";
            for (var i = 0; i < logText.Length; i++)
            {
                logInputTextObj.text = logText[i];
                yield return new WaitForSeconds(Random.Range(0.1f, 2f));
            }

            logInputTextObj.text = "Íà÷àëî íîâîãî äíÿ";
            questStates.ToNextDay();
            yield return new WaitForSeconds(4f);

            analizeLogs = false;
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i].OpenPage(false);
                buttons[i].StopBlinking();
            }

            bar.fillAmount = fillLoading;

            CloseConsole();
        }
    }
}