using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerSpriteScript : MonoBehaviour {

	public string playerSpriteName;

	private List<Sprite> subSprites;

	void Start() {
		if (playerSpriteName.Length > 0f) {
			subSprites = Resources.LoadAll<Sprite> (playerSpriteName).ToList();
		}
	}
	
	void LateUpdate () {
		if (playerSpriteName.Length > 0f) {
			SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
			renderer.sprite = subSprites.Find (subSprite => subSprite.name == renderer.sprite.name);
		}
	}
}
