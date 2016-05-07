using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

	public float playerHealth = 5f;

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.CompareTag ("Weapon") && !coll.transform.IsChildOf(transform)) {
			playerHealth -= coll.GetComponent<WeaponAttackScript> ().weaponDamage;
			Debug.Log (playerHealth);
		}
	}
}
