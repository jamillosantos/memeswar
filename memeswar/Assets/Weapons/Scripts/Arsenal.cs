using UnityEngine;
using System.Collections;
using Memewars;

public class Arsenal : MonoBehaviour
 {
	private StickmanCharacter _stickmanCharacter;

	void Start ()
	{
		this._stickmanCharacter = this.GetComponentInParent<StickmanCharacter>();
	}
	
	void Update ()
	{
		this.transform.rotation = Quaternion.Euler(this._stickmanCharacter.AimAngle, 90, 0);
	}
}
