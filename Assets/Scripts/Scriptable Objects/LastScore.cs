using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LastScore", menuName = "Data/Last Score Configuration", order = 1)]
public class LastScore : ScriptableObject
{
	public int LastScorePoints;
	public bool IsFirstTime = true;
}
