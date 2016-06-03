using UnityEngine;
using System.Collections;

public class Chunk {

	public static int size = 16;
	private static float heightModifier = 64f;
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
			int pHeight = GetOverworldHeight(x);

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
                        bool bRet = false;

                        if (!bRet) bRet = GenerateDiamond(x, y, pHeight);
                        if (!bRet) bRet = GenerateGold(x, y, pHeight);
                        if (!bRet) bRet = GenerateSilver(x, y, pHeight);
                        if (!bRet) bRet = GenerateCopper(x, y, pHeight);
                        if (!bRet) bRet = GenerateIron(x, y, pHeight);
                        if (!bRet) bRet = GenerateCoal(x, y, pHeight);

                        if (!bRet) blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE);
                    }

                    if (y == 0) blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.GROUND);
                }
                else
                {
                    blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.AIR);
                }
            }
        }
    }

    public bool GenerateCoal(int x, int y, int pHeight)
    {
        if (y >= 3 && y <= 100)
        {
            if (Random.value <= 0.05)
            {
                blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE_COAL);
                return true;
            }
        }
        return false;
    }

    public bool GenerateIron(int x, int y, int pHeight)
    {
        if (y >= 3 && y <= 40)
        {
            if (Random.value <= 0.03)
            {
                blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE_IRON);
                return true;
            }
        }
        return false;
    }

    public bool GenerateCopper(int x, int y, int pHeight)
    {
        if (y >= 40 && y <= 54)
        {
            if (Random.value <= 0.02)
            {
                blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE_COPPER);
                return true;
            }
        }
        return false;
    }

    public bool GenerateSilver(int x, int y, int pHeight)
    {
        if (y >= 25 && y <= 35)
        {
            if (Random.value <= 0.015)
            {
                blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE_SILVER);
                return true;
            }
        }
        return false;
    }

    public bool GenerateGold(int x, int y, int pHeight)
    {
        if (y >= 3 && y <= 25)
        {
            if (Random.value <= 0.01)
            {
                blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE_GOLD);
                return true;
            }
        }
        return false;
    }

    public bool GenerateDiamond(int x, int y, int pHeight)
    {
        if (y >= 3 && y <= 10)
        {
            if (Random.value <= 0.008)
            {
                blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.STONE_DIAMOND);
                return true;
            }
        }
        return false;
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

	public int GetOverworldHeight(int x)
	{
		float pValue = Mathf.PerlinNoise((position * size + x) * 0.05f, 5 * 0.05f);
		int pHeight = Mathf.RoundToInt(pValue * 16f + heightModifier);
		return pHeight;
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