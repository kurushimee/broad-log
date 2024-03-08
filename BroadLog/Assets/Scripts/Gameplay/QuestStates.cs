using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestStates : MonoBehaviour
{
    [SerializeField] GameObject noteObj;
    [SerializeField] Text noteNameT;
    [SerializeField] Text noteDiscrT;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllWindows();
        }
    }

    public void SetDiscrToNote(string name, string dscr)
    {
        noteNameT.text = name;
        noteDiscrT.text = dscr;

        noteObj.SetActive(true);
    }

    void CloseAllWindows()
    {
        noteObj.SetActive(false);
    }

}
