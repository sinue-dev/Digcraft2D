using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	public int direction = 0;
	public KeyCode leftKey, rightKey, jumpKey;
	public float moveSpeed = 3f;
	public float jumpForce = 250f;

	public LayerMask groundLayer;
	public float groundCheckDistance = 0.07f;

	public SpriteRenderer handSlotFront;
	public SpriteRenderer handSlotSide;

	private Rigidbody2D rbody;
	private Animator anim;
	private Transform groundCheck;

	private void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		groundCheck = transform.FindChild("GroundCheck");
	}

	private void Update()
	{
		UpdateControls();

		UpdateMovemenet();

		UpdateBreaking();

		UpdatePlacing();
	}

	private bool IsGrounded()
	{
		RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
		return hit.collider != null;
	}

	private void UpdateControls()
	{
		//Movement Controls
		if(Input.GetKey(leftKey))
		{
			FaceDirection(-1);
			anim.SetBool("isWalking", true);
		}
		else if(Input.GetKey(rightKey))
		{
			FaceDirection(1);
			anim.SetBool("isWalking", true);
		}
		else
		{
			FaceDirection(0);
			anim.SetBool("isWalking", false);
		}

		//Jump Controls
		if(Input.GetKeyDown(jumpKey) && IsGrounded())
		{
			Jump();
		}

		//Inventory Controls
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(GUIManager.I.bShowPlayerInventory)
			{
				GUIManager.I.ShowPlayerInventory(false);
			}
			else
			{
				GUIManager.I.ShowPlayerInventory(true);
			}
		}
	}

	private void FaceDirection(int dir)
	{
		if (dir == direction) return;

		direction = dir;

		if(dir == 0)
		{
			// Player facing front
			transform.FindChild("Side").gameObject.SetActive(false);
			transform.FindChild("Front").gameObject.SetActive(true);
		}
		else
		{
			// Player facing side
			transform.FindChild("Side").gameObject.SetActive(true);
			transform.FindChild("Front").gameObject.SetActive(false);
			transform.FindChild("Side").localScale = new Vector3(dir * -1, 1, 1);
		}
	}

	private void UpdateMovemenet()
	{
		rbody.velocity = new Vector2(moveSpeed*direction, rbody.velocity.y);
	}

	private void UpdateBreaking()
	{
		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

			if (hit.collider != null)
			{
				Debug.DrawLine(ray.origin, hit.point, Color.green);
				Debug.Log("HIT: " + hit.collider.gameObject.name);
				if(hit.collider.gameObject.tag == "Block")
				{
					WorldManager.I.DestroyBlock(hit.collider.gameObject);
				}
			}
		}
	}

	private void UpdatePlacing()
	{
		if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()) // RIGHT MOUSE
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			if (hit.collider == null)
			{
				Item item = GUIManager.I.scrPlayerHotbar.GetHeldItem();

				if (item == null) return;

				if(item.itemData.type == cItemData.ItemType_e.BLOCK)
				{
					Block block = BlockManager.I.FindBlock(item.itemData.blockID);
					WorldManager.I.PlaceBlock(block, ray.origin);

					GUIManager.I.scrPlayerInventory.RemoveItem(item.itemData.itemID, 1, ref GUIManager.I.scrPlayerInventory.itemHotbarStacks);
				}
			}
		}
	}

	public void HoldItem(Sprite sprite)
	{
		if(direction == 0)
		{
			handSlotFront.sprite = sprite;
		}
		else
		{
			handSlotSide.sprite = sprite;
		}
	}

	private void Jump()
	{
		rbody.AddForce(transform.up * jumpForce);
	}
}

[System.Serializable]
public class cPlayer
{
	public float PositionX, PositionY, PositionZ;
	public int direction;

	public cPlayer() { }

	public cPlayer(float PositionX, float PositionY, int direction)
	{
		this.PositionX = PositionX;
		this.PositionY = PositionY;
		this.direction = direction;
	}
}