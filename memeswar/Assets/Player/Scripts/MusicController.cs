using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public enum MusicControllerState
{
	Quiet,
	Action
}

public class MusicController : MonoBehaviour
{

	private float _bpm = 180;

	private float _transitionIn;

	private float _transitionOut;

	public AudioMixerSnapshot Quiet;

	public AudioMixerSnapshot Action1;

	public AudioMixerSnapshot Action2;

	private MusicControllerState _state;

	public AudioClip Action1Clip;

	private float _stopCombatSongAt;

	void Start ()
	{
		this._transitionIn = 60 / this._bpm;
		this._transitionOut = (60 / this._bpm) * 32;

		this._state = MusicControllerState.Quiet;
		this.Quiet.TransitionTo(0.1f);
	}

	public void StartCombat()
	{
		// Renova o tempo
		this._stopCombatSongAt = Time.timeSinceLevelLoad + 10f;

		// Se não for ação, iniciar a música de combate.
		if (this._state != MusicControllerState.Action)
		{
			this._state = MusicControllerState.Action;
			this.RandomizeAction();
		}
	}

	/// <summary>
	/// Randomiza qual loop vai ser usado.
	/// </summary>
	void RandomizeAction()
	{
		// Executa a mudança de música apenas se estiver no modo combate.
		if (this._state == MusicControllerState.Action)
		{
			// Randomiza qual ação vai ser utilizada.
			if (Random.value < 0.5f)
				this.Action1.TransitionTo(this._transitionIn);
			else
				this.Action2.TransitionTo(this._transitionIn);

			// Reinvoca este mesmo método de acordo com o tempo da música.
			this.Invoke("RandomizeAction", this.Action1Clip.length - (this._transitionIn / 2f));
		}
	}

	void Update()
	{
		if (this._stopCombatSongAt <= Time.timeSinceLevelLoad)
		{
			this._state = MusicControllerState.Quiet;
			this.Quiet.TransitionTo(this._transitionOut);
		}
	}
}
