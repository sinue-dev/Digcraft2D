﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour {

	public int selectedSlot = 1;
	public Image[] slots;
	public Image selector;
	private Inventory playerInv;

	private void Update()
	{
		if(playerInv == null)
		{
			playerInv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
			return;
		}

		UpdateScrolling();
		UpdateSelector();
		UpdateItems();
	}

	private void UpdateScrolling()
	{
		selectedSlot += (int)Input.mouseScrollDelta.y;

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
		selector.rectTransform.localPosition = slots[selectedSlot - 1].rectTransform.localPosition;

		playerInv.gameObject.GetComponent<Player>().HoldItem(slots[selectedSlot - 1].sprite);
	}

	private void UpdateItems()
	{
		for(int i = 0; i < 9; i++)
		{
			Image hotbarSlot = slots[8 - i];
			ItemStack invSlot = playerInv.itemStacks[i + 27];

			if(invSlot != null)
			{
				hotbarSlot.sprite = invSlot.item.sprite;
				hotbarSlot.color = new Color(1, 1, 1, 1);
				hotbarSlot.transform.FindChild("SlotText").GetComponent<Text>().text = invSlot.stackSize.ToString();
			}
			else
			{
				hotbarSlot.sprite = null;
				hotbarSlot.color = new Color(1, 1, 1, 0);
				hotbarSlot.transform.FindChild("SlotText").GetComponent<Text>().text = "";
			}
		}
	}
}
