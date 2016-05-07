using UnityEngine;
using System.Collections;

public class PlayerAttackScript : MonoBehaviour {

	public int playerNumber = 1;
	public float weaponSpeed = 0.25f;
	public float weaponDistance = 0.25f;

	private bool isAttacking = false;
	private bool isReturning = false;
	private float weaponTime = 0f;
	private GameObject weapon;

	void Start () {
	}
	
	void Update () {
		if (!isAttacking && !isReturning) {
			float attack = Input.GetAxis ("Attack " + playerNumber);
			if (attack != 0f) {
				foreach (GameObject weaponObj in GameObject.FindGameObjectsWithTag("Weapon")) {
					Debug.Log (gameObject);
					Debug.Log (weaponObj);
					if (weaponObj.transform.IsChildOf(gameObject.transform)) {
						weapon = weaponObj;
						isAttacking = true;
						weaponTime = 0f;
						break;
					}
				}
			}
		}

		Vector3 facingDirection = gameObject.transform.rotation * Vector3.right;

		if (isAttacking) {
			weaponTime += Time.deltaTime;
			weapon.transform.position = Vector3.Lerp (
				gameObject.transform.position, 
				gameObject.transform.position + weaponDistance * facingDirection, 
				weaponTime / weaponSpeed);

			if (weaponTime >= weaponSpeed) {
				isAttacking = false;
				isReturning = true;
				weaponTime = 0f;
			}
		} 
		else if (isReturning) {
			weaponTime += Time.deltaTime;
			weapon.transform.position = Vector3.Lerp (
				gameObject.transform.position, 
				gameObject.transform.position + weaponDistance * facingDirection, 
				1 - weaponTime / weaponSpeed);

			if (weaponTime >= weaponSpeed) {
				isReturning = false;
			}
		}
	}
}
