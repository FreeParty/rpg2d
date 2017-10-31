using UnityEngine;
using System.Collections;

public class BaseMortionController : MonoBehaviour {

	Animator m_Anim;
	Rigidbody2D m_Rigidbody2D;
    public bool isAnalog = false;
    public int dirNum = 8;

	[SerializeField]
	float move_speed = 7.0f;

	void Start()
	{
		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if(dirNum > 8)
        {
            dirNum = 8;
        }
	}

	/**
    * 移動
    */
	public void Move(float x, float y)
	{
        //  移動方向に力を加える
        Vector2 direction = Vector2.zero;
        if (isAnalog)
        {
            float angle = Mathf.Atan2(y, x);
            int dir = Mathf.CeilToInt((angle - (Mathf.PI / dirNum)) / (2 * Mathf.PI / dirNum));
            if(dir < 0)
            {
                dir += dirNum;
            }
            dir *= 8 / dirNum;
            switch (dir)
            {
                case 0:
                    direction = new Vector2(1, 0);
                    break;
                case 1:
                    direction = new Vector2(1, 1);
                    break;
                case 2:
                    direction = new Vector2(0, 1);
                    break;
                case 3:
                    direction = new Vector2(-1, 1);
                    break;
                case 4:
                    direction = new Vector2(-1, 0);
                    break;
                case 5:
                    direction = new Vector2(-1, -1);
                    break;
                case 6:
                    direction = new Vector2(0,-1);
                    break;
                case 7:
                    direction = new Vector2(1, -1);
                    break;
            }
            direction = direction.normalized;
        }
        else
        {
            direction = new Vector2(x, y).normalized;
        }
#if UNITY_STANDALONE_WIN
        m_Rigidbody2D.velocity = direction * move_speed;
#else
        m_Rigidbody2D.velocity = direction * (x * x + y * y) * move_speed;
#endif
        //モーション判定用のパラメータ   
        m_Anim.SetFloat("Direction_X", x);
		m_Anim.SetFloat("Direction_Y", y);
	}

}