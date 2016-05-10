using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour {

	public GameObject player;
	public float heightModifier = 20f;

	private BlockManager blockManager;

	private int width = 64;
	private int height = 128;

	private Block[,] blocks;

	void Start ()
	{
		blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();

		blocks = new Block[width, height];

		GenerateBlocks();
		SpawnBlocks();
	}

	private void GenerateBlocks()
	{
		int playerSpawnX = Random.Range(0, width);

		for(int x = 0; x < width; x++)
		{
			float pValue = Mathf.PerlinNoise(x * 0.05f, 5 * 0.05f);
			int pHeight = Mathf.RoundToInt(pValue * 10f + heightModifier);

			for(int y = 0; y < height; y++)
			{
				if(y <= pHeight)
				{
					if (y == pHeight) // Flowers
					{
						if (Random.value < 0.1)
						{
							blocks[x, y] = blockManager.FindBlock(4);
						}
					}
					else  if (y == pHeight - 1) // Grass Layer
					{
						blocks[x, y] = blockManager.FindBlock(1);
					}					
					else if(y == pHeight - 2 ||y == pHeight - 3 || y == pHeight - 4) // Dirt Layers
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

			if(x==playerSpawnX)
			{
				SpawnPlayer(x, pHeight +3);
			}
		}
	}

	private void SpawnBlocks()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (blocks[x, y] != null)
				{
					GameObject goBlock = new GameObject();
					SpriteRenderer sr = goBlock.AddComponent<SpriteRenderer>();
					sr.sprite = blocks[x, y].sprite;

					goBlock.name = blocks[x, y].sDisplayName + "[" + x + "," + y + "]";
					goBlock.transform.position = new Vector3(x, y);
					goBlock.tag = "Block";
					if (blocks[x, y].isSolid)
					{
						BoxCollider2D bc = goBlock.AddComponent<BoxCollider2D>();
					}
				}
			}
		}
	}

	public void DestroyBlock(GameObject block)
	{
		int x = (int)block.transform.position.x;
		int y = (int)block.transform.position.y;

		blocks[x, y] = blockManager.FindBlock(0);
		GameObject.Destroy(block);
	}

	private void SpawnPlayer(float x, float y)
	{
		GameObject.Instantiate(player, new Vector3(x,y), Quaternion.identity);
	}

}
