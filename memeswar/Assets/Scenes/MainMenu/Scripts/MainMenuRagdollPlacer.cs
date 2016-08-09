using UnityEngine;
using System.Collections;

/// <summary>
/// Script que efetua a criação das ragdolls para o menu principal.
/// </summary>
public class MainMenuRagdollPlacer : MonoBehaviour
{

	static UnityEngine.Object _ragdoll;

	static UnityEngine.Object _bullet;

	public GameObject BulletSpawnPoint;

	void Start ()
	{
		/// Obtém as referências dos recursos que serão utilizados pelo script.
		_ragdoll = Resources.Load("Ragdoll");
		_bullet = Resources.Load("RocketBullet");
		this.CreateRagdoll();
		this.CreateBullet();
	}

	/// <summary>
	/// Efetua a criação do foguete.
	/// </summary>
	public void CreateBullet()
	{
		GameObject go = ((GameObject)Instantiate(_bullet, this.BulletSpawnPoint.transform.position, Quaternion.identity));
		ExplosiveProjectile bullet = go.GetComponent<ExplosiveProjectile>();
		/// A marcação de fake é importante para evitar corportamentos desnecessparios do projétil,
		/// já que ele é preparado para funcionar via network.
		bullet.Fake = true;

		/// Randomiza angulos e velocidade do projétil.
		float angle = Random.Range(30f, 75f)*Mathf.Deg2Rad;
		bullet.Speed = Random.Range(10f, 20f);
		/// Dispara o projétil convertendo de angulo para um vetor de direção.
		bullet.Fire(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)));
		/// Altera a layer da arma.
		go.layer = Layer.Bullets;

		// Agenda a criação de outro foguete com tempo ranodmico.
		this.Invoke("CreateBullet", Random.Range(0.01f, 0.5f));
	}

	/// <summary>
	/// Efetua a criação da ragdoll.
	/// </summary>
	public void CreateRagdoll()
	{
		/// Cria o objeto da ragdoll com posição e rotação randomicos.
		RagdollController ragdoll = ((GameObject)Instantiate(
			_ragdoll,
			new Vector3(Random.Range(-17f, 17f), 7f, 0),
			Quaternion.Euler(
				Random.Range(-30f, 30f),
				Random.Range(-30f, 30f),
				Random.Range(-30f, 30f)
			)
		)).GetComponent<RagdollController>();
		/// Randomiza a velocidade angular de cada parte do corpo.
		ragdoll.Randomize();

		/// Agenda a criação de outra ragdoll com tempo randomico.
		this.Invoke("CreateRagdoll", Random.Range(0.01f, 0.3f));
	}
}
