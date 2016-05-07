using UnityEngine;
using System.Collections;

public class PlatformCrumbleScript : MonoBehaviour {

	public float crumbleLength = 3f;
	public float destroyTime = 10f;

	private float crumbleTime = 0f;
	private Rigidbody2D rb2d;
	private GameObject camera;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	void Update() {
		if (rb2d.isKinematic && crumbleTime > crumbleLength) {
			rb2d.isKinematic = false;
		}
	}
	
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			crumbleTime += Time.deltaTime;
		}
	}

	void OnBecameInvisible() {
		if (!rb2d.isKinematic && transform.position.y < camera.transform.position.y) {
			Destroy (gameObject);
		}
	}
}
