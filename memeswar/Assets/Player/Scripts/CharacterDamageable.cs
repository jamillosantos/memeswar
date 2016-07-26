using Memewars;
using UnityEngine;

public class CharacterDamageable : Damageable
{
	private StickmanCharacter _stickman;

	protected override void Start()
	{
		base.Start ();
		this._stickman = this.GetComponent<StickmanCharacter>();
	}

	public override void Damage(float damage)
	{
		Debug.Log ("Ouch " + damage);
		base.Damage (damage);
	}

	protected override void Die()
	{
		this._stickman.Die();
	}
}
