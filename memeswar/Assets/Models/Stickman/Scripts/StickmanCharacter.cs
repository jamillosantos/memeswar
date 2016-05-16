using UnityEngine;
using System.Collections;
using System;

namespace memewars {

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class StickmanCharacter : MonoBehaviour
	{
		public float MaxHorizontalSpeed = 7f;

		[SerializeField]
		float m_MovingTurnSpeed = 360;
		[SerializeField]
		float m_StationaryTurnSpeed = 180;
		[SerializeField]
		float m_JumpPower = 9f;
		[Range(1f, 4f)]
		[SerializeField]
		float m_GravityMultiplier = 2f;
		[SerializeField]
		float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField]
		float m_MoveSpeedMultiplier = 1f;
		[SerializeField]
		float m_AnimSpeedMultiplier = 1f;
		[SerializeField]
		float m_GroundCheckDistance = 0.1f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool _isGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;

		private float _jumpTime = 0;
		private float _jetpackTime = 0;

		private float _jetpackCapacity = 3f;
		private float _jetpackReloadDuration = 5f;
		private float _jetpackFuel;
		private float _jetpackReloadRatio;

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
			this._jetpackFuel = this._jetpackCapacity;
			this._jetpackReloadRatio = (this._jetpackCapacity / this._jetpackReloadDuration);

			this.m_Animator = this.GetComponent<Animator>();
			this.m_Rigidbody = this.GetComponent<Rigidbody>();
			this.m_Capsule = this.GetComponent<CapsuleCollider>();
			this.m_CapsuleHeight = this.m_Capsule.height;
			this.m_CapsuleCenter = this.m_Capsule.center;

			this.m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			this.m_OrigGroundCheckDistance = this.m_GroundCheckDistance;
		}

		public void JetPackUpdate()
		{
			if (this.JetPackOn && (!this.IsGrounded) && (Time.time >= this._jetpackTime) && (this._jetpackFuel > 0f))
			{
				Vector3 v = this.m_Rigidbody.velocity;
				v.y = Math.Min(v.y + 15f * Time.deltaTime, 4f);
				this.m_Rigidbody.velocity = v;
				this._jetpackFuel = Math.Max(0f, this._jetpackFuel - Time.deltaTime);
			}
			else if (!this.JetPackOn)
				this._jetpackFuel = Math.Min(this._jetpackFuel + this._jetpackReloadRatio * Time.deltaTime, this._jetpackCapacity);
		}

		public void Jump()
		{
			if (this._isGrounded)
			{
				this._jumpTime = Time.time;
				this._jetpackTime = this._jumpTime + 0.5f; // 0.5 segundos
				Vector3 v = this.m_Rigidbody.velocity;
				v.y += this.m_JumpPower;
				this.m_Rigidbody.velocity = v;
				this._isGrounded = false;
			}
		}

		public void Move(Vector3 move)
		{
			if (move.x != 0)
				this.m_Rigidbody.transform.rotation = Quaternion.Euler(0, 90 * (move.x > 0 ? 1f : -1f), 0);

			this.CheckGroundStatus();

			Vector3 v = this.m_Rigidbody.velocity;
			if (this._isGrounded)
				v.x = move.x * this.MaxHorizontalSpeed;
			else
				v.x = Math.Min(Math.Max(v.x + move.x * this.MaxHorizontalSpeed * Time.deltaTime, -this.MaxHorizontalSpeed), this.MaxHorizontalSpeed);
			this.m_Rigidbody.velocity = v;

			this.JetPackUpdate();

			this.UpdateAnimator();
		}

		void UpdateAnimator()
		{
			float amount = this.m_Rigidbody.velocity.x / this.MaxHorizontalSpeed;
			// update the animator parameters
			this.m_Animator.SetFloat("Forward", Math.Abs(amount), 0.1f, Time.deltaTime);
			this.m_Animator.SetFloat("Turn", this.m_TurnAmount, 0.5f, Time.deltaTime);
			this.m_Animator.SetBool("Crouch", this.m_Crouching);
			this.m_Animator.SetBool("OnGround", this._isGrounded);
			if (!this._isGrounded)
				this.m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);

			float runCycle = Mathf.Repeat(this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + this.m_RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < k_Half ? 1 : -1) * amount;
			if (this._isGrounded)
				this.m_Animator.SetFloat("JumpLeg", jumpLeg);

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


		void HandleAirborneMovement()
		{
			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
			this.m_Rigidbody.AddForce(extraGravityForce);

			this.m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
		}


		void HandleGroundedMovement(bool crouch, bool jump)
		{
			// check whether conditions are right to allow a jump:
			if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				// jump!
				this.m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
				this._isGrounded = false;
				this.m_Animator.applyRootMotion = false;
				this.m_GroundCheckDistance = 0.1f;
			}
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			// if (m_IsGrounded && Time.deltaTime > 0)
			if (_isGrounded && Time.deltaTime > 0)
			{
				// Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
				Vector3 v = (m_Animator.deltaPosition) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}

		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (this.m_Animator.applyRootMotion = this._isGrounded = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
				this.m_GroundNormal = hitInfo.normal;
			else
				this.m_GroundNormal = Vector3.up;
		}
	}
}
