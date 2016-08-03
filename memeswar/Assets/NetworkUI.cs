using UnityEngine;
using System.Collections;

public class NetworkUI : MonoBehaviour
{
	private Canvas _canvas;

	void Start ()
	{
		this._canvas = this.GetComponent<Canvas>();
		this._canvas.enabled = true;
	}
}
