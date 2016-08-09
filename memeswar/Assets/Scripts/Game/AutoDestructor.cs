using UnityEngine;
using System.Collections;

/// <summary>
/// Classe que auto destrói o objeto que está associada a ela. Este objeto
/// </summary>
public class AutoDestructor
	: MonoBehaviour
{
	/// <summary>
	/// Tempo de duração da vida deste componente. Após este duração a classe irá se auto destruir.
	/// </summary>
	public float Duration;

	private float _destroyAt;
	
	void Start ()
	{
		this._destroyAt = Time.timeSinceLevelLoad + this.Duration;
	}
	
	void Update ()
	{
		if (Time.timeSinceLevelLoad > this._destroyAt)
		{
			/// Efetua verificação se o componente é de network ou não, para assim efetuar a
			/// destruição de forma apropriada.
			if (this.GetComponent<PhotonView>() == null)
				Destroy(this.gameObject);
			else
				PhotonNetwork.Destroy(this.gameObject);
		}
	}
}
