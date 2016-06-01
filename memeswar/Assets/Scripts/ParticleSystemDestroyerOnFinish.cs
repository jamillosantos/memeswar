using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemDestroyerOnFinish : MonoBehaviour {

	private ParticleSystem _particleSystem;

	private float _destroyAt;
	
	void Start ()
	{
		this._particleSystem = this.GetComponent<ParticleSystem>();
		this._destroyAt = Time.timeSinceLevelLoad + this._particleSystem.duration;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.timeSinceLevelLoad > this._destroyAt)
			Destroy(this.gameObject);
	}
}
