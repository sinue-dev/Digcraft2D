using UnityEngine;
using System.Collections;

public class PickUpDrops : MonoBehaviour {

	private Inventory connectedInv;

	private void Start()
	{
		connectedInv = transform.parent.GetComponent<Inventory>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer == 9)
		{
			ItemInfo itemInfo = other.gameObject.GetComponent<ItemInfo>();
			if (itemInfo != null)
			{
				connectedInv.AddItem(itemInfo.item.itemData.itemID, 1, ref GUIManager.I.scrPlayerInventory.itemInventoryStacks);				
			}
			GameObject.Destroy(other.gameObject);
		}
	}

}
