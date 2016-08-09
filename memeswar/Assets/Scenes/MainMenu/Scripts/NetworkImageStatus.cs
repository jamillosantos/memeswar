using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Script que cuida da atualização do status de network do jogo.
/// </summary>
public class NetworkImageStatus : MonoBehaviour
{
	public Color Success;

	public Color Warning;

	public Color Error;

	private RawImage _image;

	void Start ()
	{
		this._image = this.GetComponent<RawImage>();
	}
	
	void Update()
	{
		switch (PhotonNetwork.connectionState)
		{
			case ConnectionState.Connected:
				this._image.color = this.Success;
				break;
			case ConnectionState.Disconnected:
				this._image.color = this.Error;
				break;
			case ConnectionState.Connecting:
			case ConnectionState.Disconnecting:
			case ConnectionState.InitializingApplication:
				this._image.color = this.Warning;
				break;
		}
	}
}
