using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Estados do projétil explosivo.
/// </summary>
enum ExplosiveProjectileState
{
	Default,
	WaitingExplosionCollider
}

/// <summary>
/// Classe que implementa a abstração de um projétil que gera uma explosão ao final. A implementação
/// dá-se ao primeiro contato do projétil que ao invés de aplicar um dano a um `Damageable` (como
/// na implementação padrão), este gera uma `SphereCollider` e aguarda uma nova colisão (mais um tick
/// de física) para gerar os danos de explosão e gerar as forças de explosão.
/// </summary>
public class ExplosiveProjectile : Projectile
{
	/// <summary>
	/// Raio de abrangência da explosão.
	/// </summary>
	public float Radius = 1f;

	/// <summary>
	/// Núcleo da explosão que terá dano máximo
	/// </summary>
	public float CoreRadius = 0f;

	/// <summary>
	/// Para
	/// </summary>
	private ExplosiveProjectileState _state;

	public ExplosiveProjectile() : base()
	{
		this._state = ExplosiveProjectileState.Default;
	}

	/// <summary>
	/// Cria o novo collider com o tamanho do raio da explosão.
	/// </summary>
	/// <param name="collision"></param>
	protected override void Hit(Collision collision)
	{
		if (this._state == ExplosiveProjectileState.Default)
		{
			/// Desabilita colisor padrão
			this.DefaultCollider.enabled = false;

			/// Cria colisor esférico para simular a área da explosão.
			SphereCollider collider = this.gameObject.AddComponent<SphereCollider>();
			collider.radius = this.Radius;
			collider.isTrigger = true;

			/// ALtera o estado para que próxima colisão aplique o dano, ao contrário de
			/// criar novamente uma área de explosão.
			this._state = ExplosiveProjectileState.WaitingExplosionCollider;
		}
		else
			base.Hit(collision);
	}

	/// <summary>
	/// Calcula o dano proporcional da explosão de acordo com a distância. 
	/// </summary>
	/// <param name="contact">Ponto de contato do colisor.</param>
	/// <param name="damageable">Objeto que deve sofrer o dano.</param>
	protected override void ApplyDamage(ContactPoint contact, Damageable damageable)
	{
		/// Calcula distância
		float d = Vector3.Distance(contact.point, this.gameObject.transform.position);
		
		/// Se na área de dano máximo.
		if (d <= this.CoreRadius)
			damageable.Damage(this.Damage);
		else
			/// Calcula o dano proporcional a distância. O dano é arredondado para cima, para não ser possível um caso de dano 0.
			damageable.Damage(Mathf.Ceil(this.Damage * ((d - this.CoreRadius) / this.Radius)));
	}
}
