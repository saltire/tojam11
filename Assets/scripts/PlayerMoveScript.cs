using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveScript : MonoBehaviour {

	public int playerNumber = 1;
	public float moveSpeed = 10f;
	public float jumpSpeed = 10f;

	private bool holdingJump = false;
	private Collision2D surfaceCollision;

	void FixedUpdate () {
		Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();

		bool stunned = GetComponent<PlayerDamageScript> ().stunned;

		float horiz = stunned ? 0f : Input.GetAxis ("Horizontal " + playerNumber);
		if (horiz != 0f) {
			rb2d.AddForce(new Vector2 (horiz * moveSpeed, 0), ForceMode2D.Force);

			// Change the sprite's facing direction.
			transform.localScale = new Vector3(Mathf.Sign(horiz) * Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}

		if (surfaceCollision != null && !holdingJump && !stunned) {
			float jump = Input.GetAxis ("Jump " + playerNumber);
			if (jump != 0f) {
				holdingJump = true;

				// Jump in the direction 2/3 of the way between the surface normal and up.
				rb2d.AddForce ((surfaceCollision.contacts [0].normal + Vector2.up + Vector2.up).normalized * jump * jumpSpeed, ForceMode2D.Impulse);
			}
		}

		if (holdingJump) {
			if (Input.GetAxis ("Jump " + playerNumber) == 0f) {
				holdingJump = false;
			}
		}

		// Set parameters in the animation controller.
		Animator anim = GetComponent<Animator>();
		anim.SetFloat ("MoveSpeed", Mathf.Abs(rb2d.velocity.x));
		anim.SetFloat ("VerticalSpeed", rb2d.velocity.y);
		anim.SetBool ("Standing", surfaceCollision != null && surfaceCollision.contacts [0].normal.y > 0f);
		anim.SetBool ("Sliding", surfaceCollision != null && surfaceCollision.contacts [0].normal.y == 0f);
		anim.SetBool ("Stunned", stunned);
	}

	void OnCollisionStay2D(Collision2D coll) {
		// Contacting a surface.
		if (coll.gameObject.CompareTag("Surface")) {
			if (coll.contacts [0].normal.y >= 0f) {
				surfaceCollision = coll;
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		// Leaving a surface.
		if (surfaceCollision != null && surfaceCollision.gameObject == coll.gameObject) {
			surfaceCollision = null;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// Drain health if hitting the bottom of the screen.
		if (coll.CompareTag("BottomTrigger")) {
			GetComponent<PlayerDamageScript>().DrainHealth ();
		}
	}
}
