using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGen : MonoBehaviour {

	public GameObject player;

	private BlockManager blockManager;

	private ItemDatabase itemDatabase;

	public static int height = 128;
	public int viewDistance = 4;

	private List<Chunk> chunks;

	void Start ()
	{
		blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();

		itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();

		chunks = new List<Chunk>();

		player = SpawnPlayer(7, 65);
	}

	void Update()
	{
		int playerChunk = Mathf.FloorToInt(player.transform.position.x / 16);

		// Spawn new chunks
		for (int i = playerChunk - viewDistance; i < playerChunk + viewDistance; i++)
		{
			bool spawn = true;

			foreach (Chunk chunk in chunks)
			{
				if (chunk.position == i)
				{
					spawn = false;
				}
			}

			if (spawn)
			{
				//Spawn Chunk
				Chunk newChunk = new Chunk(blockManager, i);
				newChunk.GenerateBlocks();
				SpawnBlocks(newChunk);
				chunks.Add(newChunk);
			}
		}

		// Clean old chunks
		foreach (Chunk chunk in chunks)
		{
			if (chunk.position < playerChunk - viewDistance || chunk.position > playerChunk + viewDistance)
			{
				chunk.Destroy();
				chunks.Remove(chunk);
				break;
			}
		}
	}

	private void SpawnBlocks(Chunk chunk)
	{
		for (int x = 0; x < Chunk.size; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (chunk.blocks[x, y] != null)
				{
					GameObject goBlock = new GameObject();
					SpriteRenderer sr = goBlock.AddComponent<SpriteRenderer>();
					sr.sprite = chunk.blocks[x, y].sprite;

					goBlock.name = chunk.blocks[x, y].sDisplayName + "[" + x + "," + y + "]";
					goBlock.transform.position = new Vector3((chunk.position * Chunk.size) + x, y);
					goBlock.tag = "Block";

					chunk.blockObjects[x, y] = goBlock;

					if (chunk.blocks[x, y].isSolid)
					{
						BoxCollider2D bc = goBlock.AddComponent<BoxCollider2D>();
					}
				}
			}
		}
	}

	public void DestroyBlock(GameObject block)
	{
		Vector3 blockPos = block.transform.position;
		Vector2 chunkPos = WorldPosToChunkPos(blockPos.x, blockPos.y);

		int x = (int)chunkPos.x;
		int y = (int)chunkPos.y;

		Chunk chunk = ChunkAtPos(blockPos.x);

		foreach (Drop drop in chunk.blocks[x, y].drops)
		{
			if (drop.DropChanceSuccess())
			{
				GameObject dropObject = new GameObject();
				dropObject.transform.position = block.transform.position;
				dropObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				dropObject.AddComponent<SpriteRenderer>().sprite = itemDatabase.FindItem(drop.ItemName).sprite;
				dropObject.AddComponent<PolygonCollider2D>();
				dropObject.AddComponent<Rigidbody2D>();
				dropObject.layer = 9;

				dropObject.AddComponent<Magnetism>().target = GameObject.FindWithTag("Player").transform;
				dropObject.name = drop.ItemName;
			}
		}

		chunk.blocks[x, y] = blockManager.FindBlock(0);

		GameObject.Destroy(block);
	}

	private Chunk ChunkAtPos(float x)
	{
		int chunkIndex = Mathf.FloorToInt(x / Chunk.size);

		foreach (Chunk chunk in chunks)
		{
			if (chunk.position == chunkIndex)
			{
				return chunk;
			}
		}
		return null;
	}

	public Vector2 WorldPosToChunkPos(float x, float y)
	{
		int xPos = Mathf.RoundToInt(x - (ChunkAtPos(x).position * Chunk.size));
		int yPos = Mathf.RoundToInt(y);

		return new Vector2(xPos, yPos);
	}

	public Vector2 ChunkPosToWorldPos(int x, int y, int chunkPos)
	{
		int xPos = Mathf.FloorToInt(x + (chunkPos * Chunk.size));
		int yPos = Mathf.FloorToInt(y);

		return new Vector2(xPos, yPos);
	}

	private GameObject SpawnPlayer(float x, float y)
	{
		GameObject playerObj = GameObject.Instantiate(player, new Vector3(x, y), Quaternion.identity) as GameObject;
		return playerObj;
	}

}
