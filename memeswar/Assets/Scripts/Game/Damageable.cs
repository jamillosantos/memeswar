using Memewars;
using System;
using UnityEngine;

/// <summary>
/// Script que implementa a ideia de um obeto danificável, utilizado em jogadores.
/// </summary>
public class Damageable : MonoBehaviour
{
	/// <summary>
	/// HP máximo do obejto.
	/// </summary>
	public float MaxHP = 100f;

	/// <see cref="CurrentHP" />
	private float _currentHP;

	/// <summary>
	/// HP atual do objeto.
	/// </summary>
	public virtual float CurrentHP
	{
		get
		{
			return this._currentHP;
		}
		protected set
		{
			this._currentHP = value;
		}
	}

	protected virtual void Start()
	{
		this._currentHP = this.MaxHP;
	}

	protected virtual void UpdateHP()
	{ }
	
	/// <summary>
	/// Método que computa inflige o dano neste objeto. Dependendo do estado, ele também irá disparar
	/// o método de morte.
	/// </summary>
	/// <param name="damage"></param>
	/// <see cref="Die" />
	public virtual void Damage(float damage, CollisionInfo collisionInfo)
	{
		if (this._currentHP > 0)
		{
			this.CurrentHP -= damage;
			this.UpdateHP();
			if (this._currentHP <= 0)
			{
				this.Die(new DeathInfo
				{
					Assassin = collisionInfo.StickmanCharacter,
					At = Time.timeSinceLevelLoad,
					By = collisionInfo.Weapon,
					Dead = null
				});
			}
		}
	}

	/// <summary>
	/// Método que coordena a morte do objeto. Este método deve ser chamado pelo método Damage.
	/// </summary>
	/// <see cref="Damage(float)" />
	protected virtual void Die(DeathInfo deathInfo)
	{
		Destroy(this.gameObject);
	}

	public void Reset()
	{
		this.CurrentHP = this.MaxHP;
		this.UpdateHP();
	}

	protected virtual void Update()
	{ }
}
