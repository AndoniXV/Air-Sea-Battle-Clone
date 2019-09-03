using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that lets an object to shoot.
/// </summary>
public class Launcher : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] private float rateTime;			//Time between bullets
	[SerializeField] private int maxBulletsOnScreen;	//Maximum bullets on screen at the same time
	
	[Header("References")]
	[SerializeField] private Bullet bulletPrefab;		//Prefab of the bullet
	[SerializeField] private Transform launcherObject;  //GameObject from where the bullet will be shot
	[SerializeField] private AudioClip shootingSound;	//Sound the launcher makes when shooting a bullet

	private BulletPool pool;
	private float timer;

	private void Awake()
	{
		Initialize();
	}

	// Initialize the necessary properties like the pool.
	private void Initialize()
	{
		GameObject poolObject = new GameObject("BulletPool");
		poolObject.transform.parent = transform;
		pool = poolObject.AddComponent<BulletPool>();
		//pool = new BulletPool();
		pool.Initialize(maxBulletsOnScreen, bulletPrefab);

		timer = rateTime;

		if(launcherObject == null)
		{
			launcherObject = transform;
		}
	}

	private void Update()
	{
		timer += Time.deltaTime;
	}

	/// <summary>
	/// Shoot a bullet in the launchers position with a given direction.
	/// </summary>
	/// <param name="direction"></param>
	/// <returns>Returns true if the shot could be made and false if it coudn't.</returns>
	public bool Shoot(Vector2 direction)
	{
		if(timer > rateTime)
		{
			Bullet bullet = pool.GetObject();

			if (bullet)
			{
				bullet.Initialize(direction, launcherObject.position);
				bullet.gameObject.SetActive(true);
				timer = 0.0f;
				AudioManager.Instance.PlayAudio(shootingSound);
				return true;
			}
		}

		return false;
	}

}
