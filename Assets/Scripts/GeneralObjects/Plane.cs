using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Motor))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HorizontalInScreenObject))]
public class Plane : MonoBehaviour, IGameObjectPooled<Plane>
{
	[SerializeField] private AudioClip explosionSound;

	private Motor motor;					//Component that moves the plane
	private Health health;                  //Component that manages the health of the plane

	private PlaneSpawner spawner;
	public PlaneSpawner Spawner
	{
		get { return spawner; }
		set { spawner = value; }
	}

	private ObjectPool<Plane> pool;			//Reference of the pool that the plane belongs to
	public ObjectPool<Plane> Pool			//Property of the pool
	{
		get { return pool; }
		set
		{
			//Only set pool once.
			if(pool == null)
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
		//Cache components that will be used
		motor = GetComponent<Motor>();
		health = GetComponent<Health>();

		//Set callback for when the health reaches 0
		health.SetDyingCallback(Die); 
	}

	private void Die()
	{
		AudioManager.Instance.PlayAudio(explosionSound);	//Play explosion sound
		spawner.PlaneKilled();								//Tell the plane spawner and the manager there is one plane alive less
		GameManager.instance.PlaneKilled();
		pool.ReturnToPool(this);							//Return object to the object pool
	}
}
