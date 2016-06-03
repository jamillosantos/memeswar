using UnityEngine;

/// <summary>
/// Classe que implementa o comportamento básico de um projétil (movimentação inicial, colisões
/// e danos).
/// 
/// OBS: A movimentação do projétil será definida e controlada pela classe `ProjectileTrajectory`
/// e suas especializações.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
	/// <summary>
	/// Dano infligido caso acerte um jogador.
	/// </summary>
	public float Damage;

	/// <summary>
	/// Velocidade do disparo.
	/// </summary>
	public float Speed;

	/// <see cref="Collided" />
	private bool _collided = false;

	/// <summary>
	/// Flag que guarda se o objeto já sofreu alguma colisão.
	/// </summary>
	protected bool Collided
	{
		get
		{
			return this._collided;
		}
	}

	/// <see cref="Fired" />
	private bool _fired = false;

	/// <see cref="FiredAt" />
	private float _firedAt = -1;

	private Vector3 _velocity;

	/// <see cref="DefaultCollider" />
	private Collider _defaultCollider;

	/// <summary>
	/// Colisor padrão do projétil.
	/// </summary>
	public Collider DefaultCollider
	{
		get
		{
			return this._defaultCollider;
		}
	}

	/// <summary>
	/// Velocidade vetorial do objeto.
	/// </summary>
	public Vector3 Velocity
	{
		get
		{
			return this._velocity;
		}
	}

	/// <summary>
	/// Se o objeto foi disparado ou não.
	/// </summary>
	public bool Fired
	{
		get
		{
			return this._fired;
		}
	}

	/// <summary>
	/// Momento em que o projétil foi lançado.
	/// </summary>
	public float FiredAt
	{
		get
		{
			return this._firedAt;
		}
	}


	protected virtual void Start()
	{
		this._defaultCollider = this.GetComponent<Collider>();
		// this._defaultCollider.isTrigger = true;
	}

	protected virtual void Update()
	{ }

	public void Fire(Vector3 direction)
	{
		this._firedAt = Time.timeSinceLevelLoad;
		this._velocity = direction * this.Speed;
		this._fired = true;
	}

	/// <summary>
	/// Método chamado pelo Unity para tratar a colisão.
	/// </summary>
	/// <param name="collision">Objeto que contém todos os dados da colisão.</param>
	/// <see cref="Hit" />
	public virtual void OnCollisionEnter(Collision collision)
	{
		this._collided = true;
		this.Hit(collision);
	}

	/// <summary>
	/// Método interno utilizado para tratar a colisão.
	/// </summary>
	/// <param name="collision">Objeto que contém todos os dados da colisão.</param>
	protected virtual void Hit(Collision collision)
	{
		Damageable damageable;
		foreach (ContactPoint contact in collision.contacts)
		{
			damageable = contact.otherCollider.GetComponent<Damageable>();
			if (damageable)
				 this.ApplyDamage(contact, damageable);
		}
	}

	/// <summary>
	/// Aplica o dano ao objeto.
	/// </summary>
	/// <param name="contact">Ponto de contato com o `collider`.</param>
	/// <param name="damageable">Objeto que sofrerá o dano.</param>
	protected virtual void ApplyDamage(ContactPoint contact, Damageable damageable)
	{
		Debug.Log("Recreasing " + this.Damage + " from " + damageable.CurrentHP);
		damageable.Damage(this.Damage);
	}
}

/// <summary>
/// 
/// </summary>
public class ProjectileTrajectory : MonoBehaviour
{
	protected Rigidbody _rigidbody;

	protected Projectile _projectile;

	protected virtual void Start()
	{
		this._projectile = this.GetComponent<Projectile>();
		this._rigidbody = this.GetComponent<Rigidbody>();
	}
}

