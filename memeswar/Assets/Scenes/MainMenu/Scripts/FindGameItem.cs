using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que representa cada jogo encontrado na listagem.
/// </summary>
public class FindGameItem : MonoBehaviour
{
	public Text Name;

	public Text Players;

	public Text MaxPlayers;

	/// <summary>
	/// Evento chamado quando um jogador clica no botão "Entrar" na interface dos jogos listados.
	/// </summary>
	public void JoinClick()
	{
		CreateMyPlayer.RoomToJoin = this.Name.text;
		SceneManager.LoadScene("RuaEscura");
	}

	/// <summary>
	/// Quando entrar no quarto falha.
	/// </summary>
	/// <param name="codeAndMsg"></param>
	public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		FlashMessage.Popup(this.transform, "Não foi possível entrar no jogo: " + codeAndMsg[1], 5f);
	}
}
