using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {

	public float cameraSpeed = 1f;
	public Collider2D bottomTrigger;

	private float xSize;
	private float ySize;
	private GameObject[] players;

	void Start () {
		// Workaround for bug with FindGameObjectsWithTag returning useless clones
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject obj in players) {
			if (obj.name.Substring (obj.name.Length - 7) == "(Clone)") {
				Destroy (obj);
			}
		}

		Camera camera = gameObject.GetComponent<Camera> ();
		xSize = camera.orthographicSize * camera.aspect;
		ySize = camera.orthographicSize;
	}
	
	void FixedUpdate () {
		Vector3 avgPlayerPos = Vector3.zero;
		GameObject topPlayer = null;

		foreach (GameObject player in players) {
			// Check for buggy clones again.
			Debug.Log(player.activeInHierarchy);
			if (player == null || !player.activeSelf || player.name.Substring (player.name.Length - 7) == "(Clone)") {
				continue;
			}

			avgPlayerPos += player.transform.position;

			// Keep a reference to the player that is higher up on the screen.
			if (!player.GetComponent<PlayerDeathScript>().dead && (topPlayer == null || player.transform.position.y > topPlayer.transform.position.y)) {
				topPlayer = player;
			}
		}
		avgPlayerPos /= players.Length;

		if (topPlayer != null) {
			Vector3 targetPos = new Vector3 (
				// Don't allow the higher player to go off screen.
				Mathf.Max (topPlayer.transform.position.x - xSize, Mathf.Min (topPlayer.transform.position.x + xSize * 0.8f, avgPlayerPos.x)), 
				Mathf.Max (bottomTrigger.transform.position.y + ySize, Mathf.Max (topPlayer.transform.position.y - ySize * 0.8f, avgPlayerPos.y)), 
				transform.position.z);
			
			transform.position = Vector3.Lerp (transform.position, targetPos, cameraSpeed * Time.deltaTime);
		}
	}
}
