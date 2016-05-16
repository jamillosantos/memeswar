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

	private float Speed = 20f;

	private Rigidbody _rigidbody;

	private Vector3 velocity;

	private bool _collided = false;

	void Start()
	{
		this._collided = false;
		this._rigidbody = this.GetComponent<Rigidbody>();
		this.Fire(new Vector3(0.3f, 1, 0));
	}

	void FixedUpdate()
	{
		if (Time.timeSinceLevelLoad < 3f)
			return;
		// Debug.Log(this._rigidbody.velocity);
		this._rigidbody.transform.position = this._rigidbody.transform.position + this.velocity * Time.deltaTime;
		// this._rigidbody.velocity = this.velocity;
		if (!this._collided)
			this._rigidbody.rotation = Quaternion.Euler(0f, 0f, (float)((Math.PI + Math.Atan2(this.velocity.y, this.velocity.x)) * 180f / Math.PI));
	}

	public void Fire(Vector3 direction)
	{
		this.velocity = direction * this.Speed;
	}

	public void OnCollisionEnter(Collision collision)
	{
		this._collided = true;
	}
}
