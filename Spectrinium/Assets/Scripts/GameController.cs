using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	// available wavelengths
	private enum Wavelength { RED, GREEN, BLUE };
	
	// tracks the current wavelength
	private Wavelength currentWavelength;
	
	private Map map = new Map(3, 3);
	
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

	// Use this for initialization
	void Start () {
		currentWavelength = Wavelength.BLUE;
		Debug.Log ("Current Wavelength: " + getCurrentWavelengthAsString());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T)) {
			nextWavelength();
			Debug.Log("Current Wavelength: " + getCurrentWavelengthAsString());
		}
		if(Input.GetKeyDown(KeyCode.R)) {
			prevWavelength();
			Debug.Log("Current Wavelength: " + getCurrentWavelengthAsString());
		}
	}
}
