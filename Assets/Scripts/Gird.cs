//ORG: Ghostyii & MoonLight Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gird
{
    static public int width = 16;
    static public int height = 16;

    static public int mineSum = 15;
    static public int remainUncoverableElementCount= width * height - mineSum;

    static public Element[,] elements = new Element[width, height];
    static private List<int> currentMineIndexList = new List<int>();
    static public List<int> CurrentMineIndexList
    {
        get
        {
            if (currentMineIndexList.Count > 0) return currentMineIndexList;
            else return GetRandomMineIndexList(mineSum);
        }
        set { currentMineIndexList = value; }
    }

    static public bool MineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
            return elements[x, y].isMine;

        return false;
    }

    static public int GetMineCount(int x, int y)
    {
        int count = 0;

        if (MineAt(x - 1, y - 1)) count++;
        if (MineAt(x, y - 1)) count++;
        if (MineAt(x + 1, y - 1)) count++;
        if (MineAt(x - 1, y)) count++;
        if (MineAt(x + 1, y)) count++;
        if (MineAt(x - 1, y + 1)) count++;
        if (MineAt(x, y + 1)) count++;
        if (MineAt(x + 1, y + 1)) count++;

        return count;
    }

    static public void UncoverAllMines(int clickID)
    {
        foreach (var item in elements)
        {
            if (item.isMine)
                item.UncoverMine(clickID == item.GetInstanceID());

            item.Selectable.enabled = false;
        }
    }

    static public void FFUncover(int x, int y, bool[,] visted)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (visted[x, y]) return;

            int count = GetMineCount(x, y);
            elements[x, y].UncoverNormal(count);

            if (count > 0) return;

            visted[x, y] = true;

            FFUncover(x, y - 1, visted);
            FFUncover(x - 1, y, visted);
            FFUncover(x + 1, y, visted);
            FFUncover(x, y + 1, visted);
        }
    }

    static public void DisenableAllElements()
    {
        foreach (var item in elements)
            item.Selectable.enabled = false;
    }

    static public List<int> GetRandomMineIndexList(int ms)
    {
        List<int> mineIndex = new List<int>();

        for (int i = 0; i < ms; i++)
        {
            int r = Random.Range(0, width * height);

            while (mineIndex.Contains(r))
                r = Random.Range(0, width * height);

            mineIndex.Add(r);
        }

        currentMineIndexList = mineIndex;

        return mineIndex;
    }
}

