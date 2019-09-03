using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "Data/Game Configuration", order = 0)]
public class GameConfiguration : ScriptableObject
{
	public int default_high_score = 100;
	public int points_per_plane = 1;
	public float time_limit = 30f;

	public void RestartToDefault()
	{
		default_high_score = 100;
		points_per_plane = 1;
		time_limit = 30f;
	}
}
