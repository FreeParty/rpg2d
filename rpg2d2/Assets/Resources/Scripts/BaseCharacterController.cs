using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


[RequireComponent(typeof (BaseMortionController))]
public class BaseCharacterController : MonoBehaviour {

	private BaseMortionController m_Character;
	private Animator m_Anim;
	private bool walking = false; // walking flag
	private Rigidbody2D myobj;
	private float time=0;
	private GameObject substatus;
	private Sprite[] sp;

	private void Awake()
	{
		m_Character = GetComponent<BaseMortionController>();
		m_Anim = GetComponent<Animator>();
		myobj = GetComponent<Rigidbody2D> ();
		substatus = GameObject.Find ("substatus");
		sp = Resources.LoadAll<Sprite>("Sprites/mainchara");
 	}


	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			GetComponent<SpriteRenderer> ().sprite = sp [11];
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			GetComponent<SpriteRenderer> ().sprite = sp [24];
		}

	}



	private void FixedUpdate()
	{
		// WASDの入力取得
		float x = CrossPlatformInputManager.GetAxis("Horizontal"); // X
		float y = CrossPlatformInputManager.GetAxis ("Vertical"); //y

		if (SceneManager.GetActiveScene ().name != "battle") {
			//移動
			m_Character.Move(x, y);	
			if (x > 0 && y > 0) { // 第一象限
				if (x > y) {
					walk_state_init ();
					m_Anim.SetBool ("walkingRight", walking);
				} else if (x < y) {
					walk_state_init ();
					m_Anim.SetBool ("walkingTop", walking);
				} else { // x == y
					walk_state_init ();
					m_Anim.SetBool ("walkingUnder", walking);
				}
			} else if (x < 0 && y > 0) { // 第二象限
				if (Mathf.Abs (x) > y) {
					walk_state_init ();
					m_Anim.SetBool ("walkingLeft", walking);
				} else if (Mathf.Abs (x) < y) {
					walk_state_init ();
					m_Anim.SetBool ("walkingTop", walking);
				} else {
					walk_state_init ();
					m_Anim.SetBool ("walkingUnder", walking);
				}
			} else if (x < 0 && y < 0) { //  第三象限
				if (Mathf.Abs (x) > Mathf.Abs (y)) {
					walk_state_init ();
					m_Anim.SetBool ("walkingLeft", walking);
				} else if (Mathf.Abs (x) < Mathf.Abs (y)) {
					walk_state_init ();
					m_Anim.SetBool ("walkingUnder", walking);
				} else {
					walk_state_init ();
					m_Anim.SetBool ("walkingUnder", walking);
				}
			} else if(x > 0 && y < 0) { // 第四象限
				if (x > Mathf.Abs (y)) {
					walk_state_init ();
					m_Anim.SetBool ("walkingRight", walking);
				} else if (x < Mathf.Abs (y)) {
					walk_state_init ();
					m_Anim.SetBool ("walkingUnder", walking);
				} else {
					walk_state_init ();
					m_Anim.SetBool ("walkingUnder", walking);
				}
			}
		}


			

//		if (myobj.IsSleeping ()) {
//			time += Time.deltaTime;
//			if ((int)time == 5) {
//				Debug.Log ("HOOOOI");
//				substatus.GetComponent<Canvas>().enabled = true;
//				time = 0;
//			}
//			Debug.Log(time);
//		}




	}


	private void walk_state_init()
	{
		walking = true;
		m_Anim.SetBool ("walkingRight", false);
		m_Anim.SetBool ("walkingLeft", false);
		m_Anim.SetBool ("walkingTop", false);
		m_Anim.SetBool ("walkingUnder", false);
	}
}