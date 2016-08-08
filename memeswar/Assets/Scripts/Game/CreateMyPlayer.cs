using UnityEngine;
using System.Collections;
using Memewars;

public class CreateMyPlayer : MonoBehaviour
{
	public static string RoomToJoin;

	void Start()
	{
		/*
		if (PhotonNetwork.connectionState != ConnectionState.Connected)
		{
			PhotonNetwork.ConnectUsingSettings("v0.0");
		}
		else if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
		{
			this.CreatePlayer();
		}
		else
		{
			PhotonNetwork.JoinOrCreateRoom("Sangria Desatada", new RoomOptions(), new TypedLobby());
		}
		*/
		if (!string.IsNullOrEmpty(RoomToJoin))
		{
			PhotonNetwork.JoinRoom(RoomToJoin);
		}
		else
		{
			if (PhotonNetwork.connectionState != ConnectionState.Connected)
			{
				PhotonNetwork.ConnectUsingSettings("v0.0");
			}
			else if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
			{
				this.CreatePlayer();
			}
		}
	}

	void OnJoinedLobby()
	{
		PhotonNetwork.JoinOrCreateRoom("Sangria Desatada", new RoomOptions(), new TypedLobby());
	}

	void OnJoinedRoom()
	{
		this.CreatePlayer();
	}

	private void CreatePlayer()
	{
		FlashMessage.Popup(GameObject.Find("HUD").transform, "Criando aqui!", 10f);

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
