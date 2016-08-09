using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Classe que representa um item no ranking.
/// </summary>
public class RankingItem : MonoBehaviour
{
	private Text _index;

	private int _indexValue;

	private Text _name;

	private string _nameValue;
	
	private Text _score;

	private int _scoreValue;
	
	private Text _deaths;

	private int _deathsValue;

	private RectTransform _rectTransform;

	public int Index
	{
		get
		{
			return this._indexValue;
		}
		set
		{
			this._indexValue = value;
		}
	}

	public string Name
	{
		get
		{
			return this._nameValue;
		}
		set
		{
			this._nameValue = value;
		}
	}

	public int Score
	{
		get
		{
			return this._scoreValue;
		}
		set
		{
			this._scoreValue = value;
		}
	}

	public int Deaths
	{
		get
		{
			return this._deathsValue;
		}
		set
		{
			this._deathsValue = value;
		}
	}

	void Start ()
	{
		this._rectTransform = this.GetComponent<RectTransform>();
		Text[] texts = this.GetComponentsInChildren<Text>();
		this._index = texts[0];
		this._name = texts[1];
		this._score = texts[2];
		this._deaths = texts[3];
		this.Apply();
	}

	/// <summary>
	/// Joga os dados da classe na interface.
	/// </summary>
	public void Apply()
	{ 
		this._rectTransform.anchoredPosition = new Vector3(60, -170 - (35 * this._indexValue));
		this._index.text = (this._indexValue + 1).ToString();
		this._name.text = this._nameValue;
		this._score.text = this._scoreValue.ToString();
		this._deaths.text = this._deathsValue.ToString();
	}

	void OnGUI()
	{
		GUILayout.Label("Data: " + this._indexValue + ": " + this._nameValue + ": " + this._rectTransform.anchoredPosition.y);
	}
}
