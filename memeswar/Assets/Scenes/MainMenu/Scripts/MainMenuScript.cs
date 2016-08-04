using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	void Start()
	{
		this._canvas = this.GetComponent<Canvas>();
		this.QuitGameConfirmationCanvas.enabled = false;
		this.FindGameCanvas.gameObject.SetActive(false);
		this.CreateGameCanvas.gameObject.SetActive(false);
	}

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

	public void CreateGameClick()
	{
		this.CreateGameCanvas.gameObject.SetActive(true);
		this.gameObject.SetActive(false);
	}

	public void SettingsClick()
	{
		//
	}

	public void QuitClick()
	{
		this.enabled = false;
		this.QuitGameConfirmationCanvas.enabled = true;
	}

	public void QuitYesClick()
	{
		Application.Quit();
	}

	public void QuitNoClick()
	{
		this.enabled = true;
		this.QuitGameConfirmationCanvas.enabled = false;
	}
}
