using UnityEngine;
using System.Collections;

public class CursorHover : MonoBehaviour {

	public GameObject cursorHover;

	private void Start()
	{

	}

	private void Update ()
	{
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 pos = WorldManager.I.WorldPosToGridPos(worldPos.x, worldPos.y);
		if (cursorHover.transform.position.x != pos.x || cursorHover.transform.position.y != pos.y)
		{
			//Vector2 pos = worldGen.WorldPosToChunkPos(Input.mousePosition.x, Input.mousePosition.y);

			cursorHover.transform.position = pos;
		}
	}
}
