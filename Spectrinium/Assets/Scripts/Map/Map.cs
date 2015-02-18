using UnityEngine;
using System;
using System.Collections;

// basic Map object.
public class Map
{	
	// Map Dimensions
	int length = 3, width = 3;
	
	float floor_y = 0;
	private float roof_y = 5;
	float wall_height = 5;
	
	// 2D array of tiles
	Tile[,] tiles;
	
	// Floor gameObject
	GameObject floor;
	
	// TESTING
	// red test array
	bool[,] reds = new bool[,]{
		{false, false, false},
		{false, true, false},
		{false, false, false},
	};
	
	// greens test array
	bool[,] greens = new bool[,]{
		{false, false, true},
		{false, false, true},
		{false, true, true},
	};
	
	// blues test array
	bool[,] blues = new bool[,]{
		{false, false, false},
		{false, false, false},
		{true, true, false},
	};
	
	public Map() {
		tiles = new Tile[length, width];
	}
	
	public Map(int[,,] map_array) {
		length = map_array.GetLength(0);
		width = map_array.GetLength(1);
		tiles = new Tile[length, width];
		
		BuildMap(map_array);
	}
	
	public Map(int l, int w) {
		length = l;
		width = w;
		tiles = new Tile[length, width];
		Debug.Log ("width: " + width + ",length: " + length);
		BuildMap(reds, greens, blues);		
		Debug.Log ("value at [1,1]: (" + GetTile (1,1).red + "," + GetTile(1,1).green + "," + GetTile(1,1).blue + ")");
		Debug.Log ("Trying to build a floor");
		BuildFloor();
	}
	
	// builds the map using the passed arrays
	bool BuildMap(bool[,] reds, bool[,] greens, bool[,] blues) {
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				SetTile(i, j, reds[i,j], greens[i,j], blues[i,j]);
			}
		}
		
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
	void BuildWalls() {
		Debug.Log ("Trying to build just white walls with the red source walls");
		
		for(int i = 0; i < length; i++) {
			for(int j = 0; j < width; j++) {
				Tile tile = tiles[i, j];
				if(tile.red){
					GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
					wall.name = "Red Wall";
					wall.tag = "Environment";
					wall.transform.position = new Vector3(i, floor_y + (wall_height/2), j);
					wall.transform.localScale = new Vector3(1.0f, wall.transform.localScale.y * wall_height, 1.0f);
					wall.renderer.material.color = Color.red;
				}
				if(tile.green){
					GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
					wall.name = "Green Wall";
					wall.tag = "Environment";
					wall.transform.position = new Vector3(i, floor_y + (wall_height/2), j);
					wall.transform.localScale = new Vector3(1.0f, wall.transform.localScale.y * wall_height, 1.0f);
					wall.renderer.material.color = Color.green;
				}
				if(tile.blue){
					GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
					wall.name = "Blue Wall";
					wall.tag = "Environment";
					wall.transform.position = new Vector3(i, floor_y + (wall_height/2), j);
					wall.transform.localScale = new Vector3(1.0f, wall.transform.localScale.y * wall_height, 1.0f);
					wall.renderer.material.color = Color.blue;
				}
			}
		}
	}
	
	// builds the floor using Cube Primitives
	public void BuildFloor() {
		floor = BuildPlane("Floor", "Environment",
			new Vector3(0.0F, floor_y, 0.0f),
			new Vector3(width, 1.0F, length),
			Color.white
		);
	}
	
	// builds the floor using Cube Primitives
	public void BuildRoof() {
		GameObject roof = BuildPlane(
			"Roof",
			"Environment",
			new Vector3(0.0F, roof_y, 0.0F),
			new Vector3(width, -1.0F, length),
			Color.white
		);
	}
	
	private GameObject BuildPlane(string name, string tag, Vector3 pos, Vector3 scale, Color color) {
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.name = name;
		plane.tag = tag;
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
