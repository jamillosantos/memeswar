using UnityEngine;
using System.Collections;

/// <summary>
/// Script helper para exibição da lua.
/// </summary>
public class Moon : MonoBehaviour
{

	public float Radius = 1f;

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(this.transform.position, this.Radius);
	}
}
