using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{

	public float smooth = 0.7f;         // The relative speed at which the camera will catch up.

	private Transform player;           // Reference to the player's transform.

	private Vector3 relCameraPos;       // The relative position of the camera from the player.


	// Use this for initialization
	void Awake()
	{
	/*
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.relCameraPos = this.transform.position - player.position;
	*/
	}

	void FixedUpdate()
	{
		// this.transform.position = Vector3.Lerp(this.player.position, this.player.position + this.relCameraPos, this.smooth);
	}
}
