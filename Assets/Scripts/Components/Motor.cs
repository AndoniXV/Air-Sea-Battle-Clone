using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that make the objects move.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] private float speed;		//Speed of the motor
	[SerializeField] private Vector2 direction; //Direction to where the motor is heading. Should be normalized.

	protected Rigidbody2D rb;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	protected virtual void FixedUpdate()
	{
		move();
	}

	protected virtual void move()
	{
		rb.position = rb.position + speed * direction * Time.fixedDeltaTime;
	}

	/// <summary>
	/// Change speed of the motor by a given float.
	/// </summary>
	/// <param name="amount"></param>
	public void ChangeSpeed(float amount)
	{
		speed = amount;
	}

	/// <summary>
	/// Change direction of the motor. It will be normalized.
	/// </summary>
	/// <param name="newDirection"></param>
	public void ChangeDirection(Vector2 newDirection)
	{
		//Normalize direction to prevent errors
		direction = newDirection.normalized;
	}
}
