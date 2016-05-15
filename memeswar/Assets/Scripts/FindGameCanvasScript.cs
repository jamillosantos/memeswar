using UnityEngine;
using System.Collections.Generic;

public class FindGameCanvasScript : MonoBehaviour
{

	private Canvas _canvas;

	private IList<GameObject> _list;

	public GameObject GameItemPrefab;

	// Use this for initialization
	void Start ()
	{
		this._canvas = this.GetComponent<Canvas>();
		this._list = new List<GameObject>();

		this.Add("Game 1", "127.0.0.1", 0, 0);
		this.Add("Game 2", "127.0.0.2", 3, 4);
	}

	void Add(string name, string ip, int ping, int players)
	{
		GameObject tmp;
		this._list.Add(tmp = Instantiate(GameItemPrefab));
		tmp.transform.SetParent(this._canvas.transform);
		Vector3 v = new Vector3(50, 1024 - (260 + 60 * this._list.Count), 50 + 50 * this._list.Count);
		tmp.transform.position = v;
		GameListItemScript script = tmp.GetComponent<GameListItemScript>();
		script.GameName = name;
		script.Ip = ip;
		script.Ping = ping;
		script.Players = players;
	}

	void Clear()
	{
		while (this._list.Count > 0)
		{
			Destroy(this._list[this._list.Count - 1]);
			this._list.RemoveAt(this._list.Count - 1);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//
	}
}
