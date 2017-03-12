//ORG: Ghostyii & MoonLight Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gird
{
    static public int width = 16;
    static public int height = 16;

    static public Element[,] elements = new Element[width, height];

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
        if (MineAt(x + 1, y + 1)) count++;
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
            if (item.isMine)
                item.UncoverMine(clickID == item.GetInstanceID());
    }

    static public void FFUncover(int x,int y,bool[,] visted)
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
}

