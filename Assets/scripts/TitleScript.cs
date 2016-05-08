using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	public Light spotlight;
	public float lightMoveTime = 1f;
	public float fadeTime = 0.5f;

	private Vector3 startPos;
	private Vector3 finalPos;

	void Start () {
		startPos = spotlight.transform.position;
		finalPos = new Vector3(-startPos.x, startPos.y, startPos.z);
	}
	
	void Update () {
		if (Time.time <= lightMoveTime) {
			spotlight.transform.position = Vector3.Lerp (startPos, finalPos, Time.time / lightMoveTime);
		} 
		else if (Time.time <= lightMoveTime + fadeTime) {
			SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
			renderer.color = new Color (renderer.color.r, renderer.color.g, renderer.color.b, 1 - (Time.time - lightMoveTime) / fadeTime);
		} 
		else {
			Destroy (gameObject);
		}
	}
}
