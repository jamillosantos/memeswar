using UnityEngine;
using System.Collections;

public class MainMenuRagdollPlacer : MonoBehaviour
{

	static UnityEngine.Object _ragdoll;

	static UnityEngine.Object _bullet;

	public GameObject BulletSpawnPoint;

	void Start ()
	{
		_ragdoll = Resources.Load("Ragdoll");
		_bullet = Resources.Load("RocketBullet");
		this.CreateRagdoll();
		this.CreateBullet();
	}

	public void CreateBullet()
	{
		GameObject go = ((GameObject)Instantiate(_bullet, this.BulletSpawnPoint.transform.position, Quaternion.identity));
		ExplosiveProjectile bullet = go.GetComponent<ExplosiveProjectile>();
		bullet.Fake = true;
		float angle = Random.Range(30f, 75f)*Mathf.Deg2Rad;
		bullet.Speed = Random.Range(10f, 20f);
		bullet.Fire(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)));
		go.layer = Layer.Bullets;
		this.Invoke("CreateBullet", Random.Range(0.01f, 0.5f));
	}

	public void CreateRagdoll()
	{
		RagdollController ragdoll = ((GameObject)Instantiate(
			_ragdoll,
			new Vector3(Random.Range(-17f, 17f), 7f, 0),
			Quaternion.Euler(
				Random.Range(-30f, 30f),
				Random.Range(-30f, 30f),
				Random.Range(-30f, 30f)
			)
		)).GetComponent<RagdollController>();
		ragdoll.Randomize();

		this.Invoke("CreateRagdoll", Random.Range(0.01f, 0.3f));
	}
}
