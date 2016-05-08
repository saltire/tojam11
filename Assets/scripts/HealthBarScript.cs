using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	public PlayerDamageScript player;

	private float initialHealth;
	private RectTransform rect;
	private float initialXSize;

	void Start () {
		initialHealth = player.playerHealth;
		rect = GetComponent<RectTransform> ();
		initialXSize = rect.sizeDelta.x;
	}
	
	void Update () {
		rect.sizeDelta = new Vector2 (initialXSize * player.playerHealth / initialHealth, rect.sizeDelta.y);
	}
}
