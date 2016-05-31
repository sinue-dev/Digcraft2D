using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour {

	public int selectedSlot = 1;
	public Image[] hotbarSlots;
	public Image selector;

	private void Update()
	{
		if(GUIManager.I.scrPlayerInventory == null)
		{
			return;
		}

		UpdateScrolling();
		UpdateSelector();
		UpdateItems();
	}

	private void UpdateScrolling()
	{
		selectedSlot -= (int)Input.mouseScrollDelta.y;

		if(selectedSlot < 1)
		{
			selectedSlot = 9;
		}

		if(selectedSlot > 9)
		{
			selectedSlot = 1;
		}
	}

	private void UpdateSelector()
	{
        Vector3 pos = GUIManager.I.invHotbarSlots[selectedSlot - 1].rectTransform.localPosition;
        selector.rectTransform.localPosition = new Vector3(pos.x - 306, pos.y, pos.z);
		WorldManager.I.player.GetComponent<Player>().HoldItem(GUIManager.I.invHotbarSlots[selectedSlot - 1].transform.GetChild(0).GetComponent<Image>().sprite);
	}

	private void UpdateItems()
	{
		for(int i = 0; i < GUIManager.I.invHotbarSlots.Length; i++)
		{
			Image hotbarSlot = hotbarSlots[i].transform.GetChild(0).GetComponent<Image>();
			ItemStack invSlot = (GUIManager.I.scrPlayerInventory != null) ? GUIManager.I.scrPlayerInventory.itemHotbarStacks[i] : null;

			if(invSlot != null)
			{
				hotbarSlot.color = new Color(1, 1, 1, 1);
				hotbarSlot.sprite = ItemDatabase.I.FindItem(invSlot.itemData.itemID).sprite;
                hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = invSlot.stackSize.ToString();
			}
			else
			{
				hotbarSlot.color = new Color(1, 1, 1, 0);
				hotbarSlot.sprite = null;
                hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
			}
		}
	}

	public Item GetHeldItem()
	{
		int selSlot = selectedSlot -1;

		if(GUIManager.I.scrPlayerInventory.itemHotbarStacks[selSlot] != null)
		{
			return ItemDatabase.I.FindItem(GUIManager.I.scrPlayerInventory.itemHotbarStacks[selSlot].itemData.itemID);
		}
		return null;
	}
}
