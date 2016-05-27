using UnityEngine;
using System.Collections;

public class Chunk {

	public static int size = 16;
	private static float heightModifier = 20f;
	public int position;
	public Block[,] blocks;
	public GameObject[,] blockObjects;

	public ChunkState_e chunkState = ChunkState_e.DESPAWNED;

	public enum ChunkState_e
	{
		SPAWNED,
		DESPAWNED
	}

	//public GameObject[,] blockObjects
	//{
	//	get { return _blockObjects; }
	//	set { _blockObjects = value; }
	//}

	public Chunk(int position)
	{
		this.position = position;
		blocks = new Block[size, WorldManager.height];
		blockObjects = new GameObject[size, WorldManager.height];
	}

	public void GenerateBlocks()
	{
		for (int x = 0; x < size; x++)
		{
			float pValue = Mathf.PerlinNoise((position * size + x) * 0.05f, 5 * 0.05f);
			int pHeight = Mathf.RoundToInt(pValue * 10f + heightModifier);

			for (int y = 0; y < WorldManager.height; y++)
			{
				if (y <= pHeight)
				{
					if (y == pHeight) // Flowers
					{
						if (Random.value < 0.1)
						{
							blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.GRASS4);
						}
					}
					else if (y == pHeight - 1) // Grass Layer
					{
						blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.DIRT_GRASS);
					}
					else if (y == pHeight - 2 || y == pHeight - 3 || y == pHeight - 4) // Dirt Layers
					{
						blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.DIRT);
					}
					else // Stone Layers
					{
						blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE);
					}
				}
				else
				{
					blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.AIR);
				}
			}
		}
	}

	public void Destroy()
	{
		for(int x = 0; x < size; x++)
		{
			for(int y = 0; y < WorldManager.height; y++)
			{
				blocks[x, y] = null;
				GameObject.Destroy(blockObjects[x, y]);
			}
		}
	}

	public int CountBlockObjects()
	{
		int i = 0;
		for (int x = 0; x < size; x++)
		{
			for (int y = 0; y < WorldManager.height; y++)
			{
				if (blockObjects[x, y] != null) i++;
			}
		}
		return i;
	}

	public cChunk Serializable()
	{
		return new cChunk(position, WorldManager.I.SerializableBlocks(blocks));
	}
}

[System.Serializable]
public class cChunk
{
	public static int size = 16;
	private static float heightModifier = 20f;
	public int position;
	public cBlock[,] blocks;

	public cChunk(int position, cBlock[,] blocks)
	{
		this.position = position;
		this.blocks = blocks;
	}
}