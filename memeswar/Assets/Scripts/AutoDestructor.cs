using UnityEngine;
using System.Collections;

public class AutoDestructor
	: MonoBehaviour
{

	public float Duration;

	private float _destroyAt;
	
	void Start ()
	{
		this._destroyAt = Time.timeSinceLevelLoad + this.Duration;
	}
	
	void Update ()
	{
		if (Time.timeSinceLevelLoad > this._destroyAt)
			Destroy(this.gameObject);
	}
}
