using UnityEngine;
using System.Collections;

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
