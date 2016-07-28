using UnityEngine;
using System.Collections;

public class HeadSprite : MonoBehaviour {

	private SpriteRenderer _renderer;

	private float _changeAt = 0;

	// Use this for initialization
	void Start ()
	{
		this._renderer = this.GetComponent<SpriteRenderer>();
		this.SetFace(FacesManager.lol, 10);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ((this._changeAt > 0) && (this._changeAt <= Time.timeSinceLevelLoad))
		{
			this._renderer.sprite = FacesManager.fkme;
			this._changeAt = 0;
		}
	}

	public void SetFace(Sprite face, float duration)
	{
		this._renderer.sprite = face;
		this._changeAt = Time.timeSinceLevelLoad + duration;
	}
}
