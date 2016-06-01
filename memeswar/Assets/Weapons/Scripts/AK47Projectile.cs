using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AK47Projectile : Projectile
{
	public GameObject CollisionFX;

	protected override void Update()
	{
		base.Update();
		/// Já que o projétil da AK47 é bastante rápido, assume-se que após 10
		/// segundos sem acertar nada, ele deverá se auto-destruir.
		if ((Time.timeSinceLevelLoad - this.FiredAt) > 10f)
			Destroy(this.gameObject);
	}

	public override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
		/// Inicializa o efeito de hit.
		/// O próprio collisionFX deverá se auto-destruir.
		Instantiate(this.CollisionFX, collision.contacts[0].point, Quaternion.identity);
		// Auto-destrói o projétil
		Destroy(this.gameObject);
	}
}
