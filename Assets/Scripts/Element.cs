//ORG: Ghostyii & MoonLight Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Selectable))]
public class Element : MonoBehaviour, IPointerClickHandler
{
    public bool isMine = false;
    public Sprite[] emptySprites;
    public Sprite[] mineSprites;
    public Sprite[] uncoverSprites;

    [HideInInspector]
    public Sprite rawSprite;

    private Selectable selectable = null;
    private Image image = null;

    public bool IsUncover
    {
        get
        {
            if (selectable == null)
                selectable = GetComponent<Button>();

            return selectable.enabled;
        }
    }

    private int girdX = 0, girdY = 0;

    public Selectable Selectable { get { return selectable; } }

    public void Start()
    {
        selectable = GetComponent<Selectable>();
        image = GetComponent<Image>();
        rawSprite = image.sprite;

        girdX = (int)((image.rectTransform.anchoredPosition.x - 10) / 20);
        girdY = (int)((image.rectTransform.anchoredPosition.y + 10) / -20);
        int index = girdY * Gird.height + girdX;

        isMine = Gird.CurrentMineIndexList.Contains(index);

        Gird.elements[girdX, girdY] = this;
    }

    public void UncoverMine(bool isClick)
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = isClick ? mineSprites[0] : mineSprites[1];
        selectable.enabled = false;
    }

    public void UncoverNormal(int mineCount)
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = emptySprites[mineCount];

        if (IsUncover) Gird.remainUncoverableElementCount--;

        selectable.enabled = false;
    }

    public void ElementOnClickLeft()
    {
        if (image == null) image = GetComponent<Image>();

        if (isMine)
        {
            Gird.UncoverAllMines(this.GetInstanceID());
            GameManager.instance.GameOver();
        }
        else
        {
            Gird.FFUncover(girdX, girdY, new bool[Gird.width, Gird.height]);
        }

        if (Gird.remainUncoverableElementCount <= 0)
            GameManager.instance.GameWin();
    }

    public void ElementOnClickRight()
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = uncoverSprites[GetDisplaySpriteIndex()];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsUncover) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        { ElementOnClickLeft(); }
        else
        { ElementOnClickRight(); }

    }

    private int GetDisplaySpriteIndex()
    {
        if (image == null) image = GetComponent<Image>();

        for (int i = 0; i < uncoverSprites.Length; i++)
        {
            if (uncoverSprites[i].name == image.sprite.name)
                return i == uncoverSprites.Length - 1 ? 0 : ++i;
        }

        return 0;
    }
}

