using UnityEngine;
using System.Collections;

/// <summary>
/// Exibe a tela de network status do sistema.
/// </summary>
public class NetworkUI : MonoBehaviour
{
	private Canvas _canvas;

	void Start ()
	{
		this._canvas = this.GetComponent<Canvas>();
		this._canvas.enabled = true;
	}
}
