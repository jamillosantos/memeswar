using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

class PhotonPlayerRankingComparer : IComparer<PhotonPlayer>
{
	int IComparer<PhotonPlayer>.Compare(PhotonPlayer x, PhotonPlayer y)
	{
		return y.GetScore() - x.GetScore();
	}
}

public class RankingController : MonoBehaviour {

	static UnityEngine.Object RankingItemOriginal = Resources.Load("RankingItem");

	void OnEnable()
	{
		foreach (RankingItem ri in this.GetComponentsInChildren<RankingItem>())
		{
			Destroy(ri.gameObject);
		}

		List<PhotonPlayer> players = new List<PhotonPlayer>();
		foreach (PhotonPlayer player in PhotonNetwork.playerList)
		{
			players.Add(player);
		}
		players.Sort(new PhotonPlayerRankingComparer());

		int i = 0;
		foreach (PhotonPlayer player in players)
		{
			Debug.Log(player.name + ": " + player.GetScore());
			RankingItem ri = ((GameObject)Instantiate(RankingItemOriginal)).GetComponent<RankingItem>();
			ri.transform.SetParent(this.transform, true);
			ri.Index = i++;
			ri.Name = player.name;
			ri.Score = player.GetScore();
			ri.Deaths = 0; // TODO
		}
	}

	void Update ()
	{
		//
	}
}
