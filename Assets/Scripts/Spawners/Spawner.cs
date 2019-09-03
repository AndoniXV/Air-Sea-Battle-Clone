using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class to create a spawner. 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Spawner<T> : MonoBehaviour where T : Component
{
	protected int minObjectsToSpawn;
	protected int maxObjectsToSpawn;

	protected T objectToSpawn;

	protected ObjectPool<T> pool;

	protected void Awake()
	{
		Initialize();

		SpawnObjects();
	}

	/// <summary>
	/// Initialize the necessary properties like the pool.
	/// </summary>
	protected virtual void Initialize()
	{
		//Set prefab of the object pool
		if(pool == null)
		{
			throw new System.Exception("Initialize the pool to the pool type before using it!");
		}

		pool.SetPrefab(objectToSpawn);
		pool.AddObjects(maxObjectsToSpawn);
	}

	/// <summary>
	/// Spawn the objects. The number of objects to spawn will be between a given minObjectsToSpawn and maxObjectsToSpawn.
	/// </summary>
	public void SpawnObjects()
	{
		//Error handling
		if(minObjectsToSpawn > maxObjectsToSpawn)
		{
			throw new System.Exception("Value minObjectsToSpawn should be lower than maxObjectsToSpawn.");
		}

		//Compute the random number of objects to spawn between given min and max values
		int numObjectsToSpawn = Random.Range(minObjectsToSpawn, maxObjectsToSpawn + 1);

		ComputeObjectPositions(numObjectsToSpawn);
	}

	/// <summary>
	/// This method should spawn the objects with SpawnObject() in the desired positions (depending on the objects you want to spawn).
	/// </summary>
	/// <param name="numObjectsToSpawn">Number of objects you want to spawn.</param>
	protected abstract void ComputeObjectPositions(int numObjectsToSpawn);
	
	/// <summary>
	/// Spawn an object in the given position by using an object pool.
	/// </summary>
	/// <param name="position">Position to spawn the object.</param>
	protected virtual void SpawnObject(Vector2 position)
	{
		var newObject = pool.GetObject();
		newObject.gameObject.SetActive(true);
		newObject.transform.position = position;
	}
}
