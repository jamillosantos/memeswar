using UnityEngine;
using System.Collections;

public class ProjectileLinearTrajectory : ProjectileTrajectory
{
	bool _rotated = false;

	public override void Start()
	{
		base.Start();
		this._rigidbody.useGravity = false;
	}

	void FixedUpdate()
	{
		if (this._projectile.Fired)
		{
			if (!this._rotated)
			{
				this._rigidbody.velocity = this._projectile.Velocity;
				this._rotated = true;
			}
			this._rigidbody.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(this._rigidbody.velocity.y, this._rigidbody.velocity.x) * Mathf.Rad2Deg);
		}
	}
}
