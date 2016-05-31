using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

	public cItemData itemData = new cItemData();
	public Sprite sprite;

	public Item() { }

	public Item(Item preset)
	{
		this.itemData = preset.itemData;
		this.sprite = preset.sprite;
	}
}

public class ItemStack
{
	public cItemData itemData;
    public int stackSize;
	public StackLocation_e stackLocation = StackLocation_e.INVENTORY;

	public enum StackLocation_e
	{
		CURSOR,
		INVENTORY,
		HOTBAR,
		EQUIPMENT,
		CRAFTING,
		CRAFTINGRESULT
	}

    public ItemStack(cItemData itemData, int stackSize)
    {
        this.itemData = itemData;
        this.stackSize = stackSize;
    }
}

[System.Serializable]
public class cItemData
{
	public string sItemName;
	public ItemDatabase.ItemID_e itemID;
	public BlockManager.BlockID_e blockID;
	public int iMaxStack = 64;
	public ItemType_e type;

	public enum ItemType_e
	{
		ITEM,
		BLOCK,
		WEAPON,
		ARMOR,
		TOOL,
		FOOD
	}

	public cItemData() { }

	public cItemData(string sItemName, ItemDatabase.ItemID_e itemID, BlockManager.BlockID_e blockID, int iMaxStack, ItemType_e type)
	{
		this.sItemName = sItemName;
		this.itemID = itemID;
		this.blockID = blockID;
		this.iMaxStack = iMaxStack;
		this.type = type;
	}
}