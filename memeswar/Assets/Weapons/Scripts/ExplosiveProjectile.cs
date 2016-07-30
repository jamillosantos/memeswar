using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
	/// Efeito da explosão quando o obeto é destruído.
	/// </summary>
	public GameObject ExplosionEffect;

	/// <summary>
	/// Núcleo da explosão que terá dano máximo
	/// </summary>
	public float CoreRadius = 0f;

	/// <summary>
	/// Cria o novo collider com o tamanho do raio da explosão.
	/// </summary>
	/// <param name="collision"></param>
	protected override void Hit(Collision collision)
	{
		/// Desabilita colisor padrão
		this.DefaultCollider.enabled = false;

		/// Cria colisor esférico para simular a área da explosão.
		SphereCollider collider = this.gameObject.AddComponent<SphereCollider>();
		collider.radius = this.Radius;

		/// Desabilita o renderer para que o projétil não seja mais exibido.
		this.DefaultRenderer.enabled = false;

		/// Colliders atingidos na área da explosão.
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, this.Radius);

		/// Vê o ponto mais próximo do objeto na explosão e usa esse dado para computar a distância
		/// para assim aplicar o dano.
		Vector3 contactPoint, normal;
		float d;
		Damageable damageable;
		foreach (Collider c in colliders)
		{
			damageable = c.GetComponent<Damageable>();
			if (damageable)
			{
				contactPoint = c.ClosestPointOnBounds(this.transform.position);
				d = Vector3.Distance(contactPoint, this.transform.position);
				normal = (this.transform.position - contactPoint);
				normal.Normalize();
				damageable.Damage(Mathf.Ceil(this.Damage * (1 - (Mathf.Max((d - this.CoreRadius), 0) / this.Radius))), new CollisionInfo(this.StickmanCharacter, this.Weapon, contactPoint, normal));
			}
		}
		if (this.ExplosionEffect)
		{
			UnityStandardAssets.Effects.ExplosionPhysicsForce explosion = ((GameObject)Instantiate(this.ExplosionEffect, this.transform.position, Quaternion.identity)).GetComponent<UnityStandardAssets.Effects.ExplosionPhysicsForce>();
			explosion.Radius = this.Radius;
		}
		Destroy(this.gameObject);
	}
}
