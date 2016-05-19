using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    public string itemName;
    public Sprite sprite;
    public int iMaxStack = 64;

	public enum Type
	{
		ITEM,
		BLOCK,
		WEAPON,
		ARMOR,
		TOOL,
		FOOD
	}

	public Type type;
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