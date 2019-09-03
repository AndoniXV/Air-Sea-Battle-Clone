using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Component to let objects make damage to other objects with Health component.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
	[SerializeField] private float damage = 0;

	private event Action objectCollidedEvent;

	/// <summary>
	/// Set callback for when the damager object collides with a damageable object (object with health in it).
	/// </summary>
	/// <param name="callback"></param>
	public void SetObjectCollidedCallback(Action callback)
	{
		objectCollidedEvent += callback;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// If the object has health, make damage to the object
		Health health = collision.transform.GetComponent<Health>();

		if (health)
		{
			//Make damage to the health component
			health.TakeDamage(damage);

			//Object collided callbacks
			OnObjectCollided();
		}
	}

	private void OnObjectCollided()
	{
		objectCollidedEvent?.Invoke(); //Trigger OnObjectCollided event
	}
}
