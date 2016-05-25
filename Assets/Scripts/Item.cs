using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    public string itemName;
    public Sprite sprite;
    public int iMaxStack = 64;

	public enum ItemType_e
	{
		ITEM,
		BLOCK,
		WEAPON,
		ARMOR,
		TOOL,
		FOOD
	}

	public ItemType_e type;

	public Item() { }

	public Item(Item preset)
	{
		this.itemName = preset.itemName;
		this.sprite = preset.sprite;
		this.iMaxStack = preset.iMaxStack;
		this.type = preset.type;
	}
}

public class ItemStack
{
    public Item item;
    public int stackSize;

    public ItemStack(Item item, int stackSize)
    {
        this.item = item;
        this.stackSize = stackSize;
    }
}