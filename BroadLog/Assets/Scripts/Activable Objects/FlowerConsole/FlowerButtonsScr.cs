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

    public Image bar;
    public float fillLoading;

    public bool haveTrigger;
    public bool isTrigger;
    public int triggerPos = 3;

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
        triggerPos -= 1;
        Vector2 pos;
        switch (triggerPos)
        {
            case 1:
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y + 105f);
                triggerObj.transform.position = pos;
                break;
            case 2:
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y + 105f);
                triggerObj.transform.position = pos;
                break;
            case 3:
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y + 105f);
                triggerObj.transform.position = pos;
                break;
            default:
                triggerPos = 3;
                pos = new Vector2(triggerObj.transform.position.x, triggerObj.transform.position.y - 210f);
                triggerObj.transform.position = pos;
                break;
        }
    }
    public void StartLoadFill()
    {
        bar.fillAmount = 0f;
        StartCoroutine(LoadFill());
    }
    IEnumerator LoadFill()
    {
        while (bar.fillAmount < fillLoading)
        {
            bar.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
