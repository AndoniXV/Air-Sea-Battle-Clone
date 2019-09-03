using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : Spawner<Plane>
{
	[Header("Values")]
	[SerializeField] [Range(1, 7)] private int minPlanesToSpawn;
	[SerializeField] [Range(1, 7)] private int maxPlanesToSpawn;

	[Header("References")]
	[SerializeField] private Plane planePrefab;

	private int planeCount;

	protected override void Initialize()
	{
		minObjectsToSpawn = minPlanesToSpawn;
		maxObjectsToSpawn = maxPlanesToSpawn;
		objectToSpawn = planePrefab;
		planeCount = 0;

		GameObject poolObject = new GameObject("PlanePool");
		poolObject.transform.parent = transform;
		pool = poolObject.AddComponent<PlanePool>();

		base.Initialize();
	}

	protected override void ComputeObjectPositions(int numObjectsToSpawn)
	{
		SpriteRenderer renderer = objectToSpawn.GetComponent<SpriteRenderer>();

		planeCount = numObjectsToSpawn;

		//Spawn the objects
		for (int i = 0; i < numObjectsToSpawn; i++)
		{
			//Compute object position
			Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(0,
										 Screen.height - Screen.height / 5)) - new Vector3(renderer.bounds.size.x / 2, renderer.bounds.size.y * i / 1.5f, 0);

			//Spawn object
			SpawnObject(position);
		}
	}

	protected override void SpawnObject(Vector2 position)
	{
		Plane newObject = pool.GetObject();
		newObject.gameObject.SetActive(true);
		newObject.transform.position = position;
		newObject.Spawner = this;
	}

	public void PlaneKilled()
	{
		planeCount--;

		if(planeCount == 0)
		{
			SpawnObjects();
		}
	}

}
