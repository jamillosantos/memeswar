using UnityEngine;
using System.Collections;

/// <summary>
/// Trajetória que usa a gravidade do physics engine para calcular as posições.
/// </summary>
public class ProjectileGravityTrajectory : ProjectileTrajectory
{
	bool _initialized = false;

	protected override void Start()
	{
		base.Start();
		this._rigidbody.useGravity = true;
	}

	void FixedUpdate()
	{
		if (this._projectile.Fired)
		{
			if (!this._initialized)
			{
				this._rigidbody.velocity = this._projectile.Velocity;
				this._initialized = true;
			}
			// Ajusta a rotação do objeto para cair com a ponta virada para a trajetória.
			this._rigidbody.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(this._rigidbody.velocity.y, this._rigidbody.velocity.x) * Mathf.Rad2Deg);
		}
	}
}
