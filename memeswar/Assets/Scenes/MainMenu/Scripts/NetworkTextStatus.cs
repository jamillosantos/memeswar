using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
