using UnityEngine;
using System.Collections;

public class RandomWalker
{
    public int x, y;
    int mapWidth, mapHeight;

    //int lastR; //the previous direction, used to bias it so it will create more corridors
    //int bias;

    public RandomWalker(int startX, int startY, int pMapWidth, int pMapHeight)
    {
        x = startX;
        y = startY;

        mapWidth = pMapWidth;
        mapHeight = pMapHeight;

        //lastR = UnityEngine.Random.Range(0, 4);
        //bias = 15;
    }

    public void Step()
    {
        int r;

        // 1/bias of the time just use the last direction
        //int useLast = UnityEngine.Random.Range(0, bias);
        //if (useLast == 0)
        //    r = lastR;
        //else
            r = UnityEngine.Random.Range(0, 4);

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