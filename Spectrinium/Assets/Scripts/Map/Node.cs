using UnityEngine;
using System.Collections;

public class Node
{
    public float distance;
    public float h;

    public float cost;
    public float redCost;
    public float greenCost;
    public float blueCost;

    public Vector2 prev_node_pos;




    public Node()
    {
        distance = -1.0f;

        h = -1.0f;

        redCost = blueCost = greenCost = 1.0f;
    }



    public void setRed()
    {
        cost = redCost;
    }

    public void setBlue()
    {
        cost = blueCost;
    }

    public void setGreen()
    {
        cost = greenCost;
    }
}
