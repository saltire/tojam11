using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

	public float playerHealth = 5f;
	public float drainRate = 1f;
	public float stunFactor = 0.1f;
	public bool stunned = false;

	private bool draining = false;
	private float stunLength = 0f;

	public void DrainHealth () {
		draining = true;
	}

	public void DealDamage (float damage) {
		playerHealth = Mathf.Max(0f, playerHealth - damage);
		stunLength += damage * stunFactor;
		stunned = true;
		GetComponentInChildren<WeaponAttackScript> ().InterruptAttack ();
	}

	void FixedUpdate () {
		if (stunned) {
			stunLength = Mathf.Max(0f, stunLength - Time.deltaTime);
			if (stunLength == 0f) {
				stunned = false;
			}
		}
	}

	void Update () {
		if (draining) {
			DealDamage (drainRate);
		}

		if (playerHealth <= 0f) {
			GetComponent<PlayerDeathScript> ().Kill ();
		}
	}
}
