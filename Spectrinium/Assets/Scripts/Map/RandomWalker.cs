using UnityEngine;
using System.Collections;

public class RandomWalker
{
    public int x, y;
    int mapWidth, mapHeight;

    public RandomWalker(int startX, int startY, int pMapWidth, int pMapHeight)
    {
        x = startX;
        y = startY;

        mapWidth = pMapWidth;
        mapHeight = pMapHeight;
    }

    public void Step()
    {
        int r = UnityEngine.Random.Range(0, 4);

        if (r == 0)
            x++;
        else if (r == 1)
            x--;
        else if (r == 2)
            y++;
        else
            y--;

        if (x < 0)
            x = 0;
        if (y < 0)
            y = 0;
        if (x >= mapWidth)
            x = mapWidth - 1;
        if (y >= mapHeight)
            y = mapHeight - 1;
    }
};