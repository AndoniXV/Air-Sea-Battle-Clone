using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that checks if object goes out in the right or in the top side of the screen.
/// </summary>
public class FullInScreenObject : InScreenObject
{

	protected override bool IsOutsideOfScreen()
	{
		//Check if it goes out up or right
		return transform.position.x > ScreenHelper.instance.RightSideOfScreen + sRenderer.bounds.size.x / 2 ||
			transform.position.y > ScreenHelper.instance.TopSideOfScreen + sRenderer.bounds.size.y / 2;
	}
}
