using UnityEngine;
using System.Collections;

public class PlayerAttackScript : MonoBehaviour {

	public int playerNumber = 1;
	public WeaponAttackScript weaponType;
	
	private WeaponAttackScript weapon;

	void Start () {
		weapon = Instantiate (weaponType);
		weapon.transform.parent = transform;
	}

	void Update () {
		bool stunned = GetComponent<PlayerDamageScript> ().stunned;

		if (!stunned) {
			float attack = Input.GetAxis ("Attack " + playerNumber);
			if (attack != 0f) {
				weapon.GetComponent<WeaponAttackScript> ().Attack ();
			}
		}
	}
}
