using Memewars;
using UnityEngine;

public class CharacterDamageable : Damageable
{
	private StickmanCharacter _stickman;

	private HPBar _hpBar;

	static UnityEngine.Object BloodFountain;

	protected override void Start()
	{
		if (BloodFountain == null)
			BloodFountain = Resources.Load("BloodFountain");

		base.Start ();
		this._stickman = this.GetComponent<StickmanCharacter>();
		if (this._stickman.photonView.isMine)
		{
			this._hpBar = GameObject.FindObjectOfType<HPBar>();
			this._hpBar.Max = this.MaxHP;
		}
	}

	public override void Damage(float damage, CollisionInfo collisionInfo)
	{
		base.Damage (damage, collisionInfo);
		Debug.Log("Direction: " + Quaternion.FromToRotation(transform.up, collisionInfo.Normal).eulerAngles);
		Debug.Log("Normal: " + collisionInfo.Normal);
		GameObject bloodFountain = (GameObject)Instantiate(
			BloodFountain,
			collisionInfo.Point,
			Quaternion.LookRotation(collisionInfo.Point - (Vector3.Dot(collisionInfo.Point, collisionInfo.Normal)) * collisionInfo.Normal, collisionInfo.Normal)
		);
		bloodFountain.transform.SetParent(this._stickman.transform, true);
		if (this._stickman.photonView.isMine)
			this._hpBar.Current = this.CurrentHP;
	}

	protected override void Die(DeathInfo deathInfo)
	{
		deathInfo.Dead = this._stickman;
		this._stickman.Die(deathInfo);
	}
}
