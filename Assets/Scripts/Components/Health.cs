using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Component for objects to have health.
/// </summary>
public class Health : MonoBehaviour
{
	[SerializeField] private float startingHealth;

	private float currentHealth;
	private event Action OnDied;

	private void Awake()
	{
		currentHealth = startingHealth;
	}

	/// <summary>
	/// Add a callback for the OnDied event of Health.
	/// </summary>
	/// <param name="callback"></param>
	public void SetDyingCallback(Action callback)
	{
		OnDied += callback;
	}

	/// <summary>
	/// Make damage to an object with health. 
	/// </summary>
	/// <param name="amount">Amount of damage to make.</param>
	/// <returns>Returns true if object is dead. Returns false if not.</returns>
	public bool TakeDamage(float amount)
	{
		//Take damage and prevent negative health points
		currentHealth = Mathf.Max(currentHealth - amount, 0);

		if(currentHealth == 0)
		{
			Die();
			return true;
		}

		return false;
	}

	private void Die()
	{
		OnDied?.Invoke(); //Trigger OnDied event
	}
}
