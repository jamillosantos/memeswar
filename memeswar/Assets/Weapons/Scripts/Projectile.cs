using UnityEngine;
using System.Collections;
using System;

public interface BasicProjectile
{
	void Fire(Vector3 direction);

	void OnCollisionEnter(Collision collision);
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour, BasicProjectile
{
	/// <summary>
	/// Dano infligido caso acerte um jogador.
	/// </summary>
	public float Damage;

	/// <summary>
	/// Velocidade do disparo.
	/// </summary>
	public float Speed;

	/// <summary>
	/// Flag que guarda se o objeto já sofreu alguma colisão.
	/// </summary>
	private bool _collided = false;

	/// <see cref="Fired" />
	private bool _fired = false;

	/// <see cref="FiredAt" />
	private float _firedAt = -1;

	private Vector3 _velocity;

	/// <summary>
	/// Velocidade vetorial do objeto.
	/// </summary>
	public Vector3 Velocity
	{
		get
		{
			return this._velocity;
		}
	}

	/// <summary>
	/// Se o objeto foi disparado ou não.
	/// </summary>
	public bool Fired
	{
		get
		{
			return this._fired;
		}
	}

	/// <summary>
	/// Momento em que o projétil foi lançado.
	/// </summary>
	public float FiredAt
	{
		get
		{
			return this._firedAt;
		}
	}


	protected virtual void Start()
	{ }

	protected virtual void Update()
	{ }

	public void Fire(Vector3 direction)
	{
		this._firedAt = Time.timeSinceLevelLoad;
		this._velocity = direction * this.Speed;
		this._fired = true;
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		this._collided = true;
	}
}

public class ProjectileTrajectory : MonoBehaviour
{
	protected Rigidbody _rigidbody;

	protected Projectile _projectile;

	protected virtual void Start()
	{
		this._projectile = this.GetComponent<Projectile>();
		this._rigidbody = this.GetComponent<Rigidbody>();
	}
}

