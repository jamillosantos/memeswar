using UnityEngine;
using System.Collections;

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
