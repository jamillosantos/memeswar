using UnityEngine;
using System.Collections;

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
		}
	}
}
