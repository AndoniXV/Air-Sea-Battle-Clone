using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
	private T prefab; //Prefab of the objects that will be used in the pool.
	protected Queue<T> objects = new Queue<T>(); //Queue of the objects

	/// <summary>
	/// Set the prefab of the objects that will be used in the pool.
	/// </summary>
	/// <param name="prefabToUse"></param>
	public void SetPrefab(T prefabToUse)
	{
		prefab = prefabToUse;
	}

	/// <summary>
	/// Get an object from the pool. Warning, the object will be at a random position and deactivated.
	/// </summary>
	/// <returns></returns>
    public virtual T GetObject()
	{
		if(objects.Count == 0)
		{
			AddObjects(1);
		}
		return objects.Dequeue();
	}

	/// <summary>
	/// Return an object to the pool to reuse later.
	/// </summary>
	/// <param name="returnedObject">Object to return.</param>
	public void ReturnToPool(T returnedObject)
	{
		returnedObject.gameObject.SetActive(false);
		objects.Enqueue(returnedObject);
	}

	/// <summary>
	/// Add an amount of objects to the pool. This amount should be an estimation of the number of objects that will be used, to prevent instatiation.
	/// </summary>
	/// <param name="amount">Amount of objects to add to the pool.</param>
	public virtual void AddObjects(int amount)
	{
		if (prefab == null)
		{
			throw new System.Exception("Please, set the prefab of the pool before adding objects to it.");
		}

		for (int i = 0; i < amount; i++)
		{
			var newObject = Instantiate(prefab);
			newObject.gameObject.SetActive(false);
			objects.Enqueue(newObject);

			newObject.GetComponent<IGameObjectPooled<T>>().Pool = this;
		}
	}
}
