using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

	public float playerHealth = 5f;

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.CompareTag ("Weapon") && !coll.transform.IsChildOf(transform)) {
			playerHealth = Mathf.Max(0f, playerHealth - coll.GetComponent<WeaponAttackScript> ().weaponDPS * Time.deltaTime);
		}
	}
}
