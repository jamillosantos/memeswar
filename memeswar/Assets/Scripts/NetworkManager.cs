using UnityEngine;
using System.Collections;
using System;
using Memewars;

public class NetworkManager : MonoBehaviour
{
	void Start ()
	{
		this.Connect();
	}

	void Connect()
	{
		// PhotonNetwork.offlineMode = true;
		PhotonNetwork.ConnectUsingSettings("v0.0");
	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom("Sangria desatada");
	}

	void OnJoinedRoom()
	{
		this.CreateMyPlayer();
	}

	private void CreateMyPlayer()
	{
		GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
		player.GetComponent<StickmanUserControl>().enabled = true;

		StickmanCharacter c = player.GetComponent<StickmanCharacter>();
		c.SetArsenal(new Weapon.Weapons[] {
			Weapon.Weapons.AK47, Weapon.Weapons.RocketLauncher, Weapon.Weapons.Shotgun, Weapon.Weapons.AK47, Weapon.Weapons.AK47
		});
	}
}
