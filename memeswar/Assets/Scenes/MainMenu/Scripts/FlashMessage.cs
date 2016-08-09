using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Classe que representa as mensagens temporárias do sistema na tela.
/// </summary>
public class FlashMessage : MonoBehaviour
{

	static UnityEngine.Object _original;

	private Text _text;

	public string Text;

	/// <summary>
	/// Exibe uma mensagem temporária na tela.
	/// </summary>
	/// <param name="parent">Transform onde a mensagem deverá ser exibida.</param>
	/// <param name="text">Texto que será apresentado.</param>
	/// <param name="duration">Duração, em segundos, da mensagem na tela.</param>
	/// <returns></returns>
	public static FlashMessage Popup(Transform parent, string text, float duration)
	{
		if (_original == null)
			_original = Resources.Load("FlashMessage");
		FlashMessage result = ((GameObject)Instantiate(_original)).GetComponent<FlashMessage>();
		result.transform.SetParent(parent, false);
		result.Text = text;
		if (duration > 0)
			result.Invoke("AutoDestroy", duration);
		return result;
	}

	void Start()
	{
		this._text = this.GetComponentInChildren<Text>();
		this._text.text = this.Text;
	}

	void AutoDestroy()
	{
		Destroy(this.gameObject);
	}
}
