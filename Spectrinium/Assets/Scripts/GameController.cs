using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

	// available wavelengths
	public enum Wavelength { RED, GREEN, BLUE };
	
	// tracks the current wavelength
	private Wavelength currentWavelength;
	
	private Map map;
	public GameObject wall;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		// set the start wavelength
		currentWavelength = Wavelength.BLUE;
        map = new Map(Map.GenerateMapArray(4,4), wall);
		
        // spawn the player
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.T)) {
            nextWavelength();
            map.UpdateVisibleCollidable();
            Debug.Log("Current Wavelength: " + getCurrentWavelengthAsString());
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            prevWavelength();
            map.UpdateVisibleCollidable();
            Debug.Log("Current Wavelength: " + getCurrentWavelengthAsString());
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
