using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : Spawner<Cloud>
{
	[Header("Values")]
	[SerializeField] [Range(3, 8)] private int minCloudsToSpawn = 3;
	[SerializeField] [Range(3, 8)] private int maxCloudsToSpawn = 8;

	[Header("References")]
	[SerializeField] private Cloud cloudPrefab;

	protected override void Initialize()
	{
		minObjectsToSpawn = minCloudsToSpawn;
		maxObjectsToSpawn = maxCloudsToSpawn;
		objectToSpawn = cloudPrefab;

		GameObject poolObject = new GameObject("CloudPool");
		poolObject.transform.parent = transform;
		pool = poolObject.AddComponent<CloudPool>();

		base.Initialize();
	}

	protected override void ComputeObjectPositions(int numObjectsToSpawn)
	{
		SpriteRenderer renderer = objectToSpawn.GetComponent<SpriteRenderer>();

		//Spawn the objects
		for (int i = 0; i < numObjectsToSpawn; i++)
		{
			//X and Y positions
			float x = Random.Range(ScreenHelper.instance.LeftSideOfScreen * 0.8f, ScreenHelper.instance.RightSideOfScreen * 0.8f);
			float y = Random.Range(ScreenHelper.instance.DownSideOfScreen * 0.55f, ScreenHelper.instance.TopSideOfScreen);

			Vector2 position = new Vector2(x, y);

			//Spawn object
			SpawnObject(position);
		}
	}

}
