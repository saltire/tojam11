using UnityEngine;
using System.Collections;

public class PlayerDeathScript : MonoBehaviour {

	public float pushSpeed = 1f;
	public float pushRotation = 1.5f;
	public bool dead = false;

	public void Kill () {
		if (!dead) {
			dead = true;

			// Disable movement.
			GetComponent<PlayerMoveScript> ().enabled = false;

			gameObject.layer = 9;

			// Blow the player backward.
			Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();
			float facingDirection = Mathf.Sign (transform.localScale.x);
			rb2d.freezeRotation = false;
			rb2d.AddForce (new Vector2 (-facingDirection, 0.5f) * pushSpeed, ForceMode2D.Impulse);
			rb2d.AddTorque (pushRotation * facingDirection, ForceMode2D.Impulse);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.CompareTag("BottomTrigger")) {
			Kill ();
		}
	}
}
