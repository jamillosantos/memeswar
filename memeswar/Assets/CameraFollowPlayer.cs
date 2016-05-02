using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{

	public float smooth = 0.7f;         // The relative speed at which the camera will catch up.


	private Transform player;           // Reference to the player's transform.
	private Vector3 relCameraPos;       // The relative position of the camera from the player.
	private float relCameraPosMag;      // The distance of the camera from the player.
	private Vector3 newPos;             // The position the camera is trying to reach.



	// Use this for initialization
	void Awake()
	{
		// Setting up the reference.
		this.player = GameObject.FindGameObjectWithTag("Player").transform;

		// Setting the relative position as the initial relative position of the camera in the scene.
		this.relCameraPos = transform.position - player.position;
		this.relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp(this.player.position, this.player.position + this.relCameraPos, this.smooth);
	}
}
