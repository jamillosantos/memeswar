using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controlador do jogo da HUD do jogo.
/// </summary>
public class GameController : MonoBehaviour
{
	public Canvas HUD;

	public Canvas Ranking;

	public Canvas Options;

	public InputField InputField;

	private bool _controllersEnabled;

	private static GameController _instance;

	/// <summary>
	/// Flag que controla se os controles do jogo estão ativos ou não. Caso inativa, é impossível controlar o jogador.
	/// </summary>
	public bool ControllersEnabled
	{
		get
		{
			return this._controllersEnabled;
		}
	}

	public static GameController Instance
	{
		get
		{
			return _instance;
		}
	}

	// Use this for initialization
	void Start ()
	{
		_instance = this;
		this.Ranking.gameObject.SetActive(false);
		this.Options.enabled = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.Options.enabled = !this.Options.enabled;
			if (this.Options.enabled)
				this.InputField.text = PhotonNetwork.playerName;
		}

		if (!this.Options.enabled)
		{
			this.Ranking.gameObject.SetActive(Input.GetKey(KeyCode.Tab));
			this.HUD.gameObject.SetActive(!this.Ranking.gameObject.GetActive());
		}
		this._controllersEnabled = !this.Options.enabled;
	}

	/// <summary>
	/// Método chamado pela HUD quando o jogador mudar o nome do jogador na interface.
	/// </summary>
	public void UpdateNickName()
	{
		PhotonNetwork.playerName = this.InputField.text;
		this.Options.enabled = false;
	}
}
