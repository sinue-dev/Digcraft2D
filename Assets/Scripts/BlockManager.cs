﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : Singleton<BlockManager> {

	public List<Block> blocks;

	protected BlockManager() { }

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

	public Block FindBlock(string name)
	{
		foreach (Block block in blocks)
		{
			if (block.sDisplayName == name) return block;
		}

		return null;
	}
}
