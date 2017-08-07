using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxContoroller : MonoBehaviour {

	public string treasure_name = "item00";
	private Sprite[] sp;
	// スプライトの取得
	// @param fileName ファイル名
	// @param spriteName スプライト名
//	public static Sprite GetSprite(string fileName, string spriteName) {
//		Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
//		return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
//	}

	void Start(){
		sp = Resources.LoadAll<Sprite>("Sprites/juelBox");
	}

//	void OnCollisionStay2D(Collision2D coll) {
//		
//		if (coll.gameObject.name == "Player" && Input.GetKeyDown("space")) {
//			GetComponent<SpriteRenderer> ().sprite = sp[1];
//			Debug.Log ("Hooo");
//		}
//	}

	public void OnOpenedBox(){
		GetComponent<SpriteRenderer> ().sprite = sp[1];
	}
}
