using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Image playerInventory;

    private Inventory playerInventoryScript;

	public bool bShowPlayerInventory = false;

    public Image[] slots;

    private void Update()
    {
        if(playerInventoryScript == null)
        {
            playerInventoryScript = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        }

        if (bShowPlayerInventory)
        {
            if (Input.GetMouseButtonDown(1))
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (IsMouseOverSlot(i))
                    {
                        if (playerInventoryScript.itemStacks[i] != null)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }

            RenderSltos();
        }
    }

    private void RenderSltos()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            ItemStack itemStack = playerInventoryScript.itemStacks[i];

            if(itemStack != null)
            {
                if(slots[i].color.a != 1)
                {
                    slots[i].color = new Color(1, 1, 1, 1);
                    slots[i].sprite = itemStack.item.sprite;

                    slots[i].transform.GetChild(0).GetComponent<Text>().text = itemStack.stackSize.ToString();
                }                
            }
            else
            {
                if (slots[i].color.a != 0)
                {
                    slots[i].color = new Color(1, 1, 1, 0);
                    slots[i].sprite = null;

                    slots[i].transform.GetChild(0).GetComponent<Text>().text = string.Empty;
                }
            }
        }
    }

    public void ShowPlayerInventory(bool value)
	{
		playerInventory.gameObject.SetActive(value);
		bShowPlayerInventory = value;
	}

    public bool IsMouseOverSlot(int slotIndex)
    {
        RectTransform rt = slots[slotIndex].GetComponent<RectTransform>();
        if(Input.mousePosition.x > rt.position.x - rt.sizeDelta.x * 1.5f && Input.mousePosition.x < rt.position.x + rt.sizeDelta.x *1.5f)
        {
            if (Input.mousePosition.y > rt.position.y - rt.sizeDelta.y * 1.5f && Input.mousePosition.y < rt.position.y + rt.sizeDelta.y * 1.5f)
            {
                return true;
            }
        }
        return false;
    }
}
