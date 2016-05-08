using UnityEngine;
using System.Collections;

public class TitleSpotlightScript : MonoBehaviour {

	public Light spotlight;
	public float lightMoveTime = 1f;
	public float fadeTime = 0.5f;

	private Vector3 startPos;
	private Vector3 finalPos;
	private float elapsed;

	void Start () {
		startPos = spotlight.transform.localPosition;
		finalPos = new Vector3(-startPos.x, -startPos.y, startPos.z);
	}
	
	void Update () {
		elapsed += Time.deltaTime;

		if (elapsed <= lightMoveTime) {
			spotlight.transform.localPosition = Vector3.Lerp (startPos, finalPos, elapsed / lightMoveTime);
		} 
		else if (elapsed <= lightMoveTime + fadeTime) {
			SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
			renderer.color = new Color (renderer.color.r, renderer.color.g, renderer.color.b, 1 - (elapsed - lightMoveTime) / fadeTime);
		} 
		else {
			Destroy (gameObject);
		}
	}
}
