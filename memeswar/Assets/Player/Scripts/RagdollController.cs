using UnityEngine;
using System.Collections.Generic;
using Memewars;
using System;

class Part
{
	public Transform transform;

	public Rigidbody rigidbody;
}

public class RagdollController : MonoBehaviour
{

	private Rigidbody _hipsRigidbody;
	
	public void Mimic(StickmanCharacter stickmanCharacter)
	{
		Dictionary<string, Part> parts = new Dictionary<string, Part>();
		foreach (Transform t in stickmanCharacter.GetComponentsInChildren<Transform>())
		{
			parts.Add(t.gameObject.name, new Part()
			{
				transform = t,
				rigidbody = t.gameObject.GetComponent<Rigidbody>()
			});
		}
		Transform[] transformations = this.GetComponentsInChildren<Transform>();
		Part tmp;
		Rigidbody tmpRigidbody;
		foreach (Transform t in transformations)
		{
			if (t.gameObject.name.EndsWith("_Hips"))
				this._hipsRigidbody = t.gameObject.GetComponent<Rigidbody>();

			if (parts.TryGetValue(t.gameObject.name, out tmp))
			{
				t.localPosition = tmp.transform.localPosition;
				t.localRotation = tmp.transform.localRotation;
				tmpRigidbody = t.gameObject.GetComponent<Rigidbody>();
				if ((tmpRigidbody != null) && (tmp.rigidbody != null))
				{
					tmpRigidbody.velocity = tmp.rigidbody.velocity;
					tmp.rigidbody.angularVelocity = tmp.rigidbody.angularVelocity;
				}
			}
		}
		if (this._hipsRigidbody == null)
			throw new Exception("Cannot find Hips on the Ragdoll");

		float massRatio = (this._hipsRigidbody.mass / stickmanCharacter.rootRigidbody.mass);
		this._hipsRigidbody.velocity = stickmanCharacter.rootRigidbody.velocity * massRatio;
		this._hipsRigidbody.angularVelocity = stickmanCharacter.rootRigidbody.angularVelocity * massRatio;
	}
}
