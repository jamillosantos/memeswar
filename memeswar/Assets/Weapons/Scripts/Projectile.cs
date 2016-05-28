using UnityEngine;
using System.Collections;
using System;
using memewars;

public interface BasicProjectile
{
	void Fire(Vector3 direction);

	void OnCollisionEnter(Collision collision);
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour, BasicProjectile
{
	public float Damage;

	public float Speed;

	public Vector3 Velocity;

	private bool _collided = false;
	private bool _fired = false;

	public bool Fired
	{
		get
		{
			return this._fired;
		}
	}

	void Start()
	{ }

	public void Fire(Vector3 direction)
	{
		this.Velocity = direction * this.Speed;
		this._fired = true;
	}

	public void OnCollisionEnter(Collision collision)
	{
		this._collided = true;
	}
}

public class ProjectileTrajectory : MonoBehaviour
{
	protected Rigidbody _rigidbody;

	protected Projectile _projectile;

	public virtual void Start()
	{
		this._projectile = this.GetComponent<Projectile>();
		this._rigidbody = this.GetComponent<Rigidbody>();
	}
}

