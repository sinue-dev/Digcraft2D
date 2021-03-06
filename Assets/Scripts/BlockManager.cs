﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : Singleton<BlockManager> {

	private List<Block> blocks = new List<Block>();
	public Dictionary<string, Sprite> dictSprites = new Dictionary<string, Sprite>();

	public enum BlockID_e
	{
		AIR = 0,
		GRASS = 1,
		DIRT = 2,
		DIRT_GRASS = 3,
		DIRT_SAND = 4,
		DIRT_SNOW = 5,
		GRAVEL_DIRT = 6,
		GRAVEL_STONE = 7,
		ICE = 8,
		LAVA = 9,
		SAND = 10,
		SNOW = 11,
		STONE = 12,
		STONE_GRASS = 13,
		STONE_SAND = 14,
		STONE_DIRT = 15,
		STONE_COAL = 16,
		STONE_COPPER = 17,
		STONE_IRON = 18,
		STONE_SILVER = 19,
		STONE_GOLD = 20,
		STONE_DIAMOND = 21,
		WATER = 22,
		TRUNK_SIDE = 23,
		TRUNK_WHITE_SIDE = 25,
		GRASS4 = 26,

        // UNDESTROYABLE
        GROUND = 1337
    }

	private void Awake()
	{
		LoadSpriteDict();

		AddBlockToDatabase("Dirt", BlockID_e.DIRT, dictSprites["dirt"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.DIRT, 1) });
		AddBlockToDatabase("DirtGrass", BlockID_e.DIRT_GRASS, dictSprites["dirt_grass"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.DIRT, 1) });
		AddBlockToDatabase("DirtSand", BlockID_e.DIRT_SAND, dictSprites["dirt_sand"], true, true,  new Drop[1] { new Drop(ItemDatabase.ItemID_e.DIRT, 1) });
		AddBlockToDatabase("DirtSnow", BlockID_e.DIRT_SNOW, dictSprites["dirt_snow"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.DIRT, 1) });
		AddBlockToDatabase("GravelDirt", BlockID_e.GRAVEL_DIRT, dictSprites["gravel_dirt"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.GRAVEL_DIRT, 0.5f) });
		AddBlockToDatabase("GravelStone", BlockID_e.GRAVEL_STONE, dictSprites["gravel_stone"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.GRAVEL_STONE, 1) });
		AddBlockToDatabase("Ice", BlockID_e.ICE, dictSprites["ice"], true, true, new Drop[0]);
		AddBlockToDatabase("Lava", BlockID_e.LAVA, dictSprites["lava"], false, false, new Drop[0]);
		AddBlockToDatabase("Sand", BlockID_e.SAND, dictSprites["sand"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.SAND, 1) });
		AddBlockToDatabase("Snow", BlockID_e.SNOW, dictSprites["snow"], true, true, new Drop[0]);
		AddBlockToDatabase("Stone", BlockID_e.STONE, dictSprites["stone"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.GRAVEL_STONE, 1) }); // gravel_stone
		AddBlockToDatabase("StoneGrass", BlockID_e.STONE, dictSprites["stone_dirt"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.GRAVEL_STONE, 1) }); // gravel_stone
		AddBlockToDatabase("StoneSand", BlockID_e.STONE, dictSprites["stone_sand"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.GRAVEL_STONE, 1) }); // gravel_stone
		AddBlockToDatabase("StoneSnow", BlockID_e.STONE, dictSprites["stone_snow"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.GRAVEL_STONE, 1) }); // gravel_stone
		AddBlockToDatabase("StoneCoal", BlockID_e.STONE_COAL, dictSprites["stone_coal"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.COAL, 1) });
		AddBlockToDatabase("StoneCopper", BlockID_e.STONE_COPPER, dictSprites["stone_copper"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.COPPER, 1) });
		AddBlockToDatabase("StoneIron", BlockID_e.STONE_IRON, dictSprites["stone_iron"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.IRON, 1) });
		AddBlockToDatabase("StoneSilver", BlockID_e.STONE_SILVER, dictSprites["stone_silver"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.SILVER, 1) });
		AddBlockToDatabase("StoneGold", BlockID_e.STONE_GOLD, dictSprites["stone_gold"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.GOLD, 1) });
		AddBlockToDatabase("StoneDiamond", BlockID_e.STONE_DIAMOND, dictSprites["stone_diamond"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.DIAMOND, 1) });
		AddBlockToDatabase("Water", BlockID_e.WATER, dictSprites["water"], false, false, new Drop[0]);
		AddBlockToDatabase("Trunk", BlockID_e.TRUNK_SIDE, dictSprites["trunk_side"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.TRUNK_SIDE, 1) });
		AddBlockToDatabase("TrunkWhite", BlockID_e.TRUNK_WHITE_SIDE, dictSprites["trunk_white_side"], true, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.TRUNK_WHITE_SIDE, 1) });
		AddBlockToDatabase("Grass4", BlockID_e.GRASS4, dictSprites["grass4"], false, true, new Drop[1] { new Drop(ItemDatabase.ItemID_e.SEED, 1) });

        AddBlockToDatabase("Ground", BlockID_e.GROUND, dictSprites["greystone"], true, false, new Drop[0]);
    }

	private Block AddBlockToDatabase(string DisplayName, BlockID_e id, Sprite sprite, bool isSolid, bool isDestroyable, Drop[] drops)
	{
		Block preset;
		preset = new Block();
		preset.DisplayName = DisplayName;
		preset.id = (byte)id;
		preset.sprite = sprite;
		preset.isSolid = isSolid;
        preset.isDestroyable = isDestroyable;
		preset.drops = drops;
		blocks.Add(preset);

		return preset;
	}

	public Block FindBlock(BlockID_e id)
	{
		foreach(Block block in blocks)
		{
			if (block.id == (byte)id) return block;
		}
		return null;
	}

	public Block FindBlock(byte id)
	{
		foreach(Block block in blocks)
		{
			if (block.id == id) return block;
		}
		return null;
	}

	#region Sprite Functions
	private void LoadSpriteDict()
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/voxel-pack/Tiles");

		foreach (Sprite sprite in sprites)
		{
			if (!dictSprites.ContainsKey(sprite.name)) dictSprites.Add(sprite.name, sprite);
		}
	}
	#endregion

}
