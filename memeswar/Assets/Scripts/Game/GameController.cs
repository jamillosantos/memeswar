using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Canvas HUD;

	public Canvas Ranking;

	// Use this for initialization
	void Start ()
	{
		this.Ranking.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.Ranking.enabled = Input.GetKey(KeyCode.Tab);
		this.HUD.enabled = !this.Ranking.enabled;
	}
}
