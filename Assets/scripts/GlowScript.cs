using UnityEngine;
using System.Collections;

public class GlowScript : MonoBehaviour {

	public float angle = 1f;
	public float scale = 1.01f;
	public float growTime = 10f;

	private GameObject glow;
	private ParticleSystem particle;
	private bool growing = false;
	private float elapsed = 0f;

	void Start () {
		glow = GetComponentInChildren<SpriteRenderer> ().gameObject;
	}

	void Update () {
		glow.transform.RotateAround(glow.transform.position, Vector3.forward, angle);

		if (growing && elapsed < growTime) {
			elapsed += Time.deltaTime;
			glow.transform.localScale *= scale;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.CompareTag("Player")) {
			coll.attachedRigidbody.isKinematic = true;
			growing = true;
		}
	}
}
