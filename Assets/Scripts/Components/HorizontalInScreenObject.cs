using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that checks if objects go out in the right part of the screen and puts them back in the left side.
/// </summary>
public class HorizontalInScreenObject : InScreenObject
{

	protected override void Awake()
	{
		base.Awake();

		outOfScreenEvent += ResetPosition;
	}

	protected override bool IsOutsideOfScreen()
	{
		//Check if it goes out in the right part
		return transform.position.x > ScreenHelper.instance.RightSideOfScreen + sRenderer.bounds.size.x / 2;
	}

	//Reset position of the object to the left part of the screen.
	private void ResetPosition()
	{
		transform.position = new Vector2(ScreenHelper.instance.LeftSideOfScreen - sRenderer.bounds.size.x / 2, transform.position.y);
	}
}
