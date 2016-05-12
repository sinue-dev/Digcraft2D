using UnityEngine;
using System.Collections;

public class Chunk {

	public static int size = 16;
	private static float heightModifier = 20f;
	public int position;
	public Block[,] blocks;
	public GameObject[,] blockObjects;
	private BlockManager blockManager;

	public Chunk(BlockManager blockManager, int position)
	{
		this.blockManager = blockManager;
		this.position = position;
		blocks = new Block[size,WorldGen.height];
		blockObjects = new GameObject[size, WorldGen.height];
	}

	public void GenerateBlocks()
	{
		for (int x = 0; x < size; x++)
		{
			float pValue = Mathf.PerlinNoise((position * size + x) * 0.05f, 5 * 0.05f);
			int pHeight = Mathf.RoundToInt(pValue * 10f + heightModifier);

			for (int y = 0; y < WorldGen.height; y++)
			{
				if (y <= pHeight)
				{
					if (y == pHeight) // Flowers
					{
						if (Random.value < 0.1)
						{
							blocks[x, y] = blockManager.FindBlock(4);
						}
					}
					else if (y == pHeight - 1) // Grass Layer
					{
						blocks[x, y] = blockManager.FindBlock(1);
					}
					else if (y == pHeight - 2 || y == pHeight - 3 || y == pHeight - 4) // Dirt Layers
					{
						blocks[x, y] = blockManager.FindBlock(3);
					}
					else // Stone Layers
					{
						blocks[x, y] = blockManager.FindBlock(2);
					}
				}
				else
				{
					blocks[x, y] = blockManager.FindBlock(0);
				}
			}
		}
	}

	public void Destroy()
	{
		for(int x = 0; x < size; x++)
		{
			for(int y = 0; y < WorldGen.height; y++)
			{
				blocks[x, y] = null;
				GameObject.Destroy(blockObjects[x, y]);
			}
		}
	}

}
