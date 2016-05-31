using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ItemDatabase : Singleton<ItemDatabase> {

	private List<Item> items = new List<Item>();
	private List<CraftingRecipe> craftingItems = new List<CraftingRecipe>();

	public Dictionary<string, Sprite> dictSprites = new Dictionary<string, Sprite>();

	private bool bRecipeMatched = false;

	public enum ItemID_e
	{
		AIR = 0,
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
		TRUNK_WHITE_SIDE = 24,
		WOOD = 25,
		WOOD_WHITE = 26,
		STICK = 27,
		STICK_WHITE = 28,
		GRASS4 = 29,

        //### Items
        COAL = 100,
        COPPER = 101,
        IRON = 102,
        SILVER = 103,
        GOLD = 104,
        DIAMOND = 105,
        ORE_COAL = 106,
        ORE_COPPER = 107,
        ORE_IRON = 108,
        ORE_SILVER = 109,
        ORE_GOLD = 110,
        ORE_DIAMOND = 111,
        APPLE = 112,
        SEED = 113,
        WHEAT = 114,

        // Weapons and Armor
        HAMMER_COPPER = 200,
		HAMMER_IRON = 201,
		HAMMER_SILVER = 202,
		HAMMER_GOLD = 203,
		HAMMER_DIAMOND = 204,
		AXE_COPPER = 205,
		AXE_IRON = 206,
		AXE_SILVER = 207,
		AXE_GOLD = 208,
		AXE_DIAMOND = 209,
		HOE_COPPER = 210,
		HOE_IRON = 211,
		HOE_SILVER = 212,
		HOE_GOLD = 213,
		HOE_DIAMOND = 214,
		PICK_COPPER = 215,
		PICK_IRON = 216,
		PICK_SILVER = 217,
		PICK_GOLD = 218,
		PICK_DIAMOND = 219,
		SHOVEL_COPPER = 220,
		SHOVEL_IRON = 221,
		SHOVEL_SILVER = 222,
		SHOVEL_GOLD = 223,
		SHOVEL_DIAMOND = 224,
		FLAIL_COPPER = 225,
		FLAIL_IRON = 226,
		FLAIL_SILVER = 227,
		FLAIL_GOLD = 228,
		FLAIL_DIAMOND = 229,
		SWORD_COPPER = 230,
		SWORD_IRON = 231,
		SWORD_SILVER = 232,
		SWORD_GOLD = 233,
		SWORD_DIAMOND = 234,
		BOW = 235,
		ARROW = 236,
	}

	private void Awake()
	{
		LoadSpriteDict();

		AddItemToDatabase("Dirt", ItemID_e.DIRT, BlockManager.BlockID_e.DIRT, BlockManager.I.dictSprites["dirt"], 64, cItemData.ItemType_e.BLOCK);
		AddItemToDatabase("GravelDirt", ItemID_e.GRAVEL_DIRT, BlockManager.BlockID_e.GRAVEL_DIRT, BlockManager.I.dictSprites["gravel_dirt"], 64, cItemData.ItemType_e.BLOCK);
		AddItemToDatabase("GravelStone", ItemID_e.GRAVEL_STONE, BlockManager.BlockID_e.GRAVEL_STONE, BlockManager.I.dictSprites["gravel_stone"], 64, cItemData.ItemType_e.BLOCK);
		AddItemToDatabase("Seed", ItemID_e.SEED, BlockManager.BlockID_e.AIR, dictSprites["seed"], 64, cItemData.ItemType_e.FOOD);
		AddItemToDatabase("Copper", ItemID_e.ORE_COPPER, BlockManager.BlockID_e.AIR, dictSprites["ore_copper"], 64, cItemData.ItemType_e.ITEM);
		AddItemToDatabase("Stick", ItemID_e.STICK, BlockManager.BlockID_e.AIR, dictSprites["stick"], 64, cItemData.ItemType_e.ITEM);
		AddItemToDatabase("ShovelCopper", ItemID_e.SHOVEL_COPPER, BlockManager.BlockID_e.AIR, dictSprites["shovel_copper"], 1, cItemData.ItemType_e.TOOL);

        AddItemToDatabase("Coal", ItemID_e.COAL, BlockManager.BlockID_e.AIR, dictSprites["ore_coal"], 64, cItemData.ItemType_e.ITEM);
        AddItemToDatabase("Iron", ItemID_e.IRON, BlockManager.BlockID_e.AIR, dictSprites["ore_iron"], 64, cItemData.ItemType_e.ITEM);
        AddItemToDatabase("Copper", ItemID_e.COPPER, BlockManager.BlockID_e.AIR, dictSprites["ore_copper"], 64, cItemData.ItemType_e.ITEM);
        AddItemToDatabase("Silver", ItemID_e.SILVER, BlockManager.BlockID_e.AIR, dictSprites["ore_silver"], 64, cItemData.ItemType_e.ITEM);
        AddItemToDatabase("Gold", ItemID_e.GOLD, BlockManager.BlockID_e.AIR, dictSprites["ore_gold"], 64, cItemData.ItemType_e.ITEM);
        AddItemToDatabase("Diamond", ItemID_e.DIAMOND, BlockManager.BlockID_e.AIR, dictSprites["ore_diamond"], 64, cItemData.ItemType_e.ITEM);

        AddCraftingRecipeToDatabase(ItemID_e.SHOVEL_COPPER, new object[9] { null, ItemID_e.ORE_COPPER, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.SHOVEL_IRON, new object[9] { null, ItemID_e.ORE_IRON, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.SHOVEL_SILVER, new object[9] { null, ItemID_e.ORE_SILVER, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.SHOVEL_GOLD, new object[9] { null, ItemID_e.ORE_GOLD, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.SHOVEL_DIAMOND, new object[9] { null, ItemID_e.ORE_DIAMOND, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.AXE_COPPER, new object[9] { ItemID_e.ORE_COPPER, ItemID_e.ORE_COPPER, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.AXE_IRON, new object[9] { ItemID_e.ORE_IRON, ItemID_e.ORE_IRON, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.AXE_SILVER, new object[9] { ItemID_e.ORE_SILVER, ItemID_e.ORE_SILVER, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.AXE_GOLD, new object[9] { ItemID_e.ORE_GOLD, ItemID_e.ORE_GOLD, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.AXE_DIAMOND, new object[9] { ItemID_e.ORE_DIAMOND, ItemID_e.ORE_DIAMOND, null, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.PICK_COPPER, new object[9] { ItemID_e.ORE_COPPER, ItemID_e.ORE_COPPER, ItemID_e.ORE_COPPER, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.PICK_IRON, new object[9] { ItemID_e.ORE_IRON, ItemID_e.ORE_IRON, ItemID_e.ORE_IRON, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.PICK_SILVER, new object[9] { ItemID_e.ORE_SILVER, ItemID_e.ORE_SILVER, ItemID_e.ORE_SILVER, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.PICK_GOLD, new object[9] { ItemID_e.ORE_GOLD, ItemID_e.ORE_GOLD, ItemID_e.ORE_GOLD, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
		AddCraftingRecipeToDatabase(ItemID_e.PICK_DIAMOND, new object[9] { ItemID_e.ORE_DIAMOND, ItemID_e.ORE_DIAMOND, ItemID_e.ORE_DIAMOND, null, ItemID_e.STICK, null, null, ItemID_e.STICK, null });
	}

	#region Item Functions
	public void UpdateCrafting(ItemStack[] craftingStacks)
	{
		if (craftingItems.Count == 0) return;

		if (!bRecipeMatched)
		{
			foreach (CraftingRecipe recipe in craftingItems)
			{
				bool[] m = new bool[9] { false, false, false, false, false, false, false, false, false };

				int index = 0;
				foreach (ItemStack craftingStack in craftingStacks)
				{
					if (recipe.materials[index] == null && craftingStack == null)
					{
						m[index] = true;
					}
					else if (recipe.materials[index] == null && craftingStack != null)
					{
						m[index] = false;
					}
					else if (recipe.materials[index] != null && craftingStack == null)
					{
						m[index] = false;
					}
					else
					{
						if ((ItemDatabase.ItemID_e)recipe.materials[index] == craftingStack.itemData.itemID)
						{
							m[index] = true;
						}
					}
					index++;
				}

				if (m[0] && m[1] && m[2] && m[3] && m[4] && m[5] && m[6] && m[7] && m[8])
				{
					bRecipeMatched = true;
					GUIManager.I.scrPlayerInventory.AddItem(recipe.itemID, 1, ref GUIManager.I.scrPlayerInventory.itemResultStack);
					break;
				}
			}
		}
		else
		{
            if (GUIManager.I.scrPlayerInventory.itemResultStack[0] == null || !bRecipeMatched) return;

			CraftingRecipe matchedRecipe = FindCraftingRecipe(GUIManager.I.scrPlayerInventory.itemResultStack[0].itemData.itemID);
			
			// Monitor changes
			bool[] m = new bool[9] { false, false, false, false, false, false, false, false, false };

			int index = 0;
			foreach (ItemStack craftingStack in craftingStacks)
			{
				if (matchedRecipe.materials[index] == null && craftingStack == null)
				{
					m[index] = true;
				}
				else if (matchedRecipe.materials[index] == null && craftingStack != null)
				{
					m[index] = false;
				}
				else if (matchedRecipe.materials[index] != null && craftingStack == null)
				{
					m[index] = false;
				}
				else
				{
					if ((ItemDatabase.ItemID_e)matchedRecipe.materials[index] == craftingStack.itemData.itemID)
					{
						m[index] = true;
					}
				}
				index++;
			}

			if (!m[0] || !m[1] || !m[2] || !m[3] || !m[4] || !m[5] || !m[6] || !m[7] || !m[8])
			{
				if (GUIManager.I.scrPlayerInventory.itemResultStack[0] != null)
				{
					GUIManager.I.scrPlayerInventory.itemResultStack[0] = null;
				}
				bRecipeMatched = false;
			}

		}
	}

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

	public void AddCraftingRecipeToDatabase(ItemID_e itemID, object[] materials)
	{
		CraftingRecipe preset;
		preset = new CraftingRecipe();
		preset.itemID = itemID;
		preset.materials = materials;
		craftingItems.Add(preset);
	}

	public void MoveItemStack(ref ItemStack itemStack, ItemStack.StackLocation_e newLocation)
	{
		switch(newLocation)
		{
			case ItemStack.StackLocation_e.INVENTORY:
				ItemStack temp = itemStack;
				itemStack = null;
				GUIManager.I.scrPlayerInventory.AddItem(temp.itemData.itemID, temp.stackSize, ref GUIManager.I.scrPlayerInventory.itemInventoryStacks);
				
				break;
			case ItemStack.StackLocation_e.HOTBAR:

				break;
			case ItemStack.StackLocation_e.EQUIPMENT:

				break;
			case ItemStack.StackLocation_e.CRAFTING:

				break;
			case ItemStack.StackLocation_e.CRAFTINGRESULT:

				break;
			case ItemStack.StackLocation_e.CURSOR:

				break;
		}
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

	public CraftingRecipe FindCraftingRecipe(ItemID_e id)
	{
		foreach (CraftingRecipe recipe in craftingItems)
		{
			if (recipe.itemID == id) return recipe;
		}
		return null;
	}

	public CraftingRecipe FindCraftingRecipe(byte id)
	{
		foreach (CraftingRecipe recipe in craftingItems)
		{
			if ((byte)recipe.itemID == id) return recipe;
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

public class CraftingRecipe
{
	public ItemDatabase.ItemID_e itemID { get; set; }
	public object[] materials{ get; set; }
}
