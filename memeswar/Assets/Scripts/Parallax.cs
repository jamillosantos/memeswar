using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
	public float DampingX;

	public float DampingY;

	private Vector3 _initialPosition;

	void Start()
	{
		this._initialPosition = this.transform.localPosition;
	}

	void Update ()
	{
		this.transform.localPosition = this._initialPosition + new Vector3(Camera.main.transform.position.x * this.DampingX, Camera.main.transform.position.y*this.DampingY, Camera.main.transform.position.z);
	}
}
