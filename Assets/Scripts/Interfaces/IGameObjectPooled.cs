using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for pooled objects.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGameObjectPooled<T> where T : Component
{
	ObjectPool<T> Pool { get; set; }
}
