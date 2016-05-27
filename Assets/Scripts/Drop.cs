using UnityEngine;
using System.Collections;

[System.Serializable]
public class Drop  {

	public byte id;
	[Range(0.0f, 1.0f)]
	public float dropChance;
	public int amount;

	public Drop(ItemDatabase.ItemID_e id, float dropChance, int amount = 1)
	{
		this.id = (byte)id;
		this.dropChance = dropChance;
		this.amount = amount;
	}

	public bool DropChanceSuccess()
	{
		return Random.value <= dropChance;
	}



}
