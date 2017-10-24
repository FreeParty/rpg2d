using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

	public int key_item_id = 0;
	public Sprite sprite_not;
	public AudioClip sound_enter;
	public AudioClip sound_not;
	public int wait_enter;
	public int wait_not;

	SpriteRenderer sr;
	Sprite sprite_standby;
	bool is_not = false;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
		sprite_standby = sr.sprite;
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player"){
			List<int> my_items = PlayerContoroller.my_items;
			if(my_items.Exists(p => p == key_item_id)){
				audio.PlayOneShot(sound_enter);
				Invoke("DestroyGate", wait_enter);
			}
			else{
				sr.sprite = sprite_not;
				audio.PlayOneShot(sound_not);
				LogController.logController.printText(new string[]{"ここを通るには「" + ItemName(key_item_id) + "」が必要です"});
				Invoke("ResetSprite", wait_not);
			}
		}
	}

	void ResetSprite(){
		sr.sprite = sprite_standby;
	}
	void DestroyGate(){
		Destroy(gameObject);
	}

	public static string ItemName(int id){
		string item_name = ItemList.item_table.Find (x => x.item_id == id).item_name;
		return item_name;
	}
}
