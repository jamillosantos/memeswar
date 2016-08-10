
using Memewars;
using UnityEngine;

public class AK47
	: Gun
{

	public AK47()
		: base()
	{
		/// Ajusta o tempo entre os tiros da arma.
		this.GunTrigger1.TimeBetweenShots = 0.15f;
		this.GunTrigger2.TimeBetweenShots = 0.15f;
	}

	public override void StartReloading()
	{
		if (!(this.IsReloading || this.IsFull))
		{
			this.Ammo = 0;
			base.StartReloading();
		}
	}
}
