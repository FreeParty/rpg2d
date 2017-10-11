using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour
{
    // Use this for initialization
    private bool printed;

    void Start()
    {
        printed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (printed && Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }

    public void printText(string str)
    {
        GetComponentInChildren<Text>().text = str;
        printed = true;
    }
}
