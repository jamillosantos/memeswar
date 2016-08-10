using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

namespace Memewars
{
	[RequireComponent(typeof(StickmanCharacter))]
	public class StickmanUserControl : MonoBehaviour
	{
		private StickmanCharacter StickmanCharacter; // A reference to the ThirdPersonCharacter on the object
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool _jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

		private void Start()
		{
			this.StickmanCharacter = GetComponent<StickmanCharacter>();
		}

		private void Update()
		{
			if (!GameController.Instance.ControllersEnabled)
				return;

			this.InputMouse();

			if (Input.GetKeyDown(KeyCode.Alpha1))
				this.StickmanCharacter.WeaponIndex = 0;
			else if (Input.GetKeyDown(KeyCode.Alpha2))
				this.StickmanCharacter.WeaponIndex = 1;
			else if (Input.GetKeyDown(KeyCode.Alpha3))
				this.StickmanCharacter.WeaponIndex = 2;
			else if (Input.GetKeyDown(KeyCode.Alpha4))
				this.StickmanCharacter.WeaponIndex = 3;
			else if (Input.GetKeyDown(KeyCode.Alpha5))
				this.StickmanCharacter.WeaponIndex = 4;
			else if (Input.GetKeyDown(KeyCode.L))
				this.StickmanCharacter.Ragdoll();

			if (this.StickmanCharacter.IsGrounded)
			{
				if (!this._jump)
					this._jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
			else
				this.StickmanCharacter.JetpackOn = Input.GetKey(KeyCode.Space);
		}

		private void InputMouse()
		{
			if (this.StickmanCharacter.Weapon)
			{
				if (Input.GetMouseButton(0))
				{
					if (!this.StickmanCharacter.Weapon.Trigger1.Pulled)
						this.StickmanCharacter.Weapon.Trigger1.Pull();
				}
				else
				{
					if (this.StickmanCharacter.Weapon.Trigger1.Pulled)
						this.StickmanCharacter.Weapon.Trigger1.Release();
					if (Input.GetMouseButton(1))
					{
						if (!this.StickmanCharacter.Weapon.Trigger2.Pulled)
							this.StickmanCharacter.Weapon.Trigger2.Pull();

						if (this.StickmanCharacter.Weapon.Trigger2.Pulled)
							this.StickmanCharacter.Weapon.Trigger2.Release();
						else if (this.StickmanCharacter.Weapon.Trigger2.Pulled)
							this.StickmanCharacter.Weapon.Trigger2.Release();
					}
				}
			}
		}

		private void FixedUpdate()
		{
			if (!GameController.Instance.ControllersEnabled)
				return;

			if (this._jump)
			{
				this._jump = false;
				this.StickmanCharacter.Jump();
			}
			else
			{
				this.m_Move.x = 0;
				bool
					isRight = Input.GetKey(KeyCode.D),
					isLeft = Input.GetKey(KeyCode.A);

				if (Input.GetKey(KeyCode.R))
					this.StickmanCharacter.Weapon.StartReloading();

				if (!(isRight && isLeft) && (isRight || isLeft))
				{
					if (isRight)
						this.m_Move.x = 1;
					else if (isLeft)
						this.m_Move.x = -1;
				}
				if (Input.GetKey(KeyCode.LeftShift))
					this.m_Move.x *= 0.5f;

				this.StickmanCharacter.Move(this.m_Move);
			}
		}
	}
}