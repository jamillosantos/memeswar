using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
	public float Damping;

	private Vector3 _initialPosition;

	void Start()
	{
		this._initialPosition = this.transform.localPosition;
	}

	void Update ()
	{
		this.transform.localPosition = this._initialPosition + Camera.main.transform.position * this.Damping;
	}
}
