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

		float horiz = Input.GetAxis ("Horizontal " + playerNumber);
		if (horiz != 0f) {
			rb2d.AddForce(new Vector2 (horiz * moveSpeed, 0), ForceMode2D.Force);

			// Change the sprite's facing direction.
			transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(0, horiz) * Mathf.Rad2Deg, Vector3.up);
		}

		if (holdingJump) {
			if (Input.GetAxis ("Jump " + playerNumber) == 0f) {
				holdingJump = false;
			}
		}
		else if (surfaceCollision != null) {
			float jump = Input.GetAxis ("Jump " + playerNumber);
			if (jump != 0f) {
				holdingJump = true;

				// Jump in the direction halfway between the surface normal and up.
				rb2d.AddForce ((surfaceCollision.contacts [0].normal + Vector2.up).normalized * jump * jumpSpeed, ForceMode2D.Impulse);
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Surface")) {
			if (Vector2.Angle (coll.contacts [0].normal, Vector2.up) <= 90f) {
				surfaceCollision = coll;
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (surfaceCollision != null && surfaceCollision.gameObject == coll.gameObject) {
			surfaceCollision = null;
		}
	}
}
