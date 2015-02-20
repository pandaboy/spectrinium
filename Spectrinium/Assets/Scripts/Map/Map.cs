using UnityEngine;
using System;
using System.Collections;

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
		//BuildRoof();
		
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
		//BuildRoof();
		
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
