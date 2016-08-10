using Memewars;
using UnityEngine;

/// <summary>
/// Danificável especializado para jogadores.
/// </summary>
public class CharacterDamageable : Damageable
{
	/// <summary>
	/// Referência do stickman.
	/// </summary>
	private StickmanCharacter _stickman;

	/// <summary>
	/// Barra de HP
	/// </summary>
	private HPBar _hpBar;

	/// <summary>
	/// Recurso da fonte de sangue.
	/// </summary>
	static UnityEngine.Object BloodFountain;

	protected override void Start()
	{
		/// Encontra o recurso da fonte de sangue.
		if (BloodFountain == null)
			BloodFountain = Resources.Load("BloodFountain");

		base.Start ();
		this._stickman = this.GetComponent<StickmanCharacter>();
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
		/*
		/// Cria a fonte de sangue na posição onde levou o tiro.
		GameObject bloodFountain = (GameObject)Instantiate(
			BloodFountain,
			collisionInfo.Point,
			Quaternion.LookRotation(collisionInfo.Point - (Vector3.Dot(collisionInfo.Point, collisionInfo.Normal)) * collisionInfo.Normal, collisionInfo.Normal)
		);
		bloodFountain.transform.SetParent(this._stickman.transform, true);
		*/
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
