using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

struct RandomWalker
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
        int r = UnityEngine.Random.Range(0,4);

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

public class Map
{
	// Internal storage
	private Tile[,] tiles;
	// prefab to use to create the walls - passed in through the constructor	
	private GameObject wall_prefab;
	// grouping for the walls
	private GameObject wall_group;
	
	// internal stuff.
	private int length;
	private int width;
	private float floor_y = 0;
	private float roof_y = 5;
	private float wall_height = 5;
	private float wall_thickness = 10;

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

            Debug.LogError("HERE!");

            while (steppedOnCount < 5)
            {
                if (map_array[walker.x, walker.y, wavelength] != 0)
                {
                    map_array[walker.x, walker.y, wavelength] = 0;
                    steppedOnCount++;
                }

                walker.Step();
            }

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
        }

        return map_array;
    }

	public Map(int[,,] map_array, GameObject wall, string wavelength) {
		length = map_array.GetLength(0);
		width = map_array.GetLength(1);
		tiles = new Tile[length, width];
		wall_prefab = wall;
		wall_group = new GameObject();
		wall_group.name = "Walls";
		
		BuildMap(map_array, wavelength);
	}
	
	public Map(int[][][] map_array, GameObject wall, string wavelength) {
		length = map_array.Length;
		width = map_array[0].Length;
		tiles = new Tile[length, width];
		wall_prefab = wall;
		
		wall_group = new GameObject();
		wall_group.name = "Walls";
		
		BuildMap(map_array, wavelength);
	}
	
	// builds the map using the passed arrays
	bool BuildMap(int[][][] source, string wavelength) {
		Debug.Log("This map of mine");
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				bool red = source[i][j][0] == 1 ? true : false;
				bool green = source[i][j][1] == 1 ? true : false;
				bool blue = source[i][j][2] == 1 ? true : false;
				SetTile(i, j, red, green, blue);
			}
		}
		
		//BuildWalls();
		//WavelengthWalls(wavelength);
		BuildFloor();
		BuildRoof();
		
		return true;
	}
	
	bool BuildMap(int[,,] source, string wavelength) {
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				bool red = source[i,j,0] == 1 ? true : false;
				bool green = source[i,j,1] == 1 ? true : false;
				bool blue = source[i,j,2] == 1 ? true : false;
				SetTile(i, j, red, green, blue);
			}
		}
		
		//BuildWalls();
		//WavelengthWalls(wavelength);
		BuildFloor();
		BuildRoof();
		
		return true;
	}
	
	public void WavelengthWalls(string wavelength)
	{
		ClearWalls();
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				Tile tile = tiles[i, j];
				if(tile.red && wavelength == "RED") {
					BuildWall("RED", "Environment",i,j,Color.red);
				}
				if(tile.green && wavelength == "GREEN") {
					BuildWall("GREEN", "Environment",i,j,Color.green);
				}
				if(tile.blue && wavelength == "BLUE") {
					BuildWall("BLUE", "Environment",i,j,Color.blue);
				}
			}
		}
	}
	
	void ClearWalls()
	{
		for(int i = 0; i < wall_group.transform.childCount; i++){
			GameObject child = wall_group.transform.GetChild(i).gameObject;
			GameObject.Destroy(child);
		}
	}
	
	// build walls from the tiles array
	public void BuildWalls()
	{
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				Tile tile = tiles[i, j];
				if(tile.red){
					BuildWall ("red wall", "Environment", i, j, Color.red);
				}
				if(tile.green){
					BuildWall ("green wall", "Environment", i, j, Color.green);
				}
				if(tile.blue){
					BuildWall ("blue wall", "Environment", i, j, Color.blue);
				}
			}
		}
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
		wall.renderer.material.color = color;
		wall.transform.localScale = new Vector3(
			wall_thickness,
			wall.transform.localScale.y * wall_height,
			wall_thickness
		);
		
		wall.transform.parent = wall_group.transform;
		
		return wall;
	}
	
	void BuildFloor() {
		BuildPlane("Floor", "Environment",
			new Vector3(0.0F, floor_y, 0.0f),
			new Vector3(length, 1.0F, width),
			Color.white
		);
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
		plane.renderer.material.color = color;
		
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
