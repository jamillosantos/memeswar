using UnityEngine;
using System.Collections;
using Memewars;

/// <summary>
/// Zona de morte, sempre que qualquer objeto tocar nela é automaticamente destruído.
/// </summary>
public class DeathZone : MonoBehaviour
{

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
