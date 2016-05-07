using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public float moveSpeed = 10f;
	public float jumpSpeed = 10f;

	private bool isStanding = false;
	
	void Start () {
	
	}

	void FixedUpdate () {
		Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();

		float horiz = Input.GetAxis ("Horizontal");
		if (horiz != 0f) {
			rb2d.AddForce(new Vector2 (horiz * moveSpeed, 0), ForceMode2D.Force);
		}

		if (isStanding) {
			float jump = Input.GetAxis ("Fire1");
			if (jump != 0f) {
				rb2d.AddForce (new Vector2 (0, jump * jumpSpeed), ForceMode2D.Impulse);
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "platform" && coll.contacts[0].normal == Vector2.up) {
			isStanding = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "platform") {
			isStanding = false;
		}
	}
}
