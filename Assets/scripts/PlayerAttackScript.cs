using UnityEngine;
using System.Collections;

public class PlayerAttackScript : MonoBehaviour {

	public int playerNumber = 1;
	
	void Update () {
		float attack = Input.GetAxis ("Attack " + playerNumber);
		if (attack != 0f) {
			foreach (GameObject weapon in GameObject.FindGameObjectsWithTag("Weapon")) {
				if (weapon.transform.IsChildOf(gameObject.transform)) {
					weapon.GetComponent<WeaponAttackScript>().Attack ();
					break;
				}
			}
		}
	}
}
