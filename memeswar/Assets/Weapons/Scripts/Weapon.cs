using UnityEngine;
using System.Collections;
using Memewars;

/// <summary>
/// Classe base para as implementações das armas.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
	private StickmanCharacter _stickmanCharacter;

	private Trigger _trigger1;

	private Trigger _trigger2;

	private bool _reloading = false;

	/// <summary>
	/// Nome da arma dentro do jogo.
	/// </summary>
	public string Name;

	/// <see cref="ReloadingStartedAt"/>
	private float _reloadingStartedAt;

	/// <summary>
	/// Momento em que o processo de recarregamento foi iniciado.
	/// </summary>
	public float ReloadingStartedAt
	{
		get
		{
			return this._reloadingStartedAt;
		}
	}

	public float ReloadingElapsed
	{
		get
		{
			return (Time.time - this._reloadingStartedAt);
		}
	}

	protected virtual void Start()
	{
		this._stickmanCharacter = this.GetComponentInParent<StickmanCharacter>();
	}

	protected StickmanCharacter StickmanCharacter
	{
		get
		{
			return this._stickmanCharacter;
		}
	}

	/// <summary>
	/// Cria a instância do trigger primário para esta arma.
	/// </summary>
	/// <returns>Nova instância do Trigger.</returns>
	protected virtual Trigger CreateTrigger1()
	{
		return new Trigger();
	}

	/// <summary>
	/// Cria a instância do trigger secundário da arma.
	/// </summary>
	/// <returns>Nova instância do Trigger.</returns>
	protected virtual Trigger CreateTrigger2()
	{
		return new Trigger();
	}

	public Weapon()
	{
		this._trigger1 = this.CreateTrigger1();
		this._trigger2 = this.CreateTrigger2();
	}

	/// <summary>
	/// Gatilho padrão para a arma.
	/// </summary>
	public Trigger Trigger1
	{
		get
		{
			return this._trigger1;
		}
	}

	/// <summary>
	/// Gatilho 2 para a arma. Caso seja necessário.
	/// </summary>
	public Trigger Trigger2
	{
		get
		{
			return this._trigger2;
		}
	}

	/// <summary>
	/// Inicia o processo de recarregamento.
	/// </summary>
	public virtual void StartReloading()
	{
		this._reloading = true;
		this._reloadingStartedAt = Time.time;
	}

	/// <summary>
	/// Finaliza o processo de recarregamento.
	/// </summary>
	public virtual void StopReloading()
	{
		this._reloading = false;
	}

	/// <summary>
	/// Flag re retorna se está em processo de carregamento.
	/// </summary>
	public bool IsReloading
	{
		get
		{
			return this._reloading;
		}
	}
}
