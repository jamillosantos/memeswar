using UnityEngine;
using System.Collections;
using Memewars;

public class DeathZone : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		StickmanCharacter player = collision.gameObject.GetComponent<StickmanCharacter>();
		if (player)
		{
			if (player.photonView.isMine)
			{
				player.Die(new DeathInfo
				{
					Assassin = player
				});
			}
		}
		else
			Destroy(collision.gameObject);
	}
}
