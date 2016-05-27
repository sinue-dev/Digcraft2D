using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public int slotCount = 3 * 9;
    public ItemStack[] itemStacks;

    private void Start()
    {
        itemStacks = new ItemStack[slotCount];
        itemStacks[4] = new ItemStack(ItemDatabase.I.FindItem(ItemDatabase.ItemID_e.DIRT).itemData, 64);
    }

    public void AddItem(ItemDatabase.ItemID_e id, int iCount)
    {
        ItemStack existingStack = FindExistingStackToAdd(id, iCount);

        if(existingStack != null)
        {
            existingStack.stackSize += iCount;
        }
        else
        {
            if(FindFirstAvailableSlot() >= 0)
            {
                int availableSlot = FindFirstAvailableSlot();
                itemStacks[availableSlot] = new ItemStack(ItemDatabase.I.FindItem(id).itemData, iCount);
                //itemStacks.Add(, iCount));
            }
            else
            {
                Debug.LogError("Inventar voll");
            }
        }
    }

    public void RemoveItem(ItemDatabase.ItemID_e id, int iCount)
    {
        ItemStack existingStack = FindExistingStack(id, iCount);

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

    private ItemStack FindExistingStack(ItemDatabase.ItemID_e id)
    {
        foreach(ItemStack i in itemStacks)
        {
            if(i.itemData.itemID == id)
            {
                return i;
            }
        }
        return null;
    }

    private int FindFirstAvailableSlot()
    {
        for(int i = 0; i < itemStacks.Length;i++)
        {
            if(itemStacks[i] == null)
            {
				return i;
            }
        }
        return -1;
    }

    private ItemStack FindExistingStackToAdd(ItemDatabase.ItemID_e id, int iCount)
    {
        foreach (ItemStack i in itemStacks)
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

	private ItemStack FindExistingStack(ItemDatabase.ItemID_e id, int iCount)
	{
		foreach (ItemStack i in itemStacks)
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
