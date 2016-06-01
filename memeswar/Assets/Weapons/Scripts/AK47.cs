
using Memewars;
using UnityEngine;

public class AK47
	: Gun
{

	public GameObject BulletPrefab;

	public GameObject BulletSpawnPoint;

	public ParticleSystem MuzzleParticleSystem;

	public AK47()
		: base()
	{
		this.GunTrigger1.TimeBetweenShots = 0.1f;
		this.GunTrigger2.TimeBetweenShots = 0.1f;
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void CreateProjectile1()
	{
		GameObject bullet = (GameObject)Instantiate(this.BulletPrefab, this.BulletSpawnPoint.transform.position, Quaternion.identity);
		bullet.GetComponent<Projectile>().Fire(this.StickmanCharacter.AimDirection);
		this.MuzzleParticleSystem.Play();
	}
}
