using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


[RequireComponent(typeof (BaseMortionController))]
public class BaseCharacterController : MonoBehaviour {

	private BaseMortionController m_Character;
	private Animator m_Anim;
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

			if(x == 0 && y == 0);
			else{
				walk_state_init ();
				if(Mathf.Abs(x) < Mathf.Abs(y)){
					if(0 < y){
						m_Anim.SetBool ("walkingTop", true);
					} else if(y < 0) {
						m_Anim.SetBool ("walkingUnder", true);
					}
				} else if(Mathf.Abs(y) < Mathf.Abs(x)) {
					if(0 < x){
						m_Anim.SetBool ("walkingRight", true);
					} else if(x < 0) {
						m_Anim.SetBool ("walkingLeft", true);
					}
				} else {
					if(0 < y) {
						m_Anim.SetBool ("walkingTop", true);
					} else {
						m_Anim.SetBool ("walkingUnder", true);

					}
				}
			}			
		}
	}

	private void walk_state_init()
	{
		m_Anim.SetBool ("walkingRight", false);
		m_Anim.SetBool ("walkingLeft", false);
		m_Anim.SetBool ("walkingTop", false);
		m_Anim.SetBool ("walkingUnder", false);
	}
}
