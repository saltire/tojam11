using UnityEngine;
using System.Collections;

public class PlatformCrumbleScript : MonoBehaviour {

	public float crumbleLength = 1f;
	public float fallDamage = 1f;

	private float crumbleTime = 0f;
	private Rigidbody2D rb2d;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (rb2d.isKinematic && crumbleTime > crumbleLength) {
			rb2d.isKinematic = false;
		}
	}
	
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			crumbleTime += Time.deltaTime;

			// If platform is falling, and hits a player from above, deal damage.
			if (!rb2d.isKinematic && coll.contacts [0].normal.y > 0f) {
				coll.gameObject.GetComponent<PlayerDamageScript>().DealDamage (fallDamage);
			}
		}
	}
}
