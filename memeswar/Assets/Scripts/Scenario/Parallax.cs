using UnityEngine;
using System.Collections;

/// <summary>
/// Implementação do parallax.
/// </summary>
public class Parallax : MonoBehaviour
{
	/// <summary>
	/// Amortização para o eixo X.
	/// </summary>
	public float DampingX;

	/// <summary>
	/// Amortização para o eixo Y.
	/// </summary>
	public float DampingY;

	private Vector3 _initialPosition;

	void Start()
	{
		this._initialPosition = this.transform.localPosition;
	}

	/// <summary>
	/// Atualiza a posição do componente de acordo com os parâmetros.
	/// </summary>
	void Update ()
	{
		this.transform.localPosition = this._initialPosition + new Vector3(Camera.main.transform.position.x * this.DampingX, Camera.main.transform.position.y*this.DampingY, Camera.main.transform.position.z);
	}
}
