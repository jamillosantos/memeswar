
using Memewars;
using UnityEngine;

public class Shotgun
	: Gun
{
	private const int FRAGMENTS = 4;

	public Shotgun()
		: base()
	{
		/// Ajusta o tempo de preparação entre os tiros.
		this.GunTrigger1.TimeBetweenShots = 1.4f;
		this.GunTrigger2.TimeBetweenShots = 1.4f;
	}

	/// <summary>
	/// Cria múltiplos projéteis para a shotgun. (Característica muito comum das shotguns é seus tiros
	/// terem vários estilhaços).
	/// </summary>
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
	}
}
