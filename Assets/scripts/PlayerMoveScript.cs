using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	public int playerNumber = 1;
	public float moveSpeed = 10f;
	public float jumpSpeed = 10f;

	private bool isStanding = false;
	
	void Start () {
	
	}

	void FixedUpdate () {
		Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();

		float horiz = Input.GetAxis ("Horizontal " + playerNumber);
		if (horiz != 0f) {
			rb2d.AddForce(new Vector2 (horiz * moveSpeed, 0), ForceMode2D.Force);

			// Change the sprite's facing direction.
			transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(0, horiz) * Mathf.Rad2Deg, Vector3.up);
		}

		if (isStanding) {
			float jump = Input.GetAxis ("Jump " + playerNumber);
			if (jump != 0f) {
				rb2d.AddForce (new Vector2 (0, jump * jumpSpeed), ForceMode2D.Impulse);
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Platform") && coll.contacts[0].normal == Vector2.up) {
			isStanding = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Platform")) {
			isStanding = false;
		}
	}
}
