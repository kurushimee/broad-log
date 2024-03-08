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

    [SerializeField] ConsoleButtonScr[] buttons;

    public Image bar;
    private float fillLoading;

    private bool consoleIsOpen;
    private bool mainIsOpen;
    [SerializeField] bool playingGame;

    [SerializeField] GameObject drillGameGrid;
    [SerializeField] GameObject drillGameObj;
    [SerializeField] InputField drillInputObj;

    int drillDirection;
    [SerializeField] int drillX = 11;
    [SerializeField] int drillY = 11;
    string drillInputText;

    public int choseInput;

    private void Start()
    {
        buttons[choseInput].StartBlinking();
    }
    private void Update()
    {
        if (consoleIsOpen)
        {
            if (playingGame)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    playingGame = false;
                    
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
                    buttons[buttons.Length - 1].OpenPage(false);
                    buttons[buttons.Length - 1].StopBlinking();
                    CloseConsole();
                }

                bar.fillAmount = fillLoading;
            }
        }

        if (mainIsOpen)
        {
            if (!playingGame)
            {
                SelectMenu();
            }
            else
            {
                PlayDrillGame();
                drillInputObj.ActivateInputField();
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
                    //Debug.Log("рср к╗д!");
                    drillCell.isIce = false;
                }
                
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
    }

    public void ReadDrillInput(string st)
    {
        drillInputText = st;
        //Debug.Log(st);
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
}
