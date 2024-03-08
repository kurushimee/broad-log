using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerButtonsScr : MonoBehaviour
{
    [SerializeField] Image buttonCol;
    [SerializeField] Image triggerCol;
    [SerializeField] GameObject triggerObj;
    [SerializeField] Color selfColor;
    [SerializeField] Color choseColor;

    public bool haveTrigger;
    public bool isTrigger;
    [SerializeField] int triggerPos;

    public void SelectButton(bool sel)
    {
        if (sel)
        {
            if (isTrigger)
            {
                triggerCol.color = choseColor;
            }
            else buttonCol.color = choseColor;
        }
        else
        {
            if (isTrigger)
            {
                triggerCol.color = selfColor;
            }
            else buttonCol.color = selfColor;
        }
        
    }

    public void ChangeTrigger()
    {
        triggerPos += 1;
        Vector2 pos;
        switch (triggerPos)
        {
            case 0:
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y - 105f);
                triggerObj.transform.position = pos;
                break;
            case 1:
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y - 105f);
                triggerObj.transform.position = pos;
                break;
            case 2:
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y - 105f);
                triggerObj.transform.position = pos;
                break;
            case 3:
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y + 210f);
                triggerObj.transform.position = pos;
                triggerPos = 0;
                break;
        }
    }
}
