using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateGameCanvasScript : MonoBehaviour
{

	public InputField GameName;

	public Canvas Main;

	private Canvas _canvas;

	void Start()
	{
		this._canvas = this.GetComponent<Canvas>();
	}
	
	public void CreateGame()
	{
		PhotonNetwork.JoinOrCreateRoom(this.GameName.text, new RoomOptions(), new TypedLobby());
	}

	public void OnCreatedRoom()
	{
		SceneManager.LoadScene("RuaEscura");
	}

	public void OnPhotonCreateRoomFailed(object[] codeAndMsg)
	{
		FlashMessage.Popup(this.transform, string.Format("Não foi possível criar o jogo: {0}.", codeAndMsg[1]), 5f);
	}

	public void ReturnToMain()
	{
		this.gameObject.SetActive(false);
		this.Main.gameObject.SetActive(true);
	}
}
