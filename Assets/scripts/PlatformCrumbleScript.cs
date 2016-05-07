using UnityEngine;
using System.Collections;

public class PlatformCrumbleScript : MonoBehaviour {

	public float crumbleLength = 3f;
	public float destroyTime = 10f;

	private float crumbleTime = 0f;
	private Rigidbody2D rb2d;
	private GameObject mainCamera;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	void Update() {
		if (rb2d.isKinematic && crumbleTime > crumbleLength) {
			rb2d.isKinematic = false;
		}
	}
	
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			crumbleTime += Time.deltaTime;
		}
	}

	void OnBecameInvisible() {
		if (!rb2d.isKinematic && transform.position.y < mainCamera.transform.position.y) {
			Destroy (gameObject);
		}
	}
}
