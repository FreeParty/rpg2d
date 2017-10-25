using UnityEngine;
using System.Collections;

public class BaseMortionController : MonoBehaviour {

	Animator m_Anim;
	Rigidbody2D m_Rigidbody2D;

	[SerializeField]
	float move_speed = 7.0f;

	void Start()
	{
		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	/**
    * 移動
    */
	public void Move(float h_move, float v_move)
	{
		//  移動方向に力を加える
		Vector2 direction = new Vector2 (h_move, v_move).normalized;
		m_Rigidbody2D.velocity = direction * (h_move * h_move + v_move * v_move) * move_speed;

		//モーション判定用のパラメータ   
		m_Anim.SetFloat("Direction_X", h_move);
		m_Anim.SetFloat("Direction_Y", v_move);
	}

}