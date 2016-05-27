using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ItemDatabase : Singleton<ItemDatabase> {

	private List<Item> items = new List<Item>();
	public Dictionary<string, Sprite> dictSprites = new Dictionary<string, Sprite>();

	public enum ItemID_e
	{
		//AIR = 0,
		GRASS = 1,
		DIRT = 2,
		DIRT_GRASS = 3,
		DIRT_SAND = 4,
		DIRT_SNOW = 5,
		GRAVEL_DIRT = 6,
		GRAVEL_STONE = 7,
		//ICE = 8,
		//LAVA = 9,
		SAND = 10,
		//SNOW = 11,
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
		//WATER = 22,
		TRUNK_SIDE = 23,
		TRUNK_WHITE_SIDE = 25,
		//GRASS4 = 26

		//### Items
		ORE_COAL = 100,
		ORE_COPPER = 101,
		ORE_IRON = 102,
		ORE_SILVER = 103,
		ORE_GOLD = 104,
		ORE_DIAMOND = 105,
		APPLE = 106,
		SEED = 107,
		WHEAT = 108
	}

	private void Awake()
	{
		LoadSpriteDict();

		AddItemToDatabase("Dirt", ItemID_e.DIRT, BlockManager.BlockID_e.DIRT, BlockManager.I.dictSprites["dirt"], 64, cItemData.ItemType_e.BLOCK);
		AddItemToDatabase("GravelDirt", ItemID_e.GRAVEL_DIRT, BlockManager.BlockID_e.GRAVEL_DIRT, BlockManager.I.dictSprites["gravel_dirt"], 64, cItemData.ItemType_e.BLOCK);
		AddItemToDatabase("GravelStone", ItemID_e.GRAVEL_STONE, BlockManager.BlockID_e.GRAVEL_STONE, BlockManager.I.dictSprites["gravel_stone"], 64, cItemData.ItemType_e.BLOCK);
		AddItemToDatabase("Seed", ItemID_e.SEED, BlockManager.BlockID_e.AIR, dictSprites["seed"], 64, cItemData.ItemType_e.FOOD);
	}

	#region Item Functions
	public void AddItemToDatabase(string ItemName, ItemID_e id, BlockManager.BlockID_e blockID, Sprite sprite, int MaxStackSize, cItemData.ItemType_e ItemType)
	{
		Item preset;
		preset = new Item();
		preset.itemData.sItemName = ItemName;
		preset.itemData.itemID = id;
		preset.itemData.blockID = blockID;		
		preset.itemData.iMaxStack = MaxStackSize;
		preset.itemData.type = ItemType;
		preset.sprite = sprite;
		items.Add(preset);
	}

    public Item FindItem(ItemID_e id)
    {
        foreach(Item item in items)
        {
            if (item.itemData.itemID == id) return item;
        }
        return null;
    }

	public Item FindItem(byte id)
	{
		foreach (Item item in items)
		{
			if ((byte)item.itemData.itemID == id) return item;
		}
		return null;
	}
	#endregion

	#region Sprite Functions
	private void LoadSpriteDict()
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/voxel-pack/Items");

		foreach (Sprite sprite in sprites)
		{
			if (!dictSprites.ContainsKey(sprite.name)) dictSprites.Add(sprite.name, sprite);
		}
	}
	#endregion

}
