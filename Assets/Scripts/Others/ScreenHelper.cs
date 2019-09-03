using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to make it easier to get the screen bounds in world units.
/// Use LeftSideOfScreen, RightSideOfScreen, TopSideOfScreen and DownSideOfScreen to the them.
/// </summary>
public class ScreenHelper : MonoBehaviour
{
	//Singleton
	public static ScreenHelper instance = null;

	//Bounds of the screen
	[HideInInspector] public float LeftSideOfScreen;
	[HideInInspector] public float RightSideOfScreen;
	[HideInInspector] public float TopSideOfScreen;
	[HideInInspector] public float DownSideOfScreen;

	void Awake()
	{
		//Singleton pattern
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(this);

		//Compute bounds
		computeVariables();
	}

	//Coumpute the bounds of the screen in world units
	void computeVariables()
	{
		//OrthographicSize is half of the screen height. By multiplying by the aspect ratio we can get bounds in world units.
		LeftSideOfScreen  = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
		RightSideOfScreen = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
		TopSideOfScreen   = Camera.main.transform.position.y + Camera.main.orthographicSize;
		DownSideOfScreen  = Camera.main.transform.position.y - Camera.main.orthographicSize;
	}

}
