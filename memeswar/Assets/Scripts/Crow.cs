using UnityEngine;
using System.Collections;
using Memewars;
using System;

public class Crow : MonoBehaviour
{
	private BezierCurve _curve;

	public float Duration;

	private bool _playing = false;

	private float _timeStart;

	private Vector3 _lastPosition;

	private AudioSource _audioSource;

	void Start()
	{
		this._curve = this.GetComponentInParent<BezierCurve>();
		this._audioSource = this.GetComponent<AudioSource>();
	}
	
	void Update ()
	{
		if (this._playing)
		{
			float time = (Time.timeSinceLevelLoad - this._timeStart) / this.Duration;
			if (time >= 1f)
			{
				this._playing = false;
				time = 0;
			}

			try
			{
				this._lastPosition = this.transform.position;
				this.transform.position = this._curve.GetPointAt(time);
				this.transform.rotation = Quaternion.LookRotation((this._lastPosition - this.transform.position));
			}
			catch (System.Exception)
			{ }
		}
	}

	public void Play()
	{
		if (!this._playing)
		{
			this._audioSource.Play();
			this._playing = true;
			this._timeStart = Time.timeSinceLevelLoad;
		}
	}
}
