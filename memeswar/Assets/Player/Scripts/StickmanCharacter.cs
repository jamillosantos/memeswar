using UnityEngine;
using System.Collections;
using System;

namespace Memewars
{
	public class CharacterJointData
	{
		private Vector3 anchor;
		private bool autoConfigureConnectedAnchor;
		private Vector3 axis;
		private float breakForce;
		private float breakTorque;
		private Vector3 connectedAnchor;
		private Rigidbody connectedBody;
		private bool enableCollision;
		private bool enablePreprocessing;
		private bool enableProjection;
		private SoftJointLimit highTwistLimit;
		private SoftJointLimit lowTwistLimit;
		private float projectionAngle;
		private float projectionDistance;
		private SoftJointLimit swing1Limit;
		private SoftJointLimit swing2Limit;
		private Vector3 swingAxis;
		private SoftJointLimitSpring swingLimitSpring;
		private SoftJointLimitSpring twistLimitSpring;

		public CharacterJointData(CharacterJoint jointSource)
		{
			this.connectedBody = jointSource.connectedBody;
			this.anchor = jointSource.anchor;
			this.axis = jointSource.axis;
			this.autoConfigureConnectedAnchor = jointSource.autoConfigureConnectedAnchor;
			this.connectedAnchor = jointSource.connectedAnchor;
			this.swingAxis = jointSource.swingAxis;
			this.twistLimitSpring = jointSource.twistLimitSpring;
			this.lowTwistLimit = jointSource.lowTwistLimit;
			this.highTwistLimit = jointSource.highTwistLimit;
			this.swingLimitSpring = jointSource.swingLimitSpring;
			this.swing1Limit = jointSource.swing1Limit;
			this.swing2Limit = jointSource.swing2Limit;
			this.enableProjection = jointSource.enableProjection;
			this.projectionDistance = jointSource.projectionDistance;
			this.projectionAngle = jointSource.projectionAngle;
			this.breakForce = jointSource.breakForce;
			this.breakTorque = jointSource.breakTorque;
			this.enableCollision = jointSource.enableCollision;
			this.enablePreprocessing = jointSource.enablePreprocessing;
		}

		public void Assign(CharacterJoint joint)
		{
			joint.connectedBody = this.connectedBody;
			joint.anchor = this.anchor;
			joint.axis = this.axis;
			joint.autoConfigureConnectedAnchor = this.autoConfigureConnectedAnchor;
			joint.connectedAnchor = this.connectedAnchor;
			joint.swingAxis = this.swingAxis;
			joint.twistLimitSpring = this.twistLimitSpring;
			joint.lowTwistLimit = this.lowTwistLimit;
			joint.highTwistLimit = this.highTwistLimit;
			joint.swingLimitSpring = this.swingLimitSpring;
			joint.swing1Limit = this.swing1Limit;
			joint.swing2Limit = this.swing2Limit;
			joint.enableProjection = this.enableProjection;
			joint.projectionDistance = this.projectionDistance;
			joint.projectionAngle = this.projectionAngle;
			joint.breakForce = this.breakForce;
			joint.breakTorque = this.breakTorque;
			joint.enableCollision = this.enableCollision;
			joint.enablePreprocessing = this.enablePreprocessing;

		}
	}

	class BodyPart
	{
		public GameObject gameObject;

		public Collider collider;

		public Rigidbody rigidBody;

		public CharacterJoint joint;

		public CharacterJointData jointData;

		public BodyPart(Rigidbody rigidb)
		{
			this.gameObject = rigidb.gameObject;
			this.collider = this.gameObject.GetComponent<Collider>();
			this.rigidBody = rigidb;
			this.joint = this.gameObject.GetComponent<CharacterJoint>();
			if (this.joint != null)
				this.jointData = new CharacterJointData(this.joint);
		}
	}


	class Body
	{
		public BodyPart Hips;

		public Transform RightFoot;
		public Transform LeftFoot;
	}

	[RequireComponent(typeof(Animator))]
	public class StickmanCharacter : Photon.MonoBehaviour
	{
		/// <summary>
		/// Array que guarda as referências das armas do arsenal.
		/// </summary>
		protected Weapon[] Arsenal = new Weapon[5];

		/// <summary>
		/// Mantém os tipos das armas que serão utilizados no arsenal.
		/// </summary>
		private Weapon.Weapons[] ArsenalType = new Weapon.Weapons[5] {
			Weapon.Weapons.AK47, Weapon.Weapons.AK47, Weapon.Weapons.AK47, Weapon.Weapons.AK47, Weapon.Weapons.AK47
		};

		/// <summary>
		/// </summary>
		/// <see cref="Weapon"/>
		private Weapon _currentWeapon;

		/// <summary>
		/// </summary>
		/// <see cref="WeaponIndex"/>
		private int _weaponIndex = -1;

		/// <summary>
		/// Índice da arma utilizada anteriormente. Utilizada no método ToggleWeapon.
		/// </summary>
		private int _lastWeaponIndex = -1;

		public float MaxHorizontalSpeed = 7f;

		[SerializeField]
		float _jumpPower = 9f;

		[Range(1f, 4f)]
		[SerializeField]
		float _runCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others

		[SerializeField]
		float _groundCheckDistance = 0.1f;

		private Rigidbody _rootRigidbody;
		private Collider _rootCollider;

		private BodyPart[] _bodyParts;

		private Body _body;

		private Animator _animator;

		private bool _isGrounded = true;
		
		private float _origGroundCheckDistance;
		
		private const float _half = 0.5f;

		/*
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		*/

		private float _jumpTime = 0;
		private float _jetpackTime = 0;

		/// <summary>
		/// Capacidade máxima do jetpack.
		/// </summary>
		private float _jetpackCapacity = 3f;

		/// <summary>
		/// Quanto tempo demora o carregamento do jetpack.
		/// </summary>
		private float _jetpackReloadDuration = 5f;

		/// <summary>
		/// Valor atual do combustível do jetpack.
		/// </summary>
		private float _jetpackFuel;

		/// <summary>
		/// Ratio de recarregamento. Variável constante baseada na capacidade total do tanque pelo tempo de carregamento.
		/// </summary>
		private float _jetpackReloadRatio;

		private bool _started = false;
		private Vector3 _updatedPosition;
		private Quaternion _updatedRotation;
		private Vector3 _updatedVelocity;
		private Vector3 _updatedAimDirection;

		private Bar _jetpackUIBar;
		private Bar _ammoUIBar;

		private Vector3 AimOffset = Vector3.up * 1.3f;

		private Vector3 _relCameraPos;

		private Arsenal _arsenalPlaceholder;

		/// <summary>
		/// Referência do `ParticleSystem` das chamas do jetpack.
		/// </summary>
		private ParticleSystem _jetpackFlames;

		/// <summary>
		/// Referência do `ParticleSystem` da fumaça do jetpack.
		/// </summary>
		private ParticleSystem _jetpackSmoke;

		/// <summary>
		/// Referência da luz do jetpack.
		/// </summary>
		private Light _jetpackLight;

		/// <summary>
		/// Retorna se o jogador está no chão ou não.
		/// </summary>
		public bool IsGrounded
		{
			get
			{
				return this._isGrounded;
			}
		}

		[PunRPC]
		public void SetArsenal(Weapon.Weapons[] arsenal)
		{
			Debug.Log(Time.timeSinceLevelLoad + ": SetArsenal " + this.photonView.isMine);
			if (!this.photonView.isMine)
			{
				foreach (Weapon.Weapons w in arsenal)
					Debug.Log(Time.timeSinceLevelLoad + ": SetArsenal " + w);
			}
			this.ArsenalType = arsenal;

			int i = 0;
			foreach (Weapon.Weapons w in arsenal)
			{
				this.ReplaceWeapon(i, w);
				i++;
			}

			if (this.photonView.isMine && this._started)
				this.photonView.RPC("SetArsenal", PhotonTargets.Others, arsenal);
		}

		void OnPhotonInstantiate(PhotonMessageInfo info)
		{
			Debug.Log("Instantiate " + this.photonView.isMine);
			if (this.photonView.isMine)
			{
				Debug.Log("Enviando dados!");
				this.photonView.RPC("SetArsenal", PhotonTargets.Others, this.ArsenalType);
			}
		}

		[PunRPC]
		public void ReplaceWeapon(int index, Weapon.Weapons weapon)
		{
			if (this._arsenalPlaceholder)
			{
				if (this.Arsenal[index])
					PhotonNetwork.Destroy(this.Arsenal[index].gameObject);
				UnityEngine.Object original = Resources.Load(weapon.ToString());
				GameObject go = (GameObject)Instantiate(original, Vector3.zero, Quaternion.Euler(0, -90, 0));
				this.Arsenal[index] = go.GetComponent<Weapon>();
				go.transform.SetParent(this._arsenalPlaceholder.gameObject.transform, false);
				go.SetActive(false);
			}
			else
				this.ArsenalType[index] = weapon;
			
			if (this.photonView.isMine)
				this.photonView.RPC("ReplaceWeapon", PhotonTargets.Others, index, weapon);
		}

		/// <see cref="JetpackOn" />
		private bool _jetpackOn = false;

		/// <see cref="AimDirection" />
		private Vector3 _aimDirection = Vector3.zero;

		private float _aimAngle;

		/// <summary>
		/// Altura da cabeça do boneco.
		/// </summary>
		private readonly float HEAD_HEIGHT = 1.3f;
		private bool _ragdolled = true;

		/// <summary>
		/// Variável que define se o Jetpack está ligado ou não.
		/// </summary>
		public bool JetpackOn
		{
			get
			{
				return this._jetpackOn;
			}
			set
			{
				if (this._jetpackOn != value)
				{
					this._jetpackOn = value;
					//this._jetpackLight.gameObject.SetActive(value);
					if (value)
						this._jetpackFlames.Play(true);
					else
						this._jetpackFlames.Stop(true);
				}
			}
		}

		/// <summary>
		/// Variável que garda a arma atual selecionada. Apenas para facilitar o acesso.
		/// </summary>
		public Weapon Weapon
		{
			get
			{
				return this._currentWeapon;
			}
		}

		/// <summary>
		/// Variável que guarda o índice da arma atual.
		/// </summary>
		public int WeaponIndex
		{
			get
			{
				return this._weaponIndex;
			}

			set
			{
				if (value != this._weaponIndex)
				{
					if ((value >= 0) && (value < this.Arsenal.Length))
					{
						if (this.Arsenal[value] == null)
							this.ReplaceWeapon(value, this.ArsenalType[value]);

						this._lastWeaponIndex = this._weaponIndex;
						this._weaponIndex = value;
						this.UpdateWeapon(this.Arsenal[value]);
						if ((this._ammoUIBar) && (this.Weapon) && (this.Weapon is Gun))
							this._ammoUIBar.Max = ((Gun)this.Weapon).CartridgeSize;
					}
					else
						throw new System.IndexOutOfRangeException("O índice da arma está fora do arsenal.");
				}
				if (this.photonView.isMine)
					this.photonView.RPC("SetWeaponIndex", PhotonTargets.Others, value);
			}
		}

		[PunRPC]
		public void SetWeaponIndex(int value)
		{
			this.WeaponIndex = value;
		}

		/// <summary>
		/// Troca de armas entre as duas recentemente atualizadas.
		/// </summary>
		public void ToggleWeapon()
		{
			if (this._lastWeaponIndex >= 0)
				this.WeaponIndex = this._lastWeaponIndex;
		}

		public void VisualFireEffect()
		{
			if (this.Weapon)
			{
				this.Weapon.VisualFireEffect();
			}
		}

		void Update()
		{
			if (this.photonView.isMine)
			{
				if (this.Weapon)
				{
					if (this.Weapon.IsReloading)
					{
						if (this.Weapon is Gun)
						{
							Gun g = (Gun)this.Weapon;
							this._ammoUIBar.Current = g.Ammo + (g.ReloadAmount * (g.ReloadingElapsed / g.ReloadTime));
						}
					}
					else if (this.Weapon is Gun)
						this._ammoUIBar.Current = ((Gun)this.Weapon).Ammo;
				}
			}
		}

		/// <summary>
		/// Método chamado para atualização da arma. Ele não é chamado diretamente mas sim pelo setter de `WeaponIndex`.
		/// </summary>
		/// <see cref="WeaponIndex"/>
		/// <param name="newWeapon"></param>
		protected void UpdateWeapon(Weapon newWeapon)
		{
			if (this._currentWeapon)
			{
				if (this._currentWeapon.IsReloading)
					this._currentWeapon.StopReloading();
				this._currentWeapon.gameObject.SetActive(false);
			}
			this._currentWeapon = newWeapon;
			newWeapon.gameObject.SetActive(true);
			Debug.Log(Time.timeSinceLevelLoad + ": UpdateWeapon " + newWeapon.gameObject.name);
		}

		public Boolean Ragdolled
		{
			get
			{
				return this._ragdolled;
			}
			set
			{
				if (this._ragdolled != value)
				{
					if (value)
						Debug.Log("Installing ragdoll");
					else
						Debug.Log("Removing ragdoll");

					this._ragdolled = value;
					this._animator.enabled = !value;
					foreach (BodyPart bp in this._bodyParts)
					{
						bp.rigidBody.isKinematic = this._ragdolled;
						if (this._ragdolled)
						{
							bp.collider.enabled = true;
							bp.rigidBody.isKinematic = false;
							if (bp.jointData != null)
							{
								CharacterJoint tmp = bp.gameObject.AddComponent<CharacterJoint>();
								bp.jointData.Assign(tmp);
							}
						}
						else
						{
							Destroy(bp.joint);
							bp.rigidBody.isKinematic = true;
							bp.collider.enabled = false;
						}
					}
					if (this._ragdolled)
					{
						this._rootCollider.enabled = false;
						this._body.Hips.collider.enabled = true;
					}
					else
					{
						this._body.Hips.rigidBody.isKinematic = true;
						this._body.Hips.collider.enabled = true;
						//
						this._rootRigidbody.isKinematic = false;
						this._rootCollider.enabled = true;
					}
				}
			}
		}

		void Start()
		{
			this._body = new Body();

			this._rootRigidbody = this.GetComponent<Rigidbody>();
			this._rootCollider = this.GetComponent<Collider>();
			// Radolled enabled by default.
			// this._rootCollider.enabled = false;
			// this._rootRigidbody.isKinematic = true;
			// ---

			ParticleSystem[] pSsytems = this.GetComponentsInChildren<ParticleSystem>();
			this._animator = this.GetComponent<Animator>();

			Rigidbody[] rigidBodies = this.GetComponentsInChildren<Rigidbody>();
			this._bodyParts = new BodyPart[rigidBodies.Length];
			for (int i = 0; i < rigidBodies.Length; ++i)
			{
				this._bodyParts[i] = new BodyPart(rigidBodies[i]);
				if (this._bodyParts[i].rigidBody.gameObject.name.EndsWith("_Hips"))
				{
					this._body.Hips = this._bodyParts[i];
					this._rootRigidbody = this._body.Hips.rigidBody;
					this._rootCollider = this._body.Hips.collider;
				}
			}

			Transform[] transforms = this.GetComponentsInChildren<Transform>();
			foreach (Transform t in transforms)
			{
				if (t.gameObject.name.EndsWith("_RightFoot"))
					this._body.RightFoot = t;
				else if (t.gameObject.name.EndsWith("_LeftFoot"))
					this._body.LeftFoot = t;
			}

			if (this._body.Hips == null)
				throw new Exception("Hips were not found.");
			if (this._body.LeftFoot == null)
				throw new Exception("Left foot was not found.");
			if (this._body.RightFoot == null)
				throw new Exception("Right foot was not found.");
			this.Ragdolled = false;

			this._arsenalPlaceholder = this.GetComponentInChildren<Arsenal>();

			if (true || this.photonView.isMine)
			{
				this._jetpackUIBar = GameObject.Find("JetpackBar").GetComponent<Bar>();
				this._jetpackUIBar.Max = this._jetpackCapacity;

				this._ammoUIBar = GameObject.Find("AmmoBar").GetComponent<Bar>();
			}

			this._jetpackFlames = pSsytems[0];
			this._jetpackSmoke = pSsytems[1];

			/*
			this._jetpackLight = this.GetComponentInChildren<Light>();
			this._jetpackLight.gameObject.SetActive(false);
			*/

			this._relCameraPos = new Vector3(0, 1.3f, -20f);

			this._jetpackFuel = this._jetpackCapacity;
			this._jetpackReloadRatio = (this._jetpackCapacity / this._jetpackReloadDuration);

			/*
			this.m_Capsule = this.GetComponent<CapsuleCollider>();
			this.m_CapsuleHeight = this.m_Capsule.height;
			this.m_CapsuleCenter = this.m_Capsule.center;
			*/

			this.UpdateRotation();
			this._started = true;
		}

		/// <summary>
		/// Efetua as verificações/atualizações referêntes ao Jetpack.
		/// 
		/// Se ligado com, com as condições corretas, atualiza a aceleração vertical do jogador; bem como o combustível do Jetpack.
		/// </summary>
		public void JetpackUpdate()
		{
			if (this.JetpackOn && (!this.IsGrounded) && (Time.time >= this._jetpackTime) && (this._jetpackFuel > 0f))
			{
				Vector3 v = this._rootRigidbody.velocity;
				v.y = Math.Min(v.y + 15f * Time.deltaTime, 4f);
				this._rootRigidbody.velocity = v;
				this._jetpackFuel = Math.Max(0f, this._jetpackFuel - Time.deltaTime);
				if (this.photonView.isMine)
					this._jetpackUIBar.Current = this._jetpackFuel;
			}
			else if (!this.JetpackOn)
			{
				this._jetpackFuel = Math.Min(this._jetpackFuel + this._jetpackReloadRatio * Time.deltaTime, this._jetpackCapacity);
				if (this.photonView.isMine)
					this._jetpackUIBar.Current = this._jetpackFuel;
			}
			else
			{
				this.JetpackOn = false;
			}
		}

		/// <summary>
		/// Inicia o pulo do jogador.
		/// </summary>
		public void Jump()
		{
			if (this._isGrounded)
			{
				this._jumpTime = Time.time;
				this._jetpackTime = this._jumpTime + 0.5f; // 0.5 segundos
				Vector3 v = this._rootRigidbody.velocity;
				v.y += this._jumpPower;
				this._rootRigidbody.velocity = v;
				this._isGrounded = false;
			}
		}

		protected bool IsFacingRight
		{
			get
			{
				if (this._rootRigidbody)
					return (this.transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
				else
					return true;
			}
		}

		/// <summary>
		/// Direção para onde a mira está apontando.
		/// </summary>
		public Vector3 AimDirection
		{
			get
			{
				return this._aimDirection;
			}
			private set
			{
				this._aimDirection = value;
				// this.AimHandler.transform.position = this.transform.position + (value * HEAD_HEIGHT) + this.AimOffset;
				this._aimAngle = -Mathf.Atan2(this._aimDirection.y, this._aimDirection.x) * Mathf.Rad2Deg;
			}
		}

		public float AimAngle
		{
			get
			{
				return this._aimAngle;
			}
		}

		protected void UpdateRotation()
		{
			this.transform.rotation = Quaternion.Euler(0, 90 * (this.IsFacingRight ? 1f : -1f), 0);
		}

		/// <summary>
		/// Manipula o movimento do jogador.
		/// </summary>
		/// <param name="move">Parâmetro dos movimentos do jogador.</param>
		public void Move(Vector3 move)
		{
			if (this._ragdolled)
				return;

			this.UpdateRotation();

			this.CheckGroundStatus();

			Vector3 v = this._rootRigidbody.velocity;
			if (this._isGrounded)
			{
				v.x = move.x * this.MaxHorizontalSpeed;
			}
			else
			{
				v.x = Mathf.Clamp(v.x + move.x * this.MaxHorizontalSpeed * Time.deltaTime, -this.MaxHorizontalSpeed, this.MaxHorizontalSpeed);
			}
			this._rootRigidbody.velocity = v;

			this.JetpackUpdate();

			this.UpdateAnimator();
		}

		/// <summary>
		/// Atualiza a animação do jogo.
		/// 
		/// Método baseado na implementação padrão do controlador de jogador de terceira pessoa do Unity.
		/// </summary>
		void UpdateAnimator()
		{
			Debug.Log(Time.timeSinceLevelLoad + ": " + this.IsGrounded);
			if (this._ragdolled)
				return;

			float amount = ((this.photonView.isMine) ? this._rootRigidbody.velocity.x : this._updatedVelocity.x ) / this.MaxHorizontalSpeed;
			this._animator.SetFloat("Forward", Math.Abs(amount), 0.1f, Time.deltaTime);
			// this.m_Animator.SetFloat("Turn", this.m_TurnAmount, 0.5f, Time.deltaTime);
			// this.m_Animator.SetBool("Crouch", this.m_Crouching);
			this._animator.SetBool("OnGround", this._isGrounded);
			if (!this._isGrounded)
			{
				if (this.photonView.isMine)
					this._animator.SetFloat("Jump", this._rootRigidbody.velocity.y);
				else
					this._animator.SetFloat("Jump", Mathf.Lerp(this._rootRigidbody.velocity.y, this._updatedVelocity.y, 0.2f));
			}

			float runCycle = Mathf.Repeat(this._animator.GetCurrentAnimatorStateInfo(0).normalizedTime + this._runCycleLegOffset, 1);
			float jumpLeg = (runCycle < _half ? 1 : -1) * amount;
			if (this._isGrounded)
			{ 
				this._animator.SetFloat("JumpLeg", jumpLeg);
			}

			/*
			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			if (this._isGrounded && move.magnitude > 0)
				this.m_Animator.speed = m_AnimSpeedMultiplier;
			else
				// don't use that while airborne
				this.m_Animator.speed = 1;
			*/
		}
		
		/// <summary>
		/// Verifica se o jogador está tocando, ou não, o chão.
		/// </summary>
		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(this._body.LeftFoot.position, this._body.LeftFoot.position + (Vector3.up * 0.1f) + (Vector3.down * this._groundCheckDistance), Color.blue);
			Debug.DrawLine(this._body.RightFoot.position, this._body.RightFoot.position + (Vector3.up * 0.1f) + (Vector3.down * this._groundCheckDistance), Color.green);
#endif
			this._isGrounded
				= Physics.Raycast(this._body.LeftFoot.position, Vector3.down, out hitInfo, this._groundCheckDistance)
				  || Physics.Raycast(this._body.RightFoot.position, Vector3.down, out hitInfo, this._groundCheckDistance);
		}

		/// <summary>
		/// Verifica se o jogador é um jogador de rede e aplica as animações.
		/// </summary>
		void FixedUpdate()
		{
			if (true || this.photonView.isMine)
			{
				// TODO: Ajustar de acordo com #36
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, this.transform.position + this._relCameraPos, 0.1f);

				/// Código temporário apenas para a exibição da mira. Apenas por enquanto que o jogador ainda não move os braços.
				Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position - this.AimOffset;
				m.z = 0;
				m.Normalize();
				this.AimDirection = m;
				if (this._ragdolled)
				{
					// this._rootRigidbody.transform.position = this._hipsBodypart.rigidBody.transform.position;
				}
			}
			else
			{
				//this._rigidbody.transform.position = Vector3.Lerp(this._rigidbody.transform.position, this._updatedPosition, 0.1f);
				this._rootRigidbody.transform.position = Vector3.Lerp(this._rootRigidbody.transform.position, this._updatedPosition, Time.deltaTime * 5f);
				this._rootRigidbody.transform.rotation = this._updatedRotation;
				this._rootRigidbody.velocity = this._updatedVelocity;
				this.AimDirection = Vector3.Lerp(this.AimDirection, this._updatedAimDirection, Time.deltaTime * 5f);

				this.CheckGroundStatus();
				this.UpdateAnimator();
			}
		}

		/// <summary>
		/// Caso seja local, escreve as atualizações da rede; caso seja remoto, aplica as atualizações recebidas.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="info"></param>
		void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (this._started)
			{
				if (stream.isWriting)
				{
					stream.SendNext(this._rootRigidbody.transform.position);
					stream.SendNext(this._rootRigidbody.transform.rotation);
					stream.SendNext(this._rootRigidbody.velocity);
					stream.SendNext(this._aimDirection);
				}
				else
				{
					this._updatedPosition = (Vector3)stream.ReceiveNext();
					this._updatedRotation = (Quaternion)stream.ReceiveNext();
					this._updatedVelocity = (Vector3)stream.ReceiveNext();
					this._updatedAimDirection = (Vector3)stream.ReceiveNext();
				}
			}
		}

		public virtual void Die()
		{
			Debug.Log ("It should be dead now!");
			// PhotonNetwork.Destroy(this.gameObject);
		}

		[PunRPC]
		public void CreateProjectile1(int[] networkIds, Vector3[] directions, Vector3[] positions)
		{
			if (this.Weapon is Gun)
			{
				((Gun)this.Weapon).CreateProjectile1(networkIds, directions, positions);
			}
		}
	}
}
