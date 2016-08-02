using UnityEngine;
using System.Collections;
using System;
using Memewars;

public class NetworkManager : MonoBehaviour
{
	void Start ()
	{
		PhotonNetwork.player.name = "CaBraDaPesTe";
		Debug.Log("PhotonNetwork.sendRate: " + PhotonNetwork.sendRate);
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
		GUILayout.Label("PlayerList: " + PhotonNetwork.playerList.Length);
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
		GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero + new Vector3(0, 5, 0), Quaternion.identity, 0, new object[] {
			new Weapon.Weapons[] {
				Weapon.Weapons.AK47, Weapon.Weapons.RocketLauncher, Weapon.Weapons.Shotgun, Weapon.Weapons.AK47, Weapon.Weapons.AK47
			}
		});
		player.GetComponent<StickmanUserControl>().enabled = true;
		player.GetComponent<AudioListener>().enabled = true;

		StickmanCharacter c = player.GetComponent<StickmanCharacter>();
	}
}
