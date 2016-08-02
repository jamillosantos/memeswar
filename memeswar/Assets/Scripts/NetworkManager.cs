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
