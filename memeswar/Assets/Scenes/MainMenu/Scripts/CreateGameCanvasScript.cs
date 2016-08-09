using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que cuida da parte de criação do jogos no sistema.
/// </summary>
public class CreateGameCanvasScript : MonoBehaviour
{
	/// <summary>
	/// Referência do input do nome do jogo.
	/// </summary>
	public InputField GameName;

	/// <summary>
	/// Referência da tela principal.
	/// </summary>
	public Canvas Main;

	private Canvas _canvas;

	void Start()
	{
		this._canvas = this.GetComponent<Canvas>();
	}
	
	/// <summary>
	/// Cria o jogo.
	/// </summary>
	public void CreateGame()
	{
		PhotonNetwork.JoinOrCreateRoom(this.GameName.text, new RoomOptions(), new TypedLobby());
	}

	/// <summary>
	/// Evento chamado ao quando o jogo é criado via network.
	/// </summary>
	public void OnCreatedRoom()
	{
		SceneManager.LoadScene("RuaEscura");
	}

	/// <summary>
	/// Se a criação do jogo falhar este evento é disparado.
	/// </summary>
	/// <param name="codeAndMsg">Erro direto da PhotonNetwork</param>
	public void OnPhotonCreateRoomFailed(object[] codeAndMsg)
	{
		FlashMessage.Popup(this.transform, string.Format("Não foi possível criar o jogo: {0}.", codeAndMsg[1]), 5f);
	}

	/// <summary>
	/// Método que exibe o menu principal.
	/// </summary>
	public void ReturnToMain()
	{
		this.gameObject.SetActive(false);
		this.Main.gameObject.SetActive(true);
	}
}
