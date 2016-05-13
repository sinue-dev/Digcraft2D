using UnityEngine;
using System.Collections;

public class MainMenuCam : MonoBehaviour {

	public float speed = 8f;

	private void Update()
	{
		transform.Rotate(0f, speed * Time.deltaTime, 0f);
	}
}
