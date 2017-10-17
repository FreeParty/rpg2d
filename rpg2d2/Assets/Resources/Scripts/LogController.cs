using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour
{
    private bool printed;
    private int counter;
    private string[] log;
	public GameObject window;
	private bool flg = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            if (counter == log.Length)
            {
                gameObject.SetActive(false);
                if(callback != null) callback();
            }
            else
            {
                GetComponentInChildren<Text>().text = log[counter++];
            }
        }
    }

    public void printText(string[] str)
    {
		ChangeMode ();
        counter = 0;
        log = str;
        GetComponentInChildren<Text>().text = log[counter++];
        print(counter);
    }

    public delegate void Callback();

    public Callback callback
    {
        get;
        set;
    }

	public void ChangeMode(){
		if (flg) {
			window.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
		} else {
			window.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
		}

		flg = !flg;
	}
}
