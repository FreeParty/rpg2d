using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class FadeinController : MonoBehaviour {

	float fadeSpeed = 0.03f;        //透明度が変わるスピードを管理
	public float red, green, blue, alfa;   //パネルの色、不透明度を管理

	public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
	public bool isFadeIn = true;   //フェードイン処理の開始、完了を管理するフラグ

	EncountController m_encount;

    GraphicRaycaster graphicRaycaster;

    Image fadeImage;                //透明度を変更するパネルのイメージ

	void Start () {
		fadeImage = GetComponent<Image> ();
		red = fadeImage.color.r;
		green = fadeImage.color.g;
		blue = fadeImage.color.b;
		alfa = fadeImage.color.a;
        graphicRaycaster = GameObject.Find("Window").GetComponent<GraphicRaycaster>();
    }

	void Update () {
		if(isFadeIn){
            graphicRaycaster.enabled = false;
            FadeIn ();
        }

		if (isFadeOut) {
            FadeOut();
        }
    }

    public void FadeIn()
    { // kuro -> siro => alfa=1 -> alfa=0
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
            graphicRaycaster.enabled = true;
        }
        else
        {
            fadeImage.enabled = true;
            alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
            SetAlpha();                      //b)変更した不透明度パネルに反映する
        }
    }

	public void FadeOut(){ // siro -> kuro => alfa=0 -> alfa=1
		if(alfa >= 1){             // d)完全に不透明になったら処理を抜ける
			isFadeOut = false;
        }
        else
        {
            fadeImage.enabled = true;  // a)パネルの表示をオンにする
            alfa += fadeSpeed;         // b)不透明度を徐々にあげる
            SetAlpha();               // c)変更した透明度をパネルに反映する
        }
	}

	void SetAlpha(){
		fadeImage.color = new Color(red, green, blue, alfa);
	}
}