
using UnityEngine;

public class AK47
	: Gun
{

	public GameObject BulletPrefab;

	public GameObject BulletSpawnPoint;

	public AK47()
		: base()
	{
		this.GunTrigger1.TimeBetweenShots = 0.1f;
		this.GunTrigger2.TimeBetweenShots = 0.1f;
	}

	protected override void CreateProjectile1()
	{
		Instantiate(this.BulletPrefab, Vector3.zero, Quaternion.identity);
	}
}
