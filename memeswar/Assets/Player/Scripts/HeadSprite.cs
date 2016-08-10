using UnityEngine;
using System.Collections;
using Memewars;

/// <summary>
/// Controla as mudanças de face do jogador.
/// </summary>
public class HeadSprite : MonoBehaviour
{

	private StickmanCharacter _stickmanCharacter;

	private SpriteRenderer _renderer;

	private float _changeAt = 0;

	void Start ()
	{
		this._stickmanCharacter = this.GetComponentInParent<StickmanCharacter>();
		this._renderer = this.GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
		/// Retorna face para o padrão.
		if ((this._changeAt > 0) && (this._changeAt <= Time.timeSinceLevelLoad))
		{
			this._stickmanCharacter.photonView.owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
			{
				{ "MemeFace", MemeFaces.FkMe }
			});
			this._changeAt = 0;
		}
		object face;
		if (!this._stickmanCharacter.photonView.owner.customProperties.TryGetValue("MemeFace", out face))
			face = MemeFaces.FkMe;
		if (!this._stickmanCharacter.photonView.isMine)
			Debug.Log(Time.timeSinceLevelLoad + ": " + (MemeFaces)face);
		this._renderer.sprite = FacesManager.GetSprite((MemeFaces)face);
	}

	public void SetFace(MemeFaces face, float duration)
	{
		this._stickmanCharacter.photonView.owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
		{
			{ "MemeFace", face }
		});
		this._changeAt = Time.timeSinceLevelLoad + duration;
	}
}
