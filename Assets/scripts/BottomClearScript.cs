using UnityEngine;
using System.Collections;

public class BottomClearScript : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll) {
		coll.gameObject.SetActive (false);
	}
}
