using Memewars;
using System;
using UnityEngine;

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
	public float CurrentHP
	{
		get
		{
			return this._currentHP;
		}
	}

	protected virtual void Start()
	{
		this._currentHP = this.MaxHP;
	}
	
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
			this._currentHP -= damage;
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
		this._currentHP = this.MaxHP;
	}
}
