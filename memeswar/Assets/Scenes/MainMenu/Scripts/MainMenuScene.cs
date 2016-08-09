using UnityEngine;
using System.Collections;

/// <summary>
/// Script para inicializar a cena do MainMenu.
/// </summary>
public class MainMenuScene : MonoBehaviour
{

	public Canvas MainCanvas;

	public Canvas FindGameCanvas;

	void Start ()
	{
		this.MainCanvas.enabled = true;
		this.FindGameCanvas.enabled = false;
	}
}
