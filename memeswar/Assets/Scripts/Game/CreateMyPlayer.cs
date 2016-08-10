using UnityEngine;
using System.Collections;
using Memewars;

/// <summary>
/// Classe responsável pela criação do player ao iniciar a cena. Ela cuidará de toda a parte de conexão e entrada no jogo.
/// </summary>
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

	/// <summary>
	/// Evento chamado quando o jogador entra no lobby.
	/// </summary>
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinOrCreateRoom("Sangria Desatada", new RoomOptions(), new TypedLobby());
	}

	/// <summary>
	/// Evento chamado quando o jogador entra no jogo.
	/// </summary>
	void OnJoinedRoom()
	{
		this.CreatePlayer();
	}

	/// <summary>
	/// Efetua a criação do jogador via rede.
	/// </summary>
	private void CreatePlayer()
	{
		GameObject[] spanwpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");

		GameObject player = PhotonNetwork.Instantiate("Player", spanwpoints[Random.Range(0, spanwpoints.Length)].transform.position, Quaternion.identity, 0, new object[] {
			new Weapon.Weapons[] {
				Weapon.Weapons.AK47, Weapon.Weapons.RocketLauncher, Weapon.Weapons.Shotgun, Weapon.Weapons.AK47, Weapon.Weapons.AK47
			}
		});
		player.GetComponent<StickmanUserControl>().enabled = true;
		player.GetComponent<AudioListener>().enabled = true;
	}
}
