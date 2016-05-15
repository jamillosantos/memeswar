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
		this.FindGameCanvas.enabled = false;
	}

	public void FindGameClick()
	{
		this.FindGameCanvas.enabled = true;
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
		this.QuitGameConfirmationCanvas.enabled = true;
	}

	public void QuitYesClick()
	{
		Application.Quit();
	}

	public void QuitNoClick()
	{
		this.QuitGameConfirmationCanvas.enabled = false;
	}
}
