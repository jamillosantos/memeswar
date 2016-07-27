using UnityEngine;
using System.Collections.Generic;
using Memewars;
using System;

public class RagdollController : MonoBehaviour
{
	public void Mimic(StickmanCharacter stickmanCharacter)
	{
		Dictionary<string, Transform> scTransformations = new Dictionary<string, Transform>();
		foreach (Transform t in stickmanCharacter.GetComponentsInChildren<Transform>())
		{
			scTransformations.Add(t.gameObject.name, t);
		}
		Transform[] transformations = this.GetComponentsInChildren<Transform>();
		Transform tmp;
		foreach (Transform t in transformations)
		{
			if (scTransformations.TryGetValue(t.gameObject.name, out tmp))
			{
				t.localPosition = tmp.localPosition;
				t.localRotation = tmp.localRotation;
			}
		}
	}
}
