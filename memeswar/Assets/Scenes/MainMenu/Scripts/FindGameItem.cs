using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FindGameItem : MonoBehaviour
{
	public Text Name;

	public Text Players;

	public Text MaxPlayers;

	public void JoinClick()
	{
		CreateMyPlayer.RoomToJoin = this.Name.text;
		SceneManager.LoadScene("RuaEscura");
	}

	public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		FlashMessage.Popup(this.transform, "Não foi possível entrar no jogo: " + codeAndMsg[1], 5f);
	}
}
