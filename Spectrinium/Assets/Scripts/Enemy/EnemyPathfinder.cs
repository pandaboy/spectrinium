using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathfinder : MonoBehaviour
{

    public Vector2 goalNodePos;
    public Vector2 startingNodePos;
    public Vector2 nextNodePos;

    private Node[,] nodes;

    public bool useDijkstras = false;
    public bool useAStar = true;


    public bool allowDiagonals = false;
    public bool drawPath = true;

    private bool prevDiag;

    private int width, height;

    private bool[,] known;

    private List<Vector2> bestPath;

    private bool costsSet = false;

    public string wavelength;


    // Use this for initialization
    void Start()
    {
        nodes = Map.nodes;

        width = nodes.GetLength(0);
        height = nodes.GetLength(1);

        int i, j;

        //       setCosts();




        bestPath = new List<Vector2>();



        known = new bool[width, height];



        for (i = 0; i < width; ++i)
            for (j = 0; j < height; ++j)
                known[i, j] = false;

        prevDiag = allowDiagonals;


    }



    public void setCosts()
    {

        int i, j;
        if (wavelength == "Red")
        {
            for (i = 0; i < width; i++)
                for (j = 0; j < height; j++)
                    nodes[i, j].setRed();
        }
        else if (wavelength == "Green")
        {
            for (i = 0; i < width; i++)
                for (j = 0; j < height; j++)
                    nodes[i, j].setGreen();
        }
        else if (wavelength == "Blue")
        {
            for (i = 0; i < width; i++)
                for (j = 0; j < height; j++)
                    nodes[i, j].setBlue();
        }

        costsSet = true;
    }

    // Update is called once per frame
    public bool FindPath(Vector2 start, Vector2 end, ref Vector2 next)
    {

        getHeuristics();

        startingNodePos = start;
        goalNodePos = end;


        if (prevDiag != allowDiagonals)
        {
            getHeuristics();
            prevDiag = allowDiagonals;
        }


        int i, j;

        for (i = 0; i < width; ++i)
            for (j = 0; j < height; ++j)
                known[i, j] = false;

        bool found_path = false; ;
        if (useDijkstras)
            found_path = FindDijkstrasPath();
        else if (useAStar)
            found_path = FindAStarPath();



        if (found_path)
        {
            getBestPath();


            int path_length = bestPath.Count;
            int next_tile_num = Mathf.Max(0, path_length - 2);
            next = bestPath[next_tile_num];
            return true;
        }



        return false;

    }


    // Update is called once per frame
    public bool FindPath(Vector2 start, Vector2 end, ref Vector2 next, ref List<Vector2> path)
    {

        getHeuristics();

        startingNodePos = start;
        goalNodePos = end;


        if (prevDiag != allowDiagonals)
        {
            getHeuristics();
            prevDiag = allowDiagonals;
        }


        int i, j;

        for (i = 0; i < width; ++i)
            for (j = 0; j < height; ++j)
                known[i, j] = false;

        bool found_path = false; ;
        if (useDijkstras)
            found_path = FindDijkstrasPath();
        else if (useAStar)
            found_path = FindAStarPath();



        if (found_path)
        {
            getBestPath();


            int path_length = bestPath.Count;
            int next_tile_num = Mathf.Max(0, path_length - 2);
            next = bestPath[next_tile_num];
            path = bestPath;
            return true;
        }



        return false;

    }



    private bool checkExists(int i, int j)
    {
        if ((i < 0) || (j < 0))
            return false;

        if (i > width - 1)
            return false;
        if (j > height - 1)
            return false;

        return true;
    }

    private float nodeCost(Vector2 pos)
    {
        if (wavelength == "Red")
            return nodes[(int)pos.x, (int)pos.y].redCost;

        if (wavelength == "Green")
            return nodes[(int)pos.x, (int)pos.y].greenCost;


        return nodes[(int)pos.x, (int)pos.y].blueCost;
    }

    private float nodeDist(Vector2 pos)
    {
        Node curr_node = nodes[(int)pos.x, (int)pos.y];
        return curr_node.distance;
    }


    private bool getUnknownNeighbours(Vector2 pos, List<Vector2> neighbours)
    {
        if (neighbours.Count > 0)
            neighbours.Clear();


        int x = (int)pos.x;
        int y = (int)pos.y;

        int i, j, n_x, n_y;

        Vector2 neighbourCoord;

        for (i = -1; i < 2; ++i)
        {
            n_x = x + i;

            for (j = -1; j < 2; ++j)
            {
                if ((i == 0) && (j == 0))
                    continue;

                if (!allowDiagonals)
                    if ((i != 0) && (j != 0))
                        continue;

                n_y = y + j;

                if (!checkExists(n_x, n_y))
                    continue;

                if (known[n_x, n_y])
                    continue;

                neighbourCoord = new Vector2(n_x, n_y);
                if (nodeCost(neighbourCoord) != 1)
                    continue;

                neighbours.Add(neighbourCoord);
            }
        }

        if (neighbours.Count == 0)
            return false;

        return true;
    }

    private void setKnown(Vector2 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;

        known[x, y] = true;
    }

    private float getNodeDistance(Vector2 old_pos, Vector2 new_pos)
    {
        Vector2 difference = new Vector2(Mathf.Abs(new_pos.x - old_pos.x), Mathf.Abs(new_pos.y - old_pos.y));

        float diff_mag = 1.0f;
        if ((difference.x != 0) && (difference.y != 0))
            diff_mag = 1.4f;


        float dist = (nodeDist(old_pos) + nodeCost(new_pos)) * diff_mag;
        return dist;
    }

    private void setNodeDist(Vector2 pos, float dist)
    {
        nodes[(int)pos.x, (int)pos.y].distance = dist;
    }

    private bool checkGoalKnown()
    {
        if (known[(int)goalNodePos.x, (int)goalNodePos.y])
            return true;

        return false;
    }


    private bool getLeastDistNode(ref Vector2 best_node)
    {
        int i, j;

        float shortest_dist = -1;

        float curr_dist;

        for (i = 0; i < width; i++)
            for (j = 0; j < height; j++)
                if (!known[i, j])
                {
                    curr_dist = nodes[i, j].distance;

                    if (curr_dist >= 0)
                        if ((shortest_dist < 0.0f) || (curr_dist < shortest_dist))
                        {
                            shortest_dist = curr_dist;
                            best_node = new Vector2(i, j);
                        }
                }

        if (shortest_dist < 0)
            return false;

        return true;
    }

    private bool getLeastFNode(ref Vector2 best_node)
    {
        int i, j;

        float smallest_f = -1;

        float curr_dist, curr_f;

        for (i = 0; i < width; i++)
            for (j = 0; j < height; j++)
                if (!known[i, j])
                {
                    curr_dist = nodes[i, j].distance;

                    if (curr_dist >= 0)
                    {
                        curr_f = curr_dist + nodes[i, j].h;

                        if ((smallest_f < 0) || (curr_f < smallest_f))
                        {
                            smallest_f = curr_dist;
                            best_node = new Vector2(i, j);
                        }
                    }
                }

        if (smallest_f < 0)
            return false;

        return true;
    }

    private void setNodePrev(Vector2 node, Vector2 prev_node)
    {
        nodes[(int)node.x, (int)node.y].prev_node_pos = prev_node;
    }

    private void getBestPath()
    {
        bestPath.Clear();

        Vector2 curr_pos = goalNodePos;
        Vector2 default_pos = new Vector2(-1, -1);

        while (curr_pos != default_pos)
        {
            bestPath.Add(curr_pos);

            curr_pos = nodes[(int)curr_pos.x, (int)curr_pos.y].prev_node_pos;
        }
    }

    private bool FindDijkstrasPath()
    {
        List<Vector2> neighbours = new List<Vector2>();

        int i, j;
        float dist, new_dist;
        Vector2 curr_neigh;

        for (i = 0; i < width; ++i)
            for (j = 0; j < height; ++j)
            {
                nodes[i, j].distance = -1;
                nodes[i, j].prev_node_pos = new Vector2(-1, -1);
            }

        Vector2 curr_node_pos = startingNodePos;
        setNodeDist(curr_node_pos, nodeCost(curr_node_pos));



        while (true)
        {
            setKnown(curr_node_pos);

            if (checkGoalKnown())
                return true;

            getUnknownNeighbours(curr_node_pos, neighbours);


            for (i = 0; i < neighbours.Count; ++i)
            {
                curr_neigh = neighbours[i];

                dist = nodeDist(curr_neigh);
                new_dist = getNodeDistance(curr_node_pos, curr_neigh);

                if ((dist < 0) || (new_dist < dist))
                {
                    setNodeDist(curr_neigh, new_dist);
                    setNodePrev(curr_neigh, curr_node_pos);
                }
            }


            if (!getLeastDistNode(ref curr_node_pos))
                break;
        }

        return false;
    }

    private bool FindAStarPath()
    {
        int i, j;
        for (i = 0; i < width; ++i)
            for (j = 0; j < height; ++j)
            {
                nodes[i, j].distance = -1;
                nodes[i, j].prev_node_pos = new Vector2(-1, -1);
            }

        Vector2 curr_node_pos = startingNodePos;
        setNodeDist(curr_node_pos, nodeCost(curr_node_pos));

        List<Vector2> neighbours = new List<Vector2>();
        Vector2 curr_neigh;

        float dist, new_dist;

        while (true)
        {
            setKnown(curr_node_pos);


            if (checkGoalKnown())
                return true;

            getUnknownNeighbours(curr_node_pos, neighbours);


            for (i = 0; i < neighbours.Count; ++i)
            {
                curr_neigh = neighbours[i];

                dist = nodeDist(curr_neigh);
                new_dist = getNodeDistance(curr_node_pos, curr_neigh);

                if ((dist < 0) || (new_dist < dist))
                {
                    setNodeDist(curr_neigh, new_dist);
                    setNodePrev(curr_neigh, curr_node_pos);
                }
            }
            if (!getLeastFNode(ref curr_node_pos))
                break;
        }

        return false;
    }

    private void getHeuristics()
    {
        int i, j;

        for (i = 0; i < width; ++i)
            for (j = 0; j < height; ++j)
                if (!allowDiagonals)
                    nodes[i, j].h = getManhattanDistance(new Vector2(i, j));
                else
                    nodes[i, j].h = getFancyDiagonalDistance(new Vector2(i, j));
    }

    private float getManhattanDistance(Vector2 pos)
    {
        return Mathf.Abs((int)pos.x - (int)goalNodePos.x) + Mathf.Abs((int)pos.y - (int)goalNodePos.y);
    }

    private float getSimpleDiagonalDistance(Vector2 pos)
    {
        return Mathf.Max(Mathf.Abs(pos.x - goalNodePos.x), Mathf.Abs(pos.y - goalNodePos.y));
    }

    private float getFancyDiagonalDistance(Vector2 pos)
    {
        float h_diag = Mathf.Min(Mathf.Abs(pos.x - goalNodePos.x), Mathf.Abs(pos.y - goalNodePos.y));
        float h_straight = Mathf.Abs(pos.x - goalNodePos.x) + Mathf.Abs(pos.y - goalNodePos.y);

        float h = 1.4f * h_diag + h_straight - 2.0f * h_diag;

        return h;
    }




}
