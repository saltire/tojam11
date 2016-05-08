using UnityEngine;
using System.Collections;

public class PlayerDeathScript : MonoBehaviour {

	public float pushSpeed = 1f;
	public float pushRotation = 1.5f;
	public bool dead = false;

	public GameObject defeatedNote = null;
	public GameObject fellNote = null;

	public void Kill (string cause) {
		if (!dead) {
			dead = true;

			// Remove health.
			GetComponent<PlayerDamageScript>().playerHealth = 0f;

			// Disable movement.
			GetComponent<PlayerMoveScript> ().enabled = false;

			gameObject.layer = 9;

			// Blow the player backward.
			Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();
			float facingDirection = Mathf.Sign (transform.localScale.x);
			rb2d.freezeRotation = false;
			rb2d.AddForce (new Vector2 (-facingDirection, 0.5f) * pushSpeed, ForceMode2D.Impulse);
			rb2d.AddTorque (pushRotation * facingDirection, ForceMode2D.Impulse);

			GameObject note = null;
			if (cause == "defeated" && defeatedNote != null) {
				note = Instantiate (defeatedNote);
			} else if (cause == "fell" && fellNote != null) {
				note = Instantiate (fellNote);
			}
			if (note != null) {
				note.transform.parent = GameObject.Find ("Main Camera").transform;
				note.transform.localPosition = note.transform.position;
			}
		}
	}
}
