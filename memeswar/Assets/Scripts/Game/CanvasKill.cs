using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CanvasKill : MonoBehaviour
{
	public float Duration = 0.5f;

	public float KillAmount;

	private AudioSource _audio;

	private Canvas _canvas;

	void Start()
	{
		this._canvas = this.GetComponent<Canvas>();
		this._canvas.enabled = false;
		this._audio = this.GetComponent<AudioSource>();
		this._audio.playOnAwake = false;
		this._audio.loop = false;
	}

	public void Show ()
	{
		this._canvas.enabled = true;
		this._audio.Play();
		this.Invoke("Hide", this.Duration);
	}

	void Hide()
	{
		this._canvas.enabled = false;
	}
}
