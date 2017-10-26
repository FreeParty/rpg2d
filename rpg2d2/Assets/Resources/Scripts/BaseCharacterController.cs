using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BaseMortionController))]
public class BaseCharacterController : MonoBehaviour {

    BaseMortionController m_Character;
    Animator m_Anim;
    Sprite[] sp;

    private void Start()
    {
        m_Character = GetComponent<BaseMortionController>();
        m_Anim = GetComponent<Animator>();
    }


    private void Update()
    {

    }



    private void FixedUpdate()
    {
        // WASDの入力取得
        float x = CrossPlatformInputManager.GetAxis("Horizontal"); // X
        float y = CrossPlatformInputManager.GetAxis("Vertical"); //y

        //移動
        m_Character.Move(x, y);

        if (x == 0 && y == 0) ;
        else {
            walk_state_init();
            if (Mathf.Abs(x) < Mathf.Abs(y)) {
                if (0 < y) {
                    m_Anim.SetBool("walkingTop", true);
                } else if (y < 0) {
                    m_Anim.SetBool("walkingUnder", true);
                }
            } else if (Mathf.Abs(y) < Mathf.Abs(x)) {
                if (0 < x) {
                    m_Anim.SetBool("walkingRight", true);
                } else if (x < 0) {
                    m_Anim.SetBool("walkingLeft", true);
                }
            } else {
                if (0 < y) {
                    m_Anim.SetBool("walkingTop", true);
                } else {
                    m_Anim.SetBool("walkingUnder", true);

                }
            }
        }
    }

    private void walk_state_init()
    {
        m_Anim.SetBool("walkingRight", false);
        m_Anim.SetBool("walkingLeft", false);
        m_Anim.SetBool("walkingTop", false);
        m_Anim.SetBool("walkingUnder", false);
    }
}