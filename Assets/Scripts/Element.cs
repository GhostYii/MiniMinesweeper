//ORG: Ghostyii & MoonLight Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Element : MonoBehaviour
{
    public bool isMine = false;
    public Sprite[] emptySprites;
    public Sprite[] mineSprites;
    //public 

    private Button button = null;
    private Image image = null;

    private int girdX = 0, girdY = 0;    

    private void Start()
    {
        isMine = Random.value < 0.1f;
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        girdX = (int)((image.rectTransform.anchoredPosition.x - 10) / 20);
        girdY = (int)((image.rectTransform.anchoredPosition.y + 10) / -20);

        Gird.elements[girdX, girdY] = this;
    }

    public void UncoverMine(bool isClick)
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = isClick ? mineSprites[0] : mineSprites[1];

        button.enabled = false;
    }

    public void UncoverNormal(int mineCount)
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = emptySprites[mineCount];

        button.enabled = false;
    }

    public void ElementOnClick()
    {
        if (image == null) image = GetComponent<Image>();

        if (isMine)
        {
            Gird.UncoverAllMines(this.GetInstanceID());                   
        }
        else
        {
            Gird.FFUncover(girdX, girdY, new bool[Gird.width, Gird.height]);
        }
    }
}

