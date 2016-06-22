using UnityEngine;
using System.Collections;
using Memewars;

public class CrowArea : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		StickmanCharacter player = other.gameObject.GetComponent<StickmanCharacter>();
		if (player != null)
		{
			Crow[] crows = this.transform.root.gameObject.GetComponentsInChildren<Crow>();
			foreach (Crow c in crows)
				c.Play();
		}
	}
}
