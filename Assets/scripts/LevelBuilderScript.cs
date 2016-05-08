using UnityEngine;
using System.Collections;

public class LevelBuilderScript : MonoBehaviour {

	public GameObject[] platformSets;
	public float startHeight = 0f;
	public float platformSetHeight = 6f;

	private float levelTop;
	private GameObject mainCamera;
	private float ySize;

	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		ySize = mainCamera.GetComponent<Camera> ().orthographicSize;

		levelTop = transform.position.y + startHeight;
	}

	void Update () {
		while (mainCamera.transform.position.y + ySize >= levelTop) {
			Instantiate (
				platformSets[Random.Range(0, platformSets.Length)], 
				new Vector3 (0f, levelTop, 0f), Quaternion.identity);
			levelTop += platformSetHeight;
		}
	}
}
