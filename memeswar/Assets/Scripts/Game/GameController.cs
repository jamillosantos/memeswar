using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Canvas HUD;

	public Canvas Ranking;

	// Use this for initialization
	void Start ()
	{
		this.Ranking.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
		this.Ranking.gameObject.SetActive(Input.GetKey(KeyCode.Tab));
		this.HUD.gameObject.SetActive(!this.Ranking.gameObject.GetActive());
	}
}
