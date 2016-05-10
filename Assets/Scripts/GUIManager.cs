using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Image playerInventory;
	public bool bShowPlayerInventory = false;

    public Image[] slots;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            for(int i=0; i< slots.Length; i++)
            {
                if (IsMouseOverSlot(i))
                {
                    Debug.Log("Clicked mouse over slot" + i);
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
