using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleCommands : MonoBehaviour
{

    GameObject obj;

    // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("management");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fight()
    {
        obj.GetComponent<BattleManager>().Fight();
    }

    public void Run()
    {
        SceneManager.LoadScene("main");
    }

    public void Guard()
    {

    }

    public void Item()
    {

    }
}
