using Memewars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CharacterDamageable : Damageable
{
	private StickmanCharacter _stickman;

	void Start()
	{
		this._stickman = this.GetComponent<StickmanCharacter>();
	}

	protected override void Die()
	{
		this._stickman.Die();
	}
}
