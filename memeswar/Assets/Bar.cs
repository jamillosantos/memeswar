using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{

	public int Width = 300;

	public int Height = 30;

	public int BorderSize = 2;

	public Gradient Gradient;
	public Color BorderColor;

	public float Min = 0;

	public float Max = 100;

	public string Sufix = "";

	private Image _image;
	private Image _backgroundImage;
	private float _current;
	private Text _text;

	public float Current
	{
		get
		{
			return this._current;
		}
		set
		{
			this._current = value;
			float ratio = Mathf.Clamp((this._current - this.Min) / (this.Max - this.Min), 0f, 1f);
			this._image.rectTransform.sizeDelta = new Vector2(this.Width * ratio, this.Height);
			this._image.color = this.Gradient.Evaluate(ratio);
			this._text.text = Mathf.Round(this._current).ToString() + this.Sufix;
		}
	}

	void Start()
	{
		this._text = this.GetComponentInChildren<Text>();
		Image[] images = this.GetComponentsInChildren<Image>();
		this._image = images[1];
		this._backgroundImage = images[0];

		this._image.gameObject.transform.position = this.gameObject.transform.position;
		this._backgroundImage.gameObject.transform.position = this.gameObject.transform.position + new Vector3(-this.BorderSize, -this.BorderSize);
		this._text.rectTransform.position = this.transform.position + new Vector3(this.Width + 60, 0, 0);

		this._image.rectTransform.sizeDelta = new Vector2(this.Width, this.Height);
		this._backgroundImage.rectTransform.sizeDelta = new Vector2(this.Width + this.BorderSize*2, this.Height + this.BorderSize * 2);
		this._backgroundImage.color = this.BorderColor;

		this.Current = this.Max;
	}
}
