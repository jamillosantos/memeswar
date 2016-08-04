using UnityEngine;
using System.Collections;
using Memewars;

public class CreateMyPlayer : MonoBehaviour
{

	void Start()
	{
		this.CreatePlayer();
	}

	private void CreatePlayer()
	{
		FlashMessage.Popup(GameObject.Find("HUD").transform, "Criando aqui!", 10f);

		GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero + new Vector3(0, 5, 0), Quaternion.identity, 0, new object[] {
			new Weapon.Weapons[] {
				Weapon.Weapons.AK47, Weapon.Weapons.RocketLauncher, Weapon.Weapons.Shotgun, Weapon.Weapons.AK47, Weapon.Weapons.AK47
			}
		});
		player.GetComponent<StickmanUserControl>().enabled = true;
		player.GetComponent<AudioListener>().enabled = true;

		StickmanCharacter c = player.GetComponent<StickmanCharacter>();
	}
}
