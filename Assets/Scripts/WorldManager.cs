using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : Singleton<WorldManager> {

	public static int height = 128;
	public int viewDistance = 4;

	public GameObject playerPrefab; // Player Prefab
	public GameObject player;

	public List<Chunk> chunks;

	void Start ()
	{
		chunks = new List<Chunk>();

		SaveLoadManager.I.LoadSaveGame();
		//SpawnPlayer(7, 65, 0);
	}

	void Update()
	{
		if (player == null) return;

		int playerChunk = Mathf.FloorToInt(player.transform.position.x / 16);

		// Spawn new chunks
		for (int i = playerChunk - viewDistance; i < playerChunk + viewDistance; i++)
		{
			bool spawn = true;
			foreach (Chunk chunk in chunks)
			{
				if (chunk == null) continue;

				if (chunk.position == i)
				{
					if (chunk.chunkState == Chunk.ChunkState_e.DESPAWNED)
					{
						SpawnBlocks(chunk);
					}
					spawn = false;
				}
			}

			if (spawn)
			{
				//Spawn Chunk
				Chunk newChunk = new Chunk(i);
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
				if(chunk.chunkState == Chunk.ChunkState_e.SPAWNED) DespawnBlocks(chunk);
			}
		}
	}

	public void PopulateLoadedChunks(List<cChunk> chunks_IN)
	{
		// Clean all old chunks
		foreach (Chunk chunk in chunks)
		{
			chunk.Destroy();
			chunks.Remove(chunk);
		}

		foreach (cChunk _chunk in chunks_IN)
		{
			Chunk newChunk = new Chunk(_chunk.position);
			Block[,] newBlocks = new Block[Chunk.size, height];

			for (int x = 0; x < Chunk.size; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (_chunk.blocks[x, y] != null)
					{
						newBlocks[x, y] = new Block();
						newBlocks[x, y].DisplayName = _chunk.blocks[x, y].DisplayName;
						newBlocks[x, y].id = _chunk.blocks[x, y].id;
						newBlocks[x, y].isSolid = _chunk.blocks[x, y].isSolid;
						newBlocks[x, y].sprite = BlockManager.I.FindBlock(_chunk.blocks[x, y].id).sprite;
						newBlocks[x, y].drops = _chunk.blocks[x, y].drops;
					}
					else newBlocks[x, y] = null;
				}
			}
			newChunk.blocks = newBlocks;
			SpawnBlocks(newChunk);

			chunks.Add(newChunk);
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
					sr.sprite = BlockManager.I.FindBlock(chunk.blocks[x, y].id).sprite;
					sr.material = Resources.Load("Materials/SpriteMaterial") as Material;

					goBlock.name = chunk.blocks[x, y].DisplayName + "[" + x + "," + y + "]";
					goBlock.transform.position = new Vector3((chunk.position * Chunk.size) + x, y);
					goBlock.tag = "Block";
					goBlock.transform.SetParent(transform);

					chunk.blockObjects[x, y] = goBlock;

					if (chunk.blocks[x, y].isSolid)
					{
						BoxCollider2D bc = goBlock.AddComponent<BoxCollider2D>();
					}
					else
					{
						BoxCollider2D bc = goBlock.AddComponent<BoxCollider2D>();
						bc.isTrigger = true;
					}
				}
			}
		}

		chunk.chunkState = Chunk.ChunkState_e.SPAWNED;
	}

	private void DespawnBlocks(Chunk chunk)
	{
		for (int x = 0; x < Chunk.size; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (chunk.blocks[x, y] != null)
				{
					GameObject.Destroy(chunk.blockObjects[x, y]);
					chunk.blockObjects[x, y] = null;
				}
			}
		}

		chunk.chunkState = Chunk.ChunkState_e.DESPAWNED;
	}

	public void UpdateChunk(Chunk chunk)
	{
		for(int x = 0; x < Chunk.size; x++)
		{
			for(int y = 0; y < height; y++)
			{
				if(chunk.blocks[x,y] != null && chunk.blockObjects[x,y] == null)
				{
					GameObject goBlock = new GameObject();
					SpriteRenderer sr = goBlock.AddComponent<SpriteRenderer>();
					sr.sprite = BlockManager.I.FindBlock(chunk.blocks[x, y].id).sprite;
					sr.material = Resources.Load("Materials/SpriteMaterial") as Material;

					goBlock.name = chunk.blocks[x, y].DisplayName + "[" + x + "," + y + "]";
					goBlock.transform.position = new Vector3((chunk.position * Chunk.size) + x, y);
					goBlock.tag = "Block";
					goBlock.transform.SetParent(transform);

					chunk.blockObjects[x, y] = goBlock;

					if (chunk.blocks[x, y].isSolid)
					{
						BoxCollider2D bc = goBlock.AddComponent<BoxCollider2D>();
					}
					else
					{
						BoxCollider2D bc = goBlock.AddComponent<BoxCollider2D>();
						bc.isTrigger = true;
					}
				}
				else if(chunk.blocks[x, y] == null && chunk.blockObjects[x, y] != null)
				{
					GameObject.Destroy(chunk.blockObjects[x, y]);
					chunk.blockObjects[x, y] = null;
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

        if (chunk.blocks[x, y].isDestroyable)
        {
            foreach (Drop drop in chunk.blocks[x, y].drops)
            {
                if (drop.DropChanceSuccess())
                {
                    GameObject dropObject = new GameObject();
                    dropObject.transform.position = block.transform.position;
                    dropObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                    SpriteRenderer sr = dropObject.AddComponent<SpriteRenderer>();
                    Item item = ItemDatabase.I.FindItem(drop.id);
                    sr.sprite = (item != null) ? item.sprite : null;
                    sr.material = sr.material = Resources.Load("Materials/SpriteMaterial") as Material;

                    dropObject.AddComponent<PolygonCollider2D>();
                    dropObject.AddComponent<Rigidbody2D>();
                    dropObject.layer = 9;

                    dropObject.AddComponent<Magnetism>().target = GameObject.FindWithTag("Player").transform;
                    dropObject.name = ItemDatabase.I.FindItem(drop.id).itemData.sItemName;
                    dropObject.AddComponent<ItemInfo>().item = ItemDatabase.I.FindItem(drop.id);
                }
            }

            chunk.blocks[x, y] = BlockManager.I.FindBlock(BlockManager.BlockID_e.AIR);

            GameObject.Destroy(block);
        }
	}

	public void PlaceBlock(Block block, Vector3 pos)
	{
		Chunk chunk = ChunkAtPos(pos.x);
		Vector2 chunkPos = WorldPosToChunkPos(pos.x, pos.y);

        if (chunkPos.x <= chunk.blocks.GetLength(0) && chunkPos.y <= chunk.blocks.GetLength(1))
        {
            chunk.blocks[(int)chunkPos.x, (int)chunkPos.y] = block;

            UpdateChunk(chunk);
        }
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

    public int GetBlockDistance(Vector3 targetPos, Vector3 pos)
    {
        return Mathf.RoundToInt(Vector3.Distance(pos, targetPos));
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

	public Vector2 WorldPosToGridPos(float x, float y)
	{
		int xPos = Mathf.RoundToInt(x);
		int yPos = Mathf.RoundToInt(y);

		return new Vector2(xPos, yPos);
	}

	public void SpawnPlayer(float x, float y, int direction)
	{
		player = GameObject.Instantiate(playerPrefab, new Vector3(x, y), Quaternion.identity) as GameObject;
		player.GetComponent<Player>().direction = direction;
	}

	public List<cChunk> SerializableChunks(List<Chunk> chunks)
	{
		List<cChunk> chnks = new List<cChunk>();
		foreach (Chunk chunk in chunks)
		{
			cChunk chnk = chunk.Serializable();
			chnk.blocks = SerializableBlocks(chunk.blocks);

			chnks.Add(chnk);
		}
		return chnks;
	}

	public cBlock[,] SerializableBlocks(Block[,] blocks)
	{
		cBlock[,] blcks = new cBlock[Chunk.size, height];
		for (int x = 0; x < Chunk.size; x++)
		{			
			for (int y = 0; y < height; y++)
			{
				if (blocks[x, y] != null) blcks[x, y] = blocks[x, y].Serializable(); else blcks[x, y] = null;
			}
		}
		return blcks;
	}
}
