using UnityEngine;
using Memewars;

class Part
{
	public Transform transform;

	public Rigidbody rigidbody;
}

public class RagdollController : MonoBehaviour
{
	private Rigidbody _hipsRigidbody;

	private CameraFollower _cameraFollower;

	private float _disableAt;

	void FindParts()
	{
		Transform[] transformations = this.GetComponentsInChildren<Transform>();
		foreach (Transform t in transformations)
		{
			if (t.gameObject.name.EndsWith("_Hips"))
				this._hipsRigidbody = t.gameObject.GetComponent<Rigidbody>();
		}
		if (this._hipsRigidbody == null)
			throw new System.Exception("Cannot find Hips on the Ragdoll");
	}

	void Update()
	{
		if (this._cameraFollower != null)
			this._cameraFollower.enabled = (this._disableAt > Time.timeSinceLevelLoad);
	}

	/// <summary>
	/// Pega todas as juntas do jogador e aplica a esta ragdoll.
	/// </summary>
	/// <param name="stickmanCharacter"></param>
	public void Mimic(StickmanCharacter stickmanCharacter)
	{
		this._cameraFollower = this.GetComponentInChildren<CameraFollower>();
		this._cameraFollower.enabled = false;
		if (stickmanCharacter.photonView.isMine)
		{
			this._disableAt = Time.timeSinceLevelLoad + 3f;
			this._cameraFollower.enabled = true;
		}
		else
			this._disableAt = 0;

		System.Collections.Generic.Dictionary<string, Part> parts = new System.Collections.Generic.Dictionary<string, Part>();
		foreach (Transform t in stickmanCharacter.Skeleton.GetComponentsInChildren<Transform>())
		{
			Debug.Log(t.gameObject.name);
			parts.Add(t.gameObject.name, new Part()
			{
				transform = t,
				rigidbody = t.gameObject.GetComponent<Rigidbody>()
			});
		}

		this.FindParts();
		Transform[] transformations = this.GetComponentsInChildren<Transform>();
		Part tmp;
		Rigidbody tmpRigidbody;
		foreach (Transform t in transformations)
		{
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


		float massRatio = (this._hipsRigidbody.mass / stickmanCharacter.rootRigidbody.mass);
		this._hipsRigidbody.velocity = stickmanCharacter.rootRigidbody.velocity * massRatio;
		this._hipsRigidbody.angularVelocity = stickmanCharacter.rootRigidbody.angularVelocity * massRatio;

		BloodFountain[] bloods = stickmanCharacter.GetComponentsInChildren<BloodFountain>();
		foreach (BloodFountain blood in bloods)
		{
			blood.transform.SetParent(this._hipsRigidbody.transform, false);
		}		
	}

	public void Randomize()
	{
		this.FindParts();
		float d = 100f;
		Rigidbody[] rigidbodies = this.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody r in rigidbodies)
		{
			r.angularVelocity = new Vector3(Random.Range(-d, d), Random.Range(-d, d), Random.Range(-d, d));
		}
	}
}
