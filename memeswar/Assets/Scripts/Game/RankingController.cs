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

	static UnityEngine.Object RankingItemOriginal;

	void OnEnable()
	{
		if (RankingItemOriginal == null)
			RankingItemOriginal = Resources.Load("RankingItem");
		foreach (RankingItem ri in this.GetComponentsInChildren<RankingItem>())
		{
			Destroy(ri.gameObject);
		}

		List<PhotonPlayer> players = new List<PhotonPlayer>();
		foreach (PhotonPlayer player in PhotonNetwork.playerList)
			players.Add(player);
		players.Sort(new PhotonPlayerRankingComparer());

		int i = 0;
		foreach (PhotonPlayer player in players)
		{
			RankingItem ri = ((GameObject)Instantiate(RankingItemOriginal)).GetComponent<RankingItem>();
			ri.transform.SetParent(this.gameObject.transform, true);
			ri.Index = i++;
			ri.Name = player.name;
			ri.Score = player.GetScore();
			object deaths = 0;
			if (player.customProperties.TryGetValue("Deaths", out deaths))
				ri.Deaths = (int)deaths;
			else
				ri.Deaths = 0;
		}
	}

	void OnGUI()
	{
		string d = "\n\n\n\n\n";
		foreach (RankingItem ri in this.GetComponentsInChildren<RankingItem>())
		{
			d += ri.name + ": " + ri.GetComponent<RectTransform>().anchoredPosition.x + ":" + ri.GetComponent<RectTransform>().anchoredPosition.y + "\n";
		}
		GUILayout.Label(d);
	}
}
