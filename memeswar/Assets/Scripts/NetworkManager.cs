using UnityEngine;
using Memewars;

public class NetworkManager : MonoBehaviour
{
	void Start ()
	{
		if (!PhotonNetwork.connected)
			this.Connect();
	}

	void Connect()
	{
		if (!PhotonNetwork.connected)
			PhotonNetwork.player.name = "Jogador" + Random.Range(0, 123123451).ToString();

		PhotonNetwork.ConnectUsingSettings("v0.0");
	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label("PlayerList: " + PhotonNetwork.playerList.Length);
		GUILayout.Label("RoomList: " + PhotonNetwork.GetRoomList().Length);
	}
}
