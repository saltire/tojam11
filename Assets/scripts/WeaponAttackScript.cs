using UnityEngine;
using System.Collections;

public class WeaponAttackScript : MonoBehaviour {

	public float weaponAttackTime = 0.5f;
	public float weaponDistance = 0.5f;
	public float weaponDPS = 1f;

	private Animator anim;
	private Collider2D coll;
	private bool isAttacking;
	private bool isReturning;
	private float elapsed = 0f;
	private float stageTime;

	public void Attack () {
		if (!isAttacking && !isReturning) {
			anim.SetTrigger ("Attack");
			isAttacking = true;
			coll.enabled = true;
			elapsed = 0f;
		}
	}

	void Start () {
		anim = GetComponentInParent<Animator> ();

		coll = GetComponent<Collider2D> ();
		coll.enabled = false;

		stageTime = weaponAttackTime / 2f;
	}

	void Update () {
		if (isAttacking) {
			elapsed += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (Vector3.zero, weaponDistance * Vector3.right, elapsed / stageTime);

			if (elapsed >= stageTime) {
				isAttacking = false;
				isReturning = true;
				elapsed = 0f;
			}
		} else if (isReturning) {
			elapsed += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (Vector3.zero, weaponDistance * Vector3.right, 1 - elapsed / stageTime);

			if (elapsed >= stageTime) {
				isReturning = false;
				coll.enabled = false;

				anim.SetTrigger ("AttackFinished");
			}
		}
	}
}
