using UnityEngine;
using Memewars;

/// <summary>
/// Classe que cuida do visual das armas (sprite).
/// </summary>
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
		// Inverte o sprite da arma caso o angulo da mira.
		this._sprite.flipY = (this._stickmanCharacter.AimAngle < -90) || (this._stickmanCharacter.AimAngle > 90);
	}
}
