using UnityEngine;
using System.Collections;

/// <summary>
/// Trajetória linar. Utilizada em projéteis que não sofrem influencia da gravidade. Sim, a
/// gravidade é ignorada em alguns casos.
/// </summary>
public class ProjectileLinearTrajectory : ProjectileTrajectory
{
	private bool _initialized = false;

	protected override void Start()
	{
		base.Start();
		this._rigidbody.useGravity = false;
	}

	void FixedUpdate()
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
