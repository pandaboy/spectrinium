using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class Map
{
	// Internal storage
	public static Tile[,] tiles;

	// prefabs to use to create the walls and floor - passed in through the constructor
	private GameObject red_wall_prefab;
	private GameObject green_wall_prefab;
	private GameObject blue_wall_prefab;
	private GameObject floor_prefab;
	
    // grouping for the walls
	private GameObject wall_group;
    public static GameObject red_group;
    public static GameObject blue_group;
    public static GameObject green_group;
    public static GameObject floor_group;
	
	// internal stuff.
    private static int width;
    private static int height;

	private float floor_y = 0;
	private float roof_y = 5;
	private float wall_height = 12;
    private static float wall_thickness = 10;

    public static Node[,] nodes;
	
	public Map(int[,,] map_array, GameObject red_wall, GameObject green_wall, GameObject blue_wall, GameObject floor)
	{
        width = map_array.GetLength(0);
		height = map_array.GetLength(1);

        tiles = new Tile[width, height];
        nodes = new Node[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                nodes[i, j] = new Node();
		
		red_wall_prefab = red_wall;
		green_wall_prefab = green_wall;
		blue_wall_prefab = blue_wall;
		floor_prefab = floor;
		
		wall_group = new GameObject();
		wall_group.name = "Walls";
		
		red_group = new GameObject();
		red_group.name = "Red";
		red_group.transform.parent = wall_group.transform;
		
		green_group = new GameObject();
		green_group.name = "Green";
		green_group.transform.parent = wall_group.transform;
		
		blue_group = new GameObject();
		blue_group.name = "Blue";
		blue_group.transform.parent = wall_group.transform;
		
		floor_group = new GameObject();
		floor_group.name = "Floors";
		
		BuildMap(map_array);
	}

    //This function uses a RandomWalker to generate a 3d array (x,y,colour) to be used by the map
    public static int[, ,] GenerateMapArray(int width, int height)
    {
        int[, ,] map_array = new int[width, height, 3];

        for (int wavelength = 0; wavelength < 3; wavelength++)
        {
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    map_array[w, h, wavelength] = 1;

            RandomWalker walker = new RandomWalker(width / 2, height / 2, width, height);
            int steppedOnCount = 0;

            while (steppedOnCount < (width * height)/3)
            {
                if (map_array[walker.x, walker.y, wavelength] != 0)
                {
                    map_array[walker.x, walker.y, wavelength] = 0;
                    steppedOnCount++;
                }
                //else
                //{
                //    map_array[walker.x, walker.y, wavelength] = 1;
                //    steppedOnCount--;
                //}

                walker.Step();
            }

            #region SECOND PASS
            //SECOND PASS
            //List<Vector2> SetToZero = new List<Vector2>();

            //for (int w = 1; w < width - 1; w++)
            //{
            //    for (int h = 1; h < height - 1; h++)
            //    {
            //        if (map_array[w, h, wavelength] == 1)
            //        {
            //            int neighbours = 0;

            //            for (int i = -1; i <= 1; i++)
            //                for (int j = -1; j <= 1; j++)
            //                    if (map_array[w + i, h + j, wavelength] == 1)
            //                        neighbours++;

            //            if (neighbours <= 1)
            //                SetToZero.Add(new Vector2(w, h));
            //        }
            //    }
            //}

            //for (int i = 0; i < SetToZero.Count; i++)
            //    map_array[(int)SetToZero[i].x, (int)SetToZero[i].y, wavelength] = 0;
            #endregion
        }

        return map_array;
    }

    bool BuildMap(int[,,] source)
    {
        //Set the tiles using the source
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                bool red = source[i, j, 0] == 1 ? true : false;
                bool green = source[i, j, 1] == 1 ? true : false;
                bool blue = source[i, j, 2] == 1 ? true : false;
                SetTile(i, j, red, green, blue);
            }
        }

        //Build the walls (blocks) given said tile map
        BuildWalls();

        //Set the current wavelength
        UpdateVisibleCollidable();
        
        BuildFloorTiles();
        //BuildRoof();

        return true;
    }

    #region ALTERNATE
    //public Map(int[][][] map_array, GameObject wall) 
    //{
    //    length = map_array.Length;
    //    width = map_array[0].Length;
    //    tiles = new Tile[length, width];
		
    //    wall_prefab = wall;
		
    //    wall_group = new GameObject();
    //    wall_group.name = "Walls";
		
    //    BuildMap(map_array);
    //}

    //// builds the map using the passed arrays
    //bool BuildMap(int[][][] source) 
    //{
    //    for(int i = 0; i < length; i++) {
    //        for(int j = 0; j < width; j++) {
    //            bool red = source[i][j][0] == 1 ? true : false;
    //            bool green = source[i][j][1] == 1 ? true : false;
    //            bool blue = source[i][j][2] == 1 ? true : false;
    //            SetTile(i, j, red, green, blue);
    //        }
    //    }
		
    //    //BuildWalls();
    //    //WavelengthWalls(wavelength);
    //    BuildFloor();
    //    BuildRoof();
		
    //    return true;
    //}
    #endregion

    public void UpdateVisibleCollidable()
    {
        switch (GameController.Instance.getCurrentWavelengthAsString())
        {
            case "RED":
                WavelengthWalls(red_group.transform);
                break;
            case "GREEN":
                WavelengthWalls(green_group.transform);
                break;
            case "BLUE":
                WavelengthWalls(blue_group.transform);
                break;
        }
    }

    public void WavelengthWalls(Transform wavelengthGroup)
	{
        ClearWalls();

        foreach (Transform child in wavelengthGroup)
        {
            child.transform.GetComponent<Renderer>().enabled = true;
            child.transform.GetComponent<Collider>().isTrigger = false;
        }
	}

    public void ClearWalls()
    {
        foreach (Transform child in wall_group.transform)
        {
            foreach (Transform grandchild in child)
            {
                grandchild.transform.GetComponent<Renderer>().enabled = false;
                grandchild.transform.GetComponent<Collider>().isTrigger = true;
            }
        }
    }

    // build walls from the tiles array
	public void BuildWalls()
	{
		for(int i = 0; i < width; i++)
        {
			for(int j = 0; j < height; j++)
            {
				Tile tile = tiles[i, j];
                GameObject wallObject = null;

				if(tile.red){
					wallObject = BuildWall ("red wall", "Environment", i, j, Color.red);
				}
				if(tile.green){
                    wallObject = BuildWall("green wall", "Environment", i, j, Color.green);
				}
				if(tile.blue){
                    wallObject = BuildWall("blue wall", "Environment", i, j, Color.blue);
				}

			}
		}
	}

    private int GetTileNavLayerID(Tile tile, ref Node node)
    {

        if (tile.red)
        {
            if (tile.green)
            {
                if (tile.blue)
                {
                    node.redCost = 99999.0f;
                    node.greenCost = 99999.0f;
                    node.blueCost = 99999.0f;

                    return NavAreas.RedGreenBlue;
                }
                node.redCost = 99999.0f;
                node.greenCost = 99999.0f;

                return NavAreas.RedGreen;
            }

            if (tile.blue)
            {
                node.redCost = 99999.0f;
                node.blueCost = 99999.0f;

                return NavAreas.RedBlue;
            }
            node.redCost = 99999.0f;

            return NavAreas.Red;
        }
        if (tile.green)
        {
            if (tile.blue)
            {
                node.greenCost = 99999.0f;
                node.blueCost = 99999.0f;

                return NavAreas.GreenBlue;
            }
            node.greenCost = 99999.0f;

            return NavAreas.Green;
        }

        if (tile.blue)
        {
            node.blueCost = 99999.0f;

            return NavAreas.Blue;
        }

        return NavAreas.Space;
    }
	
	GameObject BuildWall(string name, string tag, int x, int z, Color color)
	{
		float x_offset = x - (width/2);
		float z_offset = z - (height/2);

		GameObject wall_prefab = null;

		if(color == Color.red) {
			wall_prefab = red_wall_prefab;
		} else if (color == Color.green) {
			wall_prefab = green_wall_prefab;
		} else {
			wall_prefab = blue_wall_prefab;
		}

        GameObject wall = GameObject.Instantiate(wall_prefab,new Vector3(
			x_offset * wall_thickness,
			floor_y + (wall_height/2),
			z_offset * wall_thickness
			), Quaternion.identity) as GameObject;
		
		wall.transform.localScale = new Vector3(
			wall_thickness,
			wall.transform.localScale.y * wall_height,
			wall_thickness
		);

        wall.GetComponent<Renderer>().enabled = false;
        wall.GetComponent<Collider>().isTrigger = true;

        if (color == Color.red)
        {
            wall.transform.parent = red_group.transform;
            wall.layer = LayerMask.NameToLayer("Red");
        }
        else if (color == Color.green)
        {
            wall.transform.parent = green_group.transform;
            wall.layer = LayerMask.NameToLayer("Green");
        }
        else
        {
            wall.transform.parent = blue_group.transform;
            wall.layer = LayerMask.NameToLayer("Blue");
        }
        
		return wall;
	}
	
    //build floor as one plane
	void BuildFloor() {
		GameObject floorObject = BuildPlane(    "Floor", "Environment",
			                                    new Vector3(0.0F, floor_y, 0.0f),
			                                    new Vector3(width, 1.0f, height),
			                                    Color.white
		                                    );
  
        floorObject.transform.parent = floor_group.transform;
	}

    //build floor tile at position, and return tile
    GameObject BuildFloorTile(int i, int j)
    {
        GameObject floorObject = BuildPlane(
            "Floor", // object name
            "Environment", // object tag
            new Vector3((i - width / 2) * 10 + 5, floor_y, (j - height / 2) * 10 + 5),
            new Vector3(1.0f, 1.0F, 1.0f),
            Color.white
        );

        floorObject.transform.parent = floor_group.transform;
        return floorObject;
    }


    //build floor in tiles
    void BuildFloorTiles()
    {
        for(int i=0; i<width; i++)
            for (int j = 0; j < height; j++)
            {
                GameObject floorObject = BuildPlane(
                    "Floor",
                    "Environment",
                    new Vector3((i - width / 2) * 10 + 5, floor_y, (j - height / 2) * 10 + 5),
                    new Vector3(1.0f, 1.0f, 1.0f),
                    Color.white
                );

                tiles[i, j].worldPos = floorObject.transform.position;

                floorObject.transform.parent = floor_group.transform;

                Tile tile = tiles[i, j];
                int layerID = GetTileNavLayerID(tile, ref nodes[i, j]);
                tile.navArea = layerID;

                TileArea t = floorObject.GetComponent<TileArea>();
                t.navArea = layerID;
            }
    }
	
	void BuildRoof()
    {
		BuildPlane(
			"Roof",
			"Environment",
			new Vector3(0.0F, roof_y, 0.0F),
			new Vector3(width, -1.0F, height), // flip the y-axis for the roof plane
			Color.white
		);
	}

	private GameObject BuildPlane(string name, string tag, Vector3 pos, Vector3 scale, Color color)
    {
		pos.x -= wall_thickness/2;
		pos.z -= wall_thickness/2;
        
		GameObject plane = GameObject.Instantiate(floor_prefab, pos, Quaternion.identity) as GameObject;
		plane.name = name;
		plane.tag = tag;
		plane.transform.localScale = new Vector3(
			plane.transform.localScale.x * scale.x,
			plane.transform.localScale.y * scale.y,
			plane.transform.localScale.z * scale.z
		);
		
		return plane;
	}
	
	public Tile GetTile(int x, int y)
    {
		return tiles[x,y];
	}
	
	bool SetTile(int x, int y, bool r, bool g, bool b)
    {
		tiles[x,y].red = r;
		tiles[x,y].green = g;
		tiles[x,y].blue = b;
		return true;
	}
	
	// convenience methods, could also just use getTile(x,y).red
	public bool IsRedTile(int x, int y)
    {
		return GetTile(x,y).red;
	}
	
	// convenience methods, could also just use getTile(x,y).green
	public bool IsGreenTile(int x, int y)
    {
		return GetTile(x,y).green;
	}
	
	// convenience methods, could also just use getTile(x,y).blue
	public bool IsBlueTile(int x, int y)
    {
		return GetTile(x,y).blue;
	}


    public static Vector2 getClosestTileCoord(Vector3 pos)
    {
        float f_i = 0.1f * (pos.x - 5 + (0.5f * wall_thickness)) + (0.5f * width);
        float f_j = 0.1f * (pos.z - 5 + (0.5f * wall_thickness)) + (0.5f * height);



        int i = Convert.ToInt32(f_i);
        int j = Convert.ToInt32(f_j);

        return new Vector2(i, j);
    }
}
