using UnityEngine;
using System.Collections;

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
		if (this._projectile.Fired)
		{
			if (!this._initialized)
			{
				this._rigidbody.velocity = this._projectile.Velocity;
				this._initialized = true;
			}
			this._rigidbody.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(this._rigidbody.velocity.y, this._rigidbody.velocity.x) * Mathf.Rad2Deg);
		}
		Debug.Log(Time.timeSinceLevelLoad + ": " + this._rigidbody.velocity);
	}
}
