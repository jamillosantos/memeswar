using System;
using UnityEngine;


public class Gun : Weapon
{
	/// <summary>
	/// Carga atual de munição na arma, em unidades.
	/// </summary>
	public float Ammo;

	/// <summary>
	/// Tamanho do cartucho da arma, em unidades. Ex: Shotgun tem 6 tiros.
	/// </summary>
	public float CartridgeSize;

	/// <summary>
	/// O tempo que demora para recarregar a arma.
	/// </summary>
	public float ReloadTime;

	/// <summary>
	/// Quantidade de munição que será carregada a cada processo de carregamento. Ex: Shotgun vai recarregar
	/// 1 unidade para cada tempo de carregamento, equanto a AK-47 irá carregar as 30 unidades de seu pente
	/// por vez.
	/// </summary>
	public float ReloadAmount;

	/// <see cref="LastShotAt" />
	public float _lastShotAt;

	/// <summary>
	/// Momento em que o último tiro foi disparado.
	/// </summary>
	public float LastShotAt
	{
		get
		{
			return this._lastShotAt;
		}
	}

	/// <summary>
	/// Quantos segundos se passaram a partir do último tiro dado. Esta variável será atualizada quando o processo
	/// de reload for finalizado.
	/// </summary>
	public float LastShotElapsed
	{
		get
		{
			return (Time.time - this._lastShotAt);
		}
	}

	/// <summary>
	/// Prefab do projétil que será disparado.
	/// </summary>
	public GameObject BulletPrefab;

	/// <summary>
	/// Spawn point do projétil.
	/// </summary>
	public GameObject BulletSpawnPoint;

	/// <summary>
	/// Sistema de partículas que será acionado no momento do tiro.
	/// </summary>
	public ParticleSystem MuzzleParticleSystem;


	protected override Trigger CreateTrigger1()
	{
		return new GunTrigger();
	}

	protected override Trigger CreateTrigger2()
	{
		return new GunTrigger();
	}

	protected GunTrigger GunTrigger1
	{
		get
		{
			return (GunTrigger)this.Trigger1;
		}
	}

	protected GunTrigger GunTrigger2
	{
		get
		{
			return (GunTrigger)this.Trigger2;
		}
	}

	/// <summary>
	/// Decrementa a arma para o Trigger1.
	/// </summary>
	protected virtual void DecreaseAmmo1()
	{
		this.Ammo -= 1f;
	}

	/// <summary>
	/// Decrementa a arma para o Trigger2.
	/// </summary>
	protected virtual void DecreaseAmmo2()
	{
		this.Ammo -= 1f;
	}

	/// <summary>
	/// Cria os projéteis que serão disparados para o Trigger1.
	/// </summary>
	protected virtual void CreateProjectile1()
	{
		GameObject bullet = (GameObject)PhotonNetwork.Instantiate(this.BulletPrefab.name, this.BulletSpawnPoint.transform.position, Quaternion.identity, 0);
		bullet.GetComponent<Projectile>().Fire(this.StickmanCharacter.AimDirection);
		this.MuzzleParticleSystem.Play();
	}

	/// <summary>
	/// Cria os projéteis que serão disparados para o Trigger2.
	/// </summary>
	protected virtual void CreateProjectile2()
	{
		// TODO Implement this
	}

	/// <summary>
	/// Método que ativa a ação de atirar para o Trigger1.
	/// </summary>
	protected virtual void Fire1()
	{
		Debug.Log("Fire");
		this._lastShotAt = Time.time;
		this.DecreaseAmmo1();
		this.CreateProjectile1();
	}

	/// <summary>
	/// Método que ativa a ação de atirar para o Trigger2.
	/// </summary>
	protected virtual void Fire2()
	{
		Debug.Log("Fire2");
		this._lastShotAt = Time.time;
		this.DecreaseAmmo2();
		this.CreateProjectile2();
	}

	/// <summary>
	/// Verifica os triggers e dispara os métodos `Fire` correspondente.
	/// </summary>
	public virtual void Update()
	{
		if (this.IsReloading)
		{
			if (this.ReloadingElapsed >= this.ReloadTime)
			{
				this.Ammo = Mathf.Min(this.Ammo + this.ReloadAmount, this.CartridgeSize);
				this.StopReloading();
			}
		}
		else
		{
			if (this.CanFire((GunTrigger)this.Trigger1))
			{
				this.Fire1();
			}
			else if (this.CanFire((GunTrigger)this.Trigger2))
			{
				this.Fire2();
			}
		}
	}

	/// <summary>
	/// Verifica se é possível atirar, dadas as condições atuais da arma.
	/// </summary>
	/// <param name="trigger">Gatilho que será utilizado para a verificação.</param>
	/// <returns>Se é possível ou não atirar.</returns>
	protected bool CanFire(GunTrigger trigger)
	{
		return
			trigger.Pulled
			&& (!this.IsReloading)                                           // Não carregando
			&& (this.Ammo > 0)                                         // Munição disponível
			&& (trigger.PulledElapsed >= trigger.TimePrepareFirstShot)	// Tempo de preparação já ocorrido
			&& (this.LastShotElapsed >= trigger.TimeBetweenShots);      // Entre o último tiro e este já passou tempo suficiente
	}
}
