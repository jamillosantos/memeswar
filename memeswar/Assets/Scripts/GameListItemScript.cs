using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameListItemScript : MonoBehaviour
{
	private bool _started = false;

	private string _name;

	private string _ip;

	private int _ping;

	private int _players;

	public string GameName
	{
		get
		{
			return this._name;
		}
		set
		{
			this._name = value;
			if (this._started)
				this._nameText.text = this._name;
		}
	}

	public string Ip
	{
		get {
			return this._ip;
		}
		set
		{
			this._ip = value;
			if (this._started)
				this._ipText.text = this._ip;
		}
	}

	public int Ping
	{
		get
		{
			return this._ping;
		}
		set
		{
			this._ping = value;
			if (this._started)
				this._pingText.text = this._ping.ToString() + "ms";
		}
	}

	public int Players
	{
		get
		{
			return this._players;
		}
		set
		{
			this._players = value;
			if (this._started)
				this._playersText.text = this._players.ToString();
		}
	}

	private Text _nameText;

	private Text _ipText;

	private Text _pingText;

	private Text _playersText;

	void Start ()
	{
		Text[] texts = this.GetComponentsInChildren<Text>();

		this._nameText = texts[0];
		this._ipText = texts[1];
		this._pingText = texts[2];
		this._playersText = texts[3];

		Debug.Log(this._nameText);

		this._started = true;

		this._nameText.text = this._name;
		this._ipText.text = this._ip;
		this._pingText.text = this._ping.ToString() + "ms";
		this._playersText.text = this._players.ToString();
	}
}
