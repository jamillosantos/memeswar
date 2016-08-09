using UnityEngine;
using System.Collections;

/// <summary>
/// Barra de HP que fica em cima dos jogadores via network.
/// </summary>
public class Bar3D : MonoBehaviour
{

	/// <summary>
	/// Referência do "danificável" que esta barra representará.
	/// </summary>
	private CharacterDamageable _damageable;

	private Transform _cube;
	private Renderer _cubeRenderer;

	/// <summary>
	/// Gradiente que será utilizado para colorir a barra de acordo com a porcentagem do HP.
	/// </summary>
	public Gradient Gradient;

	void Start ()
	{
		this._cube = this.GetComponentsInChildren<Transform>()[1];
		this._cubeRenderer = this._cube.gameObject.GetComponent<Renderer>();
		this._damageable = this.GetComponentInParent<CharacterDamageable>();
	}
	

	/// <summary>
	/// Atualiza o status da barra.
	/// </summary>
	void Update ()
	{
		this._cube.localScale = new Vector3(Mathf.Min(1, this._damageable.CurrentHP / this._damageable.MaxHP), this._cube.localScale.y, this._cube.localScale.z);
		this._cubeRenderer.material.color = this.Gradient.Evaluate(this._cube.localScale.x);
	}
}
