using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : Singleton<GUIManager>
{
	public Image playerInventory;

	private Inventory playerInventoryScript;

	public GameObject slotPrefab;

	private ItemStack cursorStack;
	private GameObject cursorIcon;

	public Hotbar hotbar;

	public bool bShowPlayerInventory = false;

	public Image[] slots;

	public Image greyOut;

	private void Start()
	{
		cursorIcon = GameObject.Instantiate(slotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		cursorIcon.transform.SetParent(gameObject.transform);

		hotbar = GameObject.Find("Hotbar").GetComponent<Hotbar>();
	}

	private void Update()
	{
		if (playerInventoryScript == null)
		{
			playerInventoryScript = GameObject.FindWithTag("Player").GetComponent<Inventory>();
		}

		RenderCursorStack();

		if (bShowPlayerInventory)
		{
			if (Input.GetMouseButtonDown(0))
			{
				for (int i = 0; i < slots.Length; i++)
				{
					if (IsMouseOverSlot(i))
					{
						if (cursorStack == null)
						{
							if (playerInventoryScript.itemStacks[i] != null)
							{
								cursorStack = playerInventoryScript.itemStacks[i];
								playerInventoryScript.itemStacks[i] = null;
							}
							else
							{
								Debug.Log("No item in slot " + i);
							}
						}
					}
				}
			}

			if (Input.GetMouseButtonUp(0))
			{
				for (int i = 0; i < slots.Length; i++)
				{
					if (IsMouseOverSlot(i))
					{
						if (cursorStack != null)
						{
							playerInventoryScript.itemStacks[i] = cursorStack;
							cursorStack = null;
						}
					}
				}
			}

			RenderSlots();
		}
	}

	private void RenderSlots()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			ItemStack itemStack = playerInventoryScript.itemStacks[i];

			if (itemStack != null)
			{
				slots[i].color = new Color(1, 1, 1, 1);
				slots[i].sprite = itemStack.item.sprite;

				slots[i].transform.GetChild(0).GetComponent<Text>().text = itemStack.stackSize.ToString();
			}
			else
			{
				slots[i].color = new Color(1, 1, 1, 0);
				slots[i].sprite = null;

				slots[i].transform.GetChild(0).GetComponent<Text>().text = string.Empty;
			}
		}
	}

	private void RenderCursorStack()
	{
		if (cursorStack != null)
		{
			cursorIcon.transform.position = Input.mousePosition;

			if (cursorIcon.GetComponent<Image>().color.a != 1)
			{
				cursorIcon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
				cursorIcon.GetComponent<Image>().sprite = cursorStack.item.sprite;
				cursorIcon.transform.GetChild(0).GetComponent<Text>().text = cursorStack.stackSize.ToString();
			}
		}
		else
		{
			if (cursorIcon.GetComponent<Image>().color.a != 0)
			{
				cursorIcon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
				cursorIcon.GetComponent<Image>().sprite = null;
				cursorIcon.transform.GetChild(0).GetComponent<Text>().text = "";
			}
		}
	}

	public void ShowPlayerInventory(bool value)
	{
		greyOut.color = value ? new Color(0, 0, 0, 0.75f) : new Color(0, 0, 0, 0);

		playerInventory.gameObject.SetActive(value);
		bShowPlayerInventory = value;
	}

	public bool IsMouseOverSlot(int slotIndex)
	{
		RectTransform rt = slots[slotIndex].GetComponent<RectTransform>();
		if (Input.mousePosition.x > rt.position.x - rt.sizeDelta.x && Input.mousePosition.x < rt.position.x + rt.sizeDelta.x)
		{
			if (Input.mousePosition.y > rt.position.y - rt.sizeDelta.y && Input.mousePosition.y < rt.position.y + rt.sizeDelta.y)
			{
				return true;
			}
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
