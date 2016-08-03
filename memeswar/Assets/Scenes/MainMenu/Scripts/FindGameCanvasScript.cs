using UnityEngine;
using System.Collections.Generic;

public class FindGameCanvasScript : MonoBehaviour
{

	private Canvas _canvas;

	private List<GameObject> _list;

	public UnityEngine.Object _gameItemOriginal;

	public GameObject ListContainer;

	public GameObject ListEmptyWarning;

	void OnEnable()
	{
		this.RefreshRooms();
	}

	public void RefreshRooms()
	{
		if (this._canvas == null)
		{ 
			this._canvas = this.GetComponent<Canvas>();
			this._list = new List<GameObject>();
			this._gameItemOriginal = Resources.Load("FindGameItem");
		}
		this._canvas.enabled = true;

		FindGameItem[] items = this.GetComponentsInChildren<FindGameItem>();
		foreach (FindGameItem item in items)
			Destroy(item.gameObject);
		this._list.Clear();

		RoomInfo[] roomList = PhotonNetwork.GetRoomList();
		this.ListEmptyWarning.SetActive(roomList.Length == 0);
		foreach (RoomInfo room in roomList)
			this.Add(room.name, room.playerCount, room.maxPlayers);
	}

	void Add(string name, int players, int maxPlayers)
	{
		GameObject tmp = (GameObject)Instantiate(this._gameItemOriginal, new Vector3(0, (this._list.Count - 1) * 36), Quaternion.identity);
		this._list.Add(tmp);
		tmp.transform.SetParent(this.ListContainer.transform, false);
		FindGameItem item = tmp.GetComponent<FindGameItem>();
		item.Name.text = name;
		item.Players.text = players.ToString();
		item.MaxPlayers.text = maxPlayers.ToString();
	}
}
