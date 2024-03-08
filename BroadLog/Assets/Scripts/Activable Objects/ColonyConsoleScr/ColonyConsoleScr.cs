using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ColonyConsoleScr : MonoBehaviour
{
    [SerializeField] GameObject console;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject loadingWindow;
    [SerializeField] GameObject mainWindow;
    [SerializeField] QuestStates questStates;

    [SerializeField] ConsoleButtonScr[] buttons;

    public Image bar;
    private float fillLoading;

    private bool consoleIsOpen;
    private bool mainIsOpen;
    [SerializeField] bool playingGame;
    [SerializeField] bool analizeLogs;

    [SerializeField] GameObject drillGameGrid;
    [SerializeField] GameObject drillGameObj;
    [SerializeField] InputField drillInputObj;
    [SerializeField] InputField logInputObj;
    [SerializeField] Text logInputTextObj;

    int drillDirection;
    [SerializeField] int drillX = 11;
    [SerializeField] int drillY = 11;
    string drillInputText;
    string logInputText;
    DrillGameCellScr iceBlockDrilling;
    [SerializeField] bool canDrill;

    public int choseInput;

    [SerializeField] Text[] stationHave;
    [SerializeField][TextArea] string[] logText;

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


                    for (int i = 0; i < buttons.Length; i++)
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
                    for (int i = 0; i < buttons.Length; i++)
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
        if(playingGame)
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

    void CloseConsole()
    {
        consoleIsOpen = false;
        mainIsOpen = false;
        playerController.TakeControll(true);
        CloseAllWindows();
        console.SetActive(false);
    }

    void CloseAllWindows()
    {
        loadingWindow.SetActive(false);
        mainWindow.SetActive(false);
    }

    void MainWindow()
    {
        CloseAllWindows();
        mainWindow.SetActive(true);
        mainIsOpen = true;
        choseInput = 0;
        CheckStatusBase();
    }

    void SelectMenu()
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

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].OpenPage(false);
            }

            buttons[choseInput].OpenPage(true);

            if (choseInput == buttons.Length - 1)
            {
                buttons[buttons.Length - 1].OpenPage(false);
                buttons[buttons.Length - 1].StopBlinking();
                CloseConsole();
            }

            if (buttons[choseInput].isGame)
            {
                playingGame = true;
            }

            if (buttons[choseInput].isLogs)
            {
                analizeLogs = true;
                logInputObj.readOnly = false;
                logInputTextObj.text = "Логи не найдены\r\nПредоставьте логи для анализа\r\n\r\nВведите start";
            }

            CheckStatusBase();
        }
    }

    void DrillRayCast()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector2(drillGameObj.transform.position.x, drillGameObj.transform.position.y), Vector3.up, 38f);
        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent<DrillGameCellScr>(out DrillGameCellScr drillCell))
            {
                if (drillCell.isIce)
                {
                    //Debug.Log("ТУТ ЛЁД!");
                    //drillCell.isIce = false;
                    iceBlockDrilling = drillCell;
                    canDrill = true;
                }
                else canDrill = false;
            }
        }
    }

    void PlayDrillGame()
    {
        if (drillInputText == "move forward")
        {
            Vector2 pos;
            switch (drillDirection)
            {
                case 0:
                    if (drillY > 1)
                    {
                        pos = new Vector2(drillGameGrid.transform.position.x, drillGameGrid.transform.position.y - 76f);
                        drillGameGrid.transform.position = pos;
                        drillY -= 1;
                    }
                    break;
                case 1:
                    if (drillX < 21)
                    {
                        pos = new Vector2(drillGameGrid.transform.position.x - 76f, drillGameGrid.transform.position.y);
                        drillGameGrid.transform.position = pos;
                        drillX += 1;
                    }
                    break;
                case 2:
                    if (drillY < 20)
                    {
                        pos = new Vector2(drillGameGrid.transform.position.x, drillGameGrid.transform.position.y + 76f);
                        drillGameGrid.transform.position = pos;
                        drillY += 1;
                    }
                    break;
                case 3:
                    if (drillX > 1)
                    {
                        pos = new Vector2(drillGameGrid.transform.position.x + 76f, drillGameGrid.transform.position.y);
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
                        pos = new Vector2(drillGameGrid.transform.position.x, drillGameGrid.transform.position.y + 76f);
                        drillGameGrid.transform.position = pos;
                        drillY += 1;
                    }
                    break;
                case 1:
                    if (drillX > 1)
                    {
                        pos = new Vector2(drillGameGrid.transform.position.x + 76f, drillGameGrid.transform.position.y);
                        drillGameGrid.transform.position = pos;
                        drillX -= 1;
                    }
                    break;
                case 2:
                    if (drillY > 1)
                    {
                        pos = new Vector2(drillGameGrid.transform.position.x, drillGameGrid.transform.position.y - 76f);
                        drillGameGrid.transform.position = pos;
                        drillY -= 1;
                    }
                    break;
                case 3:
                    if (drillX < 21)
                    {
                        pos = new Vector2(drillGameGrid.transform.position.x - 76f, drillGameGrid.transform.position.y);
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

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].OpenPage(false);
                buttons[i].StopBlinking();
            }
            choseInput = 0;

            buttons[choseInput].StartBlinking();
        }

        if (drillInputText == "start drill")
        {
            if (canDrill)
            {
                drillInputText = "";
                playingGame = false;

                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].OpenPage(false);
                    buttons[i].StopBlinking();
                }
                choseInput = 0;

                buttons[choseInput].StartBlinking();
                questStates.stationHaveWater = true;
            }


        }
    }

    void AnalizeLogsGame()
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

    void CheckStatusBase()
    {

        if (questStates.stationHaveStorm)
        {
            stationHave[0].text = "Погода: Пылевая буря";
        }else stationHave[0].text = "Погода: Солнечно";

        if (questStates.stationHaveOxygen)
        {
            stationHave[1].text = "Кислород: Норма";
        }
        else stationHave[1].text = "Кислород: Критический уровень";

        if (questStates.stationHaveWater)
        {
            stationHave[2].text = "Выработка воды: Норма";
        }
        else stationHave[2].text = "Выработка воды: Прекращена";

        if (questStates.stationHaveElectro)
        {
            stationHave[3].text = "Электричество: Норма";
        }
        else stationHave[3].text = "Электричество: Критический уровень";

        if (questStates.stationHavePersonLife)
        {
            stationHave[4].text = "Состояние персонала: Норма";
        }
        else stationHave[4].text = "Состояние персонала: Погиб";
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
        for (int i = 0; i < logText.Length; i++)
        {
            logInputTextObj.text = logText[i];
            yield return new WaitForSeconds(Random.Range(0.1f, 2f));
            
        }
        logInputTextObj.text = "Начало нового дня";
        questStates.GoNextDay();
        yield return new WaitForSeconds(4f);

        analizeLogs = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].OpenPage(false);
            buttons[i].StopBlinking();
        }

        bar.fillAmount = fillLoading;

        CloseConsole();
    }
}
