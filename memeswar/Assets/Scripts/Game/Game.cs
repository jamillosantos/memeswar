using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Memewars;

public class DeathInfo
{
	public StickmanCharacter Assassin;

	public StickmanCharacter Dead;

	public float At;

	public Weapon By;
}

[RequireComponent(typeof(PhotonView))]
public class Game
{
	private static GameRules _gameRules = new GameRules();

	public static GameRules Rules
	{
		get
		{
			return _gameRules;
		}
	}

	public static void Death(DeathInfo deathInfo)
	{
		//
	}
}
