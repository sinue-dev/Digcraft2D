using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ItemDatabase : Singleton<ItemDatabase> {

	private List<Item> items = new List<Item>();
	public Dictionary<string, Sprite> dictSprites = new Dictionary<string, Sprite>();

	private void Start()
	{
		LoadSpriteDict();

		Item preset;
		preset = new Item();
		preset.itemName = "Grass";
		preset.sprite = dictSprites["grass_side"];
		preset.iMaxStack = 64;
		preset.type = Item.ItemType_e.BLOCK;
		items.Add(preset);

		preset = new Item();
		preset.itemName = "Dirt";
		preset.sprite = dictSprites["dirt"];
		preset.iMaxStack = 64;
		preset.type = Item.ItemType_e.BLOCK;
		items.Add(preset);

		preset = new Item();
		preset.itemName = "Stone";
		preset.sprite = dictSprites["stone"];
		preset.iMaxStack = 64;
		preset.type = Item.ItemType_e.BLOCK;
		items.Add(preset);
	}

	#region Item Functions
    public Item FindItem(string name)
    {
        foreach(Item item in items)
        {
            if (item.itemName == name) return item;
        }
        return null;
    }
	#endregion

	#region Sprite Functions
	private void LoadSpriteDict()
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");

		foreach (Sprite sprite in sprites)
		{
			if (!dictSprites.ContainsKey(sprite.name)) dictSprites.Add(sprite.name, sprite);
		}
	}
	#endregion

}
