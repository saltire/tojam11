using UnityEngine;
using System.Collections;

public class NotificationScript : MonoBehaviour {

	public float fadeTime = 1f;

	private SpriteRenderer[] renderers;
	private float elapsed = 0f;

	void Start () {
		renderers = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer rend in renderers) {
			rend.color = rend.color - new Color (0, 0, 0, 1);
		}
	}
	
	void Update () {
		if (elapsed <= fadeTime) {
			elapsed += Time.deltaTime;
			foreach (SpriteRenderer rend in renderers) {
				rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, elapsed / fadeTime);
			}
		}
	}
}
