using UnityEngine;
using System.Collections;

public class WeaponAttackScript : MonoBehaviour {

	public float weaponAttackTime = 0.5f;
	public float weaponDistance = 0.5f;
	public float weaponDPS = 1f;
	public float weaponPush = 0f;
	public float weaponCooldown = 0.1f;
	public string playerSpriteName;

	private Animator anim;
	private Collider2D coll;
	private bool isAttacking;
	private bool isReturning;
	private float elapsed = 0f;
	private float weaponCooldownTime = 0f;
	private float stageTime;

	public void Attack () {
		if (!isAttacking && !isReturning && weaponCooldownTime <= 0f) {
			anim.SetTrigger ("Attack");
			isAttacking = true;
			coll.enabled = true;
			elapsed = 0f;
		}
	}

	public void InterruptAttack() {
		isAttacking = false;
		isReturning = false;
		transform.localPosition = Vector3.zero;
	}

	void Start () {
		anim = GetComponentInParent<Animator> ();

		coll = GetComponent<Collider2D> ();
		coll.enabled = false;

		stageTime = weaponAttackTime / 2f;

		if (playerSpriteName.Length > 0f) {
			GetComponentInParent<PlayerSpriteScript> ().playerSpriteName = playerSpriteName;
		}
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
		} 
		else if (isReturning) {
			elapsed += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (Vector3.zero, weaponDistance * Vector3.right, 1 - elapsed / stageTime);

			if (elapsed >= stageTime) {
				isReturning = false;
				coll.enabled = false;

				anim.SetTrigger ("AttackFinished");
				weaponCooldownTime = weaponCooldown;
			}
		} 
		else if (weaponCooldownTime > 0f) {
			weaponCooldownTime -= Time.deltaTime;
		}
	}

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.CompareTag ("Player") && !transform.IsChildOf(coll.transform)) {
			coll.GetComponent<PlayerDamageScript>().DealDamage (weaponDPS * Time.deltaTime);

			if (weaponPush > 0f) {
				coll.attachedRigidbody.AddForce (
					Vector2.Scale (new Vector2 (transform.parent.localScale.x, 0f), Vector2.right) * weaponPush * Time.deltaTime, 
					ForceMode2D.Force);
			}
		}
	}
}
