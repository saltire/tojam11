using UnityEngine;
using System.Collections;

public class PlayerDamageScript : MonoBehaviour {

	public float playerHealth = 5f;
	public float stunFactor = 0.1f;
	public float minStunLength = 0.1f;
	public bool stunned = false;

	private float stunLength = 0f;
	private bool passedMinStun = false;

	public void DealDamage (float damage) {
		playerHealth = Mathf.Max(0f, playerHealth - damage);

		stunLength += damage * stunFactor;
		if (stunLength >= minStunLength) {
			stunned = true;
			passedMinStun = true;
		}

		GetComponentInChildren<WeaponAttackScript> ().InterruptAttack ();
	}

	void Update () {
		if (stunned && passedMinStun) {
			stunLength -= Time.deltaTime;
			if (stunLength <= 0f) {
				stunned = false;
				passedMinStun = false;
			}
		}

		if (playerHealth <= 0f) {
			GetComponent<PlayerDeathScript> ().Kill ("defeated");
		}
	}
}
