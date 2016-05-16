using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

namespace memewars
{
	[RequireComponent(typeof(StickmanCharacter))]
	public class StickmanUserControl : MonoBehaviour
	{
		private StickmanCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
		private Transform m_Cam;                  // A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool _jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


		private void Start()
		{
			// get the transform of the main camera
			if (Camera.main != null)
				this.m_Cam = Camera.main.transform;
			else
			{
				Debug.LogWarning(
					"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
				// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
			}

			// get the third person character ( this should never be null due to require component )
			this.m_Character = GetComponent<StickmanCharacter>();
		}


		private void Update()
		{
			if (this.m_Character.IsGrounded)
			{
				if (!this._jump)
					this._jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
			else
				this.m_Character.JetPackOn = Input.GetKey(KeyCode.Space);
		}


		// Fixed update is called in sync with physics
		private void FixedUpdate()
		{
			if (this._jump)
				this.m_Character.Jump();

			this.m_Move.x = 0;
			bool
				isRight = Input.GetKey(KeyCode.D),
				isLeft = Input.GetKey(KeyCode.A);
			if (!(isRight && isLeft) && (isRight || isLeft))
			{
				if (isRight)
					this.m_Move.x = 1;
				else if (isLeft)
					this.m_Move.x = -1;
			}
			if (Input.GetKey(KeyCode.LeftShift))
				this.m_Move.x *= 0.5f;

			bool crouch = Input.GetKey(KeyCode.LeftControl);
			this.m_Character.Move(this.m_Move);
			this._jump = false;
		}
	}
}