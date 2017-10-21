using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (Input.GetMouseButtonDown(0))
        {
            if (printed)
            {
                gameObject.SetActive(false);
                toggleController();
                if (callbackList.Count > 0)
                {
                    Callback function = callbackList[0];
                    callbackList.RemoveAt(0);
                    function();
                }
            }
            else if (GetComponentInChildren<Text>().text.Length == log[counter].Length)
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

    public LogController printText(string[] str)
    {
        counter = 0;
        log = str;
        GetComponentInChildren<Text>().text = log[counter].Substring(0, 1);
        printed = false;
        toggleController();
        return this;
    }

    public delegate void Callback();

    public void cancel(Callback function)
    {
        callbackList = new List<Callback>();
        if (function != null) callbackList.Add(function);
    }

    public LogController then(Callback function)
    {
        callbackList.Add(function);
        return this;
    }

    private List<Callback> callbackList = new List<Callback>();

    public void toggleController()
    {
        if (SceneManager.GetActiveScene().name != "battle")
        {
            if (GameObject.Find("Controller") != null)
            {
                GameObject.Find("Controller").SetActive(false);
            }
            else
            {
                GameObject.Find("Window").transform.Find("Controller").gameObject.SetActive(true);
            }
        }
    }
}