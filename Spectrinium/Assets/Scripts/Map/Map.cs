using UnityEngine;
using System;
using System.Collections;

// basic Map object.
public class Map
{
	public GameController controller;
	
	private int length;
	private int width;
	private float floor_y = 0;
	private float roof_y = 5;
	private float wall_height = 5;
	private float wall_thickness = 10;
	
	// 2D array of tiles
	private Tile[,] tiles;
	
	public Map(int[,,] map_array) {
		length = map_array.GetLength(0);
		width = map_array.GetLength(1);
		tiles = new Tile[length, width];
		
		BuildMap(map_array);
	}
	
	public Map(int[][][] map_array) {
		length = map_array.Length;
		width = map_array[0].Length;
		tiles = new Tile[length, width];
		
		BuildMap(map_array);
	}
	
	// builds the map using the passed arrays
	bool BuildMap(int[][][] source) {
		Debug.Log("This map of mine");
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				bool red = source[i][j][0] == 1 ? true : false;
				bool green = source[i][j][1] == 1 ? true : false;
				bool blue = source[i][j][2] == 1 ? true : false;
				SetTile(i, j, red, green, blue);
			}
		}
		BuildWalls();
		BuildFloor();
		BuildRoof();
		
		return true;
	}
	
	bool BuildMap(int[,,] source) {
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				bool red = source[i,j,0] == 1 ? true : false;
				bool green = source[i,j,1] == 1 ? true : false;
				bool blue = source[i,j,2] == 1 ? true : false;
				SetTile(i, j, red, green, blue);
			}
		}
		BuildWalls();
		BuildFloor();
		BuildRoof();
		
		return true;
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
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wall.name = name;
		wall.tag = tag;
		wall.renderer.material.color = color;
		float x_offset = x - (length/2);
		float z_offset = z - (width/2);
		wall.transform.position = new Vector3(
			x_offset * wall_thickness,
			floor_y + (wall_height/2),
			z_offset * wall_thickness
		);
		wall.transform.localScale = new Vector3(
			wall_thickness,
			wall.transform.localScale.y * wall_height,
			wall_thickness
		);
		
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
	
	// updates the map (switching from red to blue walls)
	public void Update() {
		
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
