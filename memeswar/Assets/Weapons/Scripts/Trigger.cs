
using UnityEngine;

/// <summary>
/// Representa o gatilho da arma.
/// </summary>
public class Trigger
{
	private bool _pulled;

	private float _pulledAt;

	/// <summary>
	/// Puxa o gatilho da arma.
	/// </summary>
	public void Pull()
	{
		if (!this._pulled)
		{
			this._pulled = true;
			this._pulledAt = Time.time;
		}
	}

	/// <summary>
	/// Solta o gatilho da arma.
	/// </summary>
	public void Release()
	{
		this._pulled = false;
	}

	/// <summary>
	/// Guarda a informação de quando o gatilho da arma foi pressionado.
	/// </summary>
	public float PulledAt
	{
		get
		{
			return this._pulledAt;
		}
	}

	/// <summary>
	/// Tempo, em segundos, passados a partir do momento em que o gatilho da arma foi pressionado.
	/// </summary>
	public float PulledElapsed
	{
		get
		{
			return Time.time - this._pulledAt;
		}
	}

	/// <summary>
	/// Se o gatilho foi pressionado ou não.
	/// </summary>
	public bool Pulled
	{
		get
		{
			return this._pulled;
		}
	}
}
