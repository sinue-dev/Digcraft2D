using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public int slotCount = 3 * 9;
    public ItemStack[] itemStacks;

    private ItemDatabase itemDatabase;

    private void Start()
    {
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
        itemStacks = new ItemStack[slotCount];
        itemStacks[4] = new ItemStack(itemDatabase.FindItem("Dirt"), 64);
    }

    public void AddItem(string sName, int iCount)
    {
        ItemStack existingStack = FindExistingStack(sName, iCount);

        if(existingStack != null)
        {
            existingStack.stackSize += iCount;
        }
        else
        {
            if(FindFirstAvailableSlot() >= 0)
            {
                int availableSlot = FindFirstAvailableSlot();
                itemStacks[availableSlot] = new ItemStack(itemDatabase.FindItem(name), iCount);
                //itemStacks.Add(, iCount));
            }
            else
            {
                Debug.LogError("Inventar voll");
            }
        }
    }

    public void RemoveItem(string sName, int iCount)
    {
        ItemStack existingStack = FindExistingStack(sName, iCount);

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

    private ItemStack FindExistingStack(string sName)
    {
        foreach(ItemStack i in itemStacks)
        {
            if(i.item.itemName == name)
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

    private ItemStack FindExistingStack(string sName, int iCount)
    {
        foreach (ItemStack i in itemStacks)
        {
            if (i.item.itemName == name)
            {
                if(i.stackSize + iCount <= i.item.iMaxStack)
                {
                    return i;
                }
            }
        }
        return null;
    }

}
