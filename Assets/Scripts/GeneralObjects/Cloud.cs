using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CloudMotor))]
[RequireComponent(typeof(HorizontalInScreenObject))]
public class Cloud : MonoBehaviour, IGameObjectPooled<Cloud>
{
	private CloudMotor motor;

	private ObjectPool<Cloud> pool;
	public ObjectPool<Cloud> Pool
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
		motor = GetComponent<CloudMotor>();
	}

}
