using Memewars;
using UnityEngine;

public class CharacterDamageable : Damageable
{
	private StickmanCharacter _stickman;

	static UnityEngine.Object BloodFountain;

	protected override void Start()
	{
		if (BloodFountain == null)
			BloodFountain = Resources.Load("BloodFountain");

		base.Start ();
		this._stickman = this.GetComponent<StickmanCharacter>();
	}

	public override void Damage(float damage, CollisionInfo collisionInfo)
	{
		base.Damage (damage, collisionInfo);
		Debug.Log("Direction: " + Quaternion.FromToRotation(transform.up, collisionInfo.normal).eulerAngles);
		Debug.Log("Normal: " + collisionInfo.normal);
		GameObject bloodFountain = (GameObject)Instantiate(
			BloodFountain,
			collisionInfo.point,
			Quaternion.LookRotation(collisionInfo.point - (Vector3.Dot(collisionInfo.point, collisionInfo.normal)) * collisionInfo.normal, collisionInfo.normal)
		);
		// bloodFountain.transform.SetParent(this._stickman.transform, true);
		// Debug.DrawRay(Vector3.zero, collisionInfo.normal, Color.red, 5);
	}

	protected override void Die()
	{
		this._stickman.Die();
	}
}
