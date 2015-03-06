using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Map
{
	// Internal storage
	private Tile[,] tiles;
	// prefab to use to create the walls - passed in through the constructor	
	private GameObject wall_prefab;
	
    // grouping for the walls
	private GameObject wall_group;
    public static GameObject red_group;
    public static GameObject blue_group;
    public static GameObject green_group;
    public static GameObject floor_group;
	
	// internal stuff.
	private int length;
	private int width;
	private float floor_y = 0;
	private float roof_y = 5;
	private float wall_height = 5;
	private float wall_thickness = 10;

    


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

            Debug.Log("HERE!");

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

	public Map(int[,,] map_array, GameObject wall)
    {
		length = map_array.GetLength(0);
		width = map_array.GetLength(1);
		tiles = new Tile[length, width];
		
        wall_prefab = wall;
		
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

    bool BuildMap(int[,,] source)
    {
        //Set the tiles using the source
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
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
        BuildRoof();

       

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
		for(int i = 0; i < length; i++)
        {
			for(int j = 0; j < width; j++)
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

                int layerID = GetTileNavLayerID(tile);

                if (wallObject != null)
                {
                    GameObjectUtility.SetNavMeshLayer(wallObject, layerID);
                }

          //      GameObject floorObject = BuildFloorTile(i, j);
         //       GameObjectUtility.SetNavMeshLayer(floorObject, layerID);


    
			}
		}
	}

    private int GetTileNavLayerID(Tile tile)
    {
  
        if (tile.red)
        {
            if (tile.green)
            {
                if (tile.blue)
                    return GameObjectUtility.GetNavMeshLayerFromName("RedGreenBlue");
                
                return GameObjectUtility.GetNavMeshLayerFromName("RedGreen");
            }

            if (tile.blue)
                return GameObjectUtility.GetNavMeshLayerFromName("RedBlue");
            
            return GameObjectUtility.GetNavMeshLayerFromName("Red");
        }
        if (tile.green)
        {
            if (tile.blue)
                return GameObjectUtility.GetNavMeshLayerFromName("GreenBlue");
           
            return GameObjectUtility.GetNavMeshLayerFromName("Green");
        }

        if (tile.blue)
            return GameObjectUtility.GetNavMeshLayerFromName("Blue");

        return GameObjectUtility.GetNavMeshLayerFromName("Space");

    }
	
	GameObject BuildWall(string name, string tag, int x, int z, Color color)
	{
		float x_offset = x - (length/2);
		float z_offset = z - (width/2);
		
        GameObject wall = GameObject.Instantiate(wall_prefab,new Vector3(
			x_offset * wall_thickness,
			floor_y + (wall_height/2),
			z_offset * wall_thickness
			), Quaternion.identity) as GameObject;
		
        wall.GetComponent<Renderer>().material.color = color;
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
			                                    new Vector3(length, 1.0F, width),
			                                    Color.white
		                                    );
        //make floor navagation static
        GameObjectUtility.SetStaticEditorFlags(floorObject, StaticEditorFlags.NavigationStatic);
        floorObject.transform.parent = floor_group.transform;
	}

    //build floor tile at position, and return tile
    GameObject BuildFloorTile(int i, int j)
    {
        GameObject floorObject = BuildPlane("Floor", "Environment",
                                                        new Vector3((i - length / 2) * 10 + 5, floor_y, (j - width / 2) * 10 + 5),
                                                        new Vector3(1.0f, 1.0F, 1.0f),
                                                        Color.white
                                                    );
        GameObjectUtility.SetStaticEditorFlags(floorObject, StaticEditorFlags.NavigationStatic);
        floorObject.transform.parent = floor_group.transform;
        return floorObject;
    }


    //build floor in tiles
    void BuildFloorTiles()
    {
        for(int i=0; i<length; i++)
            for (int j = 0; j < width; j++)
            {
                GameObject floorObject = BuildPlane("Floor", "Environment",
                                                        new Vector3((i - length/2)*10 + 5, floor_y, (j - width/2)*10 + 5),
                                                        new Vector3(1.0f, 1.0F, 1.0f),
                                                        Color.white
                                                    );
                //make floor navagation static
                GameObjectUtility.SetStaticEditorFlags(floorObject, StaticEditorFlags.NavigationStatic);
                floorObject.transform.parent = floor_group.transform;

                Tile tile = tiles[i, j];
                int layerID = GetTileNavLayerID(tile);

                GameObjectUtility.SetNavMeshLayer(floorObject, layerID);
            }
    }
	
	void BuildRoof() {
		BuildPlane(
			"Roof",
			"Environment",
			new Vector3(0.0F, roof_y, 0.0F),
			new Vector3(length, -1.0F, width), // flip the y-axis for the roof plane
			Color.white
		);
	}
	
	private GameObject BuildPlane(string name, string tag, Vector3 pos, Vector3 scale, Color color) {
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.name = name;
		plane.tag = tag;
		pos.x -= wall_thickness/2;
		pos.z -= wall_thickness/2;
		plane.transform.position = pos;
		plane.transform.localScale = new Vector3(
			plane.transform.localScale.x * scale.x,
			plane.transform.localScale.y * scale.y,
			plane.transform.localScale.z * scale.z
		);
		plane.GetComponent<Renderer>().material.color = color;
		
		return plane;
	}
	
	public Tile GetTile(int x, int y) {
		return tiles[x,y];
	}
	
	bool SetTile(int x, int y, bool r, bool g, bool b) {
		tiles[x,y].red = r;
		tiles[x,y].green = g;
		tiles[x,y].blue = b;
		return true;
	}
	
	// convenience methods, could also just use getTile(x,y).red
	public bool IsRedTile(int x, int y) {
		return GetTile(x,y).red;
	}
	
	// convenience methods, could also just use getTile(x,y).green
	public bool IsGreenTile(int x, int y) {
		return GetTile(x,y).green;
	}
	
	// convenience methods, could also just use getTile(x,y).blue
	public bool IsBlueTile(int x, int y) {
		return GetTile(x,y).blue;
	}
}
