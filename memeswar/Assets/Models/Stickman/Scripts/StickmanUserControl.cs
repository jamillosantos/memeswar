﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace memewars
{
	[RequireComponent(typeof(StickmanCharacter))]
	public class StickmanUserControl : MonoBehaviour
	{
		private StickmanCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
		private Transform m_Cam;                  // A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


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
			if (!this.m_Jump)
			{
				this.m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}


		// Fixed update is called in sync with physics
		private void FixedUpdate()
		{
			// read inputs
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
			bool crouch = Input.GetKey(KeyCode.C);

			// calculate move direction to pass to character
			if (this.m_Cam != null)
			{
				// calculate camera relative direction to move:
				this.m_CamForward = Vector3.Scale(this.m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				this.m_Move = v * this.m_CamForward + h * this.m_Cam.right;
			}
			else
			{
				// we use world-relative directions in the case of no main camera
				this.m_Move = v * Vector3.forward + h * Vector3.right;
			}
#if !MOBILE_INPUT
			// walk speed multiplier
			if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

			// pass all parameters to the character control script
			this.m_Character.Move(m_Move, crouch, m_Jump);
			this.m_Jump = false;
		}
	}
}