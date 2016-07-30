using UnityEngine;
using System.Collections;
using Memewars;

public class CameraFollower : MonoBehaviour
{
	private Vector3 _relCameraPos;

	void Start ()
	{
		this._relCameraPos = new Vector3(0, 1.3f, -20f);
	}

	void FixedUpdate()
	{
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, this.transform.position + this._relCameraPos, 0.1f);
	}
}
