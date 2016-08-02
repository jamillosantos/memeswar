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
		// this.gameObject.SetActive(this._stickman.photonView.isMine);
		if (this._stickman.photonView.isMine)
		{
			this._hpBar = GameObject.FindObjectOfType<HPBar>();
			this._hpBar.Max = this.MaxHP;
			this.UpdateHP();
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
	}

	protected override void Die(DeathInfo deathInfo)
	{
		deathInfo.Dead = this._stickman;
		this._stickman.Die(deathInfo);
	}

	public override float CurrentHP
	{
		get
		{
			object hp;
			if (this._stickman.photonView.owner.customProperties.TryGetValue("HP", out hp))
				return (float)hp;
			else
				return this.MaxHP;
		}
		protected set
		{
			this._stickman.photonView.owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
			{
				{ "HP", value }
			});
			base.CurrentHP = value;
		}
	}

	protected override void UpdateHP()
	{
		base.UpdateHP();
		if (this._stickman.photonView.isMine)
		{
			this._hpBar.Current = this.CurrentHP;
		}
	}
}
