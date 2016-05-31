using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    //public int slotCount = 3 * 9;
    //public ItemStack[] itemStacks;

	public ItemStack[] itemInventoryStacks;
	public ItemStack[] itemHotbarStacks;
	public ItemStack[] itemEquipmentStacks;
	public ItemStack[] itemCraftingStacks;
	public ItemStack[] itemResultStack;

    private void Start()
    {
		//itemStacks = new ItemStack[slotCount];

		itemInventoryStacks = new ItemStack[GUIManager.I.invInventorySlots.Length];
		itemHotbarStacks = new ItemStack[GUIManager.I.invHotbarSlots.Length];
		itemEquipmentStacks = new ItemStack[GUIManager.I.invEquipmentSlots.Length];
		itemCraftingStacks = new ItemStack[GUIManager.I.invCraftingSlots.Length];
		itemResultStack = new ItemStack[GUIManager.I.invCraftingResultSlots.Length];

		itemInventoryStacks[4] = new ItemStack(ItemDatabase.I.FindItem(ItemDatabase.ItemID_e.DIRT).itemData, 63);
		itemInventoryStacks[0] = new ItemStack(ItemDatabase.I.FindItem(ItemDatabase.ItemID_e.ORE_COPPER).itemData, 64);
		itemInventoryStacks[1] = new ItemStack(ItemDatabase.I.FindItem(ItemDatabase.ItemID_e.STICK).itemData, 64);
		itemInventoryStacks[2] = new ItemStack(ItemDatabase.I.FindItem(ItemDatabase.ItemID_e.STICK).itemData, 55);
	}

	private void Update()
	{
		ItemDatabase.I.UpdateCrafting(itemCraftingStacks);
	}

    public void AddItem(ItemDatabase.ItemID_e id, int iCount, ref ItemStack[] itemStack)
    {
        ItemStack existingStack = FindExistingStackToAdd(id, iCount, itemStack);

        if(existingStack != null)
        {
            existingStack.stackSize += iCount;
        }
        else
        {
			int availableSlot = FindFirstAvailableSlot(itemStack);
			if (availableSlot >= 0)
            {               
				itemStack[availableSlot] = new ItemStack(ItemDatabase.I.FindItem(id).itemData, iCount);
            }
            else
            {
                Debug.LogError("Inventar voll");
            }
        }
    }

	public void AddItem(Item item, int iCount, ref ItemStack[] itemStack)
	{
		if (item == null) return;

		ItemDatabase.ItemID_e id = item.itemData.itemID;
		ItemStack existingStack = FindExistingStackToAdd(id, iCount, itemStack);

		if (existingStack != null)
		{
			existingStack.stackSize += iCount;
		}
		else
		{
			int availableSlot = FindFirstAvailableSlot(itemStack);
			if (availableSlot >= 0)
			{
				itemStack[availableSlot] = new ItemStack(ItemDatabase.I.FindItem(id).itemData, iCount);
			}
			else
			{
				Debug.LogError("Inventar voll");
			}
		}
	}

	public bool AddItem(int iCount, ref ItemStack itemStack)
	{
		if (itemStack != null)
		{
			itemStack.stackSize += iCount;
			return true;
		}
		else
		{
			return false;
		}
	}

	public void RemoveItem(ItemDatabase.ItemID_e id, int iCount, ref ItemStack[] itemStack)
    {
        ItemStack existingStack = FindExistingStack(id, iCount, itemStack);

        if (existingStack != null)
        {
            if (existingStack.stackSize - iCount >= 1)
            {
                existingStack.stackSize -= iCount;
            }
            else
            {
                existingStack = null;
            }
        }
    }

	public void RemoveItem(Item item, int iCount, ref ItemStack[] itemStack)
	{
		if (item == null) return;

		ItemDatabase.ItemID_e id = item.itemData.itemID;
		ItemStack existingStack = FindExistingStack(id, iCount, itemStack);

		if (existingStack != null)
		{
			if (existingStack.stackSize - iCount >= 1)
			{
				existingStack.stackSize -= iCount;
			}
			else
			{
				existingStack = null;
			}
		}
	}

	public void RemoveItem(int iCount, ref ItemStack itemStack)
	{
		if (itemStack != null)
		{
			if (itemStack.stackSize - iCount >= 1)
			{
				itemStack.stackSize -= iCount;
			}
			else
			{
				itemStack = null;
			}
		}
	}

	private int FindFirstAvailableSlot(ItemStack[] itemStack)
    {
        for(int i = 0; i < itemStack.Length;i++)
        {
            if(itemStack[i] == null)
            {
				return i;
            }
        }
        return -1;
    }

    private ItemStack FindExistingStackToAdd(ItemDatabase.ItemID_e id, int iCount, ItemStack[] itemStack)
    {
        foreach (ItemStack i in itemStack)
        {
			if (i != null)
			{
				if (i.itemData.itemID == id)
				{
					if (i.stackSize + iCount <= i.itemData.iMaxStack)
					{
						return i;
					}
				}
			}
        }
        return null;
    }

	private ItemStack FindExistingStack(ItemDatabase.ItemID_e id, ItemStack[] itemStack)
	{
		foreach (ItemStack i in itemStack)
		{
			if (i.itemData.itemID == id)
			{
				return i;
			}
		}
		return null;
	}

	private ItemStack FindExistingStack(ItemDatabase.ItemID_e id, int iCount, ItemStack[] itemStack)
	{
		foreach (ItemStack i in itemStack)
		{
			if (i != null)
			{
				if (i.itemData.itemID == id)
				{
					return i;
				}
			}
		}
		return null;
	}
}
