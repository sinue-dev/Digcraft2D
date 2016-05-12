using UnityEngine;
using System.Collections;

[System.Serializable]
public class Drop  {

	public string ItemName;
	[Range(0.0f, 1.0f)]
	public float dropChance;

	public bool DropChanceSuccess()
	{
		return Random.value <= dropChance;
	}



}
