
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Trigger
{
	private bool _pulled;

	private float _pulledAt;

	public void Pull()
	{
		if (!this._pulled)
		{
			this._pulled = true;
			this._pulledAt = Time.time;
		}
	}

	public void Release()
	{
		this._pulled = false;
	}

	public float PulledAt
	{
		get
		{
			return this._pulledAt;
		}
	}

	public float PulledElapsed
	{
		get
		{
			return Time.time - this._pulledAt;
		}
	}

	public bool Pulled
	{
		get
		{
			return this._pulled;
		}
	}
}
