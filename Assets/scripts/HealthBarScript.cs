using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	public PlayerDamageScript player;

	private float initialHealth;
	private Vector3 initialScale;

	void Start () {
		initialHealth = player.playerHealth;
		initialScale = transform.localScale;
	}
	
	void Update () {
		transform.localScale = Vector3.Scale(initialScale, new Vector3 (player.playerHealth / initialHealth, 1f, 1f));
	}
}
