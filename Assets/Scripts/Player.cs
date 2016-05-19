using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int direction = 0;
	public KeyCode leftKey, rightKey, jumpKey;
	public float moveSpeed = 3f;
	public float jumpForce = 250f;

	public LayerMask groundLayer;
	public float groundCheckDistance = 0.07f;

	private Rigidbody2D rbody;
	private Animator anim;
	private GUIManager guiManager;
	private Transform groundCheck;

	public SpriteRenderer handSlotFront;
	public SpriteRenderer handSlotSide;

	private void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		guiManager = GameObject.Find("GUI").GetComponent<GUIManager>();
		groundCheck = transform.FindChild("GroundCheck");
	}

	private void Update()
	{
		UpdateControls();

		UpdateMovemenet();

		UpdateBreaking();
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
			if(guiManager.bShowPlayerInventory)
			{
				guiManager.ShowPlayerInventory(false);
			}
			else
			{
				guiManager.ShowPlayerInventory(true);
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
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(pos, transform.position);
			if(hit.collider != null)
			{
				Debug.Log(hit.collider.gameObject.name);
				if(hit.collider.gameObject.tag == "Block")
				{
					GameObject.Find("World").GetComponent<WorldGen>().DestroyBlock(hit.collider.gameObject);
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
