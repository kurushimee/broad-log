using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColonyConsoleScr : MonoBehaviour
{
    [SerializeField] GameObject console;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject loadingWindow;
    [SerializeField] GameObject mainWindow;

    [SerializeField] ConsoleButtonScr[] buttons;

    public Image bar;
    private float fillLoading;

    private bool consoleIsOpen = false;
    private bool mainIsOpen = false;

    public int choseInput;

    private void Start()
    {
        buttons[choseInput].StartBlinking();
    }
    private void Update()
    {
        if (consoleIsOpen)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                CloseConsole();
            }

            bar.fillAmount = fillLoading;
        }

        if (mainIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttons[choseInput].StopBlinking();

                choseInput -= 1;
                if (choseInput < 0)
                    choseInput = 3;

                buttons[choseInput].StartBlinking();
            }
            
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttons[choseInput].StopBlinking();

                choseInput += 1;
                if (choseInput > 3)
                    choseInput = 0;

                buttons[choseInput].StartBlinking();
            }

            if (Input.GetKeyDown(KeyCode.Return)  || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                
                for(int i=0; i < buttons.Length; i++)
                {
                    buttons[i].OpenPage(false);
                }

                buttons[choseInput].OpenPage(true);
                
                if (choseInput == 3)
                {
                    buttons[3].OpenPage(false);
                    buttons[3].StopBlinking();
                    CloseConsole();
                }
                    
            }
        }
        
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
