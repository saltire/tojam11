using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartControlScript : MonoBehaviour {

	void Update () {
		float restart = Input.GetAxis ("Restart");
		if (restart != 0f) {
			SceneManager.LoadScene ("game");
		}
	}
}
