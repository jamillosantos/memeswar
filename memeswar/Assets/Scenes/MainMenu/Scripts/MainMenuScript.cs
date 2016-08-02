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

	public Canvas QuitGameConfirmationCanvas;

	public Canvas FindGameCanvas;

	void Start()
	{
		this.QuitGameConfirmationCanvas.enabled = false;
		this.FindGameCanvas.gameObject.SetActive(false);
	}

	public void FindGameClick()
	{
		this.gameObject.SetActive(false);
		this.FindGameCanvas.gameObject.SetActive(true);
	}

	public void CreateGameClick()
	{
		//
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
