using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : Singleton<BlockManager> {

	private List<Block> blocks = new List<Block>();
	public Dictionary<string, Sprite> dictSprites = new Dictionary<string, Sprite>();

	private void Start()
	{
		LoadSpriteDict();

		Drop[] drops;
		Block preset;

		preset = new Block();
		preset.DisplayName = "Grass";
		preset.id = 1;
		preset.sprite = dictSprites["grass_side"];
		preset.isSolid = true;
		drops = new Drop[1];
		drops[0] = new Drop();
		drops[0].ItemName = "Dirt";
		drops[0].dropChance = 1;
		preset.drops = drops;
		blocks.Add(preset);

		preset = new Block();
		preset.DisplayName = "Stone";
		preset.id = 2;
		preset.sprite = dictSprites["stone"];
		preset.isSolid = true;
		drops = new Drop[1];
		drops[0] = new Drop();
		drops[0].ItemName = "Cobblestone";
		drops[0].dropChance = 1;
		preset.drops = drops;
		blocks.Add(preset);

		preset = new Block();
		preset.DisplayName = "Dirt";
		preset.id = 3;
		preset.sprite = dictSprites["dirt"];
		preset.isSolid = true;
		drops = new Drop[1];
		drops[0] = new Drop();
		drops[0].ItemName = "Dirt";
		drops[0].dropChance = 1;
		preset.drops = drops;
		blocks.Add(preset);

		preset = new Block();
		preset.DisplayName = "YellowFlower";
		preset.id = 4;
		preset.sprite = dictSprites["flower_dandelion"];
		preset.isSolid = false;
		preset.drops = new Drop[0];
		blocks.Add(preset);
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
			if (block.DisplayName == name) return block;
		}

		return null;
	}

	#region Sprite Functions
	private void LoadSpriteDict()
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");

		foreach (Sprite sprite in sprites)
		{
			if (!dictSprites.ContainsKey(sprite.name)) dictSprites.Add(sprite.name, sprite);
		}
	}
	#endregion

}
