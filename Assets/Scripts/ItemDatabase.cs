using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : Singleton<ItemDatabase> {

    public List<Item> items;

    public Item FindItem(string name)
    {
        foreach(Item item in items)
        {
            if (item.itemName == name) return item;
        }
        return null;
    }
}
