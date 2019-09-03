using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Motor))]
[RequireComponent(typeof(FullInScreenObject))]
[RequireComponent(typeof(Damager))]
public class Bullet : MonoBehaviour, IGameObjectPooled<Bullet>
{
	private Motor motor;
	private FullInScreenObject inScreenObject;
	private Damager damager;

	private ObjectPool<Bullet> pool;
	public ObjectPool<Bullet> Pool
	{
		get { return pool; }
		set
		{
			if (pool == null)
			{
				pool = value;
			}
			else
			{
				throw new System.Exception("Pool should only be set once.");
			}
		}
	}

	private void Awake()
	{
		//Cache references
		motor = GetComponent<Motor>();
		inScreenObject = GetComponent<FullInScreenObject>();
		damager = GetComponent<Damager>();

		//Set callback to return object to pool when it goes out of screen
		inScreenObject.SetOutOfScreenCallback(returnBulletToPool);

		//Set callback to return object to pool when the bullet collides with a damagable object
		damager.SetObjectCollidedCallback(returnBulletToPool);
	}

	/// <summary>
	/// Use to initialize bullet direction and position properties. For example, after being instantiated.
	/// </summary>
	/// <param name="direction"></param>
	/// <param name="position"></param>
	public void Initialize(Vector2 direction, Vector2 position)
	{
		motor.ChangeDirection(direction);
		transform.position = position;
	}

	private void returnBulletToPool()
	{
		pool.ReturnToPool(this);
	}
}
