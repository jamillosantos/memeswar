
using Memewars;
using UnityEngine;

public class Shotgun
	: Gun
{
	private const int FRAGMENTS = 4;

	public Shotgun()
		: base()
	{
		this.GunTrigger1.TimeBetweenShots = 0.4f;
		this.GunTrigger2.TimeBetweenShots = 0.4f;
	}

	protected override void TriggerCreateProjectile1()
	{
		int[] networkIds = new int[FRAGMENTS];
		Vector3[] directions = new Vector3[FRAGMENTS];
		Vector3[] positions = new Vector3[FRAGMENTS];
		for (int i = 0; i < FRAGMENTS; i++)
		{
			networkIds[i] = PhotonNetwork.AllocateViewID();
			directions[i] = this.StickmanCharacter.AimDirection + new Vector3(Random.Range(-0.07f, 0.07f), Random.Range(-0.07f, 0.07f));
			positions[i] = this.BulletSpawnPoint.transform.position;
		}
		this.CreateProjectile1(directions, positions);
		// this.StickmanCharacter.photonView.RPC("CreateProjectile1", PhotonTargets.Others, networkIds, directions, positions);
	}
}
