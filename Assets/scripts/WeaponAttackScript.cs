using UnityEngine;
using System.Collections;

public class WeaponAttackScript : MonoBehaviour {

	public float weaponSpeed = 0.1f;
	public float weaponDistance = 2f;
	public float weaponDPS = 1f;

	private Collider2D c2d;
	private bool isAttacking;
	private bool isReturning;
	private float weaponTime = 0f;

	public void Attack () {
		if (!isAttacking && !isReturning) {
			isAttacking = true;
			c2d.enabled = true;
			weaponTime = 0f;
		}
	}

	void Start () {
		c2d = GetComponent<Collider2D> ();
		c2d.enabled = false;
	}

	void Update () {
		if (isAttacking) {
			weaponTime += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (
				Vector3.zero, 
				weaponDistance * Vector3.right, 
				weaponTime / weaponSpeed);

			if (weaponTime >= weaponSpeed) {
				isAttacking = false;
				isReturning = true;
				weaponTime = 0f;
			}
		} else if (isReturning) {
			weaponTime += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (
				Vector3.zero, 
				weaponDistance * Vector3.right, 
				1 - weaponTime / weaponSpeed);

			if (weaponTime >= weaponSpeed) {
				isReturning = false;
				c2d.enabled = false;
			}
		}
	}
}
