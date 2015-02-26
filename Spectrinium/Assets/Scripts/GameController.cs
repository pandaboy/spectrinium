using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    //public GUIText currentColor;

    public Text currentColor;
    public static GameController Instance;

	// available wavelengths
	public enum Wavelength { RED, GREEN, BLUE };
	
	// tracks the current wavelength
	private Wavelength currentWavelength;
	
	private Map map;
	public GameObject wall;

    public int mapDimensionX = 16;
    public int mapDimensionY = 16;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		// set the start wavelength
		currentWavelength = Wavelength.BLUE;
        map = new Map(Map.GenerateMapArray(mapDimensionX, mapDimensionY), wall);
		
        // spawn the player
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            if(nextWavelength())
                map.UpdateVisibleCollidable();

            Debug.Log("Current Wavelength: " + getCurrentWavelengthAsString());
            currentColor.text =  getCurrentWavelengthAsString();
        }
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            if(prevWavelength())
                map.UpdateVisibleCollidable();

            Debug.Log("Current Wavelength: " + getCurrentWavelengthAsString());
            currentColor.text = getCurrentWavelengthAsString();
       

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
	public bool nextWavelength()
	{
        if (Wall.CanSwitch(currentWavelength.Next().ToString()))
        {
            currentWavelength = currentWavelength.Next();
            return true;
        }
        else
        {
            return false;
        }
	}
	
	/**
	 * Moves the current wavelength one step back
	 */
	public bool prevWavelength()
	{
        if (Wall.CanSwitch(currentWavelength.Prev().ToString()))
        {
            currentWavelength = currentWavelength.Prev();
            return true;
        }
        else
        {
            return false;
        }
	}

}
