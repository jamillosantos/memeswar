using UnityEngine;
using System.Collections;

/// <summary>
/// Script para exibir a mira do mouse na tela.
/// </summary>
public class MouseInteractions : MonoBehaviour
{

	public Camera Camera;

	void Start ()
	{
		// Cursor.visible = false;
	}

	void FixedUpdate()
	{
		this.gameObject.transform.position = Input.mousePosition;
	}
}
