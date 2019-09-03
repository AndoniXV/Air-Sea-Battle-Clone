using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet>
{
	private int maxBullets = 5;
	private Bullet bulletPrefab;

	public void Initialize(int maxBullets, Bullet bulletPrefab)
	{
		this.maxBullets = maxBullets;
		this.bulletPrefab = bulletPrefab;
		SetPrefab(bulletPrefab);
		AddObjects(maxBullets);
	}

	/// <summary>
	/// Returns a bullet from the pool only if there is one.
	/// </summary>
	/// <returns></returns>
	public override Bullet GetObject()
	{
		if (objects.Count > 0)
		{
			return objects.Dequeue();
		}

		return null;
	}

	public override void AddObjects(int amount)
	{
		if(amount + objects.Count > maxBullets)
		{
			Debug.LogWarning("Please, don't add more bullets to the pool than maxBullets.");
			return;
		}
		
		base.AddObjects(amount);
	}
}
