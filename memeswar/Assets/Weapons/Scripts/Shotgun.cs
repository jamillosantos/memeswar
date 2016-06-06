
using Memewars;
using UnityEngine;

public class Shotgun
	: Gun
{

	public Shotgun()
		: base()
	{
		this.GunTrigger1.TimeBetweenShots = 0.4f;
		this.GunTrigger2.TimeBetweenShots = 0.4f;
	}

	protected override void CreateProjectile1()
	{
		GameObject bullet;
		for (int i = 0; i < 4; i++)
		{
			bullet = (GameObject)Instantiate(this.BulletPrefab, this.BulletSpawnPoint.transform.position, Quaternion.identity);
			bullet.GetComponent<Projectile>().Fire(this.StickmanCharacter.AimDirection + new Vector3(Random.Range(-0.07f, 0.07f), Random.Range(-0.07f, 0.07f)));
		}
		this.MuzzleParticleSystem.Play();
	}
}
