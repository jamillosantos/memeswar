using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Comparador que classifica os jogadores pelos pontos.
/// </summary>
class PhotonPlayerRankingComparer : IComparer<PhotonPlayer>
{
	int IComparer<PhotonPlayer>.Compare(PhotonPlayer x, PhotonPlayer y)
	{
		return y.GetScore() - x.GetScore();
	}
}

/// <summary>
/// Controlador que exibe a interface de ranking.
/// </summary>
public class RankingController : MonoBehaviour {

	static UnityEngine.Object RankingItemOriginal;

	/// <summary>
	/// Sempre que a tela for exibida ela é ativada, assim este método é chamado.
	/// </summary>
	void OnEnable()
	{
		/// Carrega o item dos resources para poder ser clonado.
		if (RankingItemOriginal == null)
			RankingItemOriginal = Resources.Load("RankingItem");

		/// Remove todos os items do ranking
		foreach (RankingItem ri in this.GetComponentsInChildren<RankingItem>())
		{
			Destroy(ri.gameObject);
		}

		/// Coleta e organiza o ranking dos jogadores.
		List<PhotonPlayer> players = new List<PhotonPlayer>();
		foreach (PhotonPlayer player in PhotonNetwork.playerList)
			players.Add(player);
		players.Sort(new PhotonPlayerRankingComparer());

		/// Cria os elementos visuais do jogo.
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

	/// <summary>
	/// Método utilizado apenas para debug.
	/// </summary>
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
