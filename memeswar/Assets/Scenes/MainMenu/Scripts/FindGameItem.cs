using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindGameItem : MonoBehaviour
{
	public Text Name;

	public Text Players;

	public Text MaxPlayers;

	public void JoinClick()
	{
		PhotonNetwork.JoinRoom(this.Name.text);
		Application.LoadLevel("RuaEscura");
	}

}
