﻿
using Memewars;
using UnityEngine;

public class AK47
	: Gun
{

	public AK47()
		: base()
	{
		this.GunTrigger1.TimeBetweenShots = 0.1f;
		this.GunTrigger2.TimeBetweenShots = 0.1f;
	}
}
