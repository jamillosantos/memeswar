using UnityEngine;
using System.Collections;
using System;

namespace memewars {

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class StickmanCharacter : Photon.MonoBehaviour
	{
		public float MaxHorizontalSpeed = 7f;

		[SerializeField]
		float _jumpPower = 9f;
		[Range(1f, 4f)]
		[SerializeField]
		float _runCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField]
		float _groundCheckDistance = 0.1f;

		public int WeaponIndex;

		Rigidbody _rigidbody;
		Animator _animator;
		bool _isGrounded;
		float _origGroundCheckDistance;
		const float _half = 0.5f;

		/*
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		*/

		private float _jumpTime = 0;
		private float _jetpackTime = 0;

		private float _jetpackCapacity = 3f;
		private float _jetpackReloadDuration = 5f;
		private float _jetpackFuel;
		private float _jetpackReloadRatio;
		private bool _started = false;
		private Vector3 _updatedPosition;
		private Quaternion _updatedRotation;
		private Vector3 _updatedVelocity;
		private Bar _jetpackUIBar;

		public bool IsGrounded
		{
			get
			{
				return this._isGrounded;
			}
		}

		public bool JetPackOn { get; set; }

		void Start()
		{
			this._jetpackUIBar = GameObject.Find("JetpackBar").GetComponent<Bar>();
			this._jetpackUIBar.Max = this._jetpackCapacity;
			this._jetpackFuel = this._jetpackCapacity;
			this._jetpackReloadRatio = (this._jetpackCapacity / this._jetpackReloadDuration);

			this._animator = this.GetComponent<Animator>();
			this._rigidbody = this.GetComponent<Rigidbody>();

			/*
			this.m_Capsule = this.GetComponent<CapsuleCollider>();
			this.m_CapsuleHeight = this.m_Capsule.height;
			this.m_CapsuleCenter = this.m_Capsule.center;
			*/

			this._rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;

			this._started = true;
		}

		public void JetPackUpdate()
		{
			if (this.JetPackOn && (!this.IsGrounded) && (Time.time >= this._jetpackTime) && (this._jetpackFuel > 0f))
			{
				Vector3 v = this._rigidbody.velocity;
				v.y = Math.Min(v.y + 15f * Time.deltaTime, 4f);
				this._rigidbody.velocity = v;
				this._jetpackFuel = Math.Max(0f, this._jetpackFuel - Time.deltaTime);
				this._jetpackUIBar.Current = this._jetpackFuel;
			}
			else if (!this.JetPackOn)
			{
				this._jetpackFuel = Math.Min(this._jetpackFuel + this._jetpackReloadRatio * Time.deltaTime, this._jetpackCapacity);
				this._jetpackUIBar.Current = this._jetpackFuel;
			}
		}

		public void Jump()
		{
			if (this._isGrounded)
			{
				this._jumpTime = Time.time;
				this._jetpackTime = this._jumpTime + 0.5f; // 0.5 segundos
				Vector3 v = this._rigidbody.velocity;
				v.y += this._jumpPower;
				this._rigidbody.velocity = v;
				this._isGrounded = false;
			}
		}

		public void Move(Vector3 move)
		{
			if (move.x != 0)
				this._rigidbody.transform.rotation = Quaternion.Euler(0, 90 * (move.x > 0 ? 1f : -1f), 0);

			this.CheckGroundStatus();

			Vector3 v = this._rigidbody.velocity;
			if (this._isGrounded)
				v.x = move.x * this.MaxHorizontalSpeed;
			else
				v.x = Math.Min(Math.Max(v.x + move.x * this.MaxHorizontalSpeed * Time.deltaTime, -this.MaxHorizontalSpeed), this.MaxHorizontalSpeed);
			this._rigidbody.velocity = v;

			this.JetPackUpdate();

			this.UpdateAnimator();
		}

		void UpdateAnimator()
		{
			float amount = this._rigidbody.velocity.x / this.MaxHorizontalSpeed;
			// update the animator parameters
			this._animator.SetFloat("Forward", Math.Abs(amount), 0.1f, Time.deltaTime);
			// this.m_Animator.SetFloat("Turn", this.m_TurnAmount, 0.5f, Time.deltaTime);
			// this.m_Animator.SetBool("Crouch", this.m_Crouching);
			this._animator.SetBool("OnGround", this._isGrounded);
			if (!this._isGrounded)
				this._animator.SetFloat("Jump", _rigidbody.velocity.y);

			float runCycle = Mathf.Repeat(this._animator.GetCurrentAnimatorStateInfo(0).normalizedTime + this._runCycleLegOffset, 1);
			float jumpLeg = (runCycle < _half ? 1 : -1) * amount;
			if (this._isGrounded)
				this._animator.SetFloat("JumpLeg", jumpLeg);

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
		
		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(this.transform.position + (Vector3.up * 0.1f), this.transform.position + (Vector3.up * 0.1f) + (Vector3.down * this._groundCheckDistance));
#endif
			this._animator.applyRootMotion = this._isGrounded = Physics.Raycast(this.transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, this._groundCheckDistance);
			/*
			if (this.m_Animator.applyRootMotion = this._isGrounded = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
				this.m_GroundNormal = hitInfo.normal;
			else
				this.m_GroundNormal = Vector3.up;
			*/
		}

		void FixedUpdate()
		{
			if (!this.photonView.isMine)
			{
				// this._rigidbody.transform.position = this._updatedPosition + (this._updatedVelocity * Time.deltaTime);
				this._rigidbody.transform.position = Vector3.Lerp(this._rigidbody.transform.position, this._updatedPosition, 0.1f) + (this._updatedVelocity * Time.deltaTime);
				this._rigidbody.transform.rotation = this._updatedRotation;
				this._rigidbody.velocity = Vector3.Lerp(this._rigidbody.velocity, this._updatedVelocity, 0.1f);

				this.CheckGroundStatus();
				this.UpdateAnimator();
			}
		}

		void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (this._started)
			{
				if (stream.isWriting)
				{
					stream.SendNext(this._rigidbody.transform.position);
					stream.SendNext(this._rigidbody.transform.rotation);
					stream.SendNext(this._rigidbody.velocity);
				}
				else
				{
					this._updatedPosition = (Vector3)stream.ReceiveNext();
					this._updatedRotation = (Quaternion)stream.ReceiveNext();
					this._updatedVelocity = (Vector3)stream.ReceiveNext();
				}

			}
		}
	}
}
