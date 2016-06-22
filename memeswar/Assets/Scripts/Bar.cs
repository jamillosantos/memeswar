using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
	public Gradient Gradient;

	public float Min = 0;

	public float Max = 100;

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
			this._image.rectTransform.sizeDelta = new Vector2(this._backgroundImage.rectTransform.rect.width * ratio, this._backgroundImage.rectTransform.rect.height);
			// this._image.color = this.Gradient.Evaluate(ratio);
			if (this._text)
				this._text.text = Mathf.Round(this._current).ToString();
		}
	}

	void Start()
	{
		this._text = this.GetComponentInChildren<Text>();
		Image[] images = this.GetComponentsInChildren<Image>();
		foreach (Image image in images)
		{
			switch (image.gameObject.name)
			{
				case "Background":
					this._backgroundImage = image;
					break;
				case "Bar":
					this._image = image;
					break;
			}
		}
		this.Current = this.Max;
	}
}
