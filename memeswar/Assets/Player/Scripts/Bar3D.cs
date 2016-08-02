using UnityEngine;
using System.Collections;

public class Bar3D : MonoBehaviour
{

	private CharacterDamageable _damageable;

	private Transform _cube;
	private Renderer _cubeRenderer;

	public Gradient Gradient;

	void Start ()
	{
		this._cube = this.GetComponentsInChildren<Transform>()[1];
		this._cubeRenderer = this._cube.gameObject.GetComponent<Renderer>();
		this._damageable = this.GetComponentInParent<CharacterDamageable>();
	}
	
	void Update ()
	{
		this._cube.localScale = new Vector3(Mathf.Min(1, this._damageable.CurrentHP / this._damageable.MaxHP), this._cube.localScale.y, this._cube.localScale.z);
		this._cubeRenderer.material.color = this.Gradient.Evaluate(this._cube.localScale.x);
	}
}
