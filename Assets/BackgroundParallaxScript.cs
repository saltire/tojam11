using UnityEngine;
using System.Collections;

public class BackgroundParallaxScript : MonoBehaviour {

	public float parallaxFactor = 3f;

	private GameObject mainCamera;
	private float initialCameraY;
	private float initialY;

	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
		initialCameraY = mainCamera.transform.position.y;
		initialY = transform.position.y;
	}

	void Update () {
		transform.position = new Vector3(
			transform.position.x,
			initialY + (mainCamera.transform.position.y - initialCameraY) / parallaxFactor,
			transform.position.z);
	}
}
