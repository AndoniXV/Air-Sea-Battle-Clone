using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Base class that checks if the object goes out of the screen 
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public abstract class InScreenObject : MonoBehaviour
{
	protected SpriteRenderer sRenderer;
	protected event Action outOfScreenEvent;

	protected virtual void Awake()
	{
		sRenderer = GetComponent<SpriteRenderer>();
	}

	protected virtual void FixedUpdate()
	{
		//If the object is outside of the screen (right side), move it to the left part of the screen.
		if (IsOutsideOfScreen())
		{
			TriggerOutOfScreen();
		}
	}

	/// <summary>
	/// Set callback for when the object goes out of the screen.
	/// </summary>
	/// <param name="callback"></param>
	public void SetOutOfScreenCallback(Action callback)
	{
		outOfScreenEvent += callback;
	}

	//Check if the object is outside of the screen.
	protected abstract bool IsOutsideOfScreen();

	protected virtual void TriggerOutOfScreen()
	{
		outOfScreenEvent?.Invoke();
	}
}
