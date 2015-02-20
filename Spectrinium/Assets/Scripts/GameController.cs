using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	// available wavelengths
	private enum Wavelength { RED, GREEN, BLUE };
	
	// tracks the current wavelength
	private Wavelength currentWavelength;
	
	private Map map;
	public GameObject wall;

	// Use this for initialization
	void Start () {
		// set the start wavelength
		currentWavelength = Wavelength.BLUE;
		
		// Example of an auto gen level
		int[,,] auto = new int[10,10,3];
		// build a gigantic map - this is a really poor map
		for(int i = 0; i < 10; i++) {
			for(int j = 0; j < 10; j++) {
				for(int k = 0; k < 3; k++){
					if(k == 0) {
						auto[i,j,k] = (j % 2 == 0 && i % 2 == 0) ? 1 : 0;
					}
					if(k == 1) {
						auto[i,j,k] = (j % 7 == 0 && i % 7 == 0) ? 1 : 0;
					}
					if(k == 2) {
						auto[i,j,k] = (j % 5 == 0 && i % 5 == 0) ? 1 : 0;
					}
				}
			}
		}
		
		// create the map
		//map = new Map(EXAMPLES.map_source, wall, getCurrentWavelengthAsString());
		//map = new Map(EXAMPLES.extents, wall, getCurrentWavelengthAsString());
		//map = new Map(EXAMPLES.big_extents, wall, getCurrentWavelengthAsString());
		//map = new Map(EXAMPLES.simple, wall, getCurrentWavelengthAsString());
		//map = new Map(EXAMPLES.jagged_extents, wall, getCurrentWavelengthAsString());
		map = new Map(auto, wall, getCurrentWavelengthAsString());
		
		// spawn the player
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T)) {
			nextWavelength();
			map.WavelengthWalls(getCurrentWavelengthAsString());
		}
		if(Input.GetKeyDown(KeyCode.R)) {
			prevWavelength();
			map.WavelengthWalls(getCurrentWavelengthAsString());
		}
	}
	
	/**
	 * Returns the current wavelength's int value
	 */
	public int getCurrentWavelength()
	{
		return (int)currentWavelength;
	}
	
	/**
	 * Returns the current wavelength as a string e.g. "RED"
	 */
	public string getCurrentWavelengthAsString()
	{
		return currentWavelength.ToString();
	}
	
	/**
	 * Moves the current wavelength one step forward.
	 */
	public void nextWavelength()
	{
		currentWavelength = currentWavelength.Next();
	}
	
	/**
	 * Moves the current wavelength one step back
	 */
	public void prevWavelength()
	{
		currentWavelength = currentWavelength.Prev();
	}
}
