using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour
{
    private bool printed;
    private int counter;
    private string[] log;
    private float timeElapsed;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (!printed && timeElapsed > 0.1 && GetComponentInChildren<Text>().text.Length < log[counter].Length)
        {
            GetComponentInChildren<Text>().text = log[counter].Substring(0, GetComponentInChildren<Text>().text.Length + 1);
            if (counter == log.Length - 1 && GetComponentInChildren<Text>().text.Length == log[counter].Length) printed = true;
            timeElapsed = 0;
        }
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            if (printed)
            {
                gameObject.SetActive(false);
                if(callback != null) callback();
            }
            else if(GetComponentInChildren<Text>().text.Length == log[counter].Length)
            {
                counter++;
                GetComponentInChildren<Text>().text = "";
            }
            else
            {
                GetComponentInChildren<Text>().text = log[counter];
                if (counter == log.Length - 1) printed = true;
            }
        }
    }

    public void printText(string[] str)
    {
        counter = 0;
        log = str;
        GetComponentInChildren<Text>().text = log[counter].Substring(0,1);
        printed = false;
    }

    public delegate void Callback();

    public Callback callback
    {
        get;
        set;
    }
}
