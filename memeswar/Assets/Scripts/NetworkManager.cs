using UnityEngine;
using Memewars;

/// <summary>
/// Script gerenciador da rede.
/// </summary>
public class NetworkManager : MonoBehaviour
{
	void Start ()
	{
		if (!PhotonNetwork.connected)
			this.Connect();
	}

	/// <summary>
	/// Efetua a conexão com o Photon.
	/// </summary>
	void Connect()
	{
		if (!PhotonNetwork.connected)
			PhotonNetwork.player.name = "Jogador" + Random.Range(0, 123123451).ToString();

		PhotonNetwork.ConnectUsingSettings("v0.0");
	}

	/// <summary>
	/// Exibe informações de debug na tela.
	/// </summary>
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label("PlayerList: " + PhotonNetwork.playerList.Length);
		GUILayout.Label("RoomList: " + PhotonNetwork.GetRoomList().Length);
	}
}
