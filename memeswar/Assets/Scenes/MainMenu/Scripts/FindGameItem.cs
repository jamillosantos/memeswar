using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindGameItem : MonoBehaviour
{
	public Text Name;

	public Text Players;

	public Text MaxPlayers;

	public void JoinClick()
	{
		Debug.Log("Clicou no join!!!" + this.Name.text);
	}

}
