using UnityEngine;
using System.Collections;

public class BaseStats : MonoBehaviour
{
	// ---HEALTH---
	//public class HealthData
	//{
	//	private float currentHalth;
	//	public float Health
	//	{
	//		get { return currentHalth; }
	//		set { currentHalth = value; }
	//	}

	//	public HealthData()
	//	{
	//		Health = 0;
	//	}

	//	public static float DamageHealth(float damageAmount, bool ignoreArmor)
	//	{
	//		float damageAmountCalculated;

	//		// calculate damage amount with defense modifiers
	//		if (ignoreArmor == false)
	//		{
	//			damageAmountCalculated = damageAmount * DefenseData.modifier; // cannot access modifier!
	//		}

	//		// no defense modifiers
	//		else
	//		{
	//			damageAmountCalculated = damageAmount;
	//		}

	//		// get current health before it is changed below
	//		float oldValue = Health; // cannot access current!

	//		// change actual health
	//		Health -= damageAmountCalculated; // cannot access current!

	//		return damageAmountCalculated;
	//	}
	//}

	// ---DEFENSE---
	//public class DefenseData
	//{
	//	private float _amount;
	//	public float amount
	//	{
	//		get { return _amount; }
	//		private set { _amount = value; }
	//	}

	//	private float _modifier;
	//	public float modifier
	//	{
	//		get { return _modifier; }
	//		private set { _modifier = value; }
	//	}

	//	public enum DefenseType
	//	{ Unarmored = 0, Armored = 1, Shielded = 2 };

	//	private float _typeModifier;
	//	public float typeModifier
	//	{
	//		get { return _typeModifier; }
	//		private set { _typeModifier = value; }
	//	}

	//	private float _totalModifier;
	//	public float totalModifier
	//	{
	//		get { return _totalModifier; }
	//		private set { _totalModifier = value; }
	//	}

	//	public DefenseData()
	//	{
	//		amount = 0;
	//		totalModifier = modifier * typeModifier;
	//	}

	//	public static void UpdateDefense()
	//	{
	//		modifier = (100 - amount) / 100.0f; // cannot access modifier and amount!

	//		// other code here
	//	}
	//}
}
