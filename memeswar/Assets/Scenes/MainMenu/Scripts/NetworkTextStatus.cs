using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Script que cuida da atualização do status de network do jogo.
/// </summary>
public class NetworkTextStatus : MonoBehaviour
{

	private Text _text;

	void Start ()
	{
		this._text = this.GetComponent<Text>();
	}
	
	void Update ()
	{
		this._text.text = PhotonNetwork.connectionState.ToString() + '\n' + PhotonNetwork.connectionStateDetailed.ToString();
	}
}
