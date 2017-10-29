using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour {
    
    float height;
    Camera camera;
    Vector3 delta = new Vector3(0,0.1f,0);
    public string nextSceneName = "title";

    // Use this for initialization
    void Start () {
        height = GetComponent<RectTransform>().sizeDelta.y + 480;
        camera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        if(camera.WorldToScreenPoint(gameObject.transform.position).y > height)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange(nextSceneName, false);
            enabled = false;
        }
        else
        {
            gameObject.transform.position += delta;
        }
	}
}
