using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script para controlar o main menu.
/// </summary>
public class MainMenuScript : MonoBehaviour
{
	public Button FindGame;

	public Button CreateGame;

	public Button Settings;

	public Button Quit;

	private Canvas _canvas;

	public Canvas QuitGameConfirmationCanvas;

	public Canvas FindGameCanvas;

	public Canvas CreateGameCanvas;

	/// <summary>
	/// Exibe apenas o canvas correto.
	/// </summary>
	void Start()
	{
		this._canvas = this.GetComponent<Canvas>();
		this.QuitGameConfirmationCanvas.enabled = false;
		this.FindGameCanvas.gameObject.SetActive(false);
		this.CreateGameCanvas.gameObject.SetActive(false);
	}

	/// <summary>
	/// Exibe a tela de busca de jogos.
	/// </summary>
	public void FindGameClick()
	{
		if (PhotonNetwork.connected)
		{
			this.gameObject.SetActive(false);
			this.FindGameCanvas.gameObject.SetActive(true);
		}
		else
		{
			FlashMessage.Popup(this._canvas.transform, "Ei aperriado, espera conectar aí.", 5);
		}
	}

	/// <summary>
	/// Exibe a tela de criação de jogo.
	/// </summary>
	public void CreateGameClick()
	{
		this.CreateGameCanvas.gameObject.SetActive(true);
		this.gameObject.SetActive(false);
	}

	public void SettingsClick()
	{ }


	/// <summary>
	/// Exibe a tela de confirmação de sair do jogo.
	/// </summary>
	public void QuitClick()
	{
		this.enabled = false;
		this.QuitGameConfirmationCanvas.enabled = true;
	}

	/// <summary>
	/// Fecha o jogo.
	/// </summary>
	public void QuitYesClick()
	{
		Application.Quit();
	}

	/// <summary>
	/// Retorna a tela normal do main menu.
	/// </summary>
	public void QuitNoClick()
	{
		this.enabled = true;
		this.QuitGameConfirmationCanvas.enabled = false;
	}
}
