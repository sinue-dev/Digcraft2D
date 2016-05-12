using UnityEngine;
using System.Collections;

public class PickUpDrops : MonoBehaviour {

	private Inventory connectedInv;
	private ItemDatabase itemDatabase;

	private void Start()
	{
		connectedInv = transform.parent.GetComponent<Inventory>();
		itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer == 9)
		{
			connectedInv.AddItem(other.name, 1);
			GameObject.Destroy(other.gameObject);
		}
	}

}
