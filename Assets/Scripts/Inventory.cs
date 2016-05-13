using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public int slotCount = 3 * 9;
    public List<ItemStack> itemStacks;

    private ItemDatabase itemDatabase;

    private void Start()
    {
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
        itemStacks = new List<ItemStack>();
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
            if(itemStacks.Count < slotCount)
            {
                itemStacks.Add(new ItemStack(itemDatabase.FindItem(sName), iCount));
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
                itemStacks.Remove(existingStack);
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
