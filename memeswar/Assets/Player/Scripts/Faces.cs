using System.ComponentModel;
using UnityEngine;

public class FacesManager
{
	static Sprite _notokay;

	public static Sprite notokay
	{
		get
		{
			if (_notokay == null)
				_notokay = Resources.Load<Sprite>("face_notokay");
			return _notokay;
		}
	}

	static Sprite _fkme;

	public static Sprite fkme
	{
		get
		{
			if (_fkme == null)
				_fkme = Resources.Load<Sprite>("face_fkme");
			return _fkme;
		}
	}

	static Sprite _bilious;

	public static Sprite bilious
	{
		get
		{
			if (_bilious == null)
				_bilious = Resources.Load<Sprite>("face_bilious");
			return _bilious;
		}
	}

	static Sprite _lol;

	public static Sprite lol
	{
		get
		{
			if (_lol == null)
				_lol = Resources.Load<Sprite>("face_lol");
			return _lol;
		}
	}

	static Sprite _unhappy;

	public static Sprite unhappy
	{
		get
		{
			if (_unhappy == null)
				_unhappy = Resources.Load<Sprite>("face_unhappy");
			return _unhappy;
		}
	}

	static Sprite _troll;

	public static Sprite troll
	{
		get
		{
			if (_troll == null)
				_troll = Resources.Load<Sprite>("face_troll");
			return _troll;
		}
	}

	public static Sprite Die
	{
		get
		{
			int faceIndex = Random.Range(0, 2);
			switch (faceIndex)
			{
				case 0:
					return bilious;
				case 1:
					return unhappy;
			}
			return fkme;
		}
	}

	public static Sprite Win
	{
		get
		{
			int faceIndex = Random.Range(0, 2);
			switch (faceIndex)
			{
				case 0:
					return troll;
				case 1:
					return notokay;
				case 2:
					return lol;
			}
			return fkme;
		}
	}
}
