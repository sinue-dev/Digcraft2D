using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour {

	public List<Block> blocks;

	private void Start()
	{
		byte blockID = 1;
		Block block = FindBlock(blockID);
		Debug.Log(block.sDisplayName);
	}

	public Block FindBlock(byte id)
	{
		foreach(Block block in blocks)
		{
			if (block.id == id) return block;
		}

		return null;
	}

}
