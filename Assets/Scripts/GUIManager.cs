using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : Singleton<GUIManager>
{
	public GameObject playerInventory;
    public Inventory scrPlayerInventory;

    public GameObject playerHotbar;
    public Hotbar scrPlayerHotbar;

    public GameObject invInventory;
    public GameObject invHotbar;
    public GameObject invEquipment;
	public GameObject invCrafting;
	public GameObject invCraftingResult;

    public Image[] invInventorySlots;
    public Image[] invHotbarSlots;
    public Image[] invEquipmentSlots;
    public Image[] invCraftingSlots;
    public Image[] invCraftingResultSlots;

    public GameObject slotPrefab;

    public bool bShowPlayerInventory = false;

    private ItemStack cursorStack;
	private GameObject cursorIcon;	

	private bool bIsShiftDown = false;
	private bool bIsStrgDown = false;

	//public Image greyOut;

	private void Start()
	{
		cursorIcon = GameObject.Instantiate(slotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		cursorIcon.transform.SetParent(gameObject.transform);
	}

	private void Update()
	{
		if (scrPlayerInventory == null)
		{
			scrPlayerInventory = WorldManager.I.player.GetComponent<Inventory>();
		}

		if(scrPlayerHotbar == null)
		{
			scrPlayerHotbar = playerHotbar.GetComponent<Hotbar>();
		}

		RenderCursorStack();

		if (bShowPlayerInventory)
		{
            if(Input.GetKey(KeyCode.Escape))
            {
                ShowPlayerInventory(false);
            }

			#region Stack moving
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
			{
				UpdateStackMovingDown(invInventorySlots, ref scrPlayerInventory.itemInventoryStacks, ItemStack.StackLocation_e.INVENTORY);
				UpdateStackMovingDown(invHotbarSlots, ref scrPlayerInventory.itemHotbarStacks, ItemStack.StackLocation_e.HOTBAR);
				UpdateStackMovingDown(invEquipmentSlots, ref scrPlayerInventory.itemEquipmentStacks, ItemStack.StackLocation_e.EQUIPMENT);
				UpdateStackMovingDown(invCraftingSlots, ref scrPlayerInventory.itemCraftingStacks, ItemStack.StackLocation_e.CRAFTING);
				UpdateStackMovingDown(invCraftingResultSlots, ref scrPlayerInventory.itemResultStack, ItemStack.StackLocation_e.CRAFTINGRESULT);
			}

			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && !bIsShiftDown && !bIsStrgDown && Input.GetMouseButtonUp(0))
			{
				UpdateStackMovingUp(invInventorySlots, ref scrPlayerInventory.itemInventoryStacks, ItemStack.StackLocation_e.INVENTORY);
				UpdateStackMovingUp(invHotbarSlots, ref scrPlayerInventory.itemHotbarStacks, ItemStack.StackLocation_e.HOTBAR);
				UpdateStackMovingUp(invEquipmentSlots, ref scrPlayerInventory.itemEquipmentStacks, ItemStack.StackLocation_e.EQUIPMENT);
				UpdateStackMovingUp(invCraftingSlots, ref scrPlayerInventory.itemCraftingStacks, ItemStack.StackLocation_e.CRAFTING);
				//UpdateStackMovingUp(invCraftingResultSlots, ref scrPlayerInventory.itemResultStack, ItemStack.StackLocation_e.CRAFTINGRESULT);
			}
			#endregion

			#region SHIFT Stack Splitting
			if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
			{
				bIsShiftDown = true;
				UpdateShiftStackSplittingDown(invInventorySlots, ref scrPlayerInventory.itemInventoryStacks);
				UpdateShiftStackSplittingDown(invHotbarSlots, ref scrPlayerInventory.itemHotbarStacks);
				UpdateShiftStackSplittingDown(invEquipmentSlots, ref scrPlayerInventory.itemEquipmentStacks);
				UpdateShiftStackSplittingDown(invCraftingSlots, ref scrPlayerInventory.itemCraftingStacks);
				UpdateShiftStackSplittingDown(invCraftingResultSlots, ref scrPlayerInventory.itemResultStack);
			}

			if (bIsShiftDown && Input.GetMouseButtonUp(0))
			{
				UpdateShiftStackSplittingUp(invInventorySlots, ref scrPlayerInventory.itemInventoryStacks, ItemStack.StackLocation_e.INVENTORY);
				UpdateShiftStackSplittingUp(invHotbarSlots, ref scrPlayerInventory.itemHotbarStacks, ItemStack.StackLocation_e.HOTBAR);
				UpdateShiftStackSplittingUp(invEquipmentSlots, ref scrPlayerInventory.itemEquipmentStacks, ItemStack.StackLocation_e.EQUIPMENT);
				UpdateShiftStackSplittingUp(invCraftingSlots, ref scrPlayerInventory.itemCraftingStacks, ItemStack.StackLocation_e.CRAFTING);
				//UpdateShiftStackSplittingUp(invCraftingResultSlots, ref scrPlayerInventory.itemResultStack, ItemStack.StackLocation_e.CRAFTINGRESULT);
				bIsShiftDown = false;
			}
			#endregion

			#region STRG Stack Splitting
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
			{
				bIsStrgDown = true;
				UpdateStrgStackSplittingDown(invInventorySlots, ref scrPlayerInventory.itemInventoryStacks);
				UpdateStrgStackSplittingDown(invHotbarSlots, ref scrPlayerInventory.itemHotbarStacks);
				UpdateStrgStackSplittingDown(invEquipmentSlots, ref scrPlayerInventory.itemEquipmentStacks);
				UpdateStrgStackSplittingDown(invCraftingSlots, ref scrPlayerInventory.itemCraftingStacks);
				UpdateStrgStackSplittingDown(invCraftingResultSlots, ref scrPlayerInventory.itemResultStack);
			}

			if (bIsStrgDown && Input.GetMouseButtonUp(0))
			{
				UpdateStrgStackSplittingUp(invInventorySlots, ref scrPlayerInventory.itemInventoryStacks, ItemStack.StackLocation_e.INVENTORY);
				UpdateStrgStackSplittingUp(invHotbarSlots, ref scrPlayerInventory.itemHotbarStacks, ItemStack.StackLocation_e.HOTBAR);
				UpdateStrgStackSplittingUp(invEquipmentSlots, ref scrPlayerInventory.itemEquipmentStacks, ItemStack.StackLocation_e.EQUIPMENT);
				UpdateStrgStackSplittingUp(invCraftingSlots, ref scrPlayerInventory.itemCraftingStacks, ItemStack.StackLocation_e.CRAFTING);
				//UpdateStrgStackSplittingUp(invCraftingResultSlots, ref scrPlayerInventory.itemResultStack, ItemStack.StackLocation_e.CRAFTINGRESULT);
				bIsStrgDown = false;
			}
			#endregion

			#region Slot rendering
			// RENDER INVENTORY
			RenderSlots(invInventorySlots, scrPlayerInventory.itemInventoryStacks);
			// RENDER HOTBAR		
			RenderSlots(invHotbarSlots, scrPlayerInventory.itemHotbarStacks);
			// RENDER EQUIPMENT
			RenderSlots(invEquipmentSlots, scrPlayerInventory.itemEquipmentStacks);
			// RENDER CRAFTING
			RenderSlots(invCraftingSlots, scrPlayerInventory.itemCraftingStacks);
			// RENDER CRAFTINGRESULT
			RenderSlots(invCraftingResultSlots, scrPlayerInventory.itemResultStack);
			#endregion	
		}
	}

	#region ShiftStackSplitting
	private void UpdateShiftStackSplittingDown(Image[] arrSlots, ref ItemStack[] itemStacks)
	{
		for (int i = 0; i < arrSlots.Length; i++)
		{
			if (IsMouseOverSlot(i, arrSlots))
			{
				if (cursorStack == null)
				{
					if (itemStacks[i] != null)
					{
						int splitAmount = (int)(itemStacks[i].stackSize / 2);

						if (splitAmount > 0)
						{
							ItemStack splittedStack = new ItemStack(itemStacks[i].itemData, itemStacks[i].itemData.iMaxStack);
							splittedStack.stackSize = splitAmount;

							cursorStack = splittedStack;
							cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;

							scrPlayerInventory.RemoveItem(splitAmount, ref itemStacks[i]);
						}
					}
					else
					{
						Debug.Log("No item in slot " + i);
					}
				}
			}
		}
	}

	private void UpdateShiftStackSplittingUp(Image[] arrSlots, ref ItemStack[] itemStacks, ItemStack.StackLocation_e stackLocation)
	{
		for (int i = 0; i < arrSlots.Length; i++)
		{
			if (IsMouseOverSlot(i, arrSlots))
			{
				if (cursorStack != null)
				{
                    if (stackLocation == ItemStack.StackLocation_e.EQUIPMENT && cursorStack.itemData.type != cItemData.ItemType_e.ARMOR) return;
                    if (stackLocation == ItemStack.StackLocation_e.CRAFTINGRESULT) return;

                    cursorStack.stackLocation = stackLocation;

					if (itemStacks[i] != null)
					{
						if (itemStacks[i].itemData.itemID == cursorStack.itemData.itemID)
						{
							if (itemStacks[i].stackSize + cursorStack.stackSize <= itemStacks[i].itemData.iMaxStack)
							{
								// Merge Stacks because StackSize is <= maximum
								scrPlayerInventory.AddItem(cursorStack.stackSize, ref itemStacks[i]);
								cursorStack = null;
							}
							else
							{
								int iAvailableAmount = itemStacks[i].itemData.iMaxStack - itemStacks[i].stackSize;
								cursorStack.stackSize -= iAvailableAmount;
								itemStacks[i].stackSize += iAvailableAmount;
								cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
							}
						}
						else
						{
							// Swap Cursorstack and Slotstack because SlotStack and CursorStack are different Items
							ItemStack temp = itemStacks[i];
							itemStacks[i] = cursorStack;
							cursorStack = temp;
							cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
						}
					}
					else
					{
						itemStacks[i] = cursorStack;
						cursorStack = null;
					}
				}
			}
		}
	}
	#endregion

	#region StrgStackSplitting
	private void UpdateStrgStackSplittingDown(Image[] arrSlots, ref ItemStack[] itemStacks)
	{
		for (int i = 0; i < arrSlots.Length; i++)
		{
			if (IsMouseOverSlot(i, arrSlots))
			{
				if (cursorStack == null)
				{
					if (itemStacks[i] != null)
					{
						int splitAmount = 1;

						if (itemStacks[i].stackSize > 1)
						{
							ItemStack splittedStack = new ItemStack(itemStacks[i].itemData, itemStacks[i].itemData.iMaxStack);
							splittedStack.stackSize = splitAmount;

							scrPlayerInventory.RemoveItem(splitAmount, ref itemStacks[i]);

							cursorStack = splittedStack;
							cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;							
						}
						else if(itemStacks[i].stackSize == 1)
						{
							ItemStack splittedStack = new ItemStack(itemStacks[i].itemData, itemStacks[i].itemData.iMaxStack);
							splittedStack.stackSize = splitAmount;

							itemStacks[i] = null;

							cursorStack = splittedStack;
							cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
						}
					}
					else
					{
						Debug.Log("No item in slot " + i);
					}
				}
			}
		}
	}

	private void UpdateStrgStackSplittingUp(Image[] arrSlots, ref ItemStack[] itemStacks, ItemStack.StackLocation_e stackLocation)
	{
		for (int i = 0; i < arrSlots.Length; i++)
		{
			if (IsMouseOverSlot(i, arrSlots))
			{
				if (cursorStack != null)
				{
                    if (stackLocation == ItemStack.StackLocation_e.EQUIPMENT && cursorStack.itemData.type != cItemData.ItemType_e.ARMOR) return;
                    if (stackLocation == ItemStack.StackLocation_e.CRAFTINGRESULT) return;

                    cursorStack.stackLocation = stackLocation;

					if (itemStacks[i] != null)
					{
						if (itemStacks[i].itemData.itemID == cursorStack.itemData.itemID)
						{
							if(cursorStack.stackSize > 1)
							{
								if (itemStacks[i].stackSize + 1 <= itemStacks[i].itemData.iMaxStack)
								{
									scrPlayerInventory.AddItem(1, ref itemStacks[i]);
									cursorStack.stackSize -= 1;
								}
								else
								{
									int iAvailableAmount = itemStacks[i].itemData.iMaxStack - itemStacks[i].stackSize;
									cursorStack.stackSize -= iAvailableAmount;
									itemStacks[i].stackSize += iAvailableAmount;
								}
								cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
							}
							else if(cursorStack.stackSize == 1)
							{
								if (itemStacks[i].stackSize + 1 <= itemStacks[i].itemData.iMaxStack)
								{
									scrPlayerInventory.AddItem(1, ref itemStacks[i]);
									cursorStack = null;
								}
								else
								{
									// Swap Cursorstack and Slotstack because SlotStack is already at MaxStackSize
									ItemStack temp = itemStacks[i];
									itemStacks[i] = cursorStack;
									cursorStack = temp;
									cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
								}
							}							
						}
						else
						{
							// Swap Cursorstack and Slotstack because SlotStack and CursorStack are different Items
							ItemStack temp = itemStacks[i];
							itemStacks[i] = cursorStack;
							cursorStack = temp;
							cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
						}
					}
					else
					{
						itemStacks[i] = cursorStack;
						cursorStack = null;
					}
				}
			}
		}
	}
	#endregion

	#region StackMoving
	private void UpdateStackMovingDown(Image[] arrSlots, ref ItemStack[] itemStacks, ItemStack.StackLocation_e stackLocation)
	{
		for (int i = 0; i < arrSlots.Length; i++)
		{
			if (IsMouseOverSlot(i, arrSlots))
			{
				if (cursorStack == null)
				{
					if (itemStacks[i] != null)
					{
						EventPreStackMoving(ref itemStacks, stackLocation);

						cursorStack = itemStacks[i];
						cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
						itemStacks[i] = null;

						EventPostStackMoving(ref itemStacks, stackLocation);						
					}
					else
					{
						Debug.Log("No item in slot " + i);
					}
				}
			}
		}
	}

	private void UpdateStackMovingUp(Image[] arrSlots, ref ItemStack[] itemStacks, ItemStack.StackLocation_e stackLocation)
	{
		for (int i = 0; i < arrSlots.Length; i++)
		{
			if (IsMouseOverSlot(i, arrSlots))
			{
				if (cursorStack != null)
				{
                    if (stackLocation == ItemStack.StackLocation_e.EQUIPMENT && cursorStack.itemData.type != cItemData.ItemType_e.ARMOR) return;
                    if (stackLocation == ItemStack.StackLocation_e.CRAFTINGRESULT) return;

                    cursorStack.stackLocation = stackLocation;

					if (itemStacks[i] == null)
					{
						itemStacks[i] = cursorStack;
						cursorStack = null;
					}
					else
					{
						if (itemStacks[i].itemData.itemID == cursorStack.itemData.itemID)
						{
							if (itemStacks[i].stackSize + cursorStack.stackSize <= itemStacks[i].itemData.iMaxStack)
							{
								itemStacks[i].stackSize += cursorStack.stackSize;
								cursorStack = null;
							}
							else
							{
								int iAvailableAmount = itemStacks[i].itemData.iMaxStack - itemStacks[i].stackSize;
								cursorStack.stackSize -= iAvailableAmount;
								itemStacks[i].stackSize += iAvailableAmount;
								cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
							}
						}
						else
						{
							// Swap Cursorstack and Slotstack because SlotStack and CursorStack are different Items
							ItemStack temp = itemStacks[i];
							itemStacks[i] = cursorStack;
							cursorStack = temp;
							cursorStack.stackLocation = ItemStack.StackLocation_e.CURSOR;
						}
					}
				}
			}
		}
	}

	private void EventPreStackMoving(ref ItemStack[] itemStack, ItemStack.StackLocation_e stackLocation)
	{
		if (stackLocation == ItemStack.StackLocation_e.CRAFTINGRESULT && GUIManager.I.scrPlayerInventory.itemResultStack[0] != null)
		{
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[0] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[0]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[0] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[0], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[1] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[1]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[1] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[1], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[2] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[2]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[2] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[2], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[3] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[3]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[3] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[3], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[4] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[4]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[4] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[4], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[5] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[5]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[5] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[5], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[6] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[6]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[6] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[6], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[7] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[7]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[7] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[7], ItemStack.StackLocation_e.INVENTORY);
				}
			}
			if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[8] != null)
			{
				GUIManager.I.scrPlayerInventory.RemoveItem(1, ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[8]);
				if (GUIManager.I.scrPlayerInventory.itemCraftingStacks[8] != null)
				{
					ItemDatabase.I.MoveItemStack(ref GUIManager.I.scrPlayerInventory.itemCraftingStacks[8], ItemStack.StackLocation_e.INVENTORY);
				}
			}
		}
	}

	private void EventPostStackMoving(ref ItemStack[] itemStack, ItemStack.StackLocation_e stackLocation)
	{

	}
	#endregion

	private void RenderSlots(Image[] arr, ItemStack[] itemStacks)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			ItemStack itemStack = itemStacks[i];

			if (itemStack != null)
			{
				if (arr[i] != null)
				{
                    Image slotSockel = arr[i].transform.GetChild(0).GetComponent<Image>();
                    if (slotSockel != null)
                    {
                        slotSockel.color = new Color(1, 1, 1, 1);
                        slotSockel.sprite = ItemDatabase.I.FindItem(itemStack.itemData.itemID).sprite;

                        arr[i].transform.GetChild(1).GetComponent<Text>().text = itemStack.stackSize.ToString();
                    }
				}
			}
			else
			{
				if (arr[i] != null)
				{
                    Image slotSockel = arr[i].transform.GetChild(0).GetComponent<Image>();
                    if (slotSockel != null)
                    {
                        slotSockel.color = new Color(1, 1, 1, 0);
                        slotSockel.sprite = null;

                        arr[i].transform.GetChild(1).GetComponent<Text>().text = string.Empty;
                    }
				}
			}
		}
	}

	private void RenderCursorStack()
	{
        Image cursorSlot = cursorIcon.transform.GetChild(0).GetComponent<Image>();
        Text cursorText = cursorIcon.transform.GetChild(1).GetComponent<Text>();

        cursorIcon.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        if (cursorStack != null)
		{
            Vector3 newPos = new Vector3(Input.mousePosition.x - 32, Input.mousePosition.y, Input.mousePosition.z);
			cursorIcon.transform.position = newPos;            

			if (cursorSlot.color.a != 1)
			{
                
                cursorSlot.color = new Color(1, 1, 1, 1);
                cursorSlot.sprite = ItemDatabase.I.FindItem(cursorStack.itemData.itemID).sprite;
                cursorText.text = cursorStack.stackSize.ToString();
			}
		}
		else
		{
            cursorIcon.transform.position = Vector3.zero;

			if (cursorSlot.color.a != 0)
			{
                cursorSlot.color = new Color(1, 1, 1, 0);
                cursorSlot.sprite = null;
                cursorText.text = "";
			}
		}
	}

	public void ShowPlayerInventory(bool value)
	{
		//greyOut.color = value ? new Color(0, 0, 0, 0.75f) : new Color(0, 0, 0, 0);

		playerInventory.SetActive(value);
		bShowPlayerInventory = value;
	}

	public bool IsMouseOverSlot(int slotIndex, Image[] arrSlots)
	{
		RectTransform rt = arrSlots[slotIndex].GetComponent<RectTransform>();

		Vector2 mousePosition = Input.mousePosition;
		Vector3[] worldCorners = new Vector3[4];
		rt.GetWorldCorners(worldCorners);

		if (mousePosition.x >= worldCorners[0].x && mousePosition.x < worldCorners[2].x && mousePosition.y >= worldCorners[0].y && mousePosition.y < worldCorners[2].y)
		{
			return true;
		}
		return false;
	}

	public void LoadSingleplayer()
	{
		AsynchronousLoad("world");
	}

	IEnumerator AsynchronousLoad(string scene)
	{
		yield return null;

		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = false;

		while (!ao.isDone)
		{
			// [0, 0.9] > [0, 1]
			float progress = Mathf.Clamp01(ao.progress / 0.9f);
			Debug.Log("Loading progress: " + (progress * 100) + "%");

			// Loading completed
			if (ao.progress == 0.9f)
			{
				Debug.Log("Press a key to start");
				if (Input.anyKey)
					ao.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}
