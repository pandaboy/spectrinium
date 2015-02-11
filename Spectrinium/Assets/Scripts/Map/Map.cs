using UnityEngine;
using System;
using System.Collections;

// struct class for each spot on the map grid
public struct Tile {
	private bool red_value;
	public bool red
	{
		get { return red_value; }
		set { red_value = value; }
	}
	
	private bool green_value;
	public bool green
	{
		get { return green_value; }
		set { green_value = value; }
	}
	
	private bool blue_value;
	public bool blue
	{
		get { return blue_value; }
		set { blue_value = value; }
	}
}

// basic 2D Map object.
public class Map
{	
	// Map Dimensions
	int length = 10, width = 10;
	
	// 2D array of tiles
	Tile[,] tiles;
	
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
	
	Tile GetTile(int x, int y) {
		return tiles[x,y];
	}
	
	bool SetTile(int x, int y, bool r, bool g, bool b) {
		tiles[x,y].red = r;
		tiles[x,y].green = g;
		tiles[x,y].blue = b;
		return true;
	}
	
	// convenience methods, could also just use getTile(x,y).red
	bool IsRedTile(int x, int y) {
		return GetTile(x,y).red;
	}
	
	// convenience methods, could also just use getTile(x,y).green
	bool IsGreenTile(int x, int y) {
		return GetTile(x,y).green;
	}
	
	// convenience methods, could also just use getTile(x,y).blue
	bool IsBlueTile(int x, int y) {
		return GetTile(x,y).blue;
	}
}
