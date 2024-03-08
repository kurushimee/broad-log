using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleButtonScr : MonoBehaviour
{
    [SerializeField] Image frame;
    [SerializeField] GameObject chosed;
    [SerializeField] GameObject contentObj;
    public bool isGame;
    public bool isLogs;
    bool fadeIn = false;

    private void Update()
    {
        if (fadeIn)
        {
            var tempColor = frame.color;
            tempColor.a = Mathf.PingPong(Time.time*3f, 1f);
            frame.color = tempColor;
        }
    }

    public void StartBlinking()
    {
        fadeIn = true;
    }

    public void StopBlinking()
    {
        var tempColor = frame.color;
        tempColor.a = 1f;
        frame.color = tempColor;
        fadeIn = false;
    }

    public void OpenPage(bool bb)
    {
        chosed.SetActive(bb);
        contentObj.SetActive(bb);
    }
}
