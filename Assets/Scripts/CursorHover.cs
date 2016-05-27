using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CursorHover : MonoBehaviour {

	public GameObject cursorHover;

	private void Start()
	{

	}

	private void Update ()
	{
		if (Camera.main != null)
		{
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 pos = WorldManager.I.WorldPosToGridPos(worldPos.x, worldPos.y);
			if ((cursorHover.transform.position.x != pos.x || cursorHover.transform.position.y != pos.y) && !EventSystem.current.IsPointerOverGameObject())
			{
				cursorHover.transform.position = pos;
			}
		}
	}
}
