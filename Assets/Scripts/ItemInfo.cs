using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour {

	public Item item { get; set; }

	public int decayTime = 300;

	private int timeRemaining;

	void Start()
	{
		timeRemaining = decayTime;

		InvokeRepeating("decreaseTimeRemaining", 1.0f, 1.0f);
	}

	void Update()
	{
		if (timeRemaining == 0)
		{
			Destroy(transform.root.gameObject);
		}
	}

	void decreaseTimeRemaining()
	{
		timeRemaining--;
	}
}
