﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    public string itemName;
    public Sprite sprite;
    public int iMaxStack = 64;
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