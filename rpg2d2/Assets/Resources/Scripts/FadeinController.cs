using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
using UnityStandardAssets.CrossPlatformInput;

public class FadeinController : MonoBehaviour {

	public float fadeSpeed = 0.03f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理
    public bool isFadeIn = false;
    bool isFadeOutEnd = true;  //フェードアウト処理の開始、完了を管理するフラグ
	bool isFadeInEnd = true;   //フェードイン処理の開始、完了を管理するフラグ
    Image fadeImage;                //透明度を変更するパネルのイメージ

    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
        if (isFadeIn)
        {
            StartCoroutine(StartFadeIn(null));
        }
    }


    void Update()
    {
        if (!isFadeInEnd)
        {
            FadeIn();
        }
        if (!isFadeOutEnd)
        {
            FadeOut();
        }
    }

    void disableObject()
    {
        if (GameObject.Find("MenuModal") != null) {
            GameObject.Find("MenuModal").SetActive(false);
        }
        if(GameObject.Find("Controller") != null)
        {
            GameObject.Find("Controller").SetActive(false);
        }
        GetComponent<GraphicRaycaster>().enabled = true;
        GameObject.Find("Player").GetComponent<Animator>().enabled = false;
    }

    void enableObject()
    {
        GameObject root = GameObject.Find("GameManager").GetComponent<GameManager>().root;
        if (root.transform.Find("MenuModal") != null)
        {
            root.transform.Find("MenuModal").gameObject.SetActive(true);
        }
        if (root.transform.Find("Controller") != null)
        {
            root.transform.Find("Controller").gameObject.SetActive(true);
        }
        GetComponent<GraphicRaycaster>().enabled = false;
        GameObject.Find("Player").GetComponent<Animator>().enabled = true;
    }

    public delegate void Callback();
    public delegate void CallbackStrArg(string arg);

    void startFadeOut()
    {
        disableObject();
        alfa = 0;
        isFadeOutEnd = false;
        fadeImage.enabled = true;
    }

    public IEnumerator StartFadeOut(Callback callback)
    {
        startFadeOut();
        yield return new WaitUntil(() => isFadeOutEnd == true);
        if (callback != null)
        {
            callback();
        }
    }
    public IEnumerator StartFadeOut(CallbackStrArg callback,string arg)
    {
        startFadeOut();
        yield return new WaitUntil(() => isFadeOutEnd == true);
        if (callback != null)
        {
            callback(arg);
        }
    }

    public void startFadeIn()
    {
        disableObject();
        alfa = 1;
        isFadeInEnd = false;
        fadeImage.enabled = true;
    }

    public IEnumerator StartFadeIn(Callback callback)
    {
        startFadeIn();
        yield return new WaitUntil(() => isFadeInEnd == true);
        enableObject();
        if (callback != null)
        {
            callback();
        }
    }

    public IEnumerator StartFadeIn(CallbackStrArg callback,string arg)
    {
        startFadeIn();
        yield return new WaitUntil(() => isFadeInEnd == true);
        enableObject();
        if (callback != null)
        {
            callback(arg);
        }
    }
   

    void FadeIn()
    { // kuro -> siro => alfa=1 -> alfa=0
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeInEnd = true;
        }
        else
        {
            alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
            SetAlpha();                      //b)変更した不透明度パネルに反映する
        }
    }

	void FadeOut(){ // siro -> kuro => alfa=0 -> alfa=1
		if(alfa >= 1){             // d)完全に不透明になったら処理を抜ける
			isFadeOutEnd = true;
        }
        else
        {
            alfa += fadeSpeed;         // b)不透明度を徐々にあげる
            SetAlpha();               // c)変更した透明度をパネルに反映する
        }
	}

	void SetAlpha(){
        fadeImage.color = new Color(red, green, blue, alfa);
	}
}