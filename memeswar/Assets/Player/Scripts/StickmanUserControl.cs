using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

namespace Memewars
{
	[RequireComponent(typeof(StickmanCharacter))]
	public class StickmanUserControl : MonoBehaviour
	{
		private StickmanCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool _jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

		private void Start()
		{
			this.m_Character = GetComponent<StickmanCharacter>();
		}

		private void Update()
		{
			this.InputMouse();

			if (this.m_Character.IsGrounded)
			{
				if (!this._jump)
					this._jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
			else
				this.m_Character.JetpackOn = Input.GetKey(KeyCode.Space);
		}

		private void InputMouse()
		{
			if (this.m_Character.Weapon)
			{
				if (Input.GetMouseButton(0))
				{
					if (!this.m_Character.Weapon.Trigger1.Pulled)
						this.m_Character.Weapon.Trigger1.Pull();
				}
				else
				{
					if (this.m_Character.Weapon.Trigger1.Pulled)
						this.m_Character.Weapon.Trigger1.Release();
					if (Input.GetMouseButton(1))
					{
						if (!this.m_Character.Weapon.Trigger2.Pulled)
							this.m_Character.Weapon.Trigger2.Pull();

						if (this.m_Character.Weapon.Trigger2.Pulled)
							this.m_Character.Weapon.Trigger2.Release();
						else if (this.m_Character.Weapon.Trigger2.Pulled)
							this.m_Character.Weapon.Trigger2.Release();
					}
				}
			}
		}

		// Fixed update is called in sync with physics
		private void FixedUpdate()
		{
			if (this._jump)
			{
				this._jump = false;
				this.m_Character.Jump();
			}
			else
			{
				this.m_Move.x = 0;
				bool
					isRight = Input.GetKey(KeyCode.D),
					isLeft = Input.GetKey(KeyCode.A);

				if (Input.GetKeyDown(KeyCode.R))
					this.m_Character.Weapon.StartReloading();

				if (!(isRight && isLeft) && (isRight || isLeft))
				{
					if (isRight)
						this.m_Move.x = 1;
					else if (isLeft)
						this.m_Move.x = -1;
				}
				if (Input.GetKey(KeyCode.LeftShift))
					this.m_Move.x *= 0.5f;

				this.m_Character.Move(this.m_Move);
			}
		}
	}
}