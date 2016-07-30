using UnityEngine;
using System.Collections;

public enum RespawnMode
{
	RealTime,
	Denied,
	Checkpoint
}

public class GameRules
{
	public RespawnMode RespawnMode = RespawnMode.RealTime;

	public float RespawnTime = 5;
}
