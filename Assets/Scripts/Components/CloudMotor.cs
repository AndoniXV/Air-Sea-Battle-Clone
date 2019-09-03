using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMotor : Motor
{
	[SerializeField] [Range(0,0.01f)] private float sinMultiplier;

	private Vector3 startPosition;
	private float randomDifference; //Random value to make every cloud move at a different sinusoidal time

	protected override void Awake()
	{
		base.Awake();
		startPosition = transform.position;
		randomDifference = Random.Range(0, 20f);
	}

	//Make the clouds move with motor speed first, and the add a sinusoidal movement
	protected override void move()
	{
		base.move();
		rb.position = rb.position + new Vector2(0.0f, Mathf.Sin(Time.time + randomDifference) * sinMultiplier);
	}
}
