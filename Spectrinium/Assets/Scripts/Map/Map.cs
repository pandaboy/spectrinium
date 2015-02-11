using UnityEngine;
using System;
using System.Collections;

// basic Map object.
public class Map
{	
	// Map Dimensions
	int length = 10, width = 10;
	
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
	
	// builds the floor using Cube Primitives
	public void BuildFloor() {
		floor  = GameObject.CreatePrimitive(PrimitiveType.Cube);
		floor.name = "Blue Floor!";
		floor.tag = "Environment";
		floor.transform.position = new Vector3(2.0F, 1.0F, 2.0F);
		floor.transform.localScale = new Vector3(length, floor.transform.localScale.y, width);
		floor.renderer.material.color = Color.blue;
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
