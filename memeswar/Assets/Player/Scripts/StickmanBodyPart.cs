using UnityEngine;
using System.Collections;
using Memewars;

public enum BodyPart
{
	Auto, Head, Arms, Legs, Hips, Chest
}

public class StickmanBodyPart : MonoBehaviour
{

	private StickmanCharacter _stickman;

	private CharacterDamageable _damageable;

	public BodyPart Part = BodyPart.Auto;
	
	void Start ()
	{
		this._stickman = this.GetComponentInParent<StickmanCharacter>();
		this._damageable = this._stickman.GetComponent<CharacterDamageable>();
		if (this.Part == BodyPart.Auto)
		{
			if (this.gameObject.name.EndsWith("_Hips"))
				this.Part = BodyPart.Hips;
			else if (
				this.gameObject.name.EndsWith("_LeftUpLeg")
				|| this.gameObject.name.EndsWith("_RightUpLeg")
				|| this.gameObject.name.EndsWith("_LeftLeg")
				|| this.gameObject.name.EndsWith("_RightLeg"))
				this.Part = BodyPart.Legs;
			else if (this.gameObject.name.EndsWith("_Head"))
				this.Part = BodyPart.Head;
			else if (
				this.gameObject.name.EndsWith("_LeftArm")
				|| this.gameObject.name.EndsWith("_LeftForeArm")
				|| this.gameObject.name.EndsWith("_RightArm")
				|| this.gameObject.name.EndsWith("_RightForeArm"))
				this.Part = BodyPart.Arms;
		}
	}
}
