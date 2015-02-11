using UnityEngine;
using System;
using System.Collections;

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
	int length = 10;
	int width = 10;
	
	Tile[,] tiles;
	
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
	
	Map() {
		tiles = new Tile[length, width];
	}
	
	Map(int l, int w) {
		length = l;
		width = w;
		tiles = new Tile[length, width];
	}
	
	Tile getTile(int x, int y) {
		return tiles[x][y];
	}
	
	// convenience methods, could also just use getTile(x,y).red
	bool isRedTile(int x, int y) {
		return getTile(x,y).red;
	}
	
	// convenience methods, could also just use getTile(x,y).green
	bool isGreenTile(int x, int y) {
		return getTile(x,y).green;
	}
	
	// convenience methods, could also just use getTile(x,y).blue
	bool isBlueTile(int x, int y) {
		return getTile(x,y).blue;
	}
}
