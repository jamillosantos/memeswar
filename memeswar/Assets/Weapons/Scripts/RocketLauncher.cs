using UnityEngine;
using System.Collections;

public class RocketLauncher
	: Gun
{
	public RocketLauncher()
		: base()
	{
		// Ajusta o tempo de preparação da arma entre os tiros.
		this.GunTrigger1.TimeBetweenShots = 2f;
		this.GunTrigger2.TimeBetweenShots = 2f;
	}
}
