using UnityEngine;
using System.Collections;
using Memewars;

/// <summary>
/// Esta classe tem como objetivo de armar o GameObject que representa o arsenal do jogador.
/// Este gameobject é particularmente importante pois é o repositório onde todas as armas serão inseridas.
/// Ele que será rotacionado para dar a impressão de mirar com a arma.
/// </summary>
public class Arsenal : MonoBehaviour
 {
	private StickmanCharacter _stickmanCharacter;

	void Start ()
	{
		this._stickmanCharacter = this.GetComponentInParent<StickmanCharacter>();
	}
	
	void Update ()
	{
		/// Efetua a rotação de acordo com a mira do jogador.
		this.transform.rotation = Quaternion.Euler(this._stickmanCharacter.AimAngle, 90, 0);
	}
}
