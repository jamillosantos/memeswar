using UnityEngine;
using System.Collections;
using Memewars;

public class SpriteWeaponVisual : MonoBehaviour
{
	private SpriteRenderer _sprite;
	private StickmanCharacter _stickmanCharacter;

	void Start ()
	{
		this._stickmanCharacter = this.GetComponentInParent<StickmanCharacter>();
		this._sprite = this.GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
		this._sprite.flipY = (this._stickmanCharacter.AimAngle < -90) || (this._stickmanCharacter.AimAngle > 90);
	}
}
