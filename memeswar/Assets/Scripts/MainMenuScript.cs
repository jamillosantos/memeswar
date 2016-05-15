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

	public Image img;

	public Canvas QuitGameConfirmation;

	void Start()
	{
		this.QuitGameConfirmation.enabled = false;
	}

	public void FindGameClick()
	{

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
		Debug.Log(this.QuitGameConfirmation);
		this.QuitGameConfirmation.enabled = true;
	}

	public void QuitYesClick()
	{
		Application.Quit();
	}

	public void QuitNoClick()
	{
		this.QuitGameConfirmation.enabled = false;
	}
}
