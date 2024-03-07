using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillGameCellScr : MonoBehaviour
{
    public bool isIce;
    
    Image thisImage;
    [SerializeField] Sprite defSpr;

    void Start()
    {
        thisImage = GetComponent<Image>();

    }

    private void Update()
    {
        if (!isIce)
        {
            thisImage.sprite = defSpr;
        }
    }
}
